using System.Collections.Generic;
using Action.Services.Scrap.Interfaces;

namespace Action.Services.Scrap.Repositories
{
    public class CurrentPageUrlRepository : IRepository
    {
        /// <summary>
        /// List of external Urls.
        /// </summary>
        Dictionary<string, string> _listOfCurrentPageUrl;

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        public CurrentPageUrlRepository()
        {
            _listOfCurrentPageUrl = new Dictionary<string, string>();
        }

        /// <summary>
        /// List to gather Urls.
        /// </summary>
        public Dictionary<string, string> List
        {
            get
            {
                return _listOfCurrentPageUrl;
            }
        }

        /// <summary>
        /// Method to add new Url.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pText"></param>
        public void Add(string entity, string pText)
        {
            _listOfCurrentPageUrl.Add(entity, pText);
        }
    }
}
