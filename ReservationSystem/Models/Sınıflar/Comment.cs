using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationSystem.Models.Sınıflar
{
    public class Comment
    {
        [Key]
        public int CommentID { get; set; }

        public int EventID { get; set; }

        [ForeignKey("EventID")]
        public virtual Event Event { get; set; }

        [Required]
        public string AuthorName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(500)]
        public string Text { get; set; }

        public string Sentiment { get; set; }

        public DateTime CommentDate { get; set; } = DateTime.Now;
    }
}