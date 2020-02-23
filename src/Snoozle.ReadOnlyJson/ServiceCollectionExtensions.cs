using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Snoozle.Abstractions;
using Snoozle.ReadOnlyJsonFile.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Snoozle.ReadOnlyJsonFile
{
    public static class ServiceCollectionExtensions
    {
        public static IMvcBuilder AddSnoozleReadOnlyJson(this IMvcBuilder @this)
        {
            IServiceCollection serviceCollection = @this.Services;
            IReadOnlyJsonRuntimeConfigurationProvider runtimeConfigurationProvider = BuildRuntimeConfigurationProvider();

            serviceCollection.AddScoped<IDataProvider, ReadOnlyJsonDataProvider>();
            serviceCollection.AddSingleton(runtimeConfigurationProvider);

            @this.AddSnoozleCore(runtimeConfigurationProvider);

            return @this;
        }

        private static IReadOnlyJsonRuntimeConfigurationProvider BuildRuntimeConfigurationProvider()
        {
            IEnumerable<IReadOnlyJsonResourceConfiguration> resourceConfigurations =
                ResourceConfigurationBuilder.Build<IReadOnlyJsonPropertyConfiguration, IReadOnlyJsonResourceConfiguration, IReadOnlyJsonModelConfiguration>(typeof(JsonResourceConfigurationBuilder<>));

            var runtimeConfigurations = new Dictionary<Type, IReadOnlyJsonRuntimeConfiguration<IRestResource>>();

            foreach (IReadOnlyJsonResourceConfiguration configuration in resourceConfigurations)
            {
                string json = File.ReadAllText(configuration.ModelConfiguration.JsonFilePath);
                Type genericListType = typeof(List<>).MakeGenericType(configuration.ResourceType);
                MethodInfo genericMethod = typeof(JsonConvert)
                    .GetMethod(nameof(JsonConvert.DeserializeObject), 1, new Type[] { typeof(string) })
                    .MakeGenericMethod(genericListType);

                // Create the generic list of resource objects
                object resourceList = genericMethod.Invoke(null, new object[] { json });

                var runtimeConfiguration = Activator.CreateInstance(
                    typeof(ReadOnlyJsonRuntimeConfiguration<>).MakeGenericType(configuration.ResourceType),
                    configuration,
                    resourceList) as IReadOnlyJsonRuntimeConfiguration<IRestResource>;

                runtimeConfigurations.Add(configuration.ResourceType, runtimeConfiguration);
            }

            return new ReadOnlyJsonRuntimeConfigurationProvider(runtimeConfigurations);
        }
    }
}