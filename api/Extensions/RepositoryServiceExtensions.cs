namespace api.Extensions;

public static class RepositoryServiceExtensions
{
    public static IServiceCollection AddRepositoryService(this IServiceCollection services)
    {

        #region DependencyInjection
        services.AddScoped<ITokenService, TokenService>();

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        #endregion DependencyInjection

        return services;
    }
}
