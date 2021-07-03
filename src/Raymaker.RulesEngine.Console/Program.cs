using Raymaker.RulesEngine.Application;
using Raymaker.RulesEngine.Application.BusinessRuleProviders;

namespace Raymaker.RulesEngine.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Business Rules Engine Sample App");
            var service = new OrderProcessingService(new UserService(), new EmailService());
            System.Console.WriteLine("\r\nBuy membership");
            System.Console.WriteLine("==============");
            
            // Build an order for a membership
            var order = new Order
            {
                PackingSlip = "",
                Product = new Membership { IsActive = true, MemberEmail = "test@test.com", Name = "BasicMembership", UnitPrice = 100, MemberName = "foo", MembershipType = MembershipType.Basic }
            };

            // Execute rules
            service.Process(order);

            // Upgrade membership
            System.Console.WriteLine("\r\nUpgrade membership");
            System.Console.WriteLine("==============");
            var order2 = new Order
            {
                PackingSlip = "",
                Product = new MembershipUpgrade { IsActive = true, MemberEmail = "test@test.com", Name = "BasicMembership", UnitPrice = 200, MemberName = "foo", MembershipType = MembershipType.VIP }
            };

            // Execute rules
            service.Process(order2);

            System.Console.WriteLine("\r\nRules processed.All done.");
        }
    }
}
