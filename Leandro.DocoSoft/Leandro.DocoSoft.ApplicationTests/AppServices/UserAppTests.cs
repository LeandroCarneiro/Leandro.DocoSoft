using Leandro.DocoSoft.Application.Domain;
using Leandro.DocoSoft.ApplicationTests.Mocks;
using Leandro.DocoSoft.Domain.Entities;
using Leandro.DocoSoft.Domain.Interfaces;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Leandro.DocoSoft.ApplicationTests.AppServices.Tests
{
    [TestFixture()]
    public class UserAppTests : BaseTest
    {
        private readonly Mock<UserApp> _userApp = new Mock<UserApp>();
        private readonly Mock<IUserRepository> _repo = new Mock<IUserRepository>();

        public UserAppTests(IDbContext dbContext) : base(dbContext)
        {
            _repo.Setup(x => (x.Get(It.IsAny<long>(), CancellationToken.None).Result))
                .Returns(MockFaker.UserMock.FirstOrDefault()).Verifiable();

            _repo.Setup(x => (x.AddAsync(MockFaker.UserMock.FirstOrDefault(), CancellationToken.None).Result))
                .Returns(MockFaker.UserMock.FirstOrDefault().Id).Verifiable();

            _repo.Setup(x => (x.UpdateAsync(MockFaker.UserMock.FirstOrDefault(), CancellationToken.None)))
                .Verifiable();
        }

        [Test]
        public async Task Get_ShouldReturnUserContract_WhenUserExists()
        {
            var user = MockFaker.UserMock.FirstOrDefault();
            var userApp = new UserApp(_repo.Object);

            var result = await userApp.Get(user.Id, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.Age, result.Age);
            Assert.AreEqual(user.Username, result.Username);

            _repo.Verify(x => x.Get(user.Id, CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task Add_ShouldReturnUserId_WhenUserIsAdded()
        {
            var user = MockFaker.UserContractMock.FirstOrDefault();
            var userApp = new UserApp(_repo.Object);

            var result = await userApp.Create(user, CancellationToken.None);

            Assert.AreEqual(user.Id, result);
            _repo.Verify(x => x.AddAsync(It.IsAny<User>(), CancellationToken.None), Times.Once);
        }

        [Test]
        public async Task Update_ShouldCallRepositoryUpdate_WhenUserIsUpdated()
        {
            var user = MockFaker.UserContractMock.FirstOrDefault();
            var userApp = new UserApp(_repo.Object);

            await userApp.Update(user, CancellationToken.None);

            _repo.Verify(x => x.UpdateAsync(It.IsAny<User>(), CancellationToken.None), Times.Once);
        }       
    }
}