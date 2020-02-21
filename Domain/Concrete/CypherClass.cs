using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public static class CypherClass
    {
        public static Dictionary<string, string> categories = new Dictionary<string, string>() {
            ["Boksersky"] = "Боксерські перчатки",
            ["Snaryadni"] = "Снарядні перчатки",
            ["Lapy"] = "Лапи",
            ["Sholomy"] = "Захист голови",
            ["Kapy"] = "Капи",
            ["Nogi"] = "Захист ніг",
            ["Nakol"] = "Наколінники",
            ["Pah"] = "Захист паху",
            ["Inshe"] = "Інший захист",
            ["Trenuv"] = "Тренувальне обладнання",
            ["Binty"] = "Бінти",
            ["Gel"] = "Гелеві бінти",
            ["Masks"] = "Маски",
            ["Odyag"] = "Футболки і шорти",
            ["Bokserky"] = "Боксерки",
            ["Sumky"] = "Сумки"
        };
        public static string XORCipher(string data, string key)
        {
            byte[] arr = Encoding.UTF8.GetBytes(data);
            int dataLen = arr.Length;
            int keyLen = key.Length;
            byte[] output = new byte[dataLen];

            for (int i = 0; i < dataLen; ++i)
            {
                output[i] = (byte)(data[i] ^ key[i % keyLen]);
            }

            return Encoding.UTF8.GetString(output);
        }
        public static string md5(string input)
        {
            var md5 = MD5.Create();
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
        static Random rnd = new Random();
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            return source.OrderBy<T, int>((item) => rnd.Next());
        }
    }
}
