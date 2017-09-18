using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training_Practice__8
{
    class Program
    {
        public static void PrintMassiv(int[,] a) // Функция вывода двумерного массива 
        {
            Console.CursorTop = Console.CursorTop + 1;
            Console.WriteLine("Текущая матрица: ");
            int i, j;
            for (i = 0; i < a.GetLength(0); i++)
            {
                for (j = 0; j < a.GetLength(1); j++)
                {
                    Console.Write(a[i, j] + "  ");
                }
                Console.WriteLine();
            }

        }

        public static int[,] VvodMassiva2() // Функция ввода двумерного массива 
        {
            bool ok;
            int kstrok, kstolb; // переменные
            int positionY = Console.CursorTop + 1;
            Console.CursorTop = positionY;

            do
            {
                Console.WriteLine("Введите количество вершин: ");
                ok = int.TryParse(Console.ReadLine(), out kstrok);
                if (!ok || kstrok <= 0) { Console.WriteLine("Некорректный ввод. Пожалуйста, введите натуральное число"); ok = false; }
            } while (!ok);

            ok = false;

            do
            {
                Console.WriteLine("Введите количество ребер: ");
                ok = int.TryParse(Console.ReadLine(), out kstolb);
                if (!ok || kstolb <= 0 || kstolb > (kstrok*(kstrok -1)/2)) { Console.WriteLine("Некорректный ввод. Пожалуйста, введите натуральное число не превышающее {0}", kstrok * (kstrok - 1) / 2); ok = false; }
            } while (!ok);

            string[] items = { "a) Заполнить матрицу рандомно", "б) Заполнить матрицу вручную" };

            int[,] a = new int[kstrok, kstolb]; // объявим массив 

            int currentIndex = 0, previousIndex = 0;
            int positionX = 0; positionY = Console.CursorTop + 1;
            bool itemSelected = false;

            //Начальный вывод пунктов меню.
            for (int i = 0; i < items.Length; i++)
            {
                Console.CursorLeft = positionX;
                Console.CursorTop = positionY + i;
                Console.ForegroundColor = ConsoleColor.Gray; Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(items[i]);
            }

            do
            {
                // Вывод предыдущего активного пункта основным цветом.
                Console.CursorLeft = positionX;
                Console.CursorTop = positionY + previousIndex;
                Console.ForegroundColor = ConsoleColor.Gray; Console.BackgroundColor = ConsoleColor.Black;
                Console.Write(items[previousIndex]);


                //Вывод активного пункта.
                Console.CursorLeft = positionX;
                Console.CursorTop = positionY + currentIndex;
                Console.ForegroundColor = ConsoleColor.Black; Console.BackgroundColor = ConsoleColor.Gray;
                Console.Write(items[currentIndex]);

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);

                previousIndex = currentIndex;
                switch (keyInfo.Key)
                {
                    case ConsoleKey.DownArrow:
                        currentIndex++;
                        break;
                    case ConsoleKey.UpArrow:
                        currentIndex--;
                        break;
                    case ConsoleKey.Enter:
                        itemSelected = true;
                        break;
                }

                if (currentIndex == items.Length)
                    currentIndex = items.Length - 1;
                else if (currentIndex < 0)
                    currentIndex = 0;
            }
            while (!itemSelected);

            Console.ForegroundColor = ConsoleColor.Gray; Console.BackgroundColor = ConsoleColor.Black;
            Console.WriteLine();

            switch (currentIndex)
            {
                case 0:
                    int m, n; // переменные
                    Random S = new Random();
                    // заполнение массива случайными элементами
                    for (m = 0; m < kstolb; m++)
                    {
                        int CountV = 0;
                        for (n = 0; n < kstrok; n++)
                        {
                            a[n, m] = S.Next(0, 2); if (a[n, m] == 1) { CountV++; }
                                if (CountV == 2) { break; }
                        }
                        if(CountV < 2)
                        {
                            for (n = 0; n < kstrok; n++)
                            {
                                if (a[n, m] == 0)
                                {
                                    a[n, m] = 1;
                                    break;
                                }

                            }
                        }
                    }
                    break;

                case 1:
                    for (m = 0; m < kstolb; m++)
                    {
                        int CountV = 0;
                        for (n = 0; n < kstrok; n++)
                        {

                            ok = false;
                            Console.WriteLine("Введите элементы матрицы для {0} ребра: ", m + 1);
                            do
                                {
                                    ok = int.TryParse(Console.ReadLine(), out a[n,m]);
                                    if (!ok || a[n,m] < 0 || a[n,m] >=2) { Console.WriteLine("Некорректный ввод. Пожалуйста, введите 1 или 0"); ok = false; }
                                } while (!ok);
                            if (a[n, m] == 1) { CountV++; }
                            if (CountV == 2) { break; }
                        }
                        if(CountV < 2) { Console.WriteLine("Некорректный ввод. Каждое ребро должно соединять 2 вершины");}
                    }
                    break;
            }
            return a;
        }

        public static int[,] Delete(int[,] a) // Удаление столбцов в двумерном массиве 
        {
            int strok = a.GetLength(0), stobcov = a.GetLength(1), i, j, stolbec = a.GetLength(1), m, x = 0, y = 0;
            bool estnull = false;

            for (i = 0; i < stobcov; i++)
            {
                estnull = false;
                for (j = 0; j < strok; j++)
                {
                    if (a[j, i] == 0)
                    {
                        estnull = true;
                        for (m = 0; m < strok; m++)
                        {
                            a[m, i] = 0;
                        }
                        stolbec--;
                    }
                    if (estnull) break;
                }
            }

            int[,] b = new int[strok, stolbec];
            for (m = 0; m < stolbec; m++)
            {
                x = 0;
                for (i = 0; i < strok; i++)
                {
                    while (a[x, y] == 0)
                    {
                        y++;
                    }
                    b[i, m] = a[x, y];
                    x++;
                }
                y++;
            }
            return b;
        }

        static void Main(string[] args)  // основной листнинг программы 
        {
            while (1 > 0)
            {
                int[,] Dvumer = new int[1, 1];

                Dvumer = VvodMassiva2();
                PrintMassiv(Dvumer);
                Console.ReadLine();
            }
        }
    }
}