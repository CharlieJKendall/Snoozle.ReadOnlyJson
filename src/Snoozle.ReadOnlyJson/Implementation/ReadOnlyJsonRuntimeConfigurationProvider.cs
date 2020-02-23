using Snoozle.Abstractions;
using System;
using System.Collections.Generic;

namespace Snoozle.ReadOnlyJsonFile.Implementation
{
    internal class ReadOnlyJsonRuntimeConfigurationProvider : BaseRuntimeConfigurationProvider<IReadOnlyJsonRuntimeConfiguration<IRestResource>>, IReadOnlyJsonRuntimeConfigurationProvider
    {
        public ReadOnlyJsonRuntimeConfigurationProvider(Dictionary<Type, IReadOnlyJsonRuntimeConfiguration<IRestResource>> runtimeConfigurations)
            : base(runtimeConfigurations)
        {
        }
    }
}
