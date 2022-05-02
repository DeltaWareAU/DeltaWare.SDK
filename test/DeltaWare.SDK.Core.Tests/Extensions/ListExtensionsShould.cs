using Shouldly;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DeltaWare.SDK.Core.Tests.Extensions
{
    public class ListExtensionsShould
    {
        [Theory]
        [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 2)]
        [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 3)]
        [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 4)]
        [InlineData("ABCD", 6)]
        public void SplitIList(string input, int count)
        {
            IList originalList = input.ToCharArray().ToList();

            IList[] splitLists = Should.NotThrow(() => originalList.Split(count));

            splitLists.Length.ShouldBe(count);

            for (int i = 0; i < splitLists.Length; i++)
            {
                for (int j = 0; j < splitLists[i].Count; j++)
                {
                    int index = i + j * count;

                    splitLists[i][j].ShouldBe(originalList[index]);
                }
            }

            if (originalList.Count < splitLists.Length)
            {
                int startingIndex = splitLists.Length - (splitLists.Length - originalList.Count);

                for (int i = startingIndex; i < splitLists.Length; i++)
                {
                    splitLists[i].Count.ShouldBe(0);
                }
            }
        }

        [Theory]
        [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 2)]
        [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 3)]
        [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZ", 4)]
        [InlineData("ABCD", 6)]
        public void SplitList(string input, int count)
        {
            List<char> originalList = input.ToCharArray().ToList();

            List<char>[] splitLists = Should.NotThrow(() => originalList.Split(count));

            splitLists.Length.ShouldBe(count);

            for (int i = 0; i < splitLists.Length; i++)
            {
                for (int j = 0; j < splitLists[i].Count; j++)
                {
                    int index = i + j * count;

                    splitLists[i][j].ShouldBe(originalList[index]);
                }
            }

            if (originalList.Count < splitLists.Length)
            {
                int startingIndex = splitLists.Length - (splitLists.Length - originalList.Count);

                for (int i = startingIndex; i < splitLists.Length; i++)
                {
                    splitLists[i].Count.ShouldBe(0);
                }
            }
        }
    }
}