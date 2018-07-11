using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Action.Data.Models.Core.Watson;
using Action.Services;
using ActionUI.Admin.ViewModel;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ActionUI.Admin.Pages.Logged.Entities
{
    public class RegisterModel : PageModel
    {

        private readonly EntityService _entityService;
        private readonly WatsonEntityService _coreEntityService;
        private readonly IndustryService _industryService;
        public RegisterModel(EntityService entityService, WatsonEntityService coreEntityService, IndustryService industryService)
        {
            _entityService = entityService;
            _coreEntityService = coreEntityService;
            _industryService = industryService;

        }




        [BindProperty]
        public ViewModel.Entity Entity { get; set; } = new ViewModel.Entity();

        public void OnGet(long id)
        {

            
            var entitiesUnrelateds = this._coreEntityService.GetNotRelatedCoreEntities();


            if(id > 0)
            {
                var entity = this._entityService.Get(id);
                var entitiesRelateds = this._coreEntityService.GetRelatedCoreEntities(id);
                this.Entity = parseToViewModel(entity);

                if(entitiesRelateds!=null && entitiesRelateds.Count> 0)
                {
                    this.Entity.RelatedWatsonEntity = entitiesRelateds.Select(x => new WatsonEntityiewModel()
                    {
                        Id = x.Id.ToString(),
                        Type = x.Type,
                        Text = x.Text
                    }).ToList();

                }
            }


            var industries = this._industryService.Get();
            if (industries != null && industries.Count > 0)
            {
                this.Entity.Industries.AddRange(industries.Select(x => new IndustryViewModel
                {
                    Id = x.Id,
                    Name = x.Name
                }));
            }

            this.Entity.UnRelatedWatsonEntity = entitiesUnrelateds.Select(x => new WatsonEntityiewModel()
            {
                Id = x.Id.ToString(),
                Type = x.Type,
                Text = x.Text
            }).ToList();


        }

        private ViewModel.Entity parseToViewModel(Action.Data.Models.Core.Watson.Entity entity)
        {

            if (entity == null)
                return new ViewModel.Entity();

            return new ViewModel.Entity()
            {
                Id = entity.Id,
                Name = entity.Name,
                Alias = entity.Alias,
                CategoryId = (int)entity.CategoryId,
                IndustryId = entity.IndustryId,
                FacebookUser = entity.FacebookUser,
                TweeterUser = entity.TweeterUser,
                InstagranUser = entity.InstagranUser,
                YoutubeUser = entity.YoutubeUser,
                PictureUrl = entity.PictureUrl,
                SiteUrl = entity.SiteUrl,
                Tier = entity.Tier
            };
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            try
            {
                var entityModel = parseToModel();

                var entityId = await this._entityService.Save(entityModel);

                if (entityId > 0)
                {

                    //Verificar se tem entidades que não estava relac

                    if (!string.IsNullOrEmpty(this.Entity.SelectedUnRelatedWatsonEntity))
                    {
                        var arrEntityToRelate = this.Entity.SelectedUnRelatedWatsonEntity.Split(';');
                        await this._coreEntityService.AddWatsonEntity(arrEntityToRelate, entityId);

                    }

                    //Desvincular entidades
                    if (!string.IsNullOrEmpty(this.Entity.UnSelectedUnRelatedWatsonEntity))
                    {
                        var arrToRemoveRelate = this.Entity.UnSelectedUnRelatedWatsonEntity.Split(';');
                        await this._coreEntityService.RemoveWatsonEntity(arrToRemoveRelate, entityId);

                    }
                }

                return RedirectToPage("Register", new { id = entityModel.Id });

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

            //return RedirectToPage("/Index");
        }

        private Action.Data.Models.Core.Watson.Entity parseToModel()
        {
            return new Action.Data.Models.Core.Watson.Entity
            {

                Id = this.Entity.Id,
                Name = this.Entity.Name,
                Alias = this.Entity.Alias,
                CategoryId = (Action.Data.Models.Core.ECategory)this.Entity.CategoryId,
                //Category = Enum.GetName(typeof(ECategory), CategoryId);
                IndustryId = this.Entity.IndustryId,
                FacebookUser = this.Entity.FacebookUser,
                TweeterUser = this.Entity.TweeterUser,
                InstagranUser = this.Entity.InstagranUser,
                YoutubeUser = this.Entity.YoutubeUser,
                PictureUrl = this.Entity.PictureUrl,
                SiteUrl = this.Entity.SiteUrl,
                Tier = this.Entity.Tier
            };
        }
    }
}