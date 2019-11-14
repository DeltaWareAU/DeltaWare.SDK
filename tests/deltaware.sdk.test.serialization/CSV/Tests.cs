using System;
using DeltaWare.SDK.Serialization.Arguments;
using DeltaWare.SDK.Serialization.Arguments.Exceptions;
using DeltaWare.SDK.Serialization.CSV;
using DeltaWare.SDK.Tests.Serialization.Arguments.CSV.Models;
using NUnit.Framework;
using Shouldly;

namespace DeltaWare.SDK.Tests.Serialization.CSV
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void DeserializeTest_1()
        {
            string content = "John,Smith,16,True\r\nJessica,Pen-dragon,253,False";

            CsvSerializer csvSerializer = new CsvSerializer();

            ModelA[] modelA = csvSerializer.Deserialize<ModelA>(content, false);

            modelA[0].FirstName.ShouldBe("John");
            modelA[0].LastName.ShouldBe("Smith");
            modelA[0].Age.ShouldBe(16);
            modelA[0].Employed.ShouldBe(true);
            modelA[1].FirstName.ShouldBe("Jessica");
            modelA[1].LastName.ShouldBe("Pen-dragon");
            modelA[1].Age.ShouldBe(253);
            modelA[1].Employed.ShouldBe(false);
        }
    }
}
