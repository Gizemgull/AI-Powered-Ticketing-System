using ReservationSystem.Interfaces;
using ReservationSystem.Models.Sınıflar;
using System.Collections.Generic;
using System.Linq;

namespace ReservationSystem.Services
{
    public class ContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public void AddMessage(ContactMessage message)
        {
            _context.ContactMessages.Add(message);
            _context.SaveChanges();
        }

        public IEnumerable<ContactMessage> GetAllMessages()
        {
            // En yeni mesaj en üstte görünsün
            return _context.ContactMessages.OrderByDescending(m => m.SentDate).ToList();
        }

        public void DeleteMessage(int id)
        {
            var msg = _context.ContactMessages.Find(id);
            if (msg != null)
            {
                _context.ContactMessages.Remove(msg);
                _context.SaveChanges();
            }
        }
    }
}