namespace Raymaker.RulesEngine.Application
{
    public class MembershipProduct : Product
    {
        public bool IsActive { get; set; }
        public string MembershipType { get; set; } = "Basic";
        public string MemberEmail { get; set; }
        public int EmailsSent { get; set; }
    }
}
