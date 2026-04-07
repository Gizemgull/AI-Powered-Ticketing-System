using ReservationSystem.Interfaces;
using ReservationSystem.Models.Sınıflar;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace ReservationSystem.Services
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public void AddReservation(Reservation newReservation)
        {
            _context.Reservations.Add(newReservation);
            _context.SaveChanges();
        }

        public IEnumerable<Reservation> GetAllReservations()
        {
            return _context.Reservations.Include(r => r.Event).OrderByDescending(r => r.ReservationDate).ToList();
        }

        public void DeleteReservation(int id)
        {
            var res = _context.Reservations.Find(id);
            if (res != null) { _context.Reservations.Remove(res); _context.SaveChanges(); }
        }

        public bool CheckReservation(int eventId, string email)
        {
            // Bu etkinliğe, bu mail adresiyle kayıt var mı?
            return _context.Reservations.Any(r => r.EventID == eventId && r.Email == email);
        }
    }
}