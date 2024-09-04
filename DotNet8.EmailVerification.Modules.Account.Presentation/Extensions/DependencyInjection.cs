namespace DotNet8.EmailVerification.Modules.Account.Presentation.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDependencyInjection(
        this IServiceCollection services,
        WebApplicationBuilder builder)
    {
        return services.AddDbService(builder)
                       .AddDataAccessService()
                       .AddHangFireService(builder);
    }

    private static IServiceCollection AddDbService(
        this IServiceCollection services,
        WebApplicationBuilder builder)
    {
        return builder.Services.AddDbContext<AccountDbContext>(opt =>
        {
            opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
        });
    }

    public static IServiceCollection AddHangFireService(
        this IServiceCollection services,
        WebApplicationBuilder builder)
    {
        builder.Services.AddHangfire(opt =>
        {
            opt.UseSqlServerStorage(builder.Configuration.GetConnectionString("DbConnection"))
               .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
               .UseSimpleAssemblyNameTypeSerializer()
               .UseRecommendedSerializerSettings();
        });
        builder.Services.AddHangfireServer();
        return services;
    }

    public static IServiceCollection AddDataAccessService(this IServiceCollection services)
    {
        return services.AddScoped<IUserService, UserService>();
    }

    public static void AddFluentEmailService(
        this IServiceCollection services,
        WebApplicationBuilder builder)
    {
        string fromEmail = builder.Configuration.GetSection("FluentEmail:FromEmail").Value!;
        services.AddFluentEmail(fromEmail)
              .AddSmtpSender("smtp.gmail.com", 587, fromEmail, "tjqc zvli bkqd hzjt");
    }
}
