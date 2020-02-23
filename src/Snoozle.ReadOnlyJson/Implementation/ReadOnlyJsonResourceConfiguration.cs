using Snoozle.Abstractions;
using System.Collections.Generic;

namespace Snoozle.ReadOnlyJsonFile.Implementation
{
    public class ReadOnlyJsonResourceConfiguration<TResource> : BaseResourceConfiguration<TResource, IReadOnlyJsonPropertyConfiguration, IReadOnlyJsonModelConfiguration>, IReadOnlyJsonResourceConfiguration
        where TResource : class, IRestResource
    {
        public ReadOnlyJsonResourceConfiguration(
            IReadOnlyJsonModelConfiguration modelConfiguration,
            IEnumerable<IReadOnlyJsonPropertyConfiguration> propertyConfigurations)
            : base(modelConfiguration, propertyConfigurations)
        {
        }
    }
}
