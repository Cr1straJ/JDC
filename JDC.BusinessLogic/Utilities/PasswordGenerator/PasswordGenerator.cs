using System;
using System.Linq;
using System.Text;

namespace JDC.BusinessLogic.Utilities.PasswordGenerator
{
    /// <inheritdoc/>
    public class PasswordGenerator : IPasswordGenerator
    {
        /// <inheritdoc/>
        public string GeneratePassword(int length)
        {
            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char character = (char)random.Next(33, 126);
                if (!char.IsLetterOrDigit(character))
                {
                    continue;
                }

                password.Append(character);
                if (password.Length == length && IsNotValidPassword(password.ToString()))
                {
                    password.Clear();
                }
            }

            return password.ToString();
        }

        private static bool IsNotValidPassword(string password)
        {
            return password.All(i => !char.IsUpper(i))
                || password.All(i => !char.IsLower(i))
                || password.All(i => !char.IsDigit(i));
        }
    }
}
