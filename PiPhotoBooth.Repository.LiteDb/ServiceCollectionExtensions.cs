namespace PiPhotoBooth;

using Microsoft.Extensions.DependencyInjection;
using Repository;

public static class ServiceCollectionExtensions 
{
    public static void AddRepositoryServices(this IServiceCollection collection) 
    {
        collection.AddTransient<IRepository, Repository.Repository>();
    }
}
