using System.Security.Cryptography;
using System.Text;

namespace System
{
    public static class Randomize
    {
        public static int Next()
        {
            var random = 0;
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomNumber = new byte[4];//4 for int32
                rng.GetBytes(randomNumber);
               random = BitConverter.ToInt32(randomNumber, 0);
            }

            return random;
        }

        public static string NewPassword(int length = 6)
        {
            try
            {
                var senha = BitConverter.ToString(new SHA512CryptoServiceProvider()
                        .ComputeHash(Encoding.Default.GetBytes(Next().ToString())))
                    .Replace("-", string.Empty);
                return senha.Substring(0, length);
            }
            catch
            {
                return null;
            }
        }
    }
}