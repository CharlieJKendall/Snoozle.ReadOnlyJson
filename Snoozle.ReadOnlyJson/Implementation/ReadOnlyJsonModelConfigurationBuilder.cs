using Snoozle.Abstractions;

namespace Snoozle.ReadOnlyJsonFile.Implementation
{
    public class ReadOnlyJsonModelConfigurationBuilder : BaseModelConfigurationBuilder<IReadOnlyJsonModelConfiguration>, IModelConfigurationBuilder<IReadOnlyJsonModelConfiguration>
    {
        public ReadOnlyJsonModelConfigurationBuilder(IReadOnlyJsonModelConfiguration modelConfiguration)
            : base(modelConfiguration)
        {
        }
    }
}