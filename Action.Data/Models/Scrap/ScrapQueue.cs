using System;
using System.ComponentModel.DataAnnotations;

namespace Action.Data.Models.Core.Scrap
{
    public class ScrapQueue
    {
        [Key]
        public Guid Id { get; set; }

        public string Url { get; set; }
        public DateTime EnqueueDateTime { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public Boolean Completed { get; set; }
    }
}