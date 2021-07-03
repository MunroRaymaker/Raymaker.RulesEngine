namespace Raymaker.RulesEngine.Application.Model
{
    public class Video : Product
    {
    }
    public class Book : Product
    {
    }
    public class Membership : Product
    {
        public bool IsActive { get; set; }
        public MembershipType MembershipType { get; set; }
        public string MemberEmail { get; set; }
        public string MemberName { get; set; }
    }
    public class MembershipUpgrade : Membership
    {

    }
    public enum MembershipType
    {
        NotMember,
        Basic,
        VIP
    }
    public class Product
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
