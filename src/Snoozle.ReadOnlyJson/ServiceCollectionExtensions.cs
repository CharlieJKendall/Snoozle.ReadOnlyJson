using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Snoozle.Abstractions;
using Snoozle.ReadOnlyJsonFile.Configuration;
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
            return AddSnoozleReadOnlyJson(@this, options => { });
        }

        public static IMvcBuilder AddSnoozleReadOnlyJson(IMvcBuilder @this, Action<SnoozleReadOnlyJsonOptions> options)
        {
            IReadOnlyJsonRuntimeConfigurationProvider runtimeConfigurationProvider = BuildRuntimeConfigurationProvider();
            @this.Services.AddSingleton(runtimeConfigurationProvider);

            @this.AddSnoozleReadOnlyJsonCore();

            return @this.AddSnoozleCore(runtimeConfigurationProvider, options);
        }

        public static IMvcBuilder AddSnoozleReadOnlyJson(IMvcBuilder @this, IConfigurationSection configurationSection)
        {
            IReadOnlyJsonRuntimeConfigurationProvider runtimeConfigurationProvider = BuildRuntimeConfigurationProvider();
            @this.Services.AddSingleton(runtimeConfigurationProvider);

            @this.AddSnoozleReadOnlyJsonCore();

            return @this.AddSnoozleCore(runtimeConfigurationProvider, configurationSection);
        }

        private static IMvcBuilder AddSnoozleReadOnlyJsonCore(this IMvcBuilder @this)
        {
            @this.Services.AddScoped<IDataProvider, ReadOnlyJsonDataProvider>();

            return @this;
        }

        private static IReadOnlyJsonRuntimeConfigurationProvider BuildRuntimeConfigurationProvider()
        {
            IEnumerable<IReadOnlyJsonResourceConfiguration> resourceConfigurations =
                ResourceConfigurationBuilder.Build<IReadOnlyJsonPropertyConfiguration, IReadOnlyJsonResourceConfiguration, IReadOnlyJsonModelConfiguration>(typeof(JsonResourceConfigurationBuilder<>));

            var runtimeConfigurations = new Dictionary<Type, IReadOnlyJsonRuntimeConfiguration<IRestResource>>();

            foreach (IReadOnlyJsonResourceConfiguration configuration in resourceConfigurations)
            {
                try
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
                catch (Exception ex)
                {
                    throw new InvalidDataException(
                        $"An error occurred while reading the initial JSON data for {configuration.ResourceType.Name} ({configuration.ModelConfiguration.JsonFilePath}). " +
                        $"Ensure that it is well formed and the correct property types are used. See inner exception for details.",
                        ex);
                }
            }

            return new ReadOnlyJsonRuntimeConfigurationProvider(runtimeConfigurations);
        }
    }
}