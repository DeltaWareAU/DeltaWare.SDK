using System;
using DeltaWare.Tools.Serialization.Arguments.Exceptions;
using NUnit.Framework;
using DeltaWare.Tools.Serialization.Arguments.Tests;
using Shouldly;

namespace DeltaWare.Tools.Serialization.Arguments.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void SuccessfulAutoNameParseTest()
        {
            string[] args =
            {
                "-ValueA:",
                "Hello",
                "A",
                "-ValueC",
                "-ValueB:",
                "16",
            };

            Arguments.Parse<TestClassA>(args);

            TestClassA.ValueA.ShouldBe("Hello A");
            TestClassA.ValueB.ShouldBe(16);
            TestClassA.ValueC.ShouldBe(true);
        }

        [Test]
        public void SuccessfulParseSetNamesTest()
        {
            string[] args =
            {
                "-ValueSetA:",
                "Hello",
                "A",
                "-ValueSetB"
            };

            Arguments.Parse<TestClassC>(args);

            TestClassC.ValueA.ShouldBe("Hello A");
            TestClassC.ValueB.ShouldBe(true);
        }

        [Test]
        public void FlagNotBoolTypeTest()
        {
            string[] args =
            {
                "-ValueA"
            };

            Should.Throw<ArgumentException>(() => { Arguments.Parse<TestClassB>(args); });
        }

        [Test]
        public void FlagHasColon()
        {
            string[] args =
            {
                "-ValueC:"
            };

            Should.Throw<ArgumentException>(() => { Arguments.Parse<TestClassA>(args); });
        }

        [Test]
        public void ParameterHasNoColon()
        {
            string[] args =
            {
                "-ValueA"
            };

            Should.Throw<ArgumentException>(() => { Arguments.Parse<TestClassA>(args); });
        }

        [Test]
        public void ParameterNotFound()
        {
            string[] args =
            {
                "-ValueD:"
            };

            Should.Throw<ArgumentNotFoundException>(() => { Arguments.Parse<TestClassA>(args); });
        }

        [Test]
        public void FlagNotFound()
        {
            string[] args =
            {
                "-ValueD"
            };

            Should.Throw<ArgumentNotFoundException>(() => { Arguments.Parse<TestClassA>(args); });
        }

        [Test]
        public void FlagAssignedParameter_1()
        {
            string[] args =
            {
                "-ValueC",
                "Hello",
                "-ValueA:",
                "Hello"
            };

            Should.Throw<ArgumentException>(() => { Arguments.Parse<TestClassA>(args); });
        }

        [Test]
        public void FlagAssignedParameter_2()
        {
            string[] args =
            {
                "-ValueC",
                "Hello",
            };

            Should.Throw<ArgumentException>(() => { Arguments.Parse<TestClassA>(args); });
        }
    }
}
