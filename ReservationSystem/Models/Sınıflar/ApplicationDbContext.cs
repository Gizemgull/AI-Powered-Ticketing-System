using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ReservationSystem.Models.Sınıflar
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("name=EventContext")
        {
        }

        // Tabloları temsil eden DbSet'ler
        public DbSet<Event> Events { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public object Categories { get; internal set; }
    }
}