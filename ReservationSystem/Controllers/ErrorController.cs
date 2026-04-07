using System.Web.Mvc;

namespace ReservationSystem.Controllers
{
    public class ErrorController : Controller
    {
        // 404: Sayfa Bulunamadı Hatası
        public ActionResult Page404()
        {
            return View();
        }

        // 500: Sunucu/Sistem Hatası 
        public ActionResult Page500()
        {
            return View();
        }
    }
}