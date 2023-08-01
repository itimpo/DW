using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocuWare.Abstractions;

public interface IModule
{
    int OrderIndex { get; }

    bool Enabled { get; }

    void ConfigureServices(IServiceCollection services, IConfiguration configuration);
}
