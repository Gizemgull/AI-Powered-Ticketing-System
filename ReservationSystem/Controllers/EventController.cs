using ReservationSystem.Interfaces;
using ReservationSystem.Models.Sınıflar;
using ReservationSystem.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ReservationSystem.Models; // CityData için gerekli

namespace ReservationSystem.Controllers
{
    // Sadece Adminler erişebilir
    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventRepository _eventRepository = new EventRepository();

        // 2.1. ETKİNLİK LİSTELEME (READ)
       
        public ActionResult Index()
        {
            var events = _eventRepository.GetAllEvents();
            return View(events);
        }

        // 2.2. ETKİNLİK OLUŞTURMA (CREATE)
        [HttpGet] 
        public ActionResult Create()
        {
            ViewBag.CityList = CityData.GetCitySelectList(); // Listeyi taşı
            return View(new Event { Date = System.DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Event model, HttpPostedFileBase EventImage) // Resim parametresi
        {
            if (ModelState.IsValid)
            {
                if (EventImage != null && EventImage.ContentLength > 0)
                {
                    // 1. Dosya adını al ve benzersiz yap 
                    string fileName = Path.GetFileName(EventImage.FileName);
                    string uniqueName = Guid.NewGuid().ToString() + "_" + fileName;

                    // 2. Kaydedilecek yolu belirle 
                    string path = Path.Combine(Server.MapPath("~/Images/Events"), uniqueName);

                    // 3. Resmi klasöre kaydet
                    EventImage.SaveAs(path);

                    // 4. Veritabanına kaydedilecek yolu modele ata
                    model.ImageUrl = "/Images/Events/" + uniqueName;
                }
                else
                {
                    // Resim yüklenmezse varsayılan bir resim yolu ata 
                    model.ImageUrl = "/Images/Events/default.jpg";
                }

                _eventRepository.AddEvent(model);
                return RedirectToAction("Index");
            }
            ViewBag.CityList = CityData.GetCitySelectList();
            return View(model);
        }

        // 2.3. ETKİNLİK DÜZENLEME (UPDATE)

        [HttpGet] 
        public ActionResult Edit(int id)
        {
            var model = _eventRepository.GetEventById(id);
            if (model == null) return HttpNotFound();
            ViewBag.CityList = CityData.GetCitySelectList();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Event model, HttpPostedFileBase EventImage)
        {
            if (ModelState.IsValid)
            {
                if (EventImage != null && EventImage.ContentLength > 0)
                {
                    string fileName = Path.GetFileName(EventImage.FileName);
                    string uniqueName = Guid.NewGuid().ToString() + "_" + fileName;
                    string path = Path.Combine(Server.MapPath("~/Images/Events"), uniqueName);

                    EventImage.SaveAs(path);

                    // Yeni resim seçildiyse yolu güncelle
                    model.ImageUrl = "/Images/Events/" + uniqueName;
                }

                _eventRepository.UpdateEvent(model);
                return RedirectToAction("Index");
            }
            ViewBag.CityList = CityData.GetCitySelectList();
            return View(model);
        }

        // 2.4. ETKİNLİK SİLME (DELETE)

        [HttpGet] // Silme onay sayfasını gösterir
        public ActionResult Delete(int id)
        {
            var model = _eventRepository.GetEventById(id);
            if (model == null) return HttpNotFound();
            return View(model);
        }

        [HttpPost, ActionName("Delete")] // Silme işlemini POST ile onaylar
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _eventRepository.DeleteEvent(id);
            return RedirectToAction("Index"); // Listeye geri dön
        }
    }
}