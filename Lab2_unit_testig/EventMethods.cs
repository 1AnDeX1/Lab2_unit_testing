using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab2_unit_testing
{
    public static class EventMethods<T>
    {
        public static void CollectionClearedHandler(object sender, EventInfo<T> e)
        {
            Console.WriteLine($"Колекція була очищена. Дія: {e.Action}");
        }

        public static void ItemAddedHandler(object sender, EventInfo<T> e)
        {
            Console.WriteLine($"Додано елемент {e.Item}. Дія: {e.Action}");
        }

        public static void ItemRemovedHandler(object sender, EventInfo<T> e)
        {
            Console.WriteLine($"Видалено елемент {e.Item}. Дія: {e.Action}");
        }

        public static void ItemAddedToBeginningHandler(object sender, EventInfo<T> e)
        {
            Console.WriteLine($"Додано елемент {e.Item} на початок. Дія: {e.Action}");
        }

        public static void ItemAddedToEndHandler(object sender, EventInfo<T> e)
        {
            Console.WriteLine($"Додано елемент {e.Item} в кінець. Дія: {e.Action}");
        }

        public static void ItemRemovedAtHandler(object sender, EventInfo<T> e)
        {
            Console.WriteLine($"Видалено елемент {e.Item} за індексом. Дія: {e.Action}");
        }

        public static void ContainsCheckHandler(object sender, EventInfo<T> e)
        {
            Console.WriteLine($"Викликаний метод Contains з параметром {e.Item}. Дія: {e.Action}");
        }

        public static void IndexOfCheckHandler(object sender, EventInfo<T> e)
        {
            Console.WriteLine($"Викликаний метод IndexOf з параметром {e.Item}. Дія: {e.Action}");
        }
    }

}
