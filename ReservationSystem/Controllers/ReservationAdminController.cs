using ReservationSystem.Interfaces;
using ReservationSystem.Models.Sınıflar;
using ReservationSystem.Services;
using System.Linq;
using System.Web.Mvc;

namespace ReservationSystem.Controllers
{
    [Authorize]
    public class ReservationAdminController : Controller
    {
        private readonly IReservationRepository _resRepo = new ReservationRepository();
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public ActionResult Index()
        {
            var list = _resRepo.GetAllReservations();
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _resRepo.DeleteReservation(id);
            return RedirectToAction("Index");
        }
        // Belirli bir etkinliğin misafir listesini getir (Raporlama)
        public ActionResult GuestList(int eventId)
        {
            // Sadece o etkinliğe ait rezervasyonları çek
            var guests = _resRepo.GetAllReservations()
                                 .Where(r => r.EventID == eventId)
                                 .OrderBy(r => r.FullName)
                                 .ToList();

            // Eğer kimse yoksa uyarı verebilir
            return View(guests);
        }
        public ActionResult Reports()
        {
            // Tarihe göre sıralı tüm etkinlikleri getir
            var events = _context.Events.OrderByDescending(e => e.Date).ToList();
            return View(events);
        }
    }


}