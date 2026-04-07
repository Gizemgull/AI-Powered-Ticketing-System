using System.Web.Mvc;
using ReservationSystem.Interfaces;
using ReservationSystem.Services;

namespace ReservationSystem.Controllers
{
    [Authorize]
    public class CommentAdminController : Controller
    {
        private readonly ICommentRepository _commentRepo = new CommentRepository();

        public ActionResult Index()
        {
            var list = _commentRepo.GetAllComments();
            return View(list);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            _commentRepo.DeleteComment(id);
            return RedirectToAction("Index");
        }
    }
}