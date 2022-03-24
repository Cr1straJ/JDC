namespace JDC.BusinessLogic.Utilities.PasswordGenerator
{
    /// <summary>
    /// Password generator.
    /// </summary>
    public interface IPasswordGenerator
    {
        /// <summary>
        /// Generates a random password respecting the given strength requirements.
        /// </summary>
        /// <param name="length">Password length.</param>
        /// <returns>A random password.</returns>
        string GeneratePassword(int length = 8);
    }
}
