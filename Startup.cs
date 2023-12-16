using Microsoft.Extensions.FileProviders;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials();

            });
        });

        services.AddLogging(builder =>
        {
            builder.AddConsole();
        });


    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors();

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(
                Path.Combine(env.ContentRootPath, "uploads")),
            RequestPath = "/uploads"
        });


    }
}
