namespace Raymaker.RulesEngine.Application
{
    public class MembershipProduct : Product
    {
        public bool IsActive { get; set; }
        public string MenbershipType { get; set; } = "Basic";
        public string MemberEmail { get; set; }
    }
}
