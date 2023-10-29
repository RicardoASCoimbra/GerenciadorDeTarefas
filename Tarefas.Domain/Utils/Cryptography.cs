using System.Security.Cryptography;
using System.Text;

namespace Tarefas.Domain.Utils
{
    public static class Cryptography
    {
        public static string GetSalt()
        {
            var Number = new byte[32];
            var Generator = RandomNumberGenerator.Create();
            Generator.GetBytes(Number);
            return Convert.ToBase64String(Number);
        }

        public static string GetHash(string Salt, string Password)
        {
            var SHA = SHA256.Create();
            var PasswordBytes = Encoding.UTF8.GetBytes(Salt + Password);
            var Hash = SHA.ComputeHash(PasswordBytes);
            return Convert.ToBase64String(Hash);
        }
    }
}
