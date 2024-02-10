//============================================================================================//
// Modified by:BelicusBr                                                                      //
// URL:https://github.com/BelicusBr                                                           //
// original                                                                                   //
// answered Apr 16, 2012 at 15:24                                                             //
// CraigTP:https://stackoverflow.com/users/57477/craigtp                                      //
// edited May 6, 2019 at 12:46                                                                //
// Kolappan N:https://stackoverflow.com/users/5407188/kolappan-n                              //
// Encrypting & Decrypting a String in C# [duplicate]                                         //
// URL:https://stackoverflow.com/questions/10168240/encrypting-decrypting-a-string-in-c-sharp //
//============================================================================================//
using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Security.Cryptography;

namespace Cobilas.Unity.Utility.Cryptography {
    public static class StringCipher {
        public const string password = "14111996";
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainTextBytes)
            => Encrypt(plainTextBytes, password, Encoding.UTF8);

        public static string Encrypt(string plainTextBytes, Encoding encoding)
            => Encrypt(plainTextBytes, password, encoding);

        public static string Encrypt(string plainTextBytes, string passPhrase)
            => Encrypt(plainTextBytes, passPhrase, Encoding.UTF8);

        public static string Encrypt(string plainTextBytes, string passPhrase, Encoding encoding)
            => Convert.ToBase64String(EncryptBytes(encoding.GetBytes(plainTextBytes), passPhrase));

        public static byte[] EncryptBytes(byte[] plainTextBytes)
            => EncryptBytes(plainTextBytes, password);

        public static byte[] EncryptBytes(byte[] plainTextBytes, string passPhrase) {
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            byte[] saltStringBytes = Generate256BitsOfRandomEntropy();
            byte[] ivStringBytes = Generate256BitsOfRandomEntropy();
            //byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations)) {
                byte[] keyBytes = password.GetBytes(Keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged()) {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes)) {
                        using (MemoryStream memoryStream = new MemoryStream()) {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)) {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                byte[] cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return cipherTextBytes;
                                //return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText)
            => Decrypt(cipherText, password);

        public static string Decrypt(string cipherText, string passPhrase)
            => Decrypt(cipherText, passPhrase, Encoding.UTF8);

        public static string Decrypt(string cipherText, Encoding encoding)
            => Decrypt(cipherText, password, encoding);

        public static string Decrypt(string cipherText, string passPhrase, Encoding encoding)
            => encoding.GetString(DecryptBytes(Convert.FromBase64String(cipherText), passPhrase));

        public static byte[] DecryptBytes(byte[] cipherText)
            => DecryptBytes(cipherText, password);

        public static byte[] DecryptBytes(byte[] cipherText, string passPhrase) {
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            byte[] cipherTextBytesWithSaltAndIv = cipherText;
            //byte[] cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            byte[] saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            byte[] ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            byte[] cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (Rfc2898DeriveBytes password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations)) {
                byte[] keyBytes = password.GetBytes(Keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged()) {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes)) {
                        using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes)) {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)) {
                                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                _ = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                //int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return plainTextBytes;
                                //return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy() {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
                rngCsp.GetBytes(randomBytes);// Fill the array with cryptographically secure random bytes.
            return randomBytes;
        }
    }
}