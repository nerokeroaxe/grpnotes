using Contracts.Repositories;
using Contracts.Services;
using Core.Services;
using Infrastructure.Repositories;

namespace Web.Extensions;

public static class AddAppServicesExtension
{
    public static void AddAppServices(this IServiceCollection services)
    {
        services.AddSingleton<ICategoryRepository, CategoryRepository>();
        services.AddSingleton<ICategoryService, CategoryService>();

        services.AddSingleton<INoteRepository, NoteRepository>();
        services.AddSingleton<INoteService, NoteService>();
    }
}