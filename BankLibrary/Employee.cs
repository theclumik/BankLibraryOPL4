using System;

namespace BankLibrary
{
    /// <summary>
    /// Абстрактный класс Employee (Сотрудник) — наследник класса Client.
    /// Представляет общую функциональность для сотрудников банка или организации.
    /// ИЗМЕНЕНО В ЛР4: Класс сделан абстрактным, добавлены виртуальные и абстрактные методы.
    /// </summary>
    public abstract class Employee : Client
    {
        /// <summary>
        /// Табельный номер сотрудника.
        /// </summary>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Дата приёма на работу.
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// Должность сотрудника.
        /// </summary>
        public string Position { get; set; }

        // КОНСТРУКТОРЫ

        /// <summary>
        /// Конструктор по умолчанию.
        /// Вызывает конструктор базового класса Client.
        /// </summary>
        public Employee() : base()
        {
            EmployeeId = 0;
            HireDate = DateTime.Now;
            Position = "";
        }

        /// <summary>
        /// Конструктор с параметрами: фамилия, имя, табельный номер.
        /// Использует конструктор базового класса Client(lastName, firstName).
        /// </summary>
        public Employee(string lastName, string firstName, int employeeId)
            : base(lastName, firstName)
        {
            this.EmployeeId = employeeId;
            this.HireDate = DateTime.Now;
            this.Position = "";
        }

        /// <summary>
        /// Конструктор с полными данными.
        /// Использует конструктор базового класса с максимальным набором параметров.
        /// </summary>
        public Employee(int id, string lastName, string firstName, string middleName,
                       string passportNumber, string phoneNumber, decimal balance, bool isVip,
                       int employeeId, DateTime hireDate, string position)
            : base(id, lastName, firstName, middleName, passportNumber, phoneNumber, balance, isVip)
        {
            this.EmployeeId = employeeId;
            this.HireDate = hireDate;
            this.Position = position;
        }

        // МЕТОДЫ КЛАССА

        /// <summary>
        /// Вычисление стажа работы в годах.
        /// </summary>
        public int GetExperience()
        {
            DateTime today = DateTime.Today;
            int years = today.Year - HireDate.Year;

            // Если дата приёма на работу ещё не наступила в этом году, вычитаем 1
            if (HireDate.Date > today.AddYears(-years))
            {
                years--;
            }

            return years;
        }

        /// <summary>
        /// Метод для демонстрации передачи параметров по значению и по ссылке.
        /// Вычисляет среднюю зарплату за несколько месяцев.
        /// </summary>
        /// <param name="colMonth">Количество месяцев (передаётся по значению)</param>
        /// <param name="salaries">Массив зарплат (ссылочный тип)</param>
        /// <returns>Средняя зарплата</returns>
        public decimal AverageSalary(int colMonth, decimal[] salaries)
        {
            // Изменяем значение параметра colMonth
            colMonth = 999; // это изменение не сохранится после выхода из метода

            // Вычисляем среднюю зарплату
            decimal sum = 0;
            foreach (decimal salary in salaries)
            {
                sum += salary;
            }
            decimal average = salaries.Length > 0 ? sum / salaries.Length : 0;

            // Изменяем массив - это изменение СОХРАНИТСЯ (ссылочный тип)
            for (int i = 0; i < salaries.Length; i++)
            {
                salaries[i] = salaries[i] * 1.1m; // увеличиваем каждую зарплату на 10%
            }

            return average;
        }

        /// <summary>
        /// Метод для демонстрации передачи параметра по ссылке (ref).
        /// Вычисляет среднюю зарплату и изменяет количество месяцев.
        /// </summary>
        /// <param name="colMonth">Количество месяцев (передаётся по ссылке)</param>
        /// <param name="salaries">Массив зарплат</param>
        /// <returns>Средняя зарплата</returns>
        public decimal AverageSalary(ref int colMonth, decimal[] salaries)
        {
            // Изменяем значение параметра colMonth
            colMonth = 999; // это изменение СОХРАНИТСЯ (передача по ссылке)

            // Вычисляем среднюю зарплату
            decimal sum = 0;
            foreach (decimal salary in salaries)
            {
                sum += salary;
            }
            decimal average = salaries.Length > 0 ? sum / salaries.Length : 0;

            return average;
        }

        /// <summary>
        /// Метод для демонстрации выходных параметров (out).
        /// Вычисляет среднюю, минимальную и максимальную зарплату.
        /// </summary>
        /// <param name="salaries">Массив зарплат</param>
        /// <param name="minSalary">Минимальная зарплата (out)</param>
        /// <param name="maxSalary">Максимальная зарплата (out)</param>
        /// <returns>Средняя зарплата</returns>
        public decimal AverageSalary(decimal[] salaries, out decimal minSalary, out decimal maxSalary)
        {
            // Инициализируем выходные параметры
            minSalary = 0;
            maxSalary = 0;

            if (salaries.Length == 0)
            {
                return 0;
            }

            // Находим минимум, максимум и сумму
            decimal sum = 0;
            minSalary = salaries[0];
            maxSalary = salaries[0];

            foreach (decimal salary in salaries)
            {
                sum += salary;
                if (salary < minSalary) minSalary = salary;
                if (salary > maxSalary) maxSalary = salary;
            }

            return sum / salaries.Length;
        }

