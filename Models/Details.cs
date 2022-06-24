using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace LostOrFound.Models
{
    public partial class Details
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please enter First Name.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please enter LastName.")]
        public string LastName { get; set; }
      
      
        public string ItemDetails { get; set; }
        [Required()]
        public string Contact { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Date { get; set; }
        [Required]
        public string Time { get; set; }
      
    }
}
