using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using ContactFundApi.Application.Validators;

namespace ContactFundApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        
        return services;
    }
}
