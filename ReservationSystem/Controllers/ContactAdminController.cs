using System.Web.Mvc;
using ReservationSystem.Interfaces;
using ReservationSystem.Services;

namespace ReservationSystem.Controllers
{
    [Authorize]
    public class ContactAdminController : Controller
    {
        private readonly IContactRepository _contactRepo = new ContactRepository();

        public ActionResult Index()
        {
            var messages = _contactRepo.GetAllMessages();
            return View(messages);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _contactRepo.DeleteMessage(id);
            return RedirectToAction("Index");
        }
    }
}