using System;
using DeltaWare.SDK.Serialization.Arguments;
using DeltaWare.SDK.Serialization.Arguments.Exceptions;
using NUnit.Framework;
using Shouldly;

namespace DeltaWare.SDK.Tests.Serialization.Arguments
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void SuccessfulAutoNameParse()
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

            ArgumentParser.Parse<TestClassA>(args);

            TestClassA.ValueA.ShouldBe("Hello A");
            TestClassA.ValueB.ShouldBe(16);
            TestClassA.ValueC.ShouldBe(true);
        }

        [Test]
        public void SuccessfulParseSetNames()
        {
            string[] args =
            {
                "-ValueSetA:",
                "Hello",
                "A",
                "-ValueSetB"
            };

            ArgumentParser.Parse<TestClassC>(args);

            TestClassC.ValueA.ShouldBe("Hello A");
            TestClassC.ValueB.ShouldBe(true);
        }

        [Test]
        public void FlagNotBoolType()
        {
            string[] args =
            {
                "-ValueA"
            };

            Should.Throw<ArgumentException>(() => { ArgumentParser.Parse<TestClassB>(args); });
        }

        [Test]
        public void FlagHasColon()
        {
            string[] args =
            {
                "-ValueC:"
            };

            Should.Throw<ArgumentException>(() => { ArgumentParser.Parse<TestClassA>(args); });
        }

        [Test]
        public void ParameterHasNoColon()
        {
            string[] args =
            {
                "-ValueA"
            };

            Should.Throw<ArgumentException>(() => { ArgumentParser.Parse<TestClassA>(args); });
        }

        [Test]
        public void ParameterNotFound()
        {
            string[] args =
            {
                "-ValueD:"
            };

            Should.Throw<ArgumentNotFoundException>(() => { ArgumentParser.Parse<TestClassA>(args); });
        }

        [Test]
        public void FlagNotFound()
        {
            string[] args =
            {
                "-ValueD"
            };

            Should.Throw<ArgumentNotFoundException>(() => { ArgumentParser.Parse<TestClassA>(args); });
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

            Should.Throw<ArgumentException>(() => { ArgumentParser.Parse<TestClassA>(args); });
        }

        [Test]
        public void FlagAssignedParameter_2()
        {
            string[] args =
            {
                "-ValueC",
                "Hello",
            };

            Should.Throw<ArgumentException>(() => { ArgumentParser.Parse<TestClassA>(args); });
        }
    }
}
