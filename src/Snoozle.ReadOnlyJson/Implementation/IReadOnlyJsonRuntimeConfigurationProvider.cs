using Snoozle.Abstractions;

namespace Snoozle.ReadOnlyJsonFile.Implementation
{
    public interface IReadOnlyJsonRuntimeConfigurationProvider : IRuntimeConfigurationProvider<IReadOnlyJsonRuntimeConfiguration<IRestResource>>
    {
    }
}
