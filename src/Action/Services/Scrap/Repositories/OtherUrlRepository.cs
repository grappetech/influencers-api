using System.Collections.Generic;
using Action.Services.Scrap.Interfaces;

namespace Action.Services.Scrap.Repositories
{
    /// <summary>
    ///     Class for External Urls.
    /// </summary>
    public class OtherUrlRepository : IRepository
    {
        /// <summary>
        ///     List of external Urls.
        /// </summary>
        private readonly Dictionary<string, string> _listOfOtherUrl;

        /// <summary>
        ///     Constructor of the class.
        /// </summary>
        public OtherUrlRepository()
        {
            _listOfOtherUrl = new Dictionary<string, string>();
        }

        /// <summary>
        ///     List to gather Urls.
        /// </summary>
        public Dictionary<string, string> List => _listOfOtherUrl;

        /// <summary>
        ///     Method to add new Url.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pText"></param>
        public void Add(string entity, string pText)
        {
            _listOfOtherUrl.Add(entity, pText);
        }
    }
}