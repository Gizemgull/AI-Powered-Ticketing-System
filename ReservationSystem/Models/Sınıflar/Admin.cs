using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservationSystem.Models.Sınıflar
{
    public class Admin
    {
        public int AdminID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        
        public string PasswordHash { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}