using Raymaker.RulesEngine.Application.Model;

namespace Raymaker.RulesEngine.Application
{
    public interface IUserRepository
    {
        User GetUser(string userName);
        bool AddUser(User user);
        bool UpdateUser(User user);
        bool UpdateAgent(Agent agent);
    }
}
