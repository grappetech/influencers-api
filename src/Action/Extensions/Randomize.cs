using System.Security.Cryptography;

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
    }
}