using Lab2_unit_testing;

namespace MyDynamicMassiveTests
{
    public class MyDynamicMassiveTests
    {
        //Add method
        [Fact]
        public void Add_AddsItemToCollection_AddingItemAndIncreaceOfCapacity()
        {
            var collection = new MyDynamicMassive<int>(1);
            collection.Add(5);
            Assert.Single(collection);
            Assert.Equal(5, collection[0]);
        }
        [Fact]
        public void Add_ThrowsArgumentNullException_WhenItemIsNull()
        {
            var collection = new MyDynamicMassive<string>(1);
            Assert.Throws<ArgumentNullException>(() => collection.Add(null));
        }
        [Fact]
        public void Add_ResizesCollection_WhenCapacityIsExceeded()
        {
            var collection = new MyDynamicMassive<int>(1);
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);
            collection.Add(4);
            collection.Add(5);
            collection.Add(6);
            Assert.Equal(6, collection.Count);
            Assert.Equal(1, collection[0]);
            Assert.Equal(2, collection[1]);
            Assert.Equal(3, collection[2]);
            Assert.Equal(4, collection[3]);
            Assert.Equal(5, collection[4]);
            Assert.Equal(6, collection[5]);
        }

        //Clear method



    }
}