using DotNet8.EmailVerification.Modules.Account.Infrastructure.Db;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.EmailVerification.Modules.Account.Presentation.Extensions
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
            WebApplicationBuilder builder )
        {
            return builder.Services.AddDbContext<AccountDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
            });
        }
    }
}
