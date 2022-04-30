using System.Security.Cryptography;
using System.Text;

namespace Fritz.HomeAutomation.Utils
{
    /// <summary>
    /// HashUtils
    /// </summary>
    public static class HashUtils
    {
        /// <summary>
        /// Create an MD5 hash for our authentication token.
        /// </summary>
        /// <param name="input"></param>
        /// <returns>MD5 hash</returns>
        public static string GetMD5Hash(string input)
        {
            var md5Hasher = MD5.Create();
            var hash = md5Hasher.ComputeHash(Encoding.Unicode.GetBytes(input));
            var sb = new StringBuilder();
            foreach (var b in hash)
            {
                sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }
    }
}