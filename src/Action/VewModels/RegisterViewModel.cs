﻿using System.ComponentModel.DataAnnotations;

namespace Action.VewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "Surname")]
        public string Surname { get; set; }

        [Display(Name = "PlanId")]
        public int? PlanId { get; set; }
        
        [Display(Name = "AccountId")]
        public int? AccountId { get; set; }
        
        [Required]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
            MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}