using ReservationSystem.Interfaces;
using ReservationSystem.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ReservationSystem.Services
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public IEnumerable<Event> GetAllEvents()
        {
            return _context.Events.ToList();
        }

        public Event GetEventById(int id)
        {
            return _context.Events.Find(id);
        }

        public void AddEvent(Event newEvent)
        {
            _context.Events.Add(newEvent);
            _context.SaveChanges();
        }

        public void UpdateEvent(Event updatedEvent)
        {
            // EF'e modelin güncellendiğini bildirir
            _context.Entry(updatedEvent).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteEvent(int id)
        {
            var eventToDelete = _context.Events.Find(id);
            if (eventToDelete != null)
            {
                _context.Events.Remove(eventToDelete);
                _context.SaveChanges();
            }
        }
    }
}