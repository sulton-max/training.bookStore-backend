using BookStore.Core.Extensions;
using BookStore.DAL.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Infrastructure.DAL.Repositories;
using BookStore.BLL.EntityServices.Interfaces;
using BookStore.Core.Models.Entities;
using BookStore.BLL.EntityServices;
using BookStore.DAL.Repositories.Interfaces;

namespace BookStore.API.Extensions;

/// <summary>
/// Provides extension methods for Web App Builder
/// </summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    /// Registers Database contexts to Service Collection
    /// </summary>
    /// <param name="builder">Web App Builder</param>
    /// <returns>Web App Builder for method chaining</returns>
    public static WebApplicationBuilder AddDbContexts(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
        return builder;
    }

    /// <summary>
    /// Registers Entity Repositories to Service Collection
    /// </summary>
    /// <param name="builder">Web App Builder</param>
    /// <returns>Web App Builder for method chaining</returns>
    public static WebApplicationBuilder AddEntityRepositories(this WebApplicationBuilder builder)
    {
        var contextType = typeof(ApplicationDbContext);
        var dbSetTypes = contextType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(x => x.PropertyType.InheritsOrImplements(typeof(DbSet<>)))
            .Select(x => x.PropertyType.GenericTypeArguments.First())
            .ToList();

        var repoType = typeof(IRepositoryBase<>);
        var targetType = typeof(RepositoryBase<>);

        foreach (var dbSetType in dbSetTypes)
        {
            var specificInterface = repoType.MakeGenericType(dbSetType);
            var specificImplementation = targetType.MakeGenericType(dbSetType);
            builder.Services.AddScoped(repoType, targetType);
        }

        return builder;
    }

    /// <summary>
    /// Registers Entity Services to Service Collections
    /// </summary>
    /// <param name="builder"></param>
    /// <returns>Web App Builder for method chaining</returns>
    public static WebApplicationBuilder AddEntityServices(this WebApplicationBuilder builder)
    {
        // Add Base Entity services
        builder.Services
            .AddScoped<IEntityServiceBase<Book>, EntityServiceBase<Book, IRepositoryBase<Book>>>();

        //Add specific Entity services
        builder.Services
            .AddScoped<IBookService, BookService>();

        return builder;
    }

    /// <summary>
    /// Registers Routing with custom options
    /// </summary>
    /// <param name="builder">Web App Builder</param>
    /// <returns>Web App Builder for method chaining</returns>
    public static WebApplicationBuilder AddCustomRouting(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
        });

        return builder;
    }

    /// <summary>
    /// Registers Controllers with custom options
    /// </summary>
    /// <param name="builder">Web App Builder</param>
    /// <returns>Web App Builder for method chaining</returns>
    public static WebApplicationBuilder AddCustomControllers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        return builder;
    }

    /// <summary>
    /// Registers Open API tools to service collections
    /// </summary>
    /// <param name="builder">Web App Builder</param>
    /// <returns>Web App Builder for method chaining</returns>
    public static WebApplicationBuilder AddOpenApiTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        return builder;
    }
}