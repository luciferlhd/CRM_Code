using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CRM_Code;

public class CRM_CodeWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<CRM_CodeWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
