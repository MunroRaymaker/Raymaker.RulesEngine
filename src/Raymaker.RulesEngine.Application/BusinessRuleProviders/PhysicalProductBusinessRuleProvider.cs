namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class PhysicalProductBusinessRuleProvider : IBusinessRuleProvider
    {
        public string NameRequirement { get; } = "PhysicalProduct";

        // If the payment is for a physical product, generate a packing slip for shipping.
        public bool Process(Order order)
        {
            if (order.Product.GetType() == typeof(Book) ||
               order.Product.GetType() == typeof(Video))
            {
                if (!string.IsNullOrEmpty(order.PackingSlip) && 
                    order.PackingSlip.Contains("shipping"))
                {
                    return true;
                }
                order.PackingSlip += "shipping";
                return true;
            }

            return false;
        }
    }
}
