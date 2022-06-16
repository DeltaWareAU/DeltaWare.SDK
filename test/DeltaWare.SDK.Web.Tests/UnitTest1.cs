using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DeltaWare.SDK.Web.Tests
{
    public class TestUser
    {
        public int Age { get; set; }
        public DateTime BirthDate { get; set; }
        public string Name { get; set; }
    }

    public class UnitTest1
    {
        public List<TestUser> TestUsers { get; } = new List<TestUser>
        {
            new TestUser
            {
                Age = 5,
                Name = "John",
                BirthDate = new DateTime(1995, 01, 01)
            },
            new TestUser
            {
                Age = 20,
                Name = "Timmy",
                BirthDate = new DateTime(1980, 01, 01)
            },
            new TestUser
            {
                Age = 10,
                Name = "Ben",
                BirthDate = new DateTime(1990, 01, 01)
            }
        };

        [Fact]
        public void Test1()
        {
            var ordered = TestUsers.OrderBy<TestUser, object>(t => t.BirthDate).ToList();
        }
    }
}