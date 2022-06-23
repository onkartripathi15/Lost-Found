using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace LostOrFound.Models
{
    public partial class Details
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ItemDetails { get; set; }
        public string Contact { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
}
