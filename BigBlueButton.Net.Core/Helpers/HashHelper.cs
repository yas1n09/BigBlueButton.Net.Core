using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BigBlueButton.Net.Core.Helpers
{
    public class HashHelper
    {
        public static string Sha1Hash(string str, string salt = null)
        {
            if (!string.IsNullOrEmpty(salt)) str = str + salt; // Eğer salt (tuz) belirtilmişse, string'e ekleriz.

            byte[] data = Encoding.UTF8.GetBytes(str); // Veriyi UTF-8 formatında byte dizisine dönüştürür.

            var sha1 = SHA1.Create(); // SHA1 algoritmasını oluşturur.
            var result = sha1.ComputeHash(data); // Verinin hash'ini hesaplar.
            StringBuilder EnText = new StringBuilder(); // Hash sonucunu birleştireceğimiz StringBuilder nesnesi.

            foreach (byte iByte in result)
            {
                EnText.AppendFormat("{0:x2}", iByte); // Hash sonucunu hexadecimal formatta ekler.
            }

            return EnText.ToString(); // Hash değerini string olarak döndürür.
        }

    }
}
