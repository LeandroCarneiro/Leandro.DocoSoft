using SertaoArch.UserMi.Domain.Entities;
using SertaoArch.UserMi.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SertaoArch.UserMi.Data.Contexts
{
    public class AppDbContext : BaseContext, IDbContext
    {
        public DbSet<User> TblUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(_configuration.GetConnectionString("AppDB"));
            base.OnConfiguring(options);

            options.UseLoggerFactory(_loggerFactory);
        }
    }
}
