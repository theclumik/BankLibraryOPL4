using System;

namespace BankLibrary
{
    /// <summary>
    /// Класс StudentDistant (Студент-заочник) — наследник класса Student.
    /// Представляет студента заочной формы обучения.
    /// </summary>
    public class StudentDistant : Student
    {
        /// <summary>
        /// Город проживания студента-заочника.
        /// </summary>
        public string SessionCity { get; set; }

        /// <summary>
        /// Дата следующей сессии.
        /// </summary>
        public DateTime NextSessionDate { get; set; }

        /// <summary>
        /// Место работы студента-заочника.
        /// </summary>
        public string WorkPlace { get; set; }

        // КОНСТРУКТОРЫ

        /// <summary>
        /// Конструктор по умолчанию.
        /// Вызывает конструктор базового класса Student.
        /// </summary>
        public StudentDistant() : base()
        {
            SessionCity = "";
            NextSessionDate = DateTime.Now;
            WorkPlace = "";
        }

        /// <summary>
        /// Конструктор с параметрами: фамилия, имя, группа, курс, город.
        /// Использует конструктор базового класса Student(lastName, firstName, group, course).
        /// </summary>
        public StudentDistant(string lastName, string firstName, string group, int course, string sessionCity)
            : base(lastName, firstName, group, course)
        {
            this.SessionCity = sessionCity;
            this.NextSessionDate = DateTime.Now;
            this.WorkPlace = "";
        }

        /// <summary>
        /// Конструктор с полными данными.
        /// Использует конструктор базового класса Student с максимальным набором параметров.
        /// </summary>
        public StudentDistant(int id, string lastName, string firstName, string middleName,
                             string passportNumber, string phoneNumber, decimal balance, bool isVip,
                             string group, int course, double averageGrade, string specialty,
                             string sessionCity, DateTime nextSessionDate, string workPlace)
            : base(id, lastName, firstName, middleName, passportNumber, phoneNumber, balance, isVip,
                   group, course, averageGrade, specialty)
        {
            this.SessionCity = sessionCity;
            this.NextSessionDate = nextSessionDate;
            this.WorkPlace = workPlace;
        }

        // МЕТОДЫ КЛАССА

        /// <summary>
        /// Проверка, скоро ли начнётся сессия (менее 30 дней).
        /// </summary>
        public bool IsSessionSoon()
        {
            TimeSpan diff = NextSessionDate - DateTime.Now;
            return diff.TotalDays <= 30 && diff.TotalDays >= 0;
        }

        /// <summary>
        /// Получение количества дней до следующей сессии.
        /// </summary>
        public int DaysUntilSession()
        {
            TimeSpan diff = NextSessionDate - DateTime.Now;
            return diff.Days;
        }

        /// <summary>
        /// Переопределение метода ToString().
        /// Добавляет информацию о заочной форме обучения.
        /// </summary>
        public override string ToString()
        {
            return $"{GetFullName()}, Группа: {Group}, {GetCourseName()} (заочно), Город: {SessionCity}";
        }

        /// <summary>
        /// Получение полной информации о студенте-заочнике.
        /// Расширяет метод базового класса Student.
        /// </summary>
        public string GetStudentDistantInfo()
        {
            string info = GetStudentInfo(); // вызываем метод базового класса
            info += $"\n\n--- Данные заочника ---";
            info += $"\nФорма обучения: Заочная";
            info += $"\nГород проживания: {SessionCity}";
            info += $"\nМесто работы: {WorkPlace}";
            info += $"\nДата следующей сессии: {NextSessionDate:dd.MM.yyyy}";
            info += $"\nДней до сессии: {DaysUntilSession()}";
            info += $"\nСессия скоро: {(IsSessionSoon() ? "Да" : "Нет")}";
            return info;
        }

        // ПЕРЕГРУЖЕННЫЕ МЕТОДЫ РАБОТЫ С ФАЙЛАМИ

        /// <summary>
        /// Запись данных студента-заочника в текстовый файл.
        /// Версия 1: принимает путь к файлу в виде строки.
        /// Перегруженный метод базового класса - сохраняет дополнительные данные студента-заочника.
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
                    writer.WriteLine($"Группа:{Group}");
                    writer.WriteLine($"Курс:{Course}");
                    writer.WriteLine($"Специальность:{Specialty}");
                    writer.WriteLine($"СреднийБалл:{AverageGrade}");

                    // Добавляем данные студента-заочника
                    writer.WriteLine($"ФормаОбучения:Заочная");
                    writer.WriteLine($"ГородПроживания:{SessionCity}");
                    writer.WriteLine($"МестоРаботы:{WorkPlace}");
                    writer.WriteLine($"ДатаСессии:{NextSessionDate:dd.MM.yyyy}");
                    writer.WriteLine($"ДнейДоСессии:{DaysUntilSession()}");
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception($"Ошибка записи в файл: {ex.Message}");
            }
        }

        /// <summary>
        /// Запись данных студента-заочника в текстовый файл.
        /// Версия 2 (ПЕРЕГРУЖЕННЫЙ МЕТОД): принимает объект SaveFileDialog.
        /// </summary>
        public new void WriteToFile(System.Windows.Forms.SaveFileDialog dialog)
        {
            dialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            dialog.DefaultExt = "txt";
            dialog.FileName = $"StudentDistant_{Group}_{LastName}";

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                WriteToFile(dialog.FileName);
            }
        }

        /// <summary>
        /// Чтение данных студента-заочника из текстового файла.
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
                                case "ГородПроживания":
                                    SessionCity = value;
                                    break;
                                case "МестоРаботы":
                                    WorkPlace = value;
                                    break;
                                case "ДатаСессии":
                                    NextSessionDate = DateTime.ParseExact(value, "dd.MM.yyyy", null);
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
        /// Чтение данных студента-заочника из текстового файла.
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
