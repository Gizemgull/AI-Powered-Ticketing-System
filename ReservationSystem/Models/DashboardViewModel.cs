using System.Collections.Generic;
using ReservationSystem.Models.Sınıflar;

namespace ReservationSystem.Models
{
    public class DashboardViewModel
    {
        // Kartlarda görünecek sayılar
        public int TotalEvents { get; set; }
        public int TotalReservations { get; set; }
        public int TotalComments { get; set; }

        // Grafik için gerekli veriler
        public int PositiveCommentCount { get; set; }
        public int NegativeCommentCount { get; set; }
        public int NeutralCommentCount { get; set; }

        // Son eklenenler (Tablo için)
        public List<Reservation> LastReservations { get; set; }
        public List<Comment> LastComments { get; set; }
        public List<string> TopEventNames { get; set; } // Etkinlik İsimleri
        public List<int> TopEventCounts { get; set; }
        public List<ReservationSystem.Models.Sınıflar.Event> AllEvents { get; set; }
    }
}