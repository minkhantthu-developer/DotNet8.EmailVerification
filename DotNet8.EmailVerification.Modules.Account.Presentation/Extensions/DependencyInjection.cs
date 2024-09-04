﻿namespace DotNet8.EmailVerification.Modules.Account.Presentation.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(
            this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            return services.AddDbService(builder);
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

        public static void AddFluentEmailService(
            this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            string fromEmail = builder.Configuration.GetSection("FluentEmail:FromEmail").Value!;
            services.AddFluentEmail(fromEmail)
                  .AddSmtpSender("smtp.gmail.com", 587, fromEmail, "tjqc zvli bkqd hzjt");
        }
    }
}