using System;
using System.Security.Cryptography;

namespace GoodsStore.Business.Services.Concrete
{
    public static class Encryption
    {
        private static int SaltSize = 16;

        private static int HashSize = 20;

        private static string CommonPhrase = "!WeAreTheLegionWeAreTheAlphaAndOmega!";

        /// <summary>
        /// Creates a hash from a password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="iterations">Number of iterations.</param>
        /// <returns>The hash.</returns>
        public static string GetHash(this string password, int iterations = 10000)
        {
            // Create salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

            // Create hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            // Combine salt and hash
            var hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            // Convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);

            // Format hash with extra information
            return $"{CommonPhrase}{base64Hash}${iterations}";
        }

        /// <summary>
        /// Checks if hash is supported.
        /// </summary>
        /// <param name="hashString">The hash.</param>
        /// <returns>Is supported?</returns>
        public static bool IsHashSupported(this string hashString)
        {
            return hashString.Contains(CommonPhrase);
        }

        /// <summary>
        /// Verifies a password against a hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hashedPassword">The hash.</param>
        /// <returns>Could be verified?</returns>
        public static bool Verify(this string password, string hashedPassword)
        {
            // Check hash
            if (!IsHashSupported(hashedPassword))
                throw new NotSupportedException("The hashtype is not supported");

            // Extract iteration and Base64 string
            var splittedHashString = hashedPassword.Replace(CommonPhrase, "").Split('$');
            var iterations = int.Parse(splittedHashString[1]);
            var base64Hash = splittedHashString[0];

            // Get hash bytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            // Get salt
            var salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Get result
            for (var i = 0; i < HashSize; i++)
                if (hashBytes[i + SaltSize] != hash[i])
                    return false;

            return true;
        }
    }
}
