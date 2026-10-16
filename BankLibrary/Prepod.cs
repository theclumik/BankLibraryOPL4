using System;
using System.Collections.Generic;

namespace BankLibrary
{
    /// <summary>
    /// Класс Prepod (Преподаватель) — наследник класса Employee.
    /// Представляет преподавателя учебного заведения.
    /// </summary>
    public class Prepod : Employee
    {
        /// <summary>
        /// Название кафедры, на которой работает преподаватель.
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        /// Список дисциплин, которые ведёт преподаватель.
        /// </summary>
        public List<string> Disciplines { get; set; }

        // КОНСТРУКТОРЫ

        /// <summary>
        /// Конструктор по умолчанию.
        /// Вызывает конструктор базового класса Employee.
        /// </summary>
        public Prepod() : base()
        {
            Department = "";
            Disciplines = new List<string>();
        }

        /// <summary>
        /// Конструктор с параметрами: фамилия, имя, табельный номер, кафедра.
        /// Использует конструктор базового класса Employee(lastName, firstName, employeeId).
        /// </summary>
        public Prepod(string lastName, string firstName, int employeeId, string department)
            : base(lastName, firstName, employeeId)
        {
            this.Department = department;
            this.Disciplines = new List<string>();
            this.Position = "Преподаватель"; // устанавливаем должность
        }

        /// <summary>
        /// Конструктор с полными данными.
        /// Использует конструктор базового класса Employee с максимальным набором параметров.
        /// </summary>
        public Prepod(int id, string lastName, string firstName, string middleName,
                     string passportNumber, string phoneNumber, decimal balance, bool isVip,
                     int employeeId, DateTime hireDate, string position,
                     string department, List<string> disciplines)
            : base(id, lastName, firstName, middleName, passportNumber, phoneNumber, balance, isVip,
                   employeeId, hireDate, position)
        {
            this.Department = department;
            this.Disciplines = disciplines ?? new List<string>();
        }

        // МЕТОДЫ КЛАССА

        /// <summary>
        /// Добавление дисциплины в список преподаваемых дисциплин.
        /// </summary>
        public void AddDiscipline(string discipline)
        {
            if (!string.IsNullOrWhiteSpace(discipline) && !Disciplines.Contains(discipline))
            {
                Disciplines.Add(discipline);
            }
        }

        /// <summary>
        /// Удаление дисциплины из списка.
        /// </summary>
        public bool RemoveDiscipline(string discipline)
        {
            return Disciplines.Remove(discipline);
        }

        /// <summary>
        /// Получение списка всех дисциплин в виде строки.
        /// </summary>
        public string GetDisciplinesList()
        {
            if (Disciplines.Count == 0)
            {
                return "Дисциплины не назначены";
            }
            return string.Join(", ", Disciplines);
        }

        /// <summary>
        /// Переопределение метода ToString().
        /// Добавляет информацию о кафедре.
        /// </summary>
        public override string ToString()
        {
            return $"{GetFullName()}, {Position}, Кафедра: {Department}";
        }

        /// <summary>
        /// Получение полной информации о преподавателе.
        /// Расширяет метод базового класса Employee.
        /// </summary>
        public string GetPrepodInfo()
        {
            string info = GetEmployeeInfo(); // вызываем метод базового класса
            info += $"\n\n--- Данные преподавателя ---";
            info += $"\nКафедра: {Department}";
            info += $"\nДисциплины: {GetDisciplinesList()}";
            info += $"\nКоличество дисциплин: {Disciplines.Count}";
            return info;
        }

        // ==================== РЕАЛИЗАЦИЯ АБСТРАКТНЫХ МЕТОДОВ ЛР4 ====================

        /// <summary>
        /// РЕАЛИЗАЦИЯ АБСТРАКТНОГО МЕТОДА: Получение текста с именем преподавателя.
        /// Добавляет кафедру к отображению.
        /// </summary>
        public override string NameText()
        {
            return $"Преподаватель {GetFullName()}, кафедра {Department}";
        }

        /// <summary>
        /// РЕАЛИЗАЦИЯ АБСТРАКТНОГО СВОЙСТВА: Краткое описание роли преподавателя.
        /// </summary>
        public override string RoleDescription
        {
            get
            {
                return $"Преподаватель кафедры {Department}, ведёт {Disciplines.Count} дисциплин(ы)";
            }
        }

        // ==================== ПЕРЕОПРЕДЕЛЕНИЕ ВИРТУАЛЬНЫХ МЕТОДОВ ====================

        /// <summary>
        /// ПЕРЕОПРЕДЕЛЕНИЕ ВИРТУАЛЬНОГО МЕТОДА: Отображение информации о преподавателе.
        /// Расширяет базовую версию информацией о кафедре и дисциплинах.
        /// </summary>
        public sealed override string ShowInfo()
        {
            // Вызываем базовую версию метода
            string baseInfo = base.ShowInfo();

            // Добавляем информацию о преподавателе
            return baseInfo +
                   $"\nКафедра: {Department}" +
                   $"\nДисциплины: {GetDisciplinesList()}" +
                   $"\n\nЭто ЗАПЕЧАТАННЫЙ (sealed) метод - дальнейшее переопределение запрещено!";
        }

        // ПЕРЕГРУЖЕННЫЕ МЕТОДЫ РАБОТЫ С ФАЙЛАМИ

        /// <summary>
        /// Запись данных преподавателя в текстовый файл.
        /// Версия 1: принимает путь к файлу в виде строки.
        /// Перегруженный метод базового класса - сохраняет дополнительные данные преподавателя.
        /// </summary>
        public new void WriteToFile(string path)
        {
            try
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path))
                {
                    // Записываем базовые данные
                    writer.WriteLine($"ID:{Id}");
                    writer.WriteLine($"Фамилия:{LastName}");
                    writer.WriteLine($"Имя:{FirstName}");
                    writer.WriteLine($"Возраст:{GetAge()}");
                    writer.WriteLine($"ТабельныйНомер:{EmployeeId}");
                    writer.WriteLine($"Должность:{Position}");
                    writer.WriteLine($"Стаж:{GetExperience()}");

                    // Добавляем данные преподавателя
                    writer.WriteLine($"Кафедра:{Department}");
                    writer.WriteLine($"КоличествоДисциплин:{Disciplines.Count}");
                    writer.WriteLine($"Дисциплины:{GetDisciplinesList()}");
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Ошибка записи в файл: {ex.Message}");
            }
        }

        /// <summary>
        /// Запись данных преподавателя в текстовый файл.
        /// Версия 2 (ПЕРЕГРУЖЕННЫЙ МЕТОД): принимает объект SaveFileDialog.
        /// </summary>
        public new void WriteToFile(System.Windows.Forms.SaveFileDialog dialog)
        {
            dialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            dialog.DefaultExt = "txt";
            dialog.FileName = $"Prepod_{Department}_{LastName}";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                WriteToFile(dialog.FileName);
            }
        }

        /// <summary>
        /// Чтение данных преподавателя из текстового файла.
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
                                case "ТабельныйНомер":
                                    EmployeeId = int.Parse(value);
                                    break;
                                case "Должность":
                                    Position = value;
                                    break;
                                case "Кафедра":
                                    Department = value;
                                    break;
                                case "Дисциплины":
                                    // Разбираем список дисциплин
                                    if (value != "Дисциплины не назначены")
                                    {
                                        string[] disciplines = value.Split(',');
                                        Disciplines.Clear();
                                        foreach (string disc in disciplines)
                                        {
                                            Disciplines.Add(disc.Trim());
                                        }
                                    }
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
        /// Чтение данных преподавателя из текстового файла.
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
