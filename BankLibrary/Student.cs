using System;

namespace BankLibrary
{
    /// <summary>
    /// Класс Student (Студент) — наследник класса Employee.
    /// Представляет студента учебного заведения.
    /// ИЗМЕНЕНО В ЛР4: Теперь наследуется от абстрактного класса Employee.
    /// </summary>
    public class Student : Employee
    {
        /// <summary>
        /// Номер учебной группы.
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// Курс обучения (1-5).
        /// </summary>
        public int Course { get; set; }

        /// <summary>
        /// Средний балл успеваемости.
        /// </summary>
        public double AverageGrade { get; set; }

        /// <summary>
        /// Специальность/направление подготовки.
        /// </summary>
        public string Specialty { get; set; }

        // КОНСТРУКТОРЫ

        /// <summary>
        /// Конструктор по умолчанию.
        /// Вызывает конструктор базового класса Client.
        /// </summary>
        public Student() : base()
        {
            Group = "";
            Course = 1;
            AverageGrade = 0.0;
            Specialty = "";
        }

        /// <summary>
        /// Конструктор с параметрами: фамилия, имя, группа, курс.
        /// Использует конструктор базового класса Client(lastName, firstName).
        /// </summary>
        public Student(string lastName, string firstName, string group, int course)
            : base(lastName, firstName)
        {
            this.Group = group;
            this.Course = course;
            this.AverageGrade = 0.0;
            this.Specialty = "";
        }

        /// <summary>
        /// Конструктор с полными данными.
        /// Использует конструктор базового класса с максимальным набором параметров.
        /// </summary>
        public Student(int id, string lastName, string firstName, string middleName,
                      string passportNumber, string phoneNumber, decimal balance, bool isVip,
                      string group, int course, double averageGrade, string specialty)
            : base(id, lastName, firstName, middleName, passportNumber, phoneNumber, balance, isVip)
        {
            this.Group = group;
            this.Course = course;
            this.AverageGrade = averageGrade;
            this.Specialty = specialty;
        }

        // МЕТОДЫ КЛАССА

        /// <summary>
        /// Проверка, является ли студент отличником.
        /// </summary>
        public bool IsExcellentStudent()
        {
            return AverageGrade >= 4.5;
        }

        /// <summary>
        /// Проверка, есть ли стипендия у студента.
        /// </summary>
        public bool HasScholarship()
        {
            return AverageGrade >= 4.0;
        }

        /// <summary>
        /// Получение названия курса (1 курс, 2 курс и т.д.).
        /// </summary>
        public string GetCourseName()
        {
            return $"{Course} курс";
        }

        /// <summary>
        /// Переопределение метода ToString().
        /// Добавляет информацию о группе и курсе.
        /// </summary>
        public override string ToString()
        {
            return $"{GetFullName()}, Группа: {Group}, {GetCourseName()}";
        }

        /// <summary>
        /// Получение полной информации о студенте.
        /// Расширяет метод базового класса Client.
        /// </summary>
        public string GetStudentInfo()
        {
            string info = GetClientInfo(); // вызываем метод базового класса
            info += $"\n\n--- Данные студента ---";
            info += $"\nГруппа: {Group}";
            info += $"\nКурс: {Course}";
            info += $"\nСпециальность: {Specialty}";
            info += $"\nСредний балл: {AverageGrade:F2}";
            info += $"\nОтличник: {(IsExcellentStudent() ? "Да" : "Нет")}";
            info += $"\nСтипендия: {(HasScholarship() ? "Да" : "Нет")}";
            return info;
        }

        // ==================== РЕАЛИЗАЦИЯ АБСТРАКТНЫХ МЕТОДОВ ЛР4 ====================

        /// <summary>
        /// РЕАЛИЗАЦИЯ АБСТРАКТНОГО МЕТОДА: Получение текста с именем студента.
        /// </summary>
        public override string NameText()
        {
            return $"Студент {GetFullName()}, группа {Group}";
        }

