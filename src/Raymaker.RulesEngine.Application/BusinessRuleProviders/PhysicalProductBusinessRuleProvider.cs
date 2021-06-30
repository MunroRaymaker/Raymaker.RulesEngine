namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class PhysicalProductBusinessRuleProvider : IBusinessRuleProvider
    {
        public string NameRequirement { get; } = "PhysicalProduct";

        public bool Process(ref Order order)
        {
            if(order.Product.GetType() == typeof(Book) || 
               order.Product.GetType() == typeof(Video))
            {
                order.PackingSlip += "shipping";
                return true;
            }

            return false;
        }
    }
}
