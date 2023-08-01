using DocuWare.Abstractions;

namespace DocuWare.Web.Extensions;

public static class ModuleRegistration
{
    public static void AddModules(this IServiceCollection source, WebApplicationBuilder builder, params Type[] entryPointsAssembly)
    {
        for (int i = 0; i < entryPointsAssembly.Length; i++)
        {
            entryPointsAssembly[i].Assembly.ExportedTypes
                .Where((Type x) => !x.IsAbstract && typeof(IModule)!.IsAssignableFrom(x))
                .Select(new Func<Type, object>(Activator.CreateInstance)).Cast<IModule>()
                .OrderBy(q => q.OrderIndex)
                .ToList()
                .ForEach(q => q.ConfigureServices(source, builder.Configuration));
        }
    }
}
