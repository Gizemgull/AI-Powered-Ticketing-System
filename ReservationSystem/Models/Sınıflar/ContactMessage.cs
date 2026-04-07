using System;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.Models.Sınıflar
{
    public class ContactMessage
    {
        [Key]
        public int MessageID { get; set; }

        [Required(ErrorMessage = "Ad Soyad gereklidir.")]
        [Display(Name = "Ad Soyad")]
        public string SenderName { get; set; }

        [Required(ErrorMessage = "E-posta gereklidir.")]
        [EmailAddress]
        public string SenderEmail { get; set; }

        [Required(ErrorMessage = "Konu gereklidir.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Mesaj boş bırakılamaz.")]
        [StringLength(1000, ErrorMessage = "Mesaj çok uzun.")]
        public string MessageBody { get; set; }

        public DateTime SentDate { get; set; } = DateTime.Now;
    }
}