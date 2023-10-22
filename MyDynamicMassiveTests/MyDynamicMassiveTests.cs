using Lab2_unit_testing;
using Moq;
using System.Reflection;

namespace MyDynamicMassiveTests
{
    public class MyDynamicMassiveTests
    {
        //Constructor
        [Theory]
        [InlineData(4)]
        [InlineData(6)]
        [InlineData(34)]
        public void Constructor_SetNormalCapacity_ChangingCapacity(int settedcapacity)
        {
            var capacity = settedcapacity;

            var collection = new MyDynamicMassive<int>(capacity);
            
            Assert.Equal(settedcapacity, capacity);
        }
        [Theory]
        [InlineData(-6)]
        public void Constructor_SetWrongCapacity_ArgumentOutOfRangeException(int settedcapacity)
        {
            var capacity = settedcapacity;

            void action() => new MyDynamicMassive<int>(capacity);

            Assert.Throws<ArgumentOutOfRangeException>(action);
        }
        [Fact]
        public void Constructor_SetZeroCapacity_InitializesPropertiesCorrectly()
        {
            var capacity = 0;

            var collection = new MyDynamicMassive<int>(capacity);
           
            Assert.Equal(capacity, collection.Count);
            Assert.Empty(collection);
        }
        //Indexer
        [Fact]
        public void Indexer_GettingElementsFromArray_HaveAppropriateElement()
        {
            var collection = new MyDynamicMassive<int>(3);
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);

            var resultofIndex0 = collection[0];
            var resultofIndex1 = collection[1];
            var resultofIndex2 = collection[2];

            Assert.Equal(1, resultofIndex0);
            Assert.Equal(2, resultofIndex1);
            Assert.Equal(3, resultofIndex2);
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(4)]
        public void Indexer_GettingElementsFromArrayWithWrongIndex_HaveAppropriateElement(int settedIndex)
        {
            var index = settedIndex;
            var collection = new MyDynamicMassive<int>(3);
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);

            Action action = () => { var value = collection[settedIndex];};

            Assert.Throws<IndexOutOfRangeException>(action);
        }
        [Fact]
        public void Indexer_SetElementAtIndex1_ModifiesCorrectValueForIndex1()
        {
            // Arrange
            var collection = new MyDynamicMassive<int>(3);
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);

            collection[1] = 42;

            Assert.Equal(1, collection[0]);
            Assert.Equal(42, collection[1]);
            Assert.Equal(3, collection[2]);
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(4)]
        public void Indexer_SettingElementsToArrayWithWrongIndex_HaveAppropriateElement(int settedIndex)
        {
            var index = settedIndex;
            var collection = new MyDynamicMassive<int>(3);
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);

            Action action = () => { collection[settedIndex] = 3; };

            Assert.Throws<IndexOutOfRangeException>(action);
        }
        //ReadOnly
        [Fact]
        public void IsReadOnly_CheckOnFalse_ReturnsFalse()
        {
            var collection = new MyDynamicMassive<int>(3);

            var isReadOnly = collection.IsReadOnly;

            Assert.False(isReadOnly);
        }
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
        //IndexOf
        [Fact]
        public void IndexOf_FindIndexOfNumber2_IndexOfNumber2()
        {
            var collection = new MyDynamicMassive<int>(5) { 1, 2, 3, 4, 5 };

            var index = collection.IndexOf(2);

            Assert.Equal(1, index);
        }
        [Fact]
        public void IndexOf_SetUnExistedItem_OutputMinus1()
        {
            var collection = new MyDynamicMassive<int>(5) { 1, 2, 3, 4, 5 };

            var index = collection.IndexOf(32);

            Assert.Equal(-1, index);
        }
        [Fact]
        public void IndexOf_SetNullItem_ArgumentNullException()
        {
            var collection = new MyDynamicMassive<string>(5) {"1", "2", "3", "4", "5"};

            void action() => collection.IndexOf(null);

            Assert.Throws<ArgumentNullException>(action);
        }
        //Insert
        [Theory]
        [InlineData(0, 5)]
        [InlineData(1, 17)]
        [InlineData(2, 15)]
        [InlineData(3, 7)]
        [InlineData(5, 32)]
        public void Insert_InsertingIntoThirdPosition_ChanginItemIntoThirdPosition(int index, int item)
        {
            var collection = new MyDynamicMassive<int>(5) { 1, 2, 3, 4, 5 };

            collection.Insert(index, item);
            
            Assert.Equal(6, collection.Count);
            Assert.Equal(item, collection[index]);
        }
        [Fact]
        public void Insert_SetNullItem_ArgumentNullException()
        {
            var collection = new MyDynamicMassive<string>(5) {"1", "2", "3", "4", "5"};

            void action() => collection.Insert(1, null);

            Assert.Throws<ArgumentNullException>(action);
        }
        [Theory]
        [InlineData(-5, 17)]
        [InlineData(23, 15)]
        public void Insert_SetWrongIndex_ArgumentOutOfRangeException(int index, int item)
        {
            var collection = new MyDynamicMassive<int>(5) { 1, 2, 3, 4, 5 };

            void action() => collection.Insert(index, item);

            Assert.Throws<ArgumentOutOfRangeException>(action);
        }

        //Enumerator class



        //MoveNext
        [Fact]
        public void MoveNext_SettingCursorIsBeforeTheStart_ReturnsTrueOnFirstCall()
        {
            var collection = new MyDynamicMassive<int>(3){1,2,3};

            var enumerator = collection.GetEnumerator();
            bool moveNextResult = enumerator.MoveNext();

            Assert.True(moveNextResult);
        }
        [Fact]
        public void MoveNext_SettingCursorIsAtEnd_ReturnsFalse()
        {
            var collection = new MyDynamicMassive<int>(3) { 1, 2, 3 };

            var enumerator = collection.GetEnumerator();
            while (enumerator.MoveNext()) { } 
            var result = enumerator.MoveNext();

            Assert.False(result);
        }
        [Fact]
        public void MoveNext_SetNullList_NullReferenceException()
        {
            MyDynamicMassive<int>? collection = null;

            void action() => collection.GetEnumerator().MoveNext();

            Assert.Throws<NullReferenceException>(action);
        }


    }
}