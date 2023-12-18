using NUnit.Framework;
using Organizer;

namespace BornToMove.OrganizerTest
{
    public class RotateSortTest
    {
        private RotateSort<int> Sorter;
        private IComparer<int> Comparer;

        [SetUp]
        public void SetUpTest()
        {
            Sorter = new RotateSort<int>();
            Comparer = Comparer<int>.Default;
        }

        [TestCase(new int[] { })]
        [TestCase(new int[] { 3 })]
        [TestCase(new int[] { 3, 32 })]
        [TestCase(new int[] { 3, 32, 332 })]
        [TestCase(new int[] { 32, 332, 3 })] //unsorted elements
        [TestCase(new int[] { 32, 3322, 332, 3, 332, 32 })] //unsorted duplicate elements
        public void TestSort(int[] inputArray)
        {
            //prepare
            List<int> input = new List<int>(inputArray);

            //run
            var result = Sorter.Sort(input, Comparer);

            //validate
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Has.Exactly(inputArray.Length).Items);
            Assert.That(result, Is.EquivalentTo(inputArray));
            //also check if input has not been modified
            Assert.That(input, Is.EquivalentTo(inputArray));
        }
    }
    
}
