using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Lab2_unit_testing
{
    class Program
    {
        public static void Main(string[] args)
        {


            #region MainCode
            Console.OutputEncoding = Encoding.UTF8;
            var myCollection = new MyDynamicMassive<int>(4);

            IEnumerator enumerator = myCollection.GetEnumerator();

            myCollection.CollectionCleared += EventMethods<int>.CollectionClearedHandler!;
            myCollection.ItemAdded += EventMethods<int>.ItemAddedHandler!;
            myCollection.ItemRemoved += EventMethods<int>.ItemRemovedHandler!;
            myCollection.ItemAddedToBeginning += EventMethods<int>.ItemAddedToBeginningHandler!;
            myCollection.ItemAddedToEnd += EventMethods<int>.ItemAddedToEndHandler!;
            myCollection.ItemRemovedAt += EventMethods<int>.ItemRemovedAtHandler!;
            myCollection.ContainsCheck += EventMethods<int>.ContainsCheckHandler!;
            myCollection.IndexOfCheck += EventMethods<int>.IndexOfCheckHandler!;


            myCollection.Add(1);
            Console.WriteLine("Колекція:");
            Println(myCollection);
            Console.WriteLine();

            myCollection.Add(17);
            Console.WriteLine("Колекція:");
            Println(myCollection);
            Console.WriteLine();

            myCollection.Add(3);
            Console.WriteLine("Колекція:");
            Println(myCollection);
            Console.WriteLine();

            myCollection.Insert(0, 0);
            Console.WriteLine("Колекція:");
            Println(myCollection);
            Console.WriteLine();

            myCollection.Insert(4, 4);
            Console.WriteLine("Колекція:");
            Println(myCollection);
            Console.WriteLine();

            myCollection.Insert(5, 10);
            Console.WriteLine("Колекція:");
            Println(myCollection);
            Console.WriteLine();

            myCollection.Insert(6, 6);
            Console.WriteLine("Колекція:");
            Println(myCollection);
            Console.WriteLine();

            myCollection.Insert(7, 7);
            Console.WriteLine("Колекція:");
            Println(myCollection);
            Console.WriteLine();

            myCollection.Insert(8, 8);
            Console.WriteLine("Колекція:");
            Println(myCollection);
            Console.WriteLine();

            myCollection.Insert(9, 9);
            Console.WriteLine("Колекція:");
            Println(myCollection);
            Console.WriteLine();

            myCollection.Remove(10);
            Console.WriteLine("Колекція:");
            Println(myCollection);
            Console.WriteLine();

            int[] destinationArray = new int[myCollection.Count];

            myCollection.CopyTo(destinationArray, 0);

            Console.WriteLine("Масив призначення після виклику CopyTo:");
            foreach (var item in destinationArray)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();

            int valueAtIndex2 = myCollection[2];
            Console.WriteLine($"Значення за індексом 2: {valueAtIndex2}");

            Console.WriteLine("Зміна значення за індексом 2 на 10");
            myCollection[2] = 10;
            Console.WriteLine($"Значення за індексом 2 після зміни: {myCollection[2]}");


            bool contains5 = myCollection.Contains(10);
            Console.WriteLine($"Чи містить колекція значення 10: {contains5}");


            int indexOf2 = myCollection.IndexOf(2);
            Console.WriteLine($"Індекс елемента 2: {indexOf2}");
            Println(myCollection);

            int indexOf7 = myCollection.IndexOf(7);
            Console.WriteLine($"Індекс елемента 7: {indexOf7}");
            Println(myCollection);

            Console.WriteLine($"Розмір колекції: {myCollection.Count}");
            Console.WriteLine("");
            myCollection.RemoveAt(1);

            Console.WriteLine("Колекція:");
            Println(myCollection);
            myCollection.Clear();


            Console.WriteLine("Колекція після виконання всіх дій:");
            Println(myCollection);
            #endregion

            Console.ReadLine();
        }
        private static void Println<T>(MyDynamicMassive<T> collection)
        {
            foreach (var item in collection)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }

    }

}