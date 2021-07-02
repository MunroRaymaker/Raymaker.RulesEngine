namespace Raymaker.RulesEngine.Application
{
    public interface IUserRepository
    {
        User GetUser(string userName);
        bool AddUser(User user);
        bool UpdateUser(User user);
        bool UpdateAgent(Agent agent);
    }

    public class UserRepository : IUserRepository
    {
        public bool AddUser(User user)
        {
            return true;
        }

        public User GetUser(string userName)
        {
            return new User
            {
                Email = "test@test.com",
                MembershipType = MembershipType.Basic,
                UserName = userName
            };
        }

        public bool UpdateAgent(Agent agent)
        {
            return true;
        }

        public bool UpdateUser(User user)
        {
            return true;
        }
    }
}
