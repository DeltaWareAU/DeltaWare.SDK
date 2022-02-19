using DeltaWare.SDK.Common.Collections.RecyclingQueue;
using NUnit.Framework;
using Shouldly;

namespace DeltaWare.SDK.Test.Common.GenericCollections
{
    [TestFixture]
    public class RecyclingQueueTests
    {
        [Test]
        public void InstantiationTest()
        {
            RecyclingQueue<string> recyclingQueue = new RecyclingQueue<string>(10);

            recyclingQueue.Capacity.ShouldBe(10);
        }

        [Test]
        public void AddToTest()
        {
            RecyclingQueue<string> recyclingQueue = new RecyclingQueue<string>(4);

            recyclingQueue.Add("One");
            recyclingQueue.Add("Two");
            recyclingQueue.Add("Three");
            recyclingQueue.Add("Four");

            recyclingQueue[0].ShouldBe("One");
            recyclingQueue[1].ShouldBe("Two");
            recyclingQueue[2].ShouldBe("Three");
            recyclingQueue[3].ShouldBe("Four");
        }

        [Test]
        public void AddToCapacityOverFlowTest()
        {
            RecyclingQueue<string> recyclingQueue = new RecyclingQueue<string>(4);

            recyclingQueue.Add("One");
            recyclingQueue.Add("Two");
            recyclingQueue.Add("Three");
            recyclingQueue.Add("Four");

            recyclingQueue[0].ShouldBe("One");
            recyclingQueue[1].ShouldBe("Two");
            recyclingQueue[2].ShouldBe("Three");
            recyclingQueue[3].ShouldBe("Four");

            recyclingQueue.Add("Five");

            recyclingQueue[0].ShouldBe("Two");
            recyclingQueue[1].ShouldBe("Three");
            recyclingQueue[2].ShouldBe("Four");
            recyclingQueue[3].ShouldBe("Five");
        }
    }
}
