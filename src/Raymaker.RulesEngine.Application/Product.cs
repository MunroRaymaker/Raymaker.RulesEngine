namespace Raymaker.RulesEngine.Application
{
    public class Video : Product
    {
    }
    public class Book : Product
    {
    }
    public class Product
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
