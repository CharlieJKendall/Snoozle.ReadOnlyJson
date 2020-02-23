namespace Snoozle.ReadOnlyJsonFile.TestHarness.RestResources
{
    public class Cat : IRestResource
    {
        public int Id { get; set; }

        public int? HairLength { get; set; }

        public string Name { get; set; }
    }

    public class CatResourceConfiguration : CustomResourceConfigurationBuilder<Cat>
    {
        public override void Configure()
        {
            ConfigurationForModel().HasJsonFilePath("C:/temp/cats.json");
        }
    }
}
