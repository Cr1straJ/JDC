using System;
using System.Linq;
using System.Text;

namespace JDC.BusinessLogic.Utilities.PasswordGenerator
{
    public class PasswordGenerator : IPasswordGenerator
    {
        /// <summary>
        /// Generates a random password respecting the given strength requirements.
        /// </summary>
        /// <param name="length">Password length.</param>
        /// <returns>A random password.</returns>
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
                if (!password.ToString().Any(i => char.IsLetter(i)) || (!password.ToString().Any(i => char.IsDigit(i)) && password.Length == 8))
                {
                    password.Clear();
                }
            }

            return password.ToString();
        }
    }
}
