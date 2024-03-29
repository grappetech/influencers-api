﻿using System.Collections.Generic;
using Action.Services.Scrap.Interfaces;

namespace Action.Services.Scrap.Repositories
{
    /// <summary>
    ///     Class for External Urls.
    /// </summary>
    public class ExternalUrlRepository : IRepository
    {
        /// <summary>
        ///     List of external Urls.
        /// </summary>
        private readonly Dictionary<string, string> _listOfExternalUrl;

        /// <summary>
        ///     Constructor of the class.
        /// </summary>
        public ExternalUrlRepository()
        {
            _listOfExternalUrl = new Dictionary<string, string>();
        }

        /// <summary>
        ///     List to gather Urls.
        /// </summary>
        public Dictionary<string, string> List => _listOfExternalUrl;

        /// <summary>
        ///     Method to add new Url.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="pText"></param>
        public void Add(string entity, string pText)
        {
            _listOfExternalUrl.Add(entity, pText);
        }
    }
}