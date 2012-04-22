using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace HelloWorld_Hash
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetSHA1Hash("Hello World"));
            Console.WriteLine(GetSHA1Hash("Hello world"));

            Console.ReadKey();
        }

        public static string GetSHA1Hash(string text) {
            SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();

            byte[] arrayData;
            byte[] arrayResult;
            string result = null;
            string temp = null;

            // Umwandeln des Strings auf Typ byte[]
            arrayData = Encoding.ASCII.GetBytes(text);

            // Berechnung des SHA1-Hashes
            arrayResult = SHA1.ComputeHash(arrayData);

            // Umwandeln des byte[] auf Hex-String
            for (int i = 0; i < arrayResult.Length; i++)
            {
                temp = Convert.ToString(arrayResult[i], 16);
                if (temp.Length == 1)
                    temp = "0" + temp;
                result += temp;
            }
            
            return result;
        }
    }
}
