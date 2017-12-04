using System;
using System.Collections.Generic;
using Action.Models.Plans;
using Action.Models.Watson;

namespace Action.Models.ServiceAccount
{
    public class Account
    {
        public int Id { get; set; }
        public DateTime ActivationDate { get; set; }
        public EAccountStatus Status { get; set; } = EAccountStatus.Active;
        public String Name { get; set; }
        public Plan Plan { get; set; }
        public int? PlanId { get; set; }
        public User Administrator { get; set; }
        public string AdministratorId { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public List<Entity> Entities { get; set; }
    }
}