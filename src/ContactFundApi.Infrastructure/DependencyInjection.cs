using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ContactFundApi.Domain.Interfaces;
using ContactFundApi.Infrastructure.Data;
using ContactFundApi.Infrastructure.Repositories;
using ContactFundApi.Infrastructure.Services;

namespace ContactFundApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IFundRepository, FundRepository>();
        services.AddScoped<IContactFundRepository, ContactFundRepository>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
