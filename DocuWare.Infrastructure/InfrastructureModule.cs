using DocuWare.Abstractions;
using DocuWare.Abstractions.Event;
using DocuWare.Abstractions.User;
using DocuWare.Infrastructure.Events;
using DocuWare.Infrastructure.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocuWare.Infrastructure;

public class InfrastructureModule : Module
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DwDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DbConnection")));

        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}