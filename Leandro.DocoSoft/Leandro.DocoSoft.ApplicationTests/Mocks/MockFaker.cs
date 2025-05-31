using ComplexFaker;
using Leandro.DocoSoft.Contracts.AppObject;
using Leandro.DocoSoft.Domain.Entities;
using System.Collections.Generic;

namespace Leandro.DocoSoft.ApplicationTests.Mocks
{
    public static class MockFaker
    {
        public static List<User> UserMock
        {
            get
            {
                var mock = new FakeDataService().Generate<List<User>>();
                return mock;
            }
        }

        public static List<UserContract> UserContractMock
        {
            get
            {
                var mock = new FakeDataService().Generate<List<UserContract>>();
                return mock;
            }
        }
    }
}