        /// <summary>
        /// РЕАЛИЗАЦИЯ АБСТРАКТНОГО СВОЙСТВА: Краткое описание роли студента.
        /// </summary>
        public override string RoleDescription
        {
            get { return $"Студент {Course} курса, группа {Group}, средний балл: {AverageGrade:F2}"; }
        }

        // ==================== ПЕРЕОПРЕДЕЛЕНИЕ ВИРТУАЛЬНЫХ МЕТОДОВ ====================

        /// <summary>
        /// ПЕРЕОПРЕДЕЛЕНИЕ ВИРТУАЛЬНОГО МЕТОДА: Отображение информации о студенте.
        /// </summary>
        public override string ShowInfo()
        {
            string baseInfo = base.ShowInfo();
            return baseInfo + $"\nГруппа: {Group}\nКурс: {Course}\nСредний балл: {AverageGrade:F2}";
        }

        /// <summary>
        /// ПЕРЕОПРЕДЕЛЕНИЕ ВИРТУАЛЬНОГО СВОЙСТВА: Проверка возраста студента.
        /// Студент должен быть в возрасте 16-35 лет.
        /// </summary>
        public override DateTime DateOfBirth
        {
            get { return base.DateOfBirth; }
            set
            {
                int age = Client.CalculateAge(value);
                if (age < 16 || age > 35)
                {
                    throw new ArgumentException("Возраст студента должен быть от 16 до 35 лет!");
                }
                base.DateOfBirth = value;
            }
        }

        // ==================== ДЕМОНСТРАЦИЯ СОКРЫТИЯ МЕТОДА (new) ====================

        /// <summary>
        /// СОКРЫТИЕ МЕТОДА: Новая реализация WriteToFile с ключевым словом new.
        /// Заменяет (скрывает) метод базового класса Employee, а не переопределяет его.
        /// </summary>
        public new void WriteToFile(string path)
        {
            try
            {
                using (System.IO.StreamWriter writer = new System.IO.StreamWriter(path))
                {
                    writer.WriteLine($"=== ДАННЫЕ СТУДЕНТА ===");
                    writer.WriteLine($"Фамилия:{LastName}");
                    writer.WriteLine($"Имя:{FirstName}");
                    writer.WriteLine($"Группа:{Group}");
                    writer.WriteLine($"Курс:{Course}");
                    writer.WriteLine($"Средний балл:{AverageGrade:F2}");
                    writer.WriteLine($"Специальность:{Specialty}");
                    writer.WriteLine($"\nЭто СКРЫТАЯ версия метода (new), а не переопределенная (override)!");
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Ошибка записи в файл: {ex.Message}");
            }
        }

        // ПЕРЕГРУЖЕННЫЕ МЕТОДЫ РАБОТЫ С ФАЙЛАМИ

        /// <summary>
        /// Запись данных студента в текстовый файл.
        /// Версия 1: принимает путь к файлу в виде строки.
        /// Перегруженный метод базового класса - сохраняет дополнительные данные студента.
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

                    // Добавляем данные студента
                    writer.WriteLine($"Группа:{Group}");
                    writer.WriteLine($"Курс:{Course}");
                    writer.WriteLine($"Специальность:{Specialty}");
                    writer.WriteLine($"СреднийБалл:{AverageGrade}");
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Ошибка записи в файл: {ex.Message}");
            }
        }

        /// <summary>
        /// Запись данных студента в текстовый файл.
        /// Версия 2 (ПЕРЕГРУЖЕННЫЙ МЕТОД): принимает объект SaveFileDialog.
        /// </summary>
        public new void WriteToFile(System.Windows.Forms.SaveFileDialog dialog)
        {
            dialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            dialog.DefaultExt = "txt";
            dialog.FileName = $"Student_{Group}_{LastName}";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                WriteToFile(dialog.FileName);
            }
        }

        /// <summary>
        /// Чтение данных студента из текстового файла.
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
                                case "Группа":
                                    Group = value;
                                    break;
                                case "Курс":
                                    Course = int.Parse(value);
                                    break;
                                case "Специальность":
                                    Specialty = value;
                                    break;
                                case "СреднийБалл":
                                    AverageGrade = double.Parse(value);
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
        /// Чтение данных студента из текстового файла.
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
