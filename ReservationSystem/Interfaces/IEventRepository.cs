using ReservationSystem.Models.Sınıflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationSystem.Interfaces
{
    public interface IEventRepository
    {
        // READ
        IEnumerable<Event> GetAllEvents();
        Event GetEventById(int id);

        // CREATE
        void AddEvent(Event newEvent);

        // UPDATE
        void UpdateEvent(Event updatedEvent);

        // DELETE
        void DeleteEvent(int id);
    }
}