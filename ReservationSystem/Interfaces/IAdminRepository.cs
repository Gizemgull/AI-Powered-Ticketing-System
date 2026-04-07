using ReservationSystem.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationSystem.Interfaces
{
    public interface IAdminRepository
    {
        // Kullanıcı adına göre Admin nesnesini getirir
        Admin GetByUsername(string username);

        // Yeni admin kaydı ekler
        void AddAdmin(Admin admin);
    }
}