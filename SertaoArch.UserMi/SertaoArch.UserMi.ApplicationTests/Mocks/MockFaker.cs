using ComplexFaker;
using SertaoArch.UserMi.Contracts.AppObject;
using SertaoArch.UserMi.Domain.Entities;
using System.Collections.Generic;

namespace SertaoArch.UserMi.ApplicationTests.Mocks
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
