using SertaoArch.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using SertaoArch.Data.Contexts;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SertaoArch.Data;
using SertaoArch.UserMi.ApplicationTests.Mocks;

namespace SertaoArch.UserMi.ApplicationTests
{
    public class BaseTest
    {
        protected IDbContext _dbContext { get; } = GetInMemoryDbContext();
        public BaseTest()
        {
            _dbContext.TblUsers.AddRange(MockFaker.UserMock);
            _dbContext.SaveChangesAsync().Wait();

            Builder.Setup();
        }

        private static MockDb GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<BaseContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase", new InMemoryDatabaseRoot())
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .EnableSensitiveDataLogging()
                .Options;

            return new MockDb(options);
        }
    }
}
