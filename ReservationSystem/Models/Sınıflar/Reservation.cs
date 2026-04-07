using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ReservationSystem.Models.Sınıflar
{
    public class Reservation
    {
        public int ReservationID { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        public DateTime ReservationDate { get; set; } = DateTime.Now;

        // Yabancı Anahtar
        public int EventID { get; set; }

        [ForeignKey("EventID")]
        public virtual Event Event { get; set; }
    }
}