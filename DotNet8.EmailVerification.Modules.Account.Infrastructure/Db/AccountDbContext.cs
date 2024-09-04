namespace DotNet8.EmailVerification.Modules.Account.Infrastructure.Db;

public class AccountDbContext :DbContext
{
    public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options) { }

    public DbSet<Tbl_User> Tbl_User { get; set; }

    public DbSet<Tbl_Setup> Tbl_Setup { get; set; }
}
