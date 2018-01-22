using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Training_Practice__8 // 8 - Граф задан матрицей инциденций. Выяснить, является ли он деревом.
{
    class Program
    {
        static int PunktMenu() // Функция выбора пункта меню 
        {
            Console.Clear();
            // Описание переменных и массивов для программы вывода меню

            int currentIndex = 0, previousIndex = 0, i;
            int positionX = 5, positionY = 2;
            bool itemSelected = false;


            // Программа вывода меню 

            string[] items = { "1. Использовать готовые тесты", "2. Рандомное заполнение матрицы инциденций", "3. Ручное заполнение матрицы инциденций", "4. Выход" };

            //Начальный вывод пунктов меню.
            for (i = 0; i < items.Length; i++)
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
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Gray; Console.BackgroundColor = ConsoleColor.Black;
            return currentIndex;
        }

        public static void PrintMassiv(int[,] a) // Функция вывода двумерного матрицы 
        {
            Console.WriteLine("Матрица инциденций графа: ");
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

        public static int[,] InputMatrixRandom() // Функция рандомного ввода двумерной матрицы 
        {
            Console.CursorTop += 3;
            bool ok;
            int kstrok, kstolb; // переменные

            do
            {
                Console.WriteLine("Введите количество вершин: ");
                ok = int.TryParse(Console.ReadLine(), out kstrok);
                if (!ok || kstrok < 2) { Console.WriteLine("Некорректный ввод. Пожалуйста, введите натуральное число, не менее 2"); ok = false; }
            } while (!ok);

            ok = false;

            do
            {
                Console.WriteLine("Введите количество ребер: ");
                ok = int.TryParse(Console.ReadLine(), out kstolb);
                if (!ok || kstolb <= 0 || kstolb > (kstrok * (kstrok - 1) / 2)) { Console.WriteLine("Некорректный ввод. Пожалуйста, введите натуральное число не превышающее {0}", kstrok * (kstrok - 1) / 2); ok = false; }
            } while (!ok);

            Random S = new Random();

            int[,] a = new int[kstrok, kstolb];
            for (int i = 0; i < a.GetLength(1); i++)
            {
                Thread.Sleep(10);
                a = RandomEdge(a, i, S.Next(0, a.GetLength(0)));
                bool TrVa = CheckEdge(a, i);
                Thread.Sleep(10);
                while (TrVa) { a = RandomEdge(a, i, S.Next(0, a.GetLength(0))); TrVa = CheckEdge(a, i); }
            }
            return a;
        }

        public static int[,] InputMatrixManual() // Функция ручного ввода двумерной матрицы 
        {
            Console.CursorTop += 3;
            bool ok;
            int kstrok, kstolb; // переменные

            do
            {
                Console.WriteLine("Введите количество вершин: ");
                ok = int.TryParse(Console.ReadLine(), out kstrok);
                if (!ok || kstrok < 2) { Console.WriteLine("Некорректный ввод. Пожалуйста, введите натуральное число, не менее 2"); ok = false; }
            } while (!ok);

            ok = false;

            do
            {
                Console.WriteLine("Введите количество ребер: ");
                ok = int.TryParse(Console.ReadLine(), out kstolb);
                if (!ok || kstolb <= 0 || kstolb > (kstrok * (kstrok - 1) / 2)) { Console.WriteLine("Некорректный ввод. Пожалуйста, введите натуральное число не превышающее {0}", kstrok * (kstrok - 1) / 2); ok = false; }
            } while (!ok);

            int[,] a = new int[kstrok, kstolb];
            for (int m = 0; m < kstolb; m++)
            {
                ok = false;
                do
                {
                    int CountV = 0;
                    for (int n = 0; n < kstrok; n++)
                    {
                        ok = false;
                        Console.WriteLine("Введите элементы матрицы для {0} ребра: ", m + 1);
                        do
                        {
                            ok = int.TryParse(Console.ReadLine(), out a[n, m]);
                            if (!ok || a[n, m] < 0 || a[n, m] >= 2) { Console.WriteLine("Некорректный ввод. Пожалуйста, введите 1 или 0"); ok = false; }
                        } while (!ok);
                        if (a[n, m] == 1) { CountV++; }
                        if (CountV == 2) { break; }
                    }
                    if (CountV < 2) { Console.WriteLine("Некорректный ввод. Каждое ребро должно соединять 2 вершины"); ok = false; }
                    else ok = true;
                    if (CheckEdge(a, m)) { Console.WriteLine("Такое ребро уже существует, исправьте ввод"); ok = false; }
                }
                while (!ok);
            }
            return a;
        }

        public static int[,] CreateTestingGrafs(bool Tree) // Функция ручного ввода двумерной матрицы 
        {
            Console.CursorTop += 4;
            int[,] A = new int[0, 0];

            if (Tree)
            {
                Console.WriteLine("Доподлино известно что данный граф является деревом: ");
                A = new int[6, 5] { { 1, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0 }, { 1, 1, 1, 0, 0 }, { 0, 0, 0, 1, 0 }, { 0, 0, 1, 1, 1 }, { 0, 0, 0, 0, 1 } }; ;
            }
            else
            {
                Console.WriteLine("Доподлино известно что данный граф НЕ является деревом: ");
                A = new int[7, 6] { { 1, 0, 0, 0, 0, 0 }, { 0, 1, 0, 0, 0, 0 }, { 1, 1, 1, 0, 0, 0 }, { 0, 0, 0, 1, 0, 1 }, { 0, 0, 1, 1, 1, 0 }, { 0, 0, 0, 0, 1, 1 }, { 0, 0, 0, 0, 0, 0 } }; ;
            }
            return A;
        }

        static bool CheckEdge(int[,] A, int Stolbec) // Проверка ребра на существование 
        {
            int Point1 = 0, Point2 = 0, Checking = 0;
            bool Number = false;
            for (int i = 0; i < A.GetLength(0); i++)
            {
                if (A[i, Stolbec] == 1 && Number) { Point2 = i; break; }
                if (A[i, Stolbec] == 1 && !Number) { Point1 = i; Number = true; }
            }
            for (int i = 0; i < Stolbec; i++)
            {
                for (int j = 0; j < A.GetLength(0); j++)
                {
                    if (A[j, i] == A[Point1, Stolbec] && j == Point1) { Checking++; }
                    if (A[j, i] == A[Point2, Stolbec] && j == Point2) { Checking++; }
                }
            }
            if (Checking == 2) { return true; }
            else return false;
        }

        static int[,] RandomEdge(int[,] A, int i, int S1) // Функция создания рандомного ребра 
        {
            Random S2 = new Random(S1);
            Thread.Sleep(10);
            A[S1, i] = 1;
            bool Check = true;
            while (Check)
            {
                int NU = S2.Next(0, A.GetLength(1));
                if (A[NU, i] != 1) { A[NU, i] = 1; Check = false; }
            }
            return A;
        }

        static void IsTree(int[,] A) // Является ли граф деревом, алгоритм с помощью раскрашивания
        {
            if (A.GetLength(1) - A.GetLength(0) != 1)
            {
                bool[] IsColored = new bool[A.GetLength(0)];
                IsColored[0] = true;
                for (int j = 0; j < A.GetLength(0); j++)
                {
                    for (int i = 0; i < A.GetLength(1); i++)
                    {
                        if (A[j, i] == 1)
                        {
                            for (int Z = 0; Z < A.GetLength(0); Z++)
                            {
                                if (A[Z, i] == 1) { IsColored[Z] = true; }
                            }
                        }
                    }
                }
                for(int i = 0; i < IsColored.Length; i++)
                {
                    if(IsColored[i] ==false) { Console.WriteLine("Граф НЕ является деревом"); return; }                   
                }
                Console.WriteLine("Граф является деревом");
            }
            else
            {
                Console.WriteLine("Граф НЕ является деревом так как каждое дерево с вершинами N имеет в точности N-1 ребро.");
            }
        }

        static void Main(string[] args)  // основной листнинг программы 
        {
            Console.WriteLine("Учебная практика №8, Власов Виктор");
            Console.WriteLine("Граф задан матрицей инциденций. Выяснить, является ли он деревом.");

            int b = 0;
            int[,] A;
            while (1 > b)
            {
                switch (PunktMenu())
                {
                    case 0:
                        A = CreateTestingGrafs(true); // Готовый граф который доподленно является деревом
                        PrintMassiv(A);
                        Console.WriteLine("Ответ программы: ");
                        IsTree(A);
                        A = CreateTestingGrafs(false); // Готовый граф который доподленно НЕ является деревом
                        PrintMassiv(A);
                        Console.WriteLine("Ответ программы: ");
                        IsTree(A);
                        Console.ReadLine();
                        break;

                    case 1:
                        A = InputMatrixRandom();
                        PrintMassiv(A);
                        IsTree(A);
                        Console.ReadLine();
                        break;

                    case 2:
                        A = InputMatrixManual();
                        PrintMassiv(A);
                        IsTree(A);
                        Console.ReadLine();
                        break;

                    case 3:
                        b = 2;
                        break;
                }
            }
        }
    }
}