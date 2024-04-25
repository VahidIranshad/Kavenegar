using Kavenegar.Application.Contracts.Entity;
using Kavenegar.Domain.Base;
using Kavenegar.Infrastructure.Repositories.Entity;

var builder = WebApplication.CreateBuilder(args);


ConfigurationManager configuration = builder.Configuration;
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*because of Scrutor.AspNetCore we add IBlogRepository here*/
builder.Services.AddScoped<IBlogRepository, BlogRepository>();
builder.Services.Decorate<IBlogRepository, CachedRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
