using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitizenWeb.Controllers
{
    /// <summary>
    /// The  class.userd to encrypt string.
    /// </summary>
    public static class EncryptDecrypt
    {
        /// <summary>
        /// Encodes the text.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The EncodeText.</returns>
        public static string EncodeText(string input)
        {
            string keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
            string output = string.Empty;
            int chr1 = 0, chr2 = 0, chr3 = 0;
            int enc1, enc2, enc3, enc4 = 0;
            int i = 0;

            char[] inputstr = input.ToCharArray();
            do
            {
                if (i < input.Length)
                {
                    chr1 = inputstr[i++];  // (int)Char.GetNumericValue((char)inputstr[i++]);
                }

                if (i < input.Length)
                {
                    chr2 = inputstr[i++];
                }

                if (i < input.Length)
                {
                    chr3 = inputstr[i++];
                }

                enc1 = chr1 >> 2;
                enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
                enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
                enc4 = chr3 & 63;

                if (chr2 == 0)
                {
                    enc3 = enc4 = 64;
                }
                else if (chr3 == 0)
                {
                    enc4 = 64;
                }

                output = output +
                    keyStr.Substring(enc1, 1) +
                    keyStr.Substring(enc2, 1) +
                    keyStr.Substring(enc3, 1) +
                    keyStr.Substring(enc4, 1);
                chr1 = chr2 = chr3 = 0;
                enc1 = enc2 = enc3 = enc4 = 0;
            }
            while (i < input.Length);

            return output;
        }

        /// <summary>
        /// Decodes the text.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The DecodeText.</returns>
        public static string DecodeText(string input)
        {
            string keyStr = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
            var output = string.Empty;
            int chr1, chr2, chr3 = 0;
            int enc1 = 0, enc2 = 0, enc3 = 0, enc4 = 0;
            int i = 0;

            // remove all characters that are not A-Z, a-z, 0-9, +, /, or =
            // string base64test = @"/[^A-Za-z0-9\+\/\=]/g";
            // if (base64test.exec(input)) {
            //    window.alert("There were invalid base64 characters in the input text.\n" +
            //        "Valid base64 characters are A-Z, a-z, 0-9, '+', '/',and '='\n" +
            //        "Expect errors in decoding.");
            // }
            input = input.Replace(@"/[^A-Za-z0-9\+\/\=]/g", string.Empty);
            char[] ss = input.ToArray();

            do
            {
                if (i < input.Length)
                {
                    enc1 = keyStr.IndexOf(input.Substring(i++, 1));
                }

                if (i < input.Length)
                {
                    enc2 = keyStr.IndexOf(input.Substring(i++, 1));
                }

                if (i < input.Length)
                {
                    enc3 = keyStr.IndexOf(input.Substring(i++, 1));
                }

                if (i < input.Length)
                {
                    enc4 = keyStr.IndexOf(input.Substring(i++, 1));
                }

                chr1 = (enc1 << 2) | (enc2 >> 4);
                chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
                chr3 = ((enc3 & 3) << 6) | enc4;

                output = output + Convert.ToChar(chr1);

                if (enc3 != 64)
                {
                    output = output + Convert.ToChar(chr2);
                }

                if (enc4 != 64)
                {
                    output = output + Convert.ToChar(chr3);
                }

                chr1 = chr2 = chr3 = 0;
                enc1 = enc2 = enc3 = enc4 = 0;
            }
            while (i < input.Length);
            return output;
        }
    }
}
