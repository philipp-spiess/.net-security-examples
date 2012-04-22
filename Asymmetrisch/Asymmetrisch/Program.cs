using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace Asymmetrisch
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
                byte[] dataToEncrypt = ByteConverter.GetBytes("Ultra geheime Maturaangabe...");
                byte[] encryptedData;
                byte[] decryptedData;
                String private_key, public_key;


                // Erstellen einer neuen Instanz des CryptoServiceProviders um Private/Public Key zu erstellen
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    private_key = RSA.ToXmlString(true);
                    public_key = RSA.ToXmlString(false);
                    

                    Console.WriteLine("Private and Public Key as XML");
                    Console.WriteLine("=============================");
                    Console.WriteLine(private_key+ "\n");

                    Console.WriteLine("Only Public Key as XML");
                    Console.WriteLine("======================");
                    Console.WriteLine(public_key + "\n");
                    
                }
                // Verschlüsseln mit dem Public Key
                encryptedData = RSAEncrypt(dataToEncrypt, public_key);

                // Entschlüsseln mit dem Private Key
                decryptedData = RSADecrypt(encryptedData, private_key);

                Console.WriteLine("Decrypted plaintext: {0}", ByteConverter.GetString(decryptedData));
    
            }
            catch (ArgumentNullException)
            {
                // ArgumentNullException wird geworfen, wenn ein Fehler aufgetreten ist.
                Console.WriteLine("Encryption failed.");
            }

            Console.ReadKey();
        }

        static public byte[] RSAEncrypt(byte[] DataToEncrypt, String public_key)
        {
            try
            {
                byte[] encryptedData;
                // Erstellen einer Instanz des CryptoServiceProviders
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    // Importieren der Schlüssel
                    RSA.FromXmlString(public_key);

                    // Verschlüsseln.
                    encryptedData = RSA.Encrypt(DataToEncrypt, true);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        static public byte[] RSADecrypt(byte[] DataToDecrypt, String private_key)
        {
            try
            {
                byte[] decryptedData;
                // Erstellen einer Instanz des CryptoServiceProviders
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    // Importieren der Schlüssel
                    RSA.FromXmlString(private_key);

                    // Verschlüsseln.
                    decryptedData = RSA.Decrypt(DataToDecrypt, true);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}
