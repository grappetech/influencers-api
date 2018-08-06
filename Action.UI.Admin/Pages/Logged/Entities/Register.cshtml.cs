using Action.Services;
using ActionUI.Admin.ViewModel;
using ActionUI.Admin.ViewModel.Industry;
using ActionUI.Admin.ViewModel.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ActionUI.Admin.Pages.Logged.Entities
{
    public class RegisterModel : PageModel
    {

        private readonly EntityService _entityService;
        private readonly WatsonEntityService _coreEntityService;
        private readonly IndustryService _industryService;
        private readonly SourceService _sourceService;
        private readonly EntityRoleService _entityRoleService;
        public RegisterModel(EntityService entityService, WatsonEntityService coreEntityService, IndustryService industryService, SourceService sourceService, EntityRoleService entityRoleService)
        {
            _entityService = entityService;
            _coreEntityService = coreEntityService;
            _industryService = industryService;
            _sourceService = sourceService;
            _entityRoleService = entityRoleService;


        }




        [BindProperty]
        public ViewModel.Entity Entity { get; set; } = new ViewModel.Entity();

        //public void OnGet(long id)
        public async Task OnGetAsync(long id)
        {

            await Task.Run(() =>
            {
                PrepareGet(id);
            });

        }

        private void PrepareGet(long id)
        {
            var entitiesUnrelateds = this._coreEntityService.GetNotRelatedCoreEntities();


            //buscar todos as fontes 
            var listScrapSource = this._sourceService.Get();

            if (id > 0)
            {
                var entity = this._entityService.Get(id);
                var entitiesRelateds = this._coreEntityService.GetRelatedCoreEntities(id);
                var industry = entity.IndustryId.HasValue ? this._industryService.Get((int)entity.IndustryId) : null;

                this.Entity = parseToViewModel(entity);

                if (entitiesRelateds != null && entitiesRelateds.Count > 0)
                {
                    this.Entity.RelatedWatsonEntity = entitiesRelateds.Select(x => new WatsonEntityiewModel()
                    {
                        Id = x.Id.ToString(),
                        Type = x.Type,
                        Text = x.Text
                    }).ToList();

                }

                var entitiesScrapSources = entity.ScrapSources.ToList();
                if (entitiesScrapSources != null && entitiesScrapSources.Count > 0)
                {

                    this.Entity.EntitySourceScraps = entitiesScrapSources.Select(scrapSourceEntity => new EntitySourceViewModel
                    {
                        Id = scrapSourceEntity.ScrapSourceId,
                        Alias = scrapSourceEntity.ScrapSource.Alias,
                        Limit = scrapSourceEntity.ScrapSource.Limit,
                        Url = scrapSourceEntity.ScrapSource.Url,
                        IndustryId = entity.IndustryId.HasValue ? entity.IndustryId.Value  : 0,
                        IsEntityRelated = true,
                        Selected = true,
                        DisplayOrder =100,


                    }).ToList();

                    if (industry != null && industry.ScrapSources != null)
                    {
                        var industryScrapSource = industry.ScrapSources.Select(scrapSourceEntity => new EntitySourceViewModel
                        {
                            Id = scrapSourceEntity.ScrapSourceId,
                            Alias = scrapSourceEntity.ScrapSource.Alias,
                            Limit = scrapSourceEntity.ScrapSource.Limit,
                            Url = scrapSourceEntity.ScrapSource.Url,
                            IsIndustryRelated = true,
                            Selected = true
                        }).ToList();

                        if (industryScrapSource != null)
                            this.Entity.EntitySourceScraps.AddRange(industryScrapSource);




                    }
                }

            }

            //get entityRoles
            var entityRoles = this._entityRoleService.GetEntityRoles();
            this.Entity.Roles = entityRoles.Select(r => new EntityRoleViewModel
            {
                Id = r.Id,
                Name = r.Name
            }).ToList();

            //popular todos os sources
            if (listScrapSource != null && listScrapSource.Count > 0)
            {
                var allSources = listScrapSource.Select(scrapSource => new EntitySourceViewModel
                {
                    Id = scrapSource.Id,
                    Alias = scrapSource.Alias,
                    Limit = scrapSource.Limit,
                    Url = scrapSource.Url,
                    IsNotRelated = true


                }).ToList();

                if (allSources != null && allSources.Count > 0)
                {
                    var filteredScrapSource = allSources.Where(scrapSource => !this.Entity.EntitySourceScraps.Any(s1 => s1.Id == scrapSource.Id)).ToList();
                    this.Entity.EntitySourceScraps.AddRange(filteredScrapSource);
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


            //ordenar por fontes da entidade
            if (this.Entity.EntitySourceScraps != null)
            {
                this.Entity.EntitySourceScraps = this.Entity.EntitySourceScraps.OrderBy(s => s.DisplayOrder).ToList();
            }

        }

        private ViewModel.Entity parseToViewModel(Action.Data.Models.Core.Watson.Entity entity)
        {

            if (entity == null)
                return new ViewModel.Entity();

            if (entity.CategoryId == Action.Data.Models.Core.ECategory.Personality)
                entity.CategoryId = Action.Data.Models.Core.ECategory.Person;




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
                Tier = entity.Tier,
                ExecutionInterval = entity.ExecutionInterval,
                SelectedRoles = entity.RelatedRoles,
                Ethnicity = entity.Ethnicity,
                Genre = entity.Genre,
                BirthDate = entity.BirthDate
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

            if (this.Entity.CategoryId == (int)Action.Data.Models.Core.ECategory.Personality)
                this.Entity.CategoryId = (int)Action.Data.Models.Core.ECategory.Person;

           var modelEntity =new  Action.Data.Models.Core.Watson.Entity
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
                Tier = this.Entity.Tier,
                RelatedRoles = this.Entity.SelectedRoles,
                ExecutionInterval = this.Entity.ExecutionInterval,
                Ethnicity = this.Entity.Ethnicity,
                Genre = this.Entity.Genre,
                BirthDate = this.Entity.BirthDate
            };



            if (!string.IsNullOrEmpty(this.Entity.SelectedSources))
            {
                string[] arrSources = this.Entity.SelectedSources.Split(';');
                if (arrSources != null && arrSources.Count() > 0)
                {
                    int scrapSourceId;

                    foreach (string _sourceId in arrSources)
                    {
                        if (int.TryParse(_sourceId, out scrapSourceId))
                        {
                             modelEntity.ScrapSources.Add(new  Action.Data.Models.Core.Scrap.ScrapSourceEntity
                            {
                                ScrapSourceId = scrapSourceId
                            });

                            scrapSourceId = 0;
                        }
                    }

                }
            }

            return modelEntity;
        }
    }
}