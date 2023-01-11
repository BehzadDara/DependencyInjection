using Microsoft.Extensions.DependencyInjection;

#region singleton dependency injection
// MyProperty is the same for all instances
var singletonServiceProvider = new ServiceCollection()
            .AddSingleton<IMyClass, MyClass>()
            .BuildServiceProvider();

var singletonInstance1 = singletonServiceProvider.GetService<IMyClass>();
var singletonInstance2 = singletonServiceProvider.GetService<IMyClass>();

Console.WriteLine($"singleton: instance1 is [{singletonInstance1?.GetMyProperty()}] and instance2 is [{singletonInstance2?.GetMyProperty()}]");
#endregion

#region scoped dependency injection
// MyProperty is the same for all instances in one request and different from another request
var scopedServiceProvider = new ServiceCollection()
            .AddScoped<IMyClass, MyClass>()
            .BuildServiceProvider();

var scopedInstance1 = scopedServiceProvider.GetService<IMyClass>();
var scopedInstance2 = scopedServiceProvider.GetService<IMyClass>();

Console.WriteLine($"scoped: instance1 is [{scopedInstance1?.GetMyProperty()}] and instance2 is [{scopedInstance2?.GetMyProperty()}]");
#endregion

#region transient dependency injection
// MyProperty is different for each instance
var transientServiceProvider = new ServiceCollection()
            .AddTransient<IMyClass, MyClass>()
            .BuildServiceProvider();

var transientInstance1 = transientServiceProvider.GetService<IMyClass>();
var transientInstance2 = transientServiceProvider.GetService<IMyClass>();

Console.WriteLine($"transient: instance1 is [{transientInstance1?.GetMyProperty()}] and instance2 is [{transientInstance2?.GetMyProperty()}]");
#endregion


class MyClass : IMyClass
{
    public MyClass()
    {
        var random = new Random();
        _MyProperty = random.Next(1000000, 10000000);
    }

    private int _MyProperty;

    public int GetMyProperty()
    {
        return _MyProperty;
    }
}

interface IMyClass
{
    public int GetMyProperty();
}