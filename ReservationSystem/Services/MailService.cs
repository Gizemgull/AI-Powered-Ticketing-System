using System;
using System.Net;
using System.Net.Mail;
using ReservationSystem.Models.Sınıflar; // Veritabanı modellerine erişim için

namespace ReservationSystem.Services
{
    public class MailService
    {

        private static readonly string SENDER_EMAIL = "biletbullll@gmail.com";
        private static readonly string SENDER_PASSWORD = "GMAIL_SIFRESI_BURAYA_GELECEK";



        public static void SendReservationEmail(Reservation reservation, Event eventInfo)
        {
            
            string toEmail = reservation.Email;
            string subject = "Rezervasyonunuz Alındı! - BiletBul";

            
            string body = $@"
                <div style='font-family:Arial; padding:20px; border:1px solid #ddd;'>
                    <h2 style='color:#00c062;'>Rezervasyonunuz Alındı!</h2>
                    <p>Sayın <strong>{reservation.FullName}</strong>,</p>
                    <p>Etkinlik rezervasyon işleminiz başarıyla gerçekleşmiştir.</p>
                    
                    <div style='background-color:#f9f9f9; padding:15px; border-radius:5px;'>
                        <h3>Etkinlik Detayları</h3>
                        <p><strong>Etkinlik:</strong> {eventInfo.Name}</p>
                        <p><strong>Tarih:</strong> {eventInfo.Date.ToString("dd MMMM yyyy HH:mm")}</p>
                        <p><strong>Konum:</strong> {eventInfo.Location}</p>
                        <p><strong>Şehir:</strong> {eventInfo.City}</p>
                    </div>
                    <br/>
                    <p>İyi eğlenceler dileriz.<br/><strong>BiletBul Ekibi</strong></p>
                </div>";

            // Gönderme işlemini başlat
            SendEmail(toEmail, subject, body);
        }


        // --- 3. ADMİNE  GİDEN MAİL 
  
        public static void SendContactNotification(string senderName, string senderEmail, string subject, string messageBody)
        {
           
            string toAdminEmail = SENDER_EMAIL;
            string emailSubject = $"Yeni İletişim Mesajı: {subject}";

           
            string body = $@"
                <h3>Web Sitenizden Yeni Bir Mesaj Var!</h3>
                <p><strong>Gönderen:</strong> {senderName}</p>
                <p><strong>E-posta:</strong> {senderEmail}</p>
                <hr/>
                <p><strong>Mesaj:</strong></p>
                <p>{messageBody}</p>
                <br/>
                <small>Bu mail BiletBul sisteminden otomatik gönderilmiştir.</small>";

            
            SendEmail(toAdminEmail, emailSubject, body);
        }


        // ORTAK GÖNDERME  (Private)
 
        private static void SendEmail(string toEmail, string subject, string htmlBody)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    // Kimden gidiyor
                    mail.From = new MailAddress(SENDER_EMAIL, "BiletBul Bilgilendirme");
                    // Kime gidiyor
                    mail.To.Add(toEmail);
                    // Konu ve İçerik
                    mail.Subject = subject;
                    mail.Body = htmlBody;
                    mail.IsBodyHtml = true; 

                    // Gmail SMTP Ayarları
                    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                    {
                        smtp.Credentials = new NetworkCredential(SENDER_EMAIL, SENDER_PASSWORD);
                        smtp.EnableSsl = true; // Güvenli bağlantı
                        smtp.Send(mail); // Fırlat! 🚀
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata olursa program çökmesin, sadece hata mesajını konsola yazsın
                System.Diagnostics.Debug.WriteLine("Mail Gönderme Hatası: " + ex.Message);
            }
        }
    }
}