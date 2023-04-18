using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.ConfigureLogging((hostingContext, logging) =>
    {
        logging.ClearProviders();
        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
    });

    // Add services to the container.
    builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
    builder.Services.AddControllers();
    builder.Services.AddOcelot();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("ocelot.json")
                            .Build();

    builder.Services.AddSwaggerForOcelot(configuration);

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Gateway", Version = "v1" });
    });

    var app = builder.Build();

    app.UseRouting();
    app.UseSwagger();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });

    app.UseStaticFiles();

    app.Map("/swagger/{documentName}/swagger.json", async context =>
    {
        var documentName = context.Request.RouteValues["documentName"].ToString();
        var httpClient = new HttpClient();
        var url = $"http://{documentName}:80/swagger/{documentName}/swagger.json";
        Console.WriteLine($"Fetching Swagger JSON from: {url}");
        var responseMessage = await httpClient.GetAsync(url);
        var content = await responseMessage.Content.ReadAsStringAsync();
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsync(content);
    });

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/auth/swagger.json", "Auth API");
        c.SwaggerEndpoint("/swagger/fleetsubscription/swagger.json", "Fleet subscriptions API");
    });

    app.UseDeveloperExceptionPage();

    app.UseAuthorization();

    app.UseOcelot().Wait();

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Unhandled exception: {ex}");
    throw;
}
