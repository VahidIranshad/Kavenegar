using Kavenegar.API.Extensions;
using Kavenegar.API.Services;
using Kavenegar.Application.Contracts.Base;
using Kavenegar.Application.Contracts.Entity;
using Kavenegar.Domain.Base;
using Kavenegar.Infrastructure.Repositories.Entity;
using Kavenegar.Application;
using Kavenegar.Infrastructure;
using Kavenegar.API.Middleware;
using Kavenegar.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


ConfigurationManager configuration = builder.Configuration;
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwagger();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.ConfigureInfrastructureServices(configuration);
builder.Services.ConfigureApplicationServices();

/*because of Scrutor.AspNetCore we add IBlogRepository here*/
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.Decorate<IBlogRepository, CachedBlogRepository>();
builder.Services.AddStackExchangeRedisCache(redisOptions =>
{
    string connection = builder.Configuration
        .GetConnectionString("Redis");

    redisOptions.Configuration = connection;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();

/*Migration*/

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<KavenegarDbContext>();
        context.Database.SetConnectionString(configuration.GetConnectionString("KavenegarConnectionStrings"));
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }
    catch (Exception)
    {

    }

}

app.Run();
