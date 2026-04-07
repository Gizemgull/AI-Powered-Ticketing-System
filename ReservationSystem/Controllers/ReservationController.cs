using System.Web.Mvc;
using ReservationSystem.Interfaces;
using ReservationSystem.Services;
using ReservationSystem.Models.Sınıflar;

namespace ReservationSystem.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _resRepo = new ReservationRepository();

        private readonly IEventRepository _eventRepo = new EventRepository(); // Etkinlik bilgisini çekmek için
        private readonly MailService _emailService = new MailService();     // Mail göndermek için

        // REZERVASYON OLUŞTURMA

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reservation model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // 1. Veritabanına Kaydet
                    _resRepo.AddReservation(model);

                    // 2. Etkinlik Bilgilerini Çek 
                    var eventInfo = _eventRepo.GetEventById(model.EventID);

                    // 3. E-posta Gönder 
                    if (eventInfo != null)
                    {
                        MailService.SendReservationEmail(model, eventInfo);
                    }

                    // 4. Başarılı Mesajıyla Geri Dön
                    return RedirectToAction("Details", "Home", new { id = model.EventID, message = "reservation-success" });
                }
                catch (System.Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Hata: " + ex.Message);
                    return RedirectToAction("Details", "Home", new { id = model.EventID, message = "reservation-fail" });
                }
            }

            return RedirectToAction("Details", "Home", new { id = model.EventID, message = "reservation-fail" });
        }
    }
}