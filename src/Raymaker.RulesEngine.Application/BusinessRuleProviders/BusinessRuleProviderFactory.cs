using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace Raymaker.RulesEngine.Application.BusinessRuleProviders
{
    public class BusinessRuleProviderFactory
    {
        private readonly IReadOnlyDictionary<string, IBusinessRuleProvider> providers;

        public BusinessRuleProviderFactory(IUserService userService)
        {
            var businessRuleProviderType = typeof(IBusinessRuleProvider);
            this.providers = businessRuleProviderType.Assembly.ExportedTypes
                .Where(x => businessRuleProviderType.IsAssignableFrom(x)
                            && !x.IsInterface
                            && !x.IsAbstract)
                .Select(x =>
                {
                     var parameterlessCtor = x.GetConstructors().SingleOrDefault(c => c.GetParameters().Length == 0);
                    return parameterlessCtor is not null
                        ? Activator.CreateInstance(x)
                        : Activator.CreateInstance(x, userService); // Fill with parameters if constructor has arguments
                })
                .Cast<IBusinessRuleProvider>()
                .ToImmutableDictionary(k => k.NameRequirement, v => v);
        }

        public IBusinessRuleProvider GetProviderByClientName(string clientName)
        {
            var provider = this.providers.GetValueOrDefault(clientName);
            return provider ?? this.providers[string.Empty];
        }

        public IEnumerable<IBusinessRuleProvider> GetProviders()
        {
            return this.providers.Values;
        }
    }
}
