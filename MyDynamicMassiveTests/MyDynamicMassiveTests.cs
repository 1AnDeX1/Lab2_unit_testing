using Lab2_unit_testing;
using System.Reflection;

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
        public void Add_SetNull_ArgumentNullException()
        {
            var collection = new MyDynamicMassive<string>(1);

            void action() => collection.Add(null);

            Assert.Throws<ArgumentNullException>(action);
        }
        [Fact]
        public void Add_ResizesCollection_ChangingCapacity()
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
        [Fact]
        public void Clear_SettingFiveItems_ClearingMassive()
        {
            var collection = new MyDynamicMassive<int>(5)
            {
                1,2,3,4,5
            };
            
            collection.Clear();

            Assert.Empty(collection);
        }
        [Fact]
        public void Clear_SettingNull_NullReferenceException()
        {
            var collection = new MyDynamicMassive<string>(10);

            typeof(MyDynamicMassive<string>).GetField("_items", BindingFlags.Instance | BindingFlags.NonPublic)?.SetValue(collection, null);
            void action() => collection.Clear();


            Assert.Throws<NullReferenceException>(action);
        }

        //Contains method
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Contains_SearchingOfExistingNumber_True(int a)
        {
            var collection = new MyDynamicMassive<int>(7) 
            {
                1,2, 3, 4, 5, 6, 7,
            };

            bool result = collection.Contains(a);

            Assert.True(result);

        }

        [Theory]
        [InlineData(34)]
        [InlineData(8)]
        [InlineData(9)]
        public void Contains_SearchingOfUnexistingNumber_False(int a)
        {
            var collection = new MyDynamicMassive<int>(7)
            {
                1,2, 3, 4, 5, 6, 7,
            };

            bool result = collection.Contains(a);

            Assert.False(result);
        }

        [Fact]
        public void Contains_SetNull_ArgumentNullException()
        {
            var collection = new MyDynamicMassive<string>(7);

            void action() => collection.Contains(null);

            Assert.Throws<ArgumentNullException>(action);
        }
        //CopyTo
        [Fact]
        public void CopyTo_SetArrayWithEqualLength_CopiedArray()
        {
            var mainCollection = new MyDynamicMassive<int>(5)
            {
                1,2, 3, 4, 5
            };
            int[] coppiedCollection = new int[5];

            mainCollection.CopyTo(coppiedCollection, 0);

            Assert.NotEmpty(coppiedCollection);
            Assert.Equal(mainCollection, coppiedCollection);
        }
        [Fact]
        public void CopyTo_SetArrayWithMoreLengthFromTheSecondElement_CopiedArray()
        {
            var collection = new MyDynamicMassive<int>(3)
            {
                1, 2, 3
            };
            var destinationArray = new int[5];
            
            collection.CopyTo(destinationArray, 1);
            
            Assert.Equal(new int[] { 0, 1, 2, 3, 0 }, destinationArray);
        }
        [Fact]
        public void CopyTo_SetArrayWithLessLength_ArgumentException()
        {
            var mainCollection = new MyDynamicMassive<int>(5)
            {
                1,2, 3, 4, 5
            };
            int[] coppiedCollection = new int[4];

            void action() => mainCollection.CopyTo(coppiedCollection, 0);

            Assert.Throws<ArgumentException>(action);
        }
        [Fact]
        public void CopyTo_SetNullArray_ArgumentNullException()
        {
            var mainCollection = new MyDynamicMassive<int>(5)
            {
                1,2, 3, 4, 5
            };
            int[]? coppiedCollection = null;

            void action() => mainCollection.CopyTo(coppiedCollection, 0);

            Assert.Throws<ArgumentNullException>(action);
        }
        //RemoveAt

        [Fact]
        public void RemoveAt_RemovingAtIndex0_RemovedItemAtIndex0()
        {
            var collection = new MyDynamicMassive<int>(5) { 1, 2, 3, 4, 5 };

            collection.RemoveAt(0);

            Assert.Equal(new int[] { 2, 3, 4, 5 }, collection);
        }
        [Fact]
        public void RemoveAt_RemovingAtIndex2_RemovedItemAtIndex2()
        {
            var collection = new MyDynamicMassive<int>(5) { 1, 2, 3, 4, 5 };

            collection.RemoveAt(2);

            Assert.Equal(new int[]{1, 2, 4, 5}, collection);
        }
        [Fact]
        public void RemoveAt_RemovingAtIndex0_RemovedItem2()
        {
            var collection = new MyDynamicMassive<int>(5) { 1, 2, 3, 4, 5 };
            var size = collection.Count;

            collection.RemoveAt(0);

            Assert.Equal(size - 1 , collection.Count);
            Assert.Equal(new int[] { 2, 3, 4, 5 }, collection);
        }
        [Theory]
        [InlineData(-5)]
        [InlineData(10)]
        [InlineData(5)]
        public void RemoveAt_SetWrongIndex_ArgumentOutOfRangeException(int index)
        {
            var collection = new MyDynamicMassive<int>(5) { 1, 2, 3, 4, 5 };

            void action() => collection.RemoveAt(index);
            
            Assert.Throws<ArgumentOutOfRangeException>(action);
        }
        //Remove

        [Fact]
        public void Remove_RemovingItem1_RemovedItem1()
        {
            var collection = new MyDynamicMassive<int>(5) { 1, 2, 3, 4, 5 };

            collection.Remove(1);

            Assert.Equal(new int[] { 2, 3, 4, 5 }, collection);
        }
        [Fact]
        public void Remove_RemovingItem4_RemovedItem4()
        {
            var collection = new MyDynamicMassive<int>(5) { 1, 2, 3, 4, 5 };

            collection.Remove(4);

            Assert.Equal(new int[] {1, 2, 3, 5 }, collection);
        }
        [Fact]
        public void Remove_SetNull_ArgumentNullException()
        {
            var collection = new MyDynamicMassive<string>(5) {"1", "2", "3", "4", "5" };

            void action() => collection.Remove(null);

            Assert.Throws<ArgumentNullException>(action);
        }
        
    }
}