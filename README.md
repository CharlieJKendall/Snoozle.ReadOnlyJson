# Read-Only JSON Data Provider for Snoozle

[![Build Status](https://dev.azure.com/charliejkendall/Snoozle/_apis/build/status/CharlieJKendall.Snoozle.ReadOnlyJson?branchName=master)](https://dev.azure.com/charliejkendall/Snoozle/_build/latest?definitionId=3&branchName=master)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Snoozle.ReadOnlyJson)](https://www.nuget.org/packages/Snoozle.ReadOnlyJson)
[![Latest Release](https://img.shields.io/github/v/release/charliejkendall/snoozle.readonlyjson)](https://github.com/CharlieJKendall/Snoozle.ReadOnlyJson/releases/latest)

### Have a fully functioning and lightning fast REST API running in minutes by following three simple steps!

#### 1. Call the `.AddSnoozleReadOnlyJson()` extension method from the `IMvcBuilder` in Startup.cs

Configuration can by provided using the builder lambda (as below) or via an `IConfigurationSection` read from a proper configuration provider (e.g. appsettings.json).

``` cs
public void ConfigureServices(IServiceCollection services)
{
    services
        .AddControllers()
        .AddSnoozleSqlServer();
}
```

#### 2. Create a POCO model representing your JSON data schema

This *must* inherit from the `IRestResource` marker interface. Nullable columns that are modelled by value types (e.g `int`, `long`, `DateTime`) should be nullable on the model.

``` cs
public class Cat : IRestResource
{
    public int Id { get; set; }

    public long? HairLength { get; set; }

    public string Name { get; set; }
}
```

#### 3. Create a resource configuration class for your model

Inherit from the `JsonResourceConfigurationBuilder` class, passing your model as the generic type parameter and overriding the abstract `Configure()` method from the base class.

There are two key methods that the base class provides: `ConfigurationForModel()` and `ConfigurationForProperty()`. These set model- and property-level configurations for the resource. Any property named `Id` or `<resource_type_name>Id` (e.g. `CatId`) will be automatically set by convention as the primary key/identifier. If your primary key has another name, it can be set using the `IsPrimaryIdentifier()` method on the property builder.

The default route is the pluralised resource name, i.e. `api/cats`

``` cs
public class CatResourceConfiguration : JsonResourceConfigurationBuilder<Cat>
{
    public override void Configure()
    {
        ConfigurationForModel().HasJsonFilePath("C:/temp/cats.json");
    }
}

```

You should now be able to run your web application and access your resource at `<root_url>/api/<resource>`, for example `curl -k https://localhost:44343/api/cats`. Unless you have specified otherwise, you should be able to make GET requests to the resource. _Note:_ This is read-only, so only GET all and by ID requests are valid.
