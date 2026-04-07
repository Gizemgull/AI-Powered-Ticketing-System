using System.Web.Mvc;
using System.Threading.Tasks;
using ReservationSystem.Interfaces;
using ReservationSystem.Services;
using ReservationSystem.Models.Sınıflar;

namespace ReservationSystem.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepo = new CommentRepository();
        private readonly IReservationRepository _resRepo = new ReservationRepository();
        private readonly SentimentApiService _sentimentService = new SentimentApiService();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddComment(Comment model)
        {
            if (ModelState.IsValid)
            {
                // 1. Rezervasyon Kontrolü
                bool hasReservation = _resRepo.CheckReservation(model.EventID, model.Email);
                if (!hasReservation)
                {
                    // Hata mesajı
                    return RedirectToAction("Details", "Home", new { id = model.EventID, message = "no-reservation" });
                }

                // 2. API Analizi
                try
                {
                    string sentiment = await _sentimentService.GetSentimentAsync(model.Text);
                    model.Sentiment = sentiment;
                }
                catch
                {
                    model.Sentiment = "Nötr (API Hatası)";
                }

                // 3. Kayıt
                _commentRepo.AddComment(model);

                return RedirectToAction("Details", "Home", new { id = model.EventID, message = "comment-success" });
            }

            return RedirectToAction("Details", "Home", new { id = model.EventID, message = "comment-fail" });
        }
    }
}