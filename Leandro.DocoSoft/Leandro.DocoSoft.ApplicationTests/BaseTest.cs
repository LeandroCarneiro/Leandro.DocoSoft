using Leandro.DocoSoft.Domain.Interfaces;

namespace Leandro.DocoSoft.ApplicationTests
{
    public class BaseTest
    {
        protected readonly IDbContext _dbContext;
        public BaseTest(IDbContext dbContext)
        {
            _dbContext = dbContext;

            Builder.Setup();
        }
    }
}