        /// <summary>
        /// Переопределение метода ToString().
        /// Добавляет информацию о табельном номере и должности.
        /// </summary>
        public override string ToString()
        {
            return $"{GetFullName()}, Таб.№{EmployeeId}, {Position}";
        }

        /// <summary>
        /// Получение полной информации о сотруднике.
        /// Расширяет метод базового класса.
        /// </summary>
        public string GetEmployeeInfo()
        {
            string info = GetClientInfo(); // вызываем метод базового класса
            info += $"\n\n--- Данные сотрудника ---";
            info += $"\nТабельный номер: {EmployeeId}";
            info += $"\nДолжность: {Position}";
            info += $"\nДата приёма на работу: {HireDate:dd.MM.yyyy}";
            info += $"\nСтаж работы: {GetExperience()} лет";
            return info;
        }

        // ==================== НОВАЯ ФУНКЦИОНАЛЬНОСТЬ ДЛЯ ЛР4 ====================

        /// <summary>
        /// АБСТРАКТНЫЙ МЕТОД: Получение текста с именем сотрудника.
        /// Каждый класс-наследник должен предоставить свою реализацию.
        /// Используется для демонстрации полиморфизма.
        /// </summary>
        public abstract string NameText();

        /// <summary>
        /// АБСТРАКТНОЕ СВОЙСТВО: Краткое описание должности/статуса.
        /// Каждый класс-наследник должен предоставить свою реализацию.
        /// </summary>
        public abstract string RoleDescription { get; }

        /// <summary>
        /// ВИРТУАЛЬНЫЙ МЕТОД: Отображение информации о сотруднике.
        /// Может быть переопределён в классах-наследниках.
        /// </summary>
        public virtual string ShowInfo()
        {
            return $"Сотрудник: {GetFullName()}\nТабельный номер: {EmployeeId}\nДолжность: {Position}";
        }

        /// <summary>
        /// ВИРТУАЛЬНОЕ СВОЙСТВО: Дата рождения с возможностью переопределения.
        /// Позволяет наследникам добавить проверку возраста.
        /// </summary>
        public virtual DateTime DateOfBirth
        {
            get { return BirthDate; }
            set { BirthDate = value; }
        }

        // ПЕРЕГРУЖЕННЫЕ МЕТОДЫ РАБОТЫ С ФАЙЛАМИ

        /// <summary>
        /// Запись данных сотрудника в текстовый файл.
        /// Версия 1: принимает путь к файлу в виде строки.
        /// Перегруженный метод базового класса - сохраняет дополнительные данные сотрудника.
        /// </summary>
        public new void WriteToFile(string path)
        {
            try
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path))
                {
                    // Записываем базовые данные клиента
                    writer.WriteLine($"ID:{Id}");
                    writer.WriteLine($"Фамилия:{LastName}");
                    writer.WriteLine($"Имя:{FirstName}");
                    writer.WriteLine($"Возраст:{GetAge()}");
                    writer.WriteLine($"Баланс:{Balance}");

                    // Добавляем данные сотрудника
                    writer.WriteLine($"ТабельныйНомер:{EmployeeId}");
                    writer.WriteLine($"Должность:{Position}");
                    writer.WriteLine($"ДатаПриема:{HireDate:dd.MM.yyyy}");
                    writer.WriteLine($"Стаж:{GetExperience()}");
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Ошибка записи в файл: {ex.Message}");
            }
        }

        /// <summary>
        /// Запись данных сотрудника в текстовый файл.
        /// Версия 2 (ПЕРЕГРУЖЕННЫЙ МЕТОД): принимает объект SaveFileDialog.
        /// </summary>
        public new void WriteToFile(System.Windows.Forms.SaveFileDialog dialog)
        {
            dialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            dialog.DefaultExt = "txt";
            dialog.FileName = $"Employee_{EmployeeId}_{LastName}";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                WriteToFile(dialog.FileName);
            }
        }

        /// <summary>
        /// Чтение данных сотрудника из текстового файла.
        /// Версия 1: принимает путь к файлу в виде строки.
        /// </summary>
        public new void ReadFromFile(string path)
        {
            try
            {
                using (System.IO.StreamReader reader = new System.IO.StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(':');
                        if (parts.Length == 2)
                        {
                            string key = parts[0].Trim();
                            string value = parts[1].Trim();

                            switch (key)
                            {
                                case "ID":
                                    Id = int.Parse(value);
                                    break;
                                case "Фамилия":
                                    LastName = value;
                                    break;
                                case "Имя":
                                    FirstName = value;
                                    break;
                                case "Баланс":
                                    Balance = decimal.Parse(value);
                                    break;
                                case "ТабельныйНомер":
                                    EmployeeId = int.Parse(value);
                                    break;
                                case "Должность":
                                    Position = value;
                                    break;
                                case "ДатаПриема":
                                    HireDate = DateTime.ParseExact(value, "dd.MM.yyyy", null);
                                    break;
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Ошибка чтения из файла: {ex.Message}");
            }
        }

        /// <summary>
        /// Чтение данных сотрудника из текстового файла.
        /// Версия 2 (ПЕРЕГРУЖЕННЫЙ МЕТОД): принимает объект OpenFileDialog.
        /// </summary>
        public new void ReadFromFile(System.Windows.Forms.OpenFileDialog dialog)
        {
            dialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ReadFromFile(dialog.FileName);
            }
        }
    }
}
