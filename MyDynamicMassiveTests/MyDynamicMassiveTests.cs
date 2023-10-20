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


    }
}