namespace Meeting.Hub.Integration.SpecFlow.Tests.Hooks;

public record Result(string Title, int Status, string Instance, List<string> Errors);

public class CustomWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    public ApplicationDbContext ApplicationDbContext =>
        this.Services
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<ApplicationDbContext>();
    
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            // var currentDirectory = Directory.GetCurrentDirectory();
            // configBuilder.SetBasePath(currentDirectory)
            //     .AddJsonFile("appsettings.test.json");
            //             
            // var configurationRoot = configBuilder.Build();
            
             configBuilder.AddInMemoryCollection(new Dictionary<string, string>
             {
                 { "ConnectionStrings:DefaultConnection", Hook.ConnectionString },
                 { "Environment", "Test" }
             }!);
        });
    }

    public async Task InitializeAsync() { }

    public new async Task DisposeAsync() { }
    
    public async Task<T> SendAsync<T>(object obj, string uri)
    {
        var client = Hook.Client;
        client.BaseAddress = new Uri("http://localhost:5246/");
        var json = JsonSerializer.Serialize(obj);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var request = new HttpRequestMessage(HttpMethod.Post, uri);
        request.Content = content;
        var response = await client.SendAsync(request);
        var result = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return JsonSerializer.Deserialize<T>(result, options);
    }
}


