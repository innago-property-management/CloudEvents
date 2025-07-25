### [Innago\.Platform\.Messaging\.Publisher\.Amqp](../index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp').[DependencyInjection](index.md 'Innago\.Platform\.Messaging\.Publisher\.Amqp\.DependencyInjection')

## DependencyInjection\.AddAmqpCloudEventsPublisher\(this IServiceCollection, IConfiguration, string\) Method

Adds configuration and setup for an AMQP\-based CloudEvents publisher to the dependency injection container\.

```csharp
public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddAmqpCloudEventsPublisher(this Microsoft.Extensions.DependencyInjection.IServiceCollection services, Microsoft.Extensions.Configuration.IConfiguration configuration, string? sectionName="publisherAmqp");
```
#### Parameters

<a name='Innago.Platform.Messaging.Publisher.Amqp.DependencyInjection.AddAmqpCloudEventsPublisher(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection,Microsoft.Extensions.Configuration.IConfiguration,string).services'></a>

`services` [Microsoft\.Extensions\.DependencyInjection\.IServiceCollection](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection 'Microsoft\.Extensions\.DependencyInjection\.IServiceCollection')

The [Microsoft\.Extensions\.DependencyInjection\.IServiceCollection](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection 'Microsoft\.Extensions\.DependencyInjection\.IServiceCollection') to add the publisher to\.

<a name='Innago.Platform.Messaging.Publisher.Amqp.DependencyInjection.AddAmqpCloudEventsPublisher(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection,Microsoft.Extensions.Configuration.IConfiguration,string).configuration'></a>

`configuration` [Microsoft\.Extensions\.Configuration\.IConfiguration](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.configuration.iconfiguration 'Microsoft\.Extensions\.Configuration\.IConfiguration')

The application configuration used to retrieve AMQP configuration settings\.

<a name='Innago.Platform.Messaging.Publisher.Amqp.DependencyInjection.AddAmqpCloudEventsPublisher(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection,Microsoft.Extensions.Configuration.IConfiguration,string).sectionName'></a>

`sectionName` [System\.String](https://learn.microsoft.com/en-us/dotnet/api/system.string 'System\.String')

Optional\. The name of the configuration section containing AMQP settings\. Defaults to "publisherAmqp"\.

#### Returns
[Microsoft\.Extensions\.DependencyInjection\.IServiceCollection](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection 'Microsoft\.Extensions\.DependencyInjection\.IServiceCollection')  
The updated [Microsoft\.Extensions\.DependencyInjection\.IServiceCollection](https://learn.microsoft.com/en-us/dotnet/api/microsoft.extensions.dependencyinjection.iservicecollection 'Microsoft\.Extensions\.DependencyInjection\.IServiceCollection') to allow method chaining\.

#### Exceptions

[System\.InvalidOperationException](https://learn.microsoft.com/en-us/dotnet/api/system.invalidoperationexception 'System\.InvalidOperationException')  
Thrown if the specified configuration section does not exist or is missing required settings\.