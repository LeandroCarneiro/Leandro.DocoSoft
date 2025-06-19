using SertaoArch.Domain.Entities;
using SertaoArch.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace SertaoArch.Data.Contexts
{
    public class MockDb : BaseContext, IDbContext
    {
        public MockDb(DbContextOptions<BaseContext> options) : base(options)
        {
        }

        public virtual DbSet<User> TblUsers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseInMemoryDatabase("DocoSoftDB");
            base.OnConfiguring(options);

            options.UseLoggerFactory(_loggerFactory);
        }
    }
}
