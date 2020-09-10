using Amazon.Lambda.AspNetCoreServer;
using dotenv.net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Todo.API
{
    public class LambdaEntryPoint : APIGatewayProxyFunction
    {
        
        protected override void Init(IWebHostBuilder builder)
        {
            DotEnv.Config(false);
            builder
                .ConfigureAppConfiguration(config =>
                {
                    config.AddEnvironmentVariables();
                })
                .UseStartup<Startup>();
        }
    }
}