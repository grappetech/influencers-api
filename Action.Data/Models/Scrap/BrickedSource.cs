using System;
using System.ComponentModel.DataAnnotations;

namespace Action.Data.Models.Core.Scrap
{
    public class BrickedSource
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();
        [MaxLength(400)]
        public string Title { get; set; }
        [MaxLength(400)]
        public string Url { get; set; }
        [MaxLength(200)]
        public string Step { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
    }
}