using System.Collections.Generic;
using ReservationSystem.Models.Sınıflar;

namespace ReservationSystem.Interfaces
{
    public interface IContactRepository
    {
        void AddMessage(ContactMessage message); // Mesaj Kaydet
        IEnumerable<ContactMessage> GetAllMessages(); // Hepsini Getir
        void DeleteMessage(int id); // Sil
    }
}