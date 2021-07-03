namespace Raymaker.RulesEngine.Application
{
    public class UserValidator
    {
        public bool HasValidEmail(string email)
        {
            if (email.Contains("@") && email.Contains("."))
            {
                return true;
            }

            return false;
        }
    }
}
