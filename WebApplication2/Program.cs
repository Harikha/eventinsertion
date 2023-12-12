using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



class Program
{
    static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services =>
                {
                    services.AddSingleton<HomeController>(); // Register HomeController as a singleton
                })
                .Configure(app =>
                {
                    app.Run(async context =>
                    {
                        // Resolve HomeController from the DI container
                        var homeController = context.RequestServices.GetRequiredService<HomeController>();

                        // Call the Index method directly
                        Console.WriteLine("calling controller");
                        homeController.Index();

                    });
                });
            });
}
