namespace Raymaker.RulesEngine.Application.Model
{
    public class User
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public MembershipType MembershipType { get; set; }
        public bool IsActive { get; set; }
        public int EmailsSent { get; set; }
    }
}
