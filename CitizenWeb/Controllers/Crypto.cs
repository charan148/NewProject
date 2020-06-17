using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace CitizenWeb.Controllers
{
    /// <summary>
    /// The Crypto class. this class used for password encryption and Base64 convertions.
    /// </summary>
    public static class Crypto
    {
        /// <summary>
        /// Gets the crypto key.
        /// </summary>
        /// <value>The crypto key.</value>
        private static string CryptoKey
        {
            get
            {
                return "d5NVI1!";
            }
        }

        /// <summary>
        /// Determines whether the specified text is encrypted.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns>
        ///   <c>true</c> if the specified text is encrypted; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEncrypted(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            return text.Contains("=");
        }

        /// <summary>
        /// Encrypts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The encrypted string.</returns>
        public static string Encrypt(string source)
        {
            string returnValue;
            using (AesCryptoServiceProvider cryptoService = new AesCryptoServiceProvider())
            {
                byte[] bytIn = System.Text.ASCIIEncoding.ASCII.GetBytes(source);

                // create a MemoryStream so that the process can be done without I/O files
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    byte[] bytKey = GetLegalKey(CryptoKey);

                    // set the private key
                    cryptoService.Key = bytKey;
                    cryptoService.IV = bytKey;

                    // create an Encryptor from the Provider Service instance
                    using (ICryptoTransform encrypto = cryptoService.CreateEncryptor())
                    {
                        // create Crypto Stream that transforms a stream using the encryption
                        using (CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write))
                        {
                            // write out encrypted content into MemoryStream
                            cs.Write(bytIn, 0, bytIn.Length);
                            cs.FlushFinalBlock();

                            // get the output and trim the '\0' bytes
                            byte[] bytOut = ms.GetBuffer();
                            int i = 0;
                            for (i = 0; i < bytOut.Length; i++)
                            {
                                if (bytOut[i] == 0)
                                {
                                    break;
                                }
                            }

                            // convert into Base64 so that the result can be used in xml
                            // return System.Convert.ToBase64String(bytOut, 0, i);
                            returnValue = System.Convert.ToBase64String(bytOut, 0, (int)ms.Length);
                        }
                    }
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Decrypts the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>The decrypted value.</returns>
        public static string Decrypt(string source)
        {
            string returnstring;
            using (AesCryptoServiceProvider cryptoService = new AesCryptoServiceProvider())
            {
                // convert from Base64 to binary
                byte[] bytIn = System.Convert.FromBase64String(source);

                // create a MemoryStream with the input
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(bytIn, 0, bytIn.Length))
                {
                    byte[] bytKey = GetLegalKey(CryptoKey);

                    // set the private key
                    cryptoService.Key = bytKey;
                    cryptoService.IV = bytKey;

                    // create a Decryptor from the Provider Service instance
                    using (ICryptoTransform encrypto = cryptoService.CreateDecryptor())
                    {
                        // create Crypto Stream that transforms a stream using the decryption
                        using (CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read))
                        {
                            // read out the result from the Crypto Stream
                            using (System.IO.StreamReader sr = new System.IO.StreamReader(cs))
                            {
                                returnstring = sr.ReadToEnd();
                            }
                        }
                    }
                }
            }

            return returnstring;
        }

        /// <summary>
        /// Gets the legal key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The byte array of key.</returns>
        private static byte[] GetLegalKey(string key)
        {
            string tempKey;
            AesCryptoServiceProvider cryptoService = new AesCryptoServiceProvider();

            if (cryptoService.LegalKeySizes.Length > 0)
            {
                int lessSize = 0, moreSize = cryptoService.LegalKeySizes[0].MinSize;

                // key sizes are in bits
                while (key.Length * 8 > moreSize)
                {
                    lessSize = moreSize;
                    moreSize += cryptoService.LegalKeySizes[0].SkipSize;
                }

                tempKey = key.PadRight(moreSize / 8, ' ');
            }
            else
            {
                tempKey = key;
            }

            // convert the secret key to byte array
            return ASCIIEncoding.ASCII.GetBytes(tempKey);
        }
    }
}
