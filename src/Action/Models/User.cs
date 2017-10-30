using System.ComponentModel.DataAnnotations.Schema;
using Action.Models.Plans;
using Action.Models.ServiceAccount;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Action.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public Plan Plan { get; set; }
        public int? PlanId { get; set; }
        public Account Account { get; set; }
        public int? AccountId { get; set; }
        
    }
}