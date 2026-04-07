using ReservationSystem.Interfaces;
using ReservationSystem.Models.Sınıflar;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace ReservationSystem.Services
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public void AddComment(Comment newComment)
        {
            _context.Comments.Add(newComment);
            _context.SaveChanges();
        }

        public IEnumerable<Comment> GetCommentsByEventId(int eventId)
        {
            return _context.Comments.Where(c => c.EventID == eventId).OrderByDescending(c => c.CommentDate).ToList();
        }

        public IEnumerable<Comment> GetAllComments()
        {
            return _context.Comments.Include(c => c.Event).OrderByDescending(c => c.CommentDate).ToList();
        }

        public void DeleteComment(int id)
        {
            var comment = _context.Comments.Find(id);
            if (comment != null) { _context.Comments.Remove(comment); _context.SaveChanges(); }
        }
    }
}