using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

internal class Program
{
    static async Task<int> Main(string[] args)
    {
        using IHost host = Host.CreateDefaultBuilder(args).UseConsoleLifetime().Build();
        var serviceProvider = new ServiceCollection().AddHttpClient().BuildServiceProvider();
        IHttpClientFactory httpClientFactory = serviceProvider.GetRequiredService<IHttpClientFactory>();
        var client = httpClientFactory.CreateClient();
        Console.WriteLine($"Application PID: {Process.GetCurrentProcess().Id}");

        IConfiguration config = host.Services.GetRequiredService<IConfiguration>();
        for (int i = 0; i < 5; i++)
            await WriteTelemetry(config, client);

        // Print active connections
        PrintActiveConnections();

        return 0;
    }

    private static async Task WriteTelemetry(IConfiguration config, HttpClient client)
    {
        try
        {
            string url = "https://localhost:7153/Event";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url);
            string data = JsonSerializer.Serialize(new
            {
                name = "bts",
                Salary = 7821,
                Department = 1
            });
            request.Content = new StringContent(data, Encoding.UTF8, "application/json");

            var policy = Policy.Handle<HttpRequestException>().WaitAndRetryAsync(3, retryAttemt => TimeSpan.FromSeconds(Math.Pow(2, retryAttemt)));
            HttpResponseMessage response = await policy.ExecuteAsync(async () => await client.SendAsync(request));
            response.EnsureSuccessStatusCode();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private static void PrintActiveConnections()
    {
        IPGlobalProperties properties = IPGlobalProperties.GetIPGlobalProperties();
        TcpConnectionInformation[] connections = properties.GetActiveTcpConnections();

        Console.WriteLine("Active TCP Connections:");
        foreach (TcpConnectionInformation connection in connections)
        {
            Console.WriteLine($"Local Address: {connection.LocalEndPoint.Address}:{connection.LocalEndPoint.Port}");
            Console.WriteLine($"Foreign Address: {connection.RemoteEndPoint.Address}:{connection.RemoteEndPoint.Port}");
            Console.WriteLine($"State: {connection.State}");
            Console.WriteLine($"Protocol: TCP"); // HttpClient uses TCP
            Console.WriteLine();
        }
    }
}
