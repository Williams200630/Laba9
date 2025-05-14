using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace EmployeeTable
{
    class Program
    {
        enum Post
        {
            P, // Преподаватель
            S, // Студент
            A  // Аспирант
        }

        struct Employee
        {
            public string Surname;
            public Post PostType;
            public int Year;
            public double Salary;
        }

        enum LogOperationType
        {
            ADD,
            DELETE,
            UPDATE
        }

        struct LogEntry
        {
            public DateTime Timestamp;
            public LogOperationType OperationType;
            public Employee EmployeeData;
        }

        static LogEntry[] log = new LogEntry[50]; // Массив для лога
        static int logCount = 0;                // Счетчик записей в логе
        static DateTime lastActionTime = DateTime.Now; // Время последнего действия
        static TimeSpan longestInactivity = TimeSpan.Zero; // Самый долгий простой
        static Stopwatch stopwatch = new Stopwatch(); // Таймер для измерения времени
        static int capacity = 10; // начальная емкость массива
        static Employee[] employees = new Employee[capacity];
        static int count = 0; // Текущее количество элементов в массиве

        static void Main(string[] args)
        {
            stopwatch.Start(); // Запускаем секундомер

            int choice;

            do
            {
                // Вычисляем время простоя
                TimeSpan inactivity = DateTime.Now - lastActionTime;
                if (inactivity > longestInactivity)
                {
                    longestInactivity = inactivity;
                }

                Console.WriteLine("\nВыберите действие:");
                Console.WriteLine("1 – Просмотр таблицы (Автоматически)");
                Console.WriteLine("2 – Добавить запись");
                Console.WriteLine("3 – Удалить запись");
                Console.WriteLine("4 – Обновить запись");
                Console.WriteLine("5 – Поиск записей");
                Console.WriteLine("6 – Просмотреть лог");
                Console.WriteLine("7 – Выход");
                Console.Write("Ваш выбор: ");

                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 7)
                {
                    Console.WriteLine("Некорректный выбор. Пожалуйста, выберите число от 1 до 7.");
                    Console.Write("Ваш выбор: ");
                }

                lastActionTime = DateTime.Now; // Обновляем время последнего действия

                switch (choice)
                {
                    case 1:
                        // Заполняем данными, если таблица пуста
                        if (count == 0)
                        {
                            FillWithDefaultData();
                        }
                        PrintEmployeeTable(employees, count); //  Вывод таблицы при выборе пункта 1
                        break;
                    case 2:
                        AddEmployee(); // Добавление сотрудника
                        break;
                    case 3:
                        DeleteEmployee(ref employees, ref count);
                        break;
                    case 4:
                        UpdateEmployee(employees, count);
                        break;
                    case 5:
                        SearchEmployee(employees, count);
                        break;
                    case 6:
                        PrintLog();
                        break;
                    case 7:
                        Console.WriteLine("Выход из программы.");
                        break;
                }
            } while (choice != 7);

            stopwatch.Stop(); // Останавливаем секундомер
        }

        static void FillWithDefaultData()
        {
            string[] Surnames = new string[] { "Иванов И.И", "Петренко П.П", "Сидоревич М.С" };
            string[] PostTypes = new string[] { "P", "S", "A" }; // Соответствующие P, S, A
            int[] Years = new int[] { 1975, 1996, 1990 };
            double[] Salaries = new double[] { 4170.50, 790.10, 2200.00 };

            int numDefaultEmployees = Math.Min(Surnames.Length, Math.Min(PostTypes.Length, Math.Min(Years.Length, Salaries.Length)));

            for (int i = 0; i < numDefaultEmployees; i++)
            {
                if (count == employees.Length) // Проверяем, нужно ли увеличивать размер массива
                {
                    Array.Resize(ref employees, employees.Length * 2);
                }

                employees[count].Surname = Surnames[i];
                switch (PostTypes[i])
                {
                    case "P":
                        employees[count].PostType = Post.P;
                        break;
                    case "S":
                        employees[count].PostType = Post.S;
                        break;
                    case "A":
                        employees[count].PostType = Post.A;
                        break;
                    default:
                        employees[count].PostType = Post.P; // Значение по умолчанию, если что-то не так
                        break;
                }
                employees[count].Year = Years[i];
                employees[count].Salary = Salaries[i];

                count++;
            }
        }
        static void AddEmployee()
        {
            if (count < employees.Length)
            {
                Employee newEmployee = InputEmployeeData();
                employees[count] = newEmployee;
                count++;
                AddToLog(LogOperationType.ADD, newEmployee); // Логируем добавление
            }
            else
            {
                Array.Resize(ref employees, employees.Length * 2);
                Employee newEmployee = InputEmployeeData();
                employees[count] = newEmployee;
                count++;
                AddToLog(LogOperationType.ADD, newEmployee); // Логируем добавление
            }
        }

        static Employee InputEmployeeData()
        {
            Employee employee = new Employee();

            Console.Write("Фамилия: ");
            employee.Surname = Console.ReadLine();

            Console.Write("Должность (P - преподаватель, S - студент, A - аспирант): ");
            string postInput;
            do
            {
                postInput = Console.ReadLine()?.ToUpper(); // Added null check
                if (postInput == "P") employee.PostType = Post.P;
                else if (postInput == "S") employee.PostType = Post.S;
                else if (postInput == "A") employee.PostType = Post.A;
                else Console.WriteLine("Некорректная должность. Попробуйте еще раз:");
            } while (postInput != "P" && postInput != "S" && postInput != "A");

            Console.Write("Год рождения: ");
            while (!int.TryParse(Console.ReadLine(), out employee.Year) || employee.Year < 1900 || employee.Year > DateTime.Now.Year)
            {
                Console.WriteLine("Некорректный год. Введите целое число между 1900 и текущим годом:");
                Console.Write("Год рождения: "); // Prompt again
            }

            Console.Write("Оклад: ");
            while (!double.TryParse(Console.ReadLine(), out employee.Salary) || employee.Salary < 0)
            {
                Console.WriteLine("Некорректный оклад. Введите неотрицательное число:");
                Console.Write("Оклад: "); // Prompt again
            }

            return employee;
        }

        static void PrintEmployeeTable(Employee[] employees, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Таблица пуста.");
                return;
            }

            Console.WriteLine("\nОтдел кадров");
            Console.WriteLine("");
            Console.WriteLine("{0,-3} {1,-20} {2,-10} {3,-10} {4,-12}", "№", "Фамилия", "Должность", "Год рожд.", "Оклад (грн)");
            Console.WriteLine("");

            for (int i = 0; i < count; i++)
            {
                string postString = employees[i].PostType switch
                {
                    Post.P => "П",
                    Post.S => "С",
                    Post.A => "A",
                    _ => "Неизвестно"
                };

                Console.WriteLine("{0,-3} {1,-20} {2,-10} {3,-10} {4,-12:F2}", i + 1, employees[i].Surname, postString, employees[i].Year, employees[i].Salary);
            }

            Console.WriteLine("");
            Console.WriteLine("\nПеречисляемый тип: П - преподаватель, С - студент, А - аспирант");
        }

        static void DeleteEmployee(ref Employee[] employees, ref int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Таблица пуста, удалять нечего.");
                return;
            }

            Console.Write("Введите номер записи для удаления (от 1 до {0}): ", count);
            int indexToDelete;

            while (!int.TryParse(Console.ReadLine(), out indexToDelete) || indexToDelete < 1 || indexToDelete > count)
            {
                Console.WriteLine("Некорректный номер записи. Попробуйте еще раз.");
                Console.Write("Введите номер записи для удаления (от 1 до {0}): ", count);
            }

            indexToDelete--; // Convert to 0-based index

            // Сдвигаем элементы массива влево
            Employee deletedEmployee = employees[indexToDelete];  // Сохраняем данные удаляемого сотрудника
            for (int i = indexToDelete; i < count - 1; i++)
            {
                employees[i] = employees[i + 1];
            }
            count--;
            AddToLog(LogOperationType.DELETE, deletedEmployee); // Логируем удаление
            Console.WriteLine("Запись удалена.");
        }

        static void UpdateEmployee(Employee[] employees, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Таблица пуста, обновлять нечего.");
                return;
            }

            Console.Write("Введите номер записи для обновления (от 1 до {0}): ", count);
            int indexToUpdate;

            while (!int.TryParse(Console.ReadLine(), out indexToUpdate) || indexToUpdate < 1 || indexToUpdate > count)
            {
                Console.WriteLine("Некорректный номер записи. Попробуйте еще раз.");
                Console.Write("Введите номер записи для обновления (от 1 до {0}): ", count);
            }

            indexToUpdate--; // Convert to 0-based index
            Console.WriteLine($"Обновление данных для сотрудника {indexToUpdate + 1}:");
            Employee oldEmployee = employees[indexToUpdate];  // Сохраняем старые данные
            employees[indexToUpdate] = InputEmployeeData(); // Перезаписываем данные
            AddToLog(LogOperationType.UPDATE, employees[indexToUpdate]); // Логируем обновление
            Console.WriteLine("Запись обновлена.");
        }

        static void SearchEmployee(Employee[] employees, int count)
        {
            if (count == 0)
            {
                Console.WriteLine("Таблица пуста, искать нечего.");
                return;
            }

            Console.Write("Введите год рождения для поиска: ");
            int searchYear;
            while (!int.TryParse(Console.ReadLine(), out searchYear) || searchYear < 1900 || searchYear > DateTime.Now.Year)
            {
                Console.WriteLine("Некорректный год. Введите целое число между 1900 и текущим годом:");
                Console.Write("Введите год рождения для поиска: ");
            }

            Console.WriteLine("\nРезультаты поиска:");
            Console.WriteLine("");
            Console.WriteLine("{0,-3} {1,-20} {2,-10} {3,-10} {4,-12}", "№", "Фамилия", "Должность", "Год рожд.", "Оклад (грн)");
            Console.WriteLine("");

            bool found = false;
            for (int i = 0; i < count; i++)
            {
                if (employees[i].Year == searchYear)
                {
                    string postString = employees[i].PostType switch
                    {
                        Post.P => "П",
                        Post.S => "С",
                        Post.A => "A",
                        _ => "Неизвестно"
                    };

                    Console.WriteLine("{0,-3} {1,-20} {2,-10} {3,-10} {4,-12:F2}", i + 1, employees[i].Surname, postString, employees[i].Year, employees[i].Salary);
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("Записи не найдены.");
            }
            Console.WriteLine("");
        }

        static void PrintLog()
        {
            Console.WriteLine("\nЛог операций:");
            if (logCount == 0)
            {
                Console.WriteLine("Лог пуст.");
            }
            else
            {
                for (int i = 0; i < logCount; i++)
                {
                    Console.WriteLine($"{log[i].Timestamp:HH:mm:ss} – {GetLogMessage(log[i])}");
                }
            }

            Console.WriteLine();
            Console.WriteLine($"{longestInactivity:mm\\:ss} – Самый долгий период бездействия пользователя"); // Вывод времени простоя
        }

        static void AddToLog(LogOperationType operationType, Employee employeeData)
        {
            if (logCount < log.Length)
            {
                log[logCount].Timestamp = DateTime.Now;
                log[logCount].OperationType = operationType;
                log[logCount].EmployeeData = employeeData;
                logCount++;
            }
            else
            {
                // Лог переполнен, можно реализовать циклическую перезапись (FIFO)
                // В данном примере просто не добавляем новую запись
                Console.WriteLine("Лог переполнен, запись не добавлена.");
            }
        }

        static string GetLogMessage(LogEntry logEntry)
        {
            switch (logEntry.OperationType)
            {
                case LogOperationType.ADD:
                    return $"Добавлена запись “{logEntry.EmployeeData.Surname}”";
                case LogOperationType.DELETE:
                    return $"Удалена запись “{logEntry.EmployeeData.Surname}”";
                case LogOperationType.UPDATE:
                    return $"Обновлена запись “{logEntry.EmployeeData.Surname}”";
                default:
                    return "Неизвестная операция";
            }
        }
    }
}