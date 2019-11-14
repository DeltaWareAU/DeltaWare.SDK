using System;
using DeltaWare.SDK.Serialization.Arguments;
using DeltaWare.SDK.Serialization.Arguments.Exceptions;
using DeltaWare.SDK.Tests.Serialization.Arguments.Models;
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

            ArgumentParser.Parse<ModelA>(args);

            ModelA.ValueA.ShouldBe("Hello A");
            ModelA.ValueB.ShouldBe(16);
            ModelA.ValueC.ShouldBe(true);
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

            ArgumentParser.Parse<ModelC>(args);

            ModelC.ValueA.ShouldBe("Hello A");
            ModelC.ValueB.ShouldBe(true);
        }

        [Test]
        public void FlagNotBoolType()
        {
            string[] args =
            {
                "-ValueA"
            };

            Should.Throw<ArgumentException>(() => { ArgumentParser.Parse<ModelB>(args); });
        }

        [Test]
        public void FlagHasColon()
        {
            string[] args =
            {
                "-ValueC:"
            };

            Should.Throw<ArgumentException>(() => { ArgumentParser.Parse<ModelA>(args); });
        }

        [Test]
        public void ParameterHasNoColon()
        {
            string[] args =
            {
                "-ValueA"
            };

            Should.Throw<ArgumentException>(() => { ArgumentParser.Parse<ModelA>(args); });
        }

        [Test]
        public void ParameterNotFound()
        {
            string[] args =
            {
                "-ValueD:"
            };

            Should.Throw<ArgumentNotFoundException>(() => { ArgumentParser.Parse<ModelA>(args); });
        }

        [Test]
        public void FlagNotFound()
        {
            string[] args =
            {
                "-ValueD"
            };

            Should.Throw<ArgumentNotFoundException>(() => { ArgumentParser.Parse<ModelA>(args); });
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

            Should.Throw<ArgumentException>(() => { ArgumentParser.Parse<ModelA>(args); });
        }

        [Test]
        public void FlagAssignedParameter_2()
        {
            string[] args =
            {
                "-ValueC",
                "Hello",
            };

            Should.Throw<ArgumentException>(() => { ArgumentParser.Parse<ModelA>(args); });
        }
    }
}
