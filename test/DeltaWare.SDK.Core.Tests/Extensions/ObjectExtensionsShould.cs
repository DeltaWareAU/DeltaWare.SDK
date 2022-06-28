using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace DeltaWare.SDK.Core.Tests.Extensions
{
    public class ObjectExtensionsShould
    {
        private class TestClass
        {
            public string PublicString { get; set; }

            public int PublicInt { get; set; }

            public DateTime PublicDateTime { get; set; }
        }

        [Fact]
        public void GetPublicPropertiesAsDictionary()
        {
            TestClass test = new TestClass
            {
                PublicDateTime = new DateTime(2020, 05, 01),
                PublicString = "Hello World",
                PublicInt = 42
            };

            Dictionary<string, object> testDictionary = test.GetPublicPropertiesAsDictionary();

            testDictionary.Count.ShouldBe(3);
            testDictionary["PublicDateTime"].ShouldBe(new DateTime(2020, 05, 01));
            testDictionary["PublicString"].ShouldBe("Hello World");
            testDictionary["PublicInt"].ShouldBe(42);
        }
    }
}
