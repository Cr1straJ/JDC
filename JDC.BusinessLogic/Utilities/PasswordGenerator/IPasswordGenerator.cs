namespace JDC.BusinessLogic.Utilities.PasswordGenerator
{
    public interface IPasswordGenerator
    {
        string GeneratePassword(int length = 8);
    }
}
