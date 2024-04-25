using FluentValidation;
using Kavenegar.Application.Behaviors;
using Kavenegar.Application.Dto.Entity.BlogDtos;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kavenegar.Application
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            services.AddTransient<IValidator<BlogCrudDto>, BlogCrudDtoValidator>();

            return services;
        }
    }
}
