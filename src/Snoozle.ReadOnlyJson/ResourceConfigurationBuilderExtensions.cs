using Snoozle.Abstractions;
using Snoozle.Exceptions;
using Snoozle.ReadOnlyJsonFile.Implementation;

namespace Snoozle.ReadOnlyJsonFile
{
    public static class ModelConfigurationBuilderExtensions
    {
        public static IModelConfigurationBuilder<IReadOnlyJsonModelConfiguration> HasJsonFilePath(
            this IModelConfigurationBuilder<IReadOnlyJsonModelConfiguration> @this,
            string jsonFilePath)
        {
            ExceptionHelper.ArgumentNull.ThrowIfNecessary(jsonFilePath, nameof(jsonFilePath));

            @this.ModelConfiguration.JsonFilePath = jsonFilePath;

            return @this;
        }
    }

    public static class PropertyConfigurationBuilderExtensions
    {
    }
}
