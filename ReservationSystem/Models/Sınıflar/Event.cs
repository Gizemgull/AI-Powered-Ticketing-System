using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ReservationSystem.Models.Sınıflar
{
    public class Event
    {
        public int EventID { get; set; }

        [Required]
        [StringLength(150)]
        public string Name { get; set; }

        public string Description { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [StringLength(200)]
        public string Location { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigasyon Özellikleri
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string City { get; set; }
        public string Category { get; set; }
        public int Quota { get; set; } = 10;
    }
}