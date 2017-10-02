using System.Collections.Generic;
using Action.Services.Scrap.Interfaces;

namespace Action.Services.Scrap.Repositories
{
    /// <summary>
    /// Class for Failed Urls.
    /// </summary>
    public class FailedUrlRepository : IRepository
    {
        /// <summary>
        /// List of failed Urls.
        /// </summary>
        Dictionary<string, string> _listOfFailedUrl;

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        public FailedUrlRepository()
        {
            _listOfFailedUrl = new Dictionary<string, string>();
        }

        /// <summary>
        /// List to gather Urls.
        /// </summary>
        public Dictionary<string, string> List
        {
            get
            {
                return _listOfFailedUrl;
            }
        }

        /// <summary>
        /// Method to add new Url.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pText"></param>
        public void Add(string entity, string pText)
        {
            _listOfFailedUrl.Add(entity, pText);
        }
    }
}
