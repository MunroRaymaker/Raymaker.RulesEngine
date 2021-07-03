using Raymaker.RulesEngine.Application.Model;

namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class ShippingBusinessRuleProvider : IBusinessRuleProvider
    {
        private readonly IUserService userService;

        public ShippingBusinessRuleProvider(IUserService userService)
        {
            this.userService = userService;
        }

        public string NameRequirement { get; } = "PhysicalProduct";

        // If the payment is for a physical product, generate a packing slip for shipping.
        public (bool isSatisfied, string message) Process(Order order)
        {
            if (order.Product.GetType() == typeof(Book) ||
               order.Product.GetType() == typeof(Video))
            {               
                if (!string.IsNullOrEmpty(order.PackingSlip) && 
                    order.PackingSlip.Contains("shipping"))
                {
                    return (true, $"{NameRequirement}: Shipping already added");
                }
                order.PackingSlip += "shipping";
                return (true, $"{NameRequirement}: Added shipping");
            }

            return (false, $"{NameRequirement}: No action");
        }
    }
}
