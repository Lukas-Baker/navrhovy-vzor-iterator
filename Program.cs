using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IteratorExample
{
    // Zde definujeme rozhraní pro náš iterátor
    interface IIterator<T>
    {
        void First(); // reset iterátoru, přesun na první pozici
        void Next(); // posunutí o prvek vpřed
        bool IsDone(); // kontrola konce průchodu
        T CurrentItem(); // získání prvku, na který iterátor právě "ukazuje"
    }

    // Konkrétní implementace iterátoru pro pole
    class ArrayIterator<T> : IIterator<T>
    {
        private T[] _array;
        private int _currentIndex;

        // V konstruktoru si předáme pole, jež budeme procházet iterátorem
        public ArrayIterator(T[] array)
        {
            // provedeme ošetření pro prázdné pole
            if (array == null)
                throw new ArgumentException("Array cannot be null");

            _array = array;
            _currentIndex = 0;
        }

        // Metoda vrátí hodnotu, na kterou iterátor nyní ukazuje
        public T CurrentItem()
        {
            if (IsDone()) // ošetříme, zda není průchod hotový
                throw new IteratorOutOfBoundException();
            else
                return _array[_currentIndex];
        }

        // Metoda resetuje iterátor do původního stavu -
        // přesuneme se na první prvek průchodu
        public void First()
        {
            _currentIndex = 0;
        }

        // Pomocí této metody zjišťujeme, zda není průchod ukončen
        public bool IsDone()
        {
            return _currentIndex >= _array.Length;
        }

        // Metoda posouvá iterátor o jeden prvek dopředu
        public void Next()
        {
            ++_currentIndex;
        }
    }

    // Konkrétní implementace iterátoru, jež pole prochází pozpátku
    // Myšlenka je stejná jako u implementace výše, pouze začínáme od konce
    // a při posouvání na další prvek se indexem posouváme blíže k začátku
    class ReverseArrayIterator<T> : IIterator<T>
    {
        private T[] _array;
        private int _currentIndex;

        public ReverseArrayIterator(T[] array)
        {
            if (array == null)
                throw new ArgumentException("Array cannot be null");

            _array = array;
            _currentIndex = _array.Length - 1;
        }

        public T CurrentItem()
        {
            if (IsDone())
                throw new IteratorOutOfBoundException();
            else
                return _array[_currentIndex];
        }

        public void First()
        {
            _currentIndex = _array.Length - 1;
        }

        public bool IsDone()
        {
            return _currentIndex < 0;
        }

        public void Next()
        {
            --_currentIndex;
        }
    }

    class IteratorOutOfBoundException : SystemException
    {
        // there should be some more meaningful implementation =)
    }

    class Program
    {
        // Metoda vezme instanci iterátoru a postará se o výpis všech prvků
        // Zároveň se zde ukazuje klasické využití iterátorových funkcí v cyklu for
        // pro průchod všemi prvky
        static void printAllUsingIterator(IIterator<int> iterator)
        {
            for (iterator.First(); !iterator.IsDone(); iterator.Next())
                Console.Write(iterator.CurrentItem());
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            // Prvky, které budeme pomocí iterátorů procházet
            int[] intArr = { 1, 2, 3, 4, 5 };

            // Nejprve použijeme první iterátor a vypíšeme prvky
            Console.WriteLine("Using normal iterator: ");
            IIterator<int> iterator = new ArrayIterator<int>(intArr);
            printAllUsingIterator(iterator);

            // Nyní použijeme reverse iterátor a znovu vypíšeme prvky
            // Můžeme si všimnout, že pro výpis všech prvků používáme stejnou funkci
            // (printAllUsingIterator) a přitom jsou výpisy různé, protože se o logiku 
            // průchodu starají iterátory
            Console.WriteLine("Using reverse iterator: ");
            iterator = new ReverseArrayIterator<int>(intArr);
            printAllUsingIterator(iterator);

            Console.Read();
        }
    }
}
