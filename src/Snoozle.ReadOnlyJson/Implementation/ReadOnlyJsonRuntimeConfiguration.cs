using Snoozle.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Snoozle.ReadOnlyJsonFile.Implementation
{
    internal class ReadOnlyJsonRuntimeConfiguration<TResource> : BaseRuntimeConfiguration<IReadOnlyJsonPropertyConfiguration, IReadOnlyJsonModelConfiguration, TResource>, IReadOnlyJsonRuntimeConfiguration<TResource>
        where TResource : class, IRestResource
    {
        private readonly Dictionary<string, TResource> _data;

        // This is created by reflection, so be careful when changing/adding parameters
        public ReadOnlyJsonRuntimeConfiguration(IReadOnlyJsonResourceConfiguration resourceConfiguration, List<TResource> data)
            : base(resourceConfiguration)
        {
            _data = data.ToDictionary(x => GetPrimaryKeyValue(x).ToString());
        }

        public string CustomRuntimeConfigurationValue { get; }

        public IEnumerable<TResource> GetAllEntries()
        {
            return _data.Values.AsEnumerable();
        }

        public TResource GetEntryByPrimaryKey(string primaryKey)
        {
            return _data.GetValueOrDefault(primaryKey);
        }
    }
}
