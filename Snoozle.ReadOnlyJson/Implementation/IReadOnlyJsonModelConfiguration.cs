using Snoozle.Abstractions;

namespace Snoozle.ReadOnlyJsonFile.Implementation
{
    public interface IReadOnlyJsonModelConfiguration : IModelConfiguration
    {
        string JsonFilePath { get; set; }
    }
}