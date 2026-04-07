using ReservationSystem.Interfaces;
using ReservationSystem.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationSystem.Services
{
    public class AdminRepository : IAdminRepository
    {
        // veritabanı bağlamı
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public Admin GetByUsername(string username)
        {
            // Büyük/küçük harf duyarlılığı olmadan arama
            return _context.Admins.FirstOrDefault(a => a.Username.Equals(username, System.StringComparison.OrdinalIgnoreCase));
        }

        public void AddAdmin(Admin admin)
        {
            _context.Admins.Add(admin);
            _context.SaveChanges();
        }
    }
}