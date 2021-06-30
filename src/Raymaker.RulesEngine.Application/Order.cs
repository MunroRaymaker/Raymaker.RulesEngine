namespace Raymaker.RulesEngine.Application
{
    public class Order
    {
        public string PackingSlip { get; set; }
        public Payment Payment { get; set; }
        public Product Product { get; set; }
    }
}
