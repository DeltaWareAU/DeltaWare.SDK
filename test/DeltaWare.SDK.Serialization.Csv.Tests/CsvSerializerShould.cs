using DeltaWare.SDK.Serialization.Csv.Tests.Models;
using Shouldly;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DeltaWare.SDK.Serialization.Csv.Tests
{
    public class CsvSerializerShould
    {
        [Fact]
        public void SerializeCsv()
        {
            CsvSerializer serializer = new();

            Stream stream = new FileStream("./_Data/TEST.Persons.csv", FileMode.Open);

            PersonWithHeader[] persons = Should.NotThrow(() => serializer.Deserialize<PersonWithHeader>(stream)).ToArray();

            stream.Dispose();

            persons.Length.ShouldBe(3);

            persons[0].Id.ShouldBe(0);
            persons[0].FirstName.ShouldBe("John");
            persons[0].LastName.ShouldBe("Smith");
            persons[0].BirthDate.ShouldBe(new DateTime(1987, 01, 01));
            persons[0].Active.ShouldBe(true);
            persons[0].ClassId.ShouldBeNull();

            persons[1].Id.ShouldBe(1);
            persons[1].FirstName.ShouldBe("Jeb");
            persons[1].LastName.ShouldBe("Kerbal");
            persons[1].BirthDate.ShouldBe(new DateTime(1970, 04, 24));
            persons[1].Active.ShouldBe(false);
            persons[1].ClassId.Value.ShouldBe(15);

            persons[2].Id.ShouldBe(2);
            persons[2].FirstName.ShouldBe("Del \"Fonzie\" Fon");
            persons[2].LastName.ShouldBe("Mathi");
            persons[2].BirthDate.ShouldBe(new DateTime(2000, 06, 27));
            persons[2].Active.ShouldBe(true);
            persons[2].ClassId.ShouldBeNull();

            stream = new MemoryStream();

            serializer.Serialize(persons, stream);

            stream.Seek(0, SeekOrigin.Begin);

            persons = Should.NotThrow(() => serializer.Deserialize<PersonWithHeader>(stream)).ToArray();

            persons.Length.ShouldBe(3);

            persons[0].Id.ShouldBe(0);
            persons[0].FirstName.ShouldBe("John");
            persons[0].LastName.ShouldBe("Smith");
            persons[0].BirthDate.ShouldBe(new DateTime(1987, 01, 01));
            persons[0].Active.ShouldBe(true);
            persons[0].ClassId.ShouldBeNull();

            persons[1].Id.ShouldBe(1);
            persons[1].FirstName.ShouldBe("Jeb");
            persons[1].LastName.ShouldBe("Kerbal");
            persons[1].BirthDate.ShouldBe(new DateTime(1970, 04, 24));
            persons[1].Active.ShouldBe(false);
            persons[1].ClassId.Value.ShouldBe(15);

            persons[2].Id.ShouldBe(2);
            persons[2].FirstName.ShouldBe("Del \"Fonzie\" Fon");
            persons[2].LastName.ShouldBe("Mathi");
            persons[2].BirthDate.ShouldBe(new DateTime(2000, 06, 27));
            persons[2].Active.ShouldBe(true);
            persons[2].ClassId.ShouldBeNull();
        }

        [Fact]
        public async Task SerializeCsvAsync()
        {
            CsvSerializer serializer = new();

            Stream stream = new FileStream("./_Data/TEST.Persons.csv", FileMode.Open);

            PersonWithHeader[] persons = Should.NotThrow(async () => await serializer.DeserializeAsync<PersonWithHeader>(stream)).ToArray();

            await stream.DisposeAsync();

            persons.Length.ShouldBe(3);

            persons[0].Id.ShouldBe(0);
            persons[0].FirstName.ShouldBe("John");
            persons[0].LastName.ShouldBe("Smith");
            persons[0].BirthDate.ShouldBe(new DateTime(1987, 01, 01));
            persons[0].Active.ShouldBe(true);
            persons[0].ClassId.ShouldBeNull();

            persons[1].Id.ShouldBe(1);
            persons[1].FirstName.ShouldBe("Jeb");
            persons[1].LastName.ShouldBe("Kerbal");
            persons[1].BirthDate.ShouldBe(new DateTime(1970, 04, 24));
            persons[1].Active.ShouldBe(false);
            persons[1].ClassId.Value.ShouldBe(15);

            persons[2].Id.ShouldBe(2);
            persons[2].FirstName.ShouldBe("Del \"Fonzie\" Fon");
            persons[2].LastName.ShouldBe("Mathi");
            persons[2].BirthDate.ShouldBe(new DateTime(2000, 06, 27));
            persons[2].Active.ShouldBe(true);
            persons[2].ClassId.ShouldBeNull();

            stream = new MemoryStream();

            await serializer.SerializeAsync(persons, stream);

            stream.Seek(0, SeekOrigin.Begin);

            persons = Should.NotThrow(async () => await serializer.DeserializeAsync<PersonWithHeader>(stream)).ToArray();

            persons.Length.ShouldBe(3);

            persons[0].Id.ShouldBe(0);
            persons[0].FirstName.ShouldBe("John");
            persons[0].LastName.ShouldBe("Smith");
            persons[0].BirthDate.ShouldBe(new DateTime(1987, 01, 01));
            persons[0].Active.ShouldBe(true);
            persons[0].ClassId.ShouldBeNull();

            persons[1].Id.ShouldBe(1);
            persons[1].FirstName.ShouldBe("Jeb");
            persons[1].LastName.ShouldBe("Kerbal");
            persons[1].BirthDate.ShouldBe(new DateTime(1970, 04, 24));
            persons[1].Active.ShouldBe(false);
            persons[1].ClassId.Value.ShouldBe(15);

            persons[2].Id.ShouldBe(2);
            persons[2].FirstName.ShouldBe("Del \"Fonzie\" Fon");
            persons[2].LastName.ShouldBe("Mathi");
            persons[2].BirthDate.ShouldBe(new DateTime(2000, 06, 27));
            persons[2].Active.ShouldBe(true);
            persons[2].ClassId.ShouldBeNull();
        }
    }
}