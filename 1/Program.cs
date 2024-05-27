using System;

namespace InterfaceTask
{
    interface Ix
    {
        void IxF0(int w);
        void IxF1();
    }

    interface Iy
    {
        void F0(int w);
        void F1();
    }

    interface Iz
    {
        void F0(int w);
        void F1();
    }

    class TestClass : Ix, Iy, Iz
    {
        public int W { get; private set; }

        public TestClass(int w)
        {
            W = w;
        }

        // Реализация интерфейса Ix
        public void IxF0(int w)
        {
            W = w;
            Console.WriteLine($"Ix IxF0: {W + 5}");
        }

        public void IxF1()
        {
            Console.WriteLine($"Ix IxF1: {W + 5}");
        }

        // Неявная неоднозначная реализация Iy и Iz
        public void F0(int w)
        {
            W = w;
            Console.WriteLine($"Iy/Iz F0: {Math.Pow(W, 3)}");
        }

        public void F1()
        {
            Console.WriteLine($"Iy/Iz F1: {Math.Pow(W, 3)}");
        }

        // Явная реализация интерфейса Iz
        void Iz.F0(int w)
        {
            W = w;
            Console.WriteLine($"Iz F0: {7 * W - 2}");
        }

        void Iz.F1()
        {
            Console.WriteLine($"Iz F1: {7 * W - 2}");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TestClass test = new TestClass(3);

            // Работаем через интерфейс Ix
            test.IxF0(3);
            test.IxF1();

            // Работаем через интерфейс Iy (неявная неоднозначная реализация)
            test.F0(3);
            test.F1();

            // Вызов функций с явным приведением к типу интерфейса Iz
            ((Iz)test).F0(3);
            ((Iz)test).F1();

            // Вызов метода для объекта посредством интерфейсной ссылки
            Ix ix = test; // Интерфейсная ссылка на Ix
            Iy iy = test; // Интерфейсная ссылка на Iy
            Iz iz = test; // Интерфейсная ссылка на Iz

            // Вызов методов интерфейса Ix
            ix.IxF0(3);
            ix.IxF1();

            // Вызов методов интерфейса Iy
            iy.F0(3);
            iy.F1();

            // Вызов методов интерфейса Iz через интерфейсную ссылку
            iz.F0(3); // Будет вызывать метод Iz.F0 за счет явной реализации
            iz.F1();  // Будет вызывать метод Iz.F1 за счет явной реализации
        }
    }
}