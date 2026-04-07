using System.Collections.Generic;
using ReservationSystem.Models.Sınıflar;

namespace ReservationSystem.Interfaces
{
    public interface ICommentRepository
    {
        void AddComment(Comment newComment);
        IEnumerable<Comment> GetCommentsByEventId(int eventId);

        // Admin İçin
        IEnumerable<Comment> GetAllComments();
        void DeleteComment(int id);
    }
}