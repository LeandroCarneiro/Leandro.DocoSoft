using SertaoArch.UserMi.Application.Domain;
using SertaoArch.UserMi.ApplicationTests.Mocks;
using SertaoArch.UserMi.Business.Domain;
using SertaoArch.UserMi.Contracts.AppObject;
using SertaoArch.UserMi.Domain.Interfaces;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SertaoArch.UserMi.ApplicationTests.AppServices.Tests
{
    [TestFixture()]
    public class UserAppTests : BaseTest
    {
        private readonly IUserRepository _repo;
        private readonly List<UserContract> _users;

        public UserAppTests() : base()
        {
            _repo = new UserRepository(_dbContext);
            _users = MockFaker.UserContractMock.ToList();
        }

        [Test]
        public async Task Get_ShouldReturnUserContract_WhenUserExists()
        {
            var user = _dbContext.TblUsers.FirstOrDefault();
            var userApp = new UserApp(_repo);

            var result = await userApp.Get(user.Id, CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(user.FirstName, result.FirstName);
            Assert.AreEqual(user.LastName, result.LastName);
            Assert.AreEqual(user.Age, result.Age);
            Assert.AreEqual(user.Username, result.Username);
        }

        [Test]
        public async Task Add_ShouldReturnUserId_WhenUserIsAdded()
        {
            var user = _users.FirstOrDefault();
            var userApp = new UserApp(_repo);

            var result = await userApp.Create(user, CancellationToken.None);

            Assert.AreEqual(user.Id, result);
        }    
    }
}