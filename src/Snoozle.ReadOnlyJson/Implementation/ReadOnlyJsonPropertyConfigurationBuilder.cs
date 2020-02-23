using Snoozle;
using Snoozle.Abstractions;

namespace Snoozle.ReadOnlyJsonFile.Implementation
{
    public class ReadOnlyJsonPropertyConfigurationBuilder<TResource, TProperty> : BasePropertyConfigurationBuilder<TResource, TProperty, IReadOnlyJsonPropertyConfiguration>, IPropertyConfigurationBuilder<TProperty, IReadOnlyJsonPropertyConfiguration>
        where TResource : class, IRestResource
    {
        public ReadOnlyJsonPropertyConfigurationBuilder(IReadOnlyJsonPropertyConfiguration propertyConfiguration)
            : base(propertyConfiguration)
        {
        }
    }
}
