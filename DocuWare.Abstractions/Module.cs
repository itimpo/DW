using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DocuWare.Abstractions;

public abstract class Module : IModule
{
    public virtual int OrderIndex => 0;

    public virtual bool Enabled { get; protected set; } = true;


    public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
    }
}
