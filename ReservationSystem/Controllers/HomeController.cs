using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ReservationSystem.Services;
using ReservationSystem.Interfaces;
using ReservationSystem.Models.Sınıflar;

namespace ReservationSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventRepository _eventRepository = new EventRepository();
        private readonly ICommentRepository _commentRepository = new CommentRepository();

        // İletişim servisi
        private readonly IContactRepository _contactRepo = new ContactRepository();

        //  ANA SAYFA 
        public ActionResult Index(string search, string filter, string category, decimal? maxPrice, bool? yakinda, string city)
        {
            var query = _eventRepository.GetAllEvents().AsQueryable();
            query = query.Where(e => e.IsActive);

            if (!string.IsNullOrEmpty(city)) { query = query.Where(e => e.City == city); ViewBag.CurrentCity = city; }
            else { ViewBag.CurrentCity = "Konum Seç"; }

            if (!string.IsNullOrEmpty(category) && category != "Trendler") { query = query.Where(e => e.Category == category); }
            if (maxPrice.HasValue && maxPrice.Value > 0) { query = query.Where(e => e.Price <= maxPrice.Value); }
            if (yakinda.HasValue && yakinda.Value == true) { query = query.Where(e => e.Date <= System.DateTime.Now.AddDays(30)); }
            if (!string.IsNullOrEmpty(search)) { string s = search.ToLower(); query = query.Where(e => e.Name.ToLower().Contains(s) || e.Description.ToLower().Contains(s) || e.Location.ToLower().Contains(s)); }

            if (!string.IsNullOrEmpty(filter))
            {
                var today = System.DateTime.Today;
                if (filter == "Bugun") query = query.Where(e => e.Date.Date == today);
                else if (filter == "Yarin") query = query.Where(e => e.Date.Date == today.AddDays(1));
                else if (filter == "BuHafta") query = query.Where(e => e.Date >= today && e.Date <= today.AddDays(7));
            }
            else { query = query.Where(e => e.Date > System.DateTime.Now); }

            var model = query.OrderBy(e => e.Date).ToList();
            return View(model);
        }

        public ActionResult Details(int id, string message = null)
        {
            var eventDetail = _eventRepository.GetEventById(id);
            if (eventDetail == null || !eventDetail.IsActive) return HttpNotFound();
            ViewBag.Comments = _commentRepository.GetCommentsByEventId(id);
            ViewBag.Message = message;
            return View(eventDetail);
        }

        // HAKKIMIZDA 
        public ActionResult About()
        {
            return View();
        }

        // İLETİŞİM 
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }


        // İLETİŞİM (MESAJ GÖNDERME) 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactMessage model)
        {
            if (ModelState.IsValid)
            {
                // 1. Veritabanına Kaydet 
                _contactRepo.AddMessage(model);

                MailService.SendContactNotification(model.SenderName, model.SenderEmail, model.Subject, model.MessageBody);

                // 3. Başarı Mesajı
                ViewBag.Success = "Mesajınız başarıyla iletildi! Yöneticilerimize e-posta bildirimi gönderildi.";
                ModelState.Clear();
                return View();
            }
            return View(model);
        }
    }
}