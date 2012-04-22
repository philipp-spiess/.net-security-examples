using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Symmetrisch
{
    class Program
    {
        static void Main(string[] args)
        {
            String key = GenerateKey();
            String plain = "Ultra secret Maturaaufgabe...";

            Console.WriteLine("      key = " + key);
            Console.WriteLine("    plain = " + plain);

            // Verschlüsseln des Klartextes
            String encrypted = Encrypt(plain, key);

            Console.WriteLine("encrypted = " + encrypted);

            // Entschlüsseln
            String decrypted = Decrypt(encrypted, key);

            Console.WriteLine("decrypted = " + decrypted);


            Console.ReadKey();
        }

        static string GenerateKey()
        {
            // Instanz des CryptoServiceProviders wird erstellt. Der Schlüssel und Initialisierungsvektor wird automatisch erstellt.
            DESCryptoServiceProvider des = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();

            // Den automatisch generierten Schlüssel zurückgeben
            return Convert.ToBase64String(des.Key);
        }

        static string Encrypt(string plain, string key)
        {
            // Konvertieren in Byte Array
            byte[] plain_ba = Encoding.UTF8.GetBytes(plain);
            byte[] key_ba = Convert.FromBase64String(key);

            // Initialisieren des CryptoServiceProviders
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = key_ba;
            des.IV  = key_ba;

            // CryptoTransform Schnittstelle zum Verschlüsseln
            ICryptoTransform desencrypt = des.CreateEncryptor();

            // verschlüsseln
            byte[] encrypted = desencrypt.TransformFinalBlock(plain_ba, 0, plain_ba.GetLength(0));

            // den Base64 string zurückgeben
            return Convert.ToBase64String(encrypted);
        }

        static string Decrypt(string encrypted, string key)
        {
            // Konvertieren in Byte Array
            byte[] encrypted_ba = Convert.FromBase64String(encrypted);
            byte[] key_ba = Convert.FromBase64String(key);

            // Initialisieren des CryptoServiceProviders
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = key_ba;
            des.IV = key_ba;

            // CryptoTransform Schnittstelle zum Entschlüsseln
            ICryptoTransform desdecrypt = des.CreateDecryptor();

            // entschlüsseln
            byte[] decrytped = desdecrypt.TransformFinalBlock(encrypted_ba, 0, encrypted_ba.GetLength(0));

            // in String convertieren
            return Encoding.UTF8.GetString(decrytped);
        }
    }
}
