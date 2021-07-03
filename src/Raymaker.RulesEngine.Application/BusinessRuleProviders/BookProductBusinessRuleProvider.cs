using Raymaker.RulesEngine.Application.Model;

namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class BookProductBusinessRuleProvider : IBusinessRuleProvider
    {
        private readonly IUserService userService;

        public BookProductBusinessRuleProvider(IUserService userService)
        {
            this.userService = userService;
        }

        public string NameRequirement => "BookProduct";

        // If the payment is for a book, create a duplicate packing slip for the royalty department.
        public (bool isSatisfied, string message) Process(Order order)
        {
            if (order.Product.GetType() == typeof(Book))
            {
                if (!string.IsNullOrEmpty(order.PackingSlip) && 
                    order.PackingSlip.Contains("royalty"))
                {
                    return (true, $"{NameRequirement}: Royalty slip already added");
                }
                order.PackingSlip += "royalty";
                return (true, $"{NameRequirement}: Added royalty slip");
            }

            return (false, $"{NameRequirement}: No action");
        }
    }
}
