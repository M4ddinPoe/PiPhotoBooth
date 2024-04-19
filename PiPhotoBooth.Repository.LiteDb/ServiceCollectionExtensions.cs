namespace PiPhotoBooth;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions 
{
    public static void AddRepositoryServices(this IServiceCollection collection) 
    {
        collection.AddTransient<IRepository, Repository>();
    }
}
