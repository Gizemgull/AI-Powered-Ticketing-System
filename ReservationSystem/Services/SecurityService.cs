using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ReservationSystem.Services
{
    public class SecurityService
    {
        /// <summary>Şifreyi kriptografik olarak güvenli bir hash değere çevirir.</summary>
        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 12);
        }

        /// <summary>Hashlenmiş şifre ile girilen şifreyi karşılaştırır.</summary>
        public bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}