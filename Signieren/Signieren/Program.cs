using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Signieren
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Erstellen eines UnicodeEncoder zum convertieren zwischen Byte-Array und String
                UnicodeEncoding ByteConverter = new UnicodeEncoding();

                // Erstellen der Byte-Arrays
                byte[] dataToSignate = ByteConverter.GetBytes("Diese Nachricht ist von mir!"), hash, signed_hash;
                String private_key, public_key;
                Boolean verified;

                SHA1CryptoServiceProvider SHA1 = new SHA1CryptoServiceProvider();


                // Erstellen einer neuen Instanz des CryptoServiceProviders um Private/Public Key zu erstellen
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    private_key = RSA.ToXmlString(true);
                    public_key = RSA.ToXmlString(false);
                }

                // Erstellen eines Hash-Wertes
                hash = SHA1.ComputeHash(dataToSignate);

                // Signieren
                signed_hash = RSASign(hash, private_key);

                Console.WriteLine("Signierter Hash");
                Console.WriteLine("===============");
                Console.WriteLine(Convert.ToBase64String(signed_hash) + "\n");

                // Verifizieren
                verified = RSAVerify(hash, signed_hash, public_key);
                Console.WriteLine("Verified?");
                Console.WriteLine("=========");
                Console.WriteLine((verified ? "Yes" : "No") + "\n");

            }
            catch (ArgumentNullException)
            {
                // ArgumentNullException wird geworfen, wenn ein Fehler aufgetreten ist.
                Console.WriteLine("Signing failed.");
            }

            Console.ReadKey();
        }


        static public Byte[] RSASign(Byte[] hash, String private_key)
        {
            try
            {
                byte[] signed;
                // Erstellen einer Instanz des CryptoServiceProviders
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    // Importieren der Schlüssel
                    RSA.FromXmlString(private_key);

                    // Verschlüsseln.
                    signed = RSA.SignHash(hash, CryptoConfig.MapNameToOID("SHA1"));
                }
                return signed;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        static public Boolean RSAVerify(Byte[] hash, Byte[] signed_hash, String public_key)
        {
            try
            {
                Boolean verified;
                // Erstellen einer Instanz des CryptoServiceProviders
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    // Importieren der Schlüssel
                    RSA.FromXmlString(public_key);

                    // Verschlüsseln.
                    verified = RSA.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA1"), signed_hash);
                }
                return verified;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return false;
            }
        }
    }
}
