using System;
using System.Text;
using System.Security.Cryptography;

namespace SocialTopology 
{
    public static class SecurityHelper 
    {
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                // превращаем строку в массив байтов, потому что криптография работает только с байтами
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                
                // собираем байты обратно в длинную 16 строку хэш
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                
                return builder.ToString();
            }
        }
    }
}