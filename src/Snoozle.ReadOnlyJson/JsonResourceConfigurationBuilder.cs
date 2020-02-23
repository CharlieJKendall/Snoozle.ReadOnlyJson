using Snoozle.Abstractions;
using Snoozle.ReadOnlyJsonFile.Implementation;

namespace Snoozle.ReadOnlyJsonFile
{
    public abstract class JsonResourceConfigurationBuilder<TResource> : BaseResourceConfigurationBuilder<TResource, IReadOnlyJsonPropertyConfiguration, IReadOnlyJsonResourceConfiguration, IReadOnlyJsonModelConfiguration>
        where TResource : class, IRestResource
    {
        protected override IPropertyConfigurationBuilder<TProperty, IReadOnlyJsonPropertyConfiguration> CreatePropertyConfigurationBuilder<TProperty>(
            IReadOnlyJsonPropertyConfiguration propertyConfiguration)
        {
            return new ReadOnlyJsonPropertyConfigurationBuilder<TResource, TProperty>(propertyConfiguration);
        }

        protected override IReadOnlyJsonResourceConfiguration CreateResourceConfiguration()
        {
            return new ReadOnlyJsonResourceConfiguration<TResource>(ModelConfiguration, PropertyConfigurations);
        }

        protected override IReadOnlyJsonModelConfiguration CreateModelConfiguration()
        {
            return new ReadOnlyJsonModelConfiguration<TResource>();
        }

        protected override IReadOnlyJsonPropertyConfiguration CreatePropertyConfiguration()
        {
            return new ReadOnlyJsonPropertyConfiguration();
        }

        protected override IModelConfigurationBuilder<IReadOnlyJsonModelConfiguration> CreateModelConfigurationBuilder()
        {
            return new ReadOnlyJsonModelConfigurationBuilder(ModelConfiguration);
        }

        protected override void SetPropertyConfigurationDefaults()
        {
            base.SetPropertyConfigurationDefaults();
        }
    }
}
