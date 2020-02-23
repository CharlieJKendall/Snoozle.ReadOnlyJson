using Snoozle.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Snoozle.ReadOnlyJsonFile.Implementation
{
    internal class ReadOnlyJsonDataProvider : IDataProvider
    {
        private readonly IReadOnlyJsonRuntimeConfigurationProvider _customRuntimeConfigurationProvider;

        public ReadOnlyJsonDataProvider(IReadOnlyJsonRuntimeConfigurationProvider customRuntimeConfigurationProvider)
        {
            _customRuntimeConfigurationProvider = customRuntimeConfigurationProvider;
        }

        public Task<bool> DeleteByIdAsync<TResource>(object primaryKey)
            where TResource : class, IRestResource
        {
            throw new NotSupportedException();
        }

        public Task<TResource> InsertAsync<TResource>(TResource resourceToCreate)
            where TResource : class, IRestResource
        {
            throw new NotSupportedException();
        }

        public Task<IEnumerable<TResource>> SelectAllAsync<TResource>()
            where TResource : class, IRestResource
        {
            IReadOnlyJsonRuntimeConfiguration<TResource> config = GetConfig<TResource>();

            return Task.FromResult(config.GetAllEntries());
        }

        public Task<TResource> SelectByIdAsync<TResource>(object primaryKey)
            where TResource : class, IRestResource
        {
            IReadOnlyJsonRuntimeConfiguration<TResource> config = GetConfig<TResource>();

            return Task.FromResult(config.GetEntryByPrimaryKey(primaryKey.ToString()));
        }

        public Task<TResource> UpdateAsync<TResource>(TResource resourceToUpdate, object primaryKey)
            where TResource : class, IRestResource
        {
            throw new NotSupportedException();
        }

        private IReadOnlyJsonRuntimeConfiguration<TResource> GetConfig<TResource>()
            where TResource : class, IRestResource
        {
            return (IReadOnlyJsonRuntimeConfiguration<TResource>)_customRuntimeConfigurationProvider.GetRuntimeConfigurationForType(typeof(TResource));
        }
    }
}
