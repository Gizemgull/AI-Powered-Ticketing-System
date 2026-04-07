using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.Entity; // Include ve Count işlemleri için gerekli
using ReservationSystem.Interfaces;
using ReservationSystem.Models; // DashboardViewModel burada
using ReservationSystem.Models.Sınıflar;
using ReservationSystem.Services;

namespace ReservationSystem.Controllers
{
    public class AdminController : Controller
    {
        // Login işlemleri için mevcut servisler
        private readonly IAdminRepository _adminRepository = new AdminRepository();
        private readonly SecurityService _securityService = new SecurityService();

        // Dashboard verilerini (İstatistikleri) çekmek için Context
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public AdminController()
        {

            if (_adminRepository.GetByUsername("ADMIN_KULLANICI_ADI") == null)
            {
                _adminRepository.AddAdmin(new Admin
                {
                    Username = "ADMIN_KULLANICI_ADI",
                    PasswordHash = _securityService.HashPassword("ADMIN_SIFRESI_BURAYA_GELECEK"),
                    Email = "admin@example.com"
                });
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string Username, string Password)
        {
            var admin = _adminRepository.GetByUsername(Username);

            if (admin != null && _securityService.VerifyPassword(Password, admin.PasswordHash))
            {
                FormsAuthentication.SetAuthCookie(admin.Username, false);
                return RedirectToAction("Index", "Admin");
            }

            ModelState.AddModelError("", "Kullanıcı adı veya şifre yanlış.");
            return View();
        }


        [Authorize]
        public ActionResult Index()
        {
            var model = new DashboardViewModel();

            // 1. Temel Sayılar
            model.TotalEvents = _context.Events.Count();
            model.TotalReservations = _context.Reservations.Count();
            model.TotalComments = _context.Comments.Count();

            // 2. Pasta Grafiği 
            model.PositiveCommentCount = _context.Comments.Count(c => c.Sentiment == "Positive");
            model.NegativeCommentCount = _context.Comments.Count(c => c.Sentiment == "Negative");
            model.NeutralCommentCount = _context.Comments.Count(c => c.Sentiment == "Neutral");

            // 3. EN POPÜLER 5 ETKİNLİK
            var topEvents = _context.Reservations
                                    .GroupBy(r => r.Event.Name) // Etkinlik ismine göre grupla
                                    .Select(g => new { Name = g.Key, Count = g.Count() }) // Sayılarını al
                                    .OrderByDescending(x => x.Count) // En çoktan aza sırala
                                    .Take(5) // İlk 5'i al
                                    .ToList();

            model.TopEventNames = topEvents.Select(x => x.Name).ToList();
            model.TopEventCounts = topEvents.Select(x => x.Count).ToList();

            model.LastReservations = _context.Reservations.Include(r => r.Event).OrderByDescending(r => r.ReservationDate).Take(5).ToList();
            model.LastComments = _context.Comments.Include(c => c.Event).OrderByDescending(c => c.CommentDate).Take(5).ToList();

            
            model.AllEvents = _context.Events.OrderByDescending(e => e.Date).ToList();

            return View(model);
        }
        [HttpPost]
        public JsonResult GetEventStats(int id)
        {
            var comments = _context.Comments.Where(c => c.EventID == id).ToList();

            int positive = comments.Count(c => c.Sentiment == "Positive");
            int negative = comments.Count(c => c.Sentiment == "Negative");

            int neutral = comments.Count(c => c.Sentiment == "Neutral");

            return Json(new { pos = positive, neg = negative, neu = neutral });
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Admin");
        }
    }
}