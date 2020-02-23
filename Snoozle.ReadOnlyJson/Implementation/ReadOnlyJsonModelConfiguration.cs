using Snoozle.Abstractions;

namespace Snoozle.ReadOnlyJsonFile.Implementation
{
    public class ReadOnlyJsonModelConfiguration<TResource> : BaseModelConfiguration<TResource>, IReadOnlyJsonModelConfiguration
        where TResource : class, IRestResource
    {
        public string JsonFilePath { get; set; }
    }
}
