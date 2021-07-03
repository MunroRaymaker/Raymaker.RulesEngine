using Raymaker.RulesEngine.Application.Model;

namespace Raymaker.RulesEngine.Application
{
    public interface IUserService
    {
        User GetUser(string userName);
        bool UpdateUser(User user);
        bool UpdateAgent(Agent agent);
    }
}