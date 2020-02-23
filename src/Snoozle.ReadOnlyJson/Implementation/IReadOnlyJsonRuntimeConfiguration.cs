using Snoozle.Abstractions;
using System.Collections.Generic;

namespace Snoozle.ReadOnlyJsonFile.Implementation
{
    public interface IReadOnlyJsonRuntimeConfiguration<out TResource> : IRuntimeConfiguration
        where TResource : class, IRestResource
    {
        string CustomRuntimeConfigurationValue { get; }

        TResource GetEntryByPrimaryKey(string primaryKey);

        IEnumerable<TResource> GetAllEntries();
    }
}