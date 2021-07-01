namespace Raymaker.RulesEngine.Application
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
        public string MemberEmail { get; set; } // TODO: Not part of membership - should be part of a user class.
        public int EmailsSent { get; set; } // TODO: Not part of membership - should be part of a user class.
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
