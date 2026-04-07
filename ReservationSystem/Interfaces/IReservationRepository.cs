using System.Collections.Generic;
using ReservationSystem.Models.Sınıflar;

namespace ReservationSystem.Interfaces
{
    public interface IReservationRepository
    {
        void AddReservation(Reservation newReservation);

        // Admin İçin: Hepsini Getir
        IEnumerable<Reservation> GetAllReservations();

        // Admin İçin: Sil
        void DeleteReservation(int id);

        // Kullanıcı İçin: Rezervasyon Var mı Kontrolü
        bool CheckReservation(int eventId, string email);
    }
}