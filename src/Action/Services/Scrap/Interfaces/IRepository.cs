using System.Collections.Generic;

namespace Action.Services.Scrap.Interfaces
{
    /// <summary>
    ///     Repository interface
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        ///     List of Url
        /// </summary>
        Dictionary<string, string> List { get; }

        /// <summary>
        ///     To add url into List.
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pText"></param>
        void Add(string url, string pText);
    }
}