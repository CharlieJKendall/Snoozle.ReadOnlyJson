using Snoozle.Abstractions;
using Snoozle.Exceptions;
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
            ExceptionHelper.Argument.ThrowIfTrue(
                data == null,
                $"Null data was read from the JSON file for resource: {typeof(TResource).Name}",
                nameof(data));

            _data = data.ToDictionary(x => GetPrimaryKeyValue(x).ToString());
        }

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
