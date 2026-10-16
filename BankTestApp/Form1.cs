using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using BankLibrary;

namespace BankTestApp
{

    public partial class Form1 : Form
    {
        // КОЛЛЕКЦИЯ для хранения клиентов
        private List<Client> clients = new List<Client>();

        // Текущий выбранный клиент
        private Client currentClient;

        // Объекты для тестирования наследования
        private Employee currentEmployee;
        private Prepod currentPrepod;
        private Student currentStudent;
        private StudentDistant currentStudentDistant;

        public Form1()
        {
            InitializeComponent();
            InitializeBindings();
        }

        /// <summary>
        /// Настройка привязки данных к элементам формы
        /// </summary>
        private void InitializeBindings()
        {
            // Создаем первого клиента по умолчанию
            currentClient = new Client("Иванов", "Иван", 10000m);
            currentClient.BirthDate = new DateTime(1990, 5, 15);
            currentClient.IsVip = false;

            // Привязка свойств объекта к элементам управления
            txtLastName.DataBindings.Add("Text", currentClient, "LastName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtFirstName.DataBindings.Add("Text", currentClient, "FirstName", false, DataSourceUpdateMode.OnPropertyChanged);
            txtBalance.DataBindings.Add("Text", currentClient, "Balance", true, DataSourceUpdateMode.OnPropertyChanged, "0", "F2");
            dtpBirthDate.DataBindings.Add("Value", currentClient, "BirthDate", false, DataSourceUpdateMode.OnPropertyChanged);
            chkIsVip.DataBindings.Add("Checked", currentClient, "IsVip", false, DataSourceUpdateMode.OnPropertyChanged);
        }


        /// <summary>
        /// Добавление нового клиента в коллекцию
        /// </summary>
        private void btnAddClient_Click(object sender, EventArgs e)
        {
            try
            {
                // Создаем нового клиента с данными из формы
                Client newClient = new Client
                {
                    Id = clients.Count + 1,
                    LastName = txtLastName.Text,
                    FirstName = txtFirstName.Text,
                    Balance = decimal.Parse(txtBalance.Text),
                    BirthDate = dtpBirthDate.Value,
                    IsVip = chkIsVip.Checked
                };

                // Добавляем в коллекцию
                clients.Add(newClient);

                // Обновляем ListBox
                UpdateClientList();

                Output($"✅ Клиент добавлен в коллекцию!\n\n" +
                       $"Всего клиентов: {clients.Count}\n" +
                       $"Добавлен: {newClient.ToString()}");
            }
            catch (ArgumentException ex)
            {
                Output($"❌ ОШИБКА: {ex.Message}");
            }
            catch (FormatException)
            {
                Output("❌ ОШИБКА: Проверьте правильность ввода данных!");
            }
        }

        /// <summary>
        /// Обновление списка клиентов в ListBox
        /// </summary>
        private void UpdateClientList()
        {
            listBoxClients.Items.Clear();
            foreach (Client c in clients)
            {
                listBoxClients.Items.Add(c.ToString());
            }
        }

        /// <summary>
        /// Вызов статического метода класса Client
        /// </summary>
        private void btnStaticMethod_Click(object sender, EventArgs e)
        {
            if (currentClient == null)
            {
                Output("❌ ОШИБКА: Клиент не создан!");
                return;
            }

            // Демонстрация статического метода
            int age = Client.CalculateAge(currentClient.BirthDate);

            string info = "🔍 ДЕМОНСТРАЦИЯ СТАТИЧЕСКОГО МЕТОДА\n\n";
            info += $"Вызван метод: Client.CalculateAge(birthDate)\n\n";
            info += $"Клиент: {currentClient.GetFullName()}\n";
            info += $"Дата рождения: {currentClient.BirthDate:dd.MM.yyyy}\n";
            info += $"Возраст: {age} лет\n\n";
            info += "Статический метод вызывается через имя класса,\n";
            info += "без создания объекта!";

            // Если есть несколько клиентов, сравним их балансы
            if (clients.Count >= 2)
            {
                Client richer = Client.GetRicherClient(clients[0], clients[1]);
                info += $"\n\n─────────────────────────\n";
                info += $"Сравнение балансов:\n";
                info += $"Клиент 1: {clients[0].GetFullName()} - {clients[0].Balance:F2} руб.\n";
                info += $"Клиент 2: {clients[1].GetFullName()} - {clients[1].Balance:F2} руб.\n";
                info += $"Богаче: {richer?.GetFullName()}";
            }

            Output(info);
        }

        /// <summary>
        /// Загрузка фотографии через OpenFileDialog
        /// </summary>
        private void btnLoadFoto_Click(object sender, EventArgs e)
        {
            if (openFileDialogFoto.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Загружаем изображение
                    Image img = Image.FromFile(openFileDialogFoto.FileName);
                    pictureBoxFoto.Image = img;
                    pictureBoxFoto.SizeMode = PictureBoxSizeMode.Zoom;

                    // Сохраняем путь к фото в текущем клиенте
                    if (currentClient != null)
                    {
                        currentClient.PhotoPath = openFileDialogFoto.FileName;
                    }

                    Output($"✅ Фото загружено успешно!\n\n" +
                           $"Путь: {openFileDialogFoto.FileName}\n\n" +
                           "Демонстрация работы с графикой:\n" +
                           "- OpenFileDialog для выбора файла\n" +
                           "- PictureBox для отображения изображения\n" +
                           "- Image.FromFile() для загрузки");
                }
                catch (Exception ex)
                {
                    Output($"❌ ОШИБКА при загрузке изображения:\n{ex.Message}");
                }
            }
        }

        /// <summary>
        /// Обработчик выбора клиента из списка
        /// </summary>
        private void listBoxClients_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxClients.SelectedIndex >= 0)
            {
                Client selectedClient = clients[listBoxClients.SelectedIndex];

                // Отображаем информацию о выбранном клиенте
                Output($"📋 ВЫБРАН КЛИЕНТ:\n\n{selectedClient.GetClientInfo()}");

                // Если есть фото, загружаем его
                if (!string.IsNullOrEmpty(selectedClient.PhotoPath))
                {
                    try
                    {
                        pictureBoxFoto.Image = Image.FromFile(selectedClient.PhotoPath);
                    }
                    catch { }
                }
            }
        }

        private void Output(string text)
        {
            txtOutput.Text = text;
        }

        // ==================== ОБРАБОТЧИКИ ДЛЯ РАБОТЫ С ФАЙЛАМИ ====================

        /// <summary>
        /// Сохранение данных клиента в файл (перегруженный метод с SaveFileDialog)
        /// </summary>
        private void btnSaveToFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentClient == null)
                {
                    Output("❌ ОШИБКА: Клиент не создан!");
                    return;
                }

                // Используем перегруженный метод WriteToFile с SaveFileDialog
                currentClient.WriteToFile(saveFileDialog);

                Output($"✅ ДАННЫЕ СОХРАНЕНЫ В ФАЙЛ!\n\n" +
                       $"Использован метод: WriteToFile(SaveFileDialog)\n\n" +
                       $"Сохранены данные:\n" +
                       $"- ID: {currentClient.Id}\n" +
                       $"- Фамилия: {currentClient.LastName}\n" +
                       $"- Имя: {currentClient.FirstName}\n" +
                       $"- Возраст: {currentClient.GetAge()} лет\n" +
                       $"- Баланс: {currentClient.Balance:F2} руб.\n\n" +
                       $"Демонстрация перегрузки методов:\n" +
                       $"Метод WriteToFile() имеет 2 версии:\n" +
                       $"1. WriteToFile(string path)\n" +
                       $"2. WriteToFile(SaveFileDialog dialog)");
            }
            catch (Exception ex)
            {
                Output($"❌ ОШИБКА: {ex.Message}");
            }
        }

        /// <summary>
        /// Загрузка данных клиента из файла (перегруженный метод с OpenFileDialog)
        /// </summary>
        private void btnLoadFromFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentClient == null)
                {
                    Output("❌ ОШИБКА: Клиент не создан!");
                    return;
                }

                // Используем перегруженный метод ReadFromFile с OpenFileDialog
                currentClient.ReadFromFile(openFileDialog);

                // Обновляем отображение
                txtLastName.Text = currentClient.LastName;
                txtFirstName.Text = currentClient.FirstName;
                txtBalance.Text = currentClient.Balance.ToString("F2");
                dtpBirthDate.Value = currentClient.BirthDate;

                Output($"✅ ДАННЫЕ ЗАГРУЖЕНЫ ИЗ ФАЙЛА!\n\n" +
                       $"Использован метод: ReadFromFile(OpenFileDialog)\n\n" +
                       $"Загруженные данные:\n" +
                       $"{currentClient.GetClientInfo()}\n\n" +
                       $"Демонстрация перегрузки методов:\n" +
                       $"Метод ReadFromFile() имеет 2 версии:\n" +
                       $"1. ReadFromFile(string path)\n" +
                       $"2. ReadFromFile(OpenFileDialog dialog)");
            }
            catch (Exception ex)
            {
                Output($"❌ ОШИБКА: {ex.Message}");
            }
        }

        // ==================== ОБРАБОТЧИКИ ДЛЯ ПЕРЕДАЧИ ПАРАМЕТРОВ ====================

        /// <summary>
        /// Демонстрация передачи параметров ПО ЗНАЧЕНИЮ
        /// </summary>
        private void btnTestByValue_Click(object sender, EventArgs e)
        {
            try
            {
                // Создаём сотрудника для демонстрации
                Employee emp = new Employee("Петров", "Петр", 101);
                emp.Position = "Менеджер";

                // Создаём массив зарплат
                decimal[] salaries = { 100m, 150m, 100m };
                int colMonth = 3;

                Output($"🔍 ПЕРЕДАЧА ПАРАМЕТРОВ ПО ЗНАЧЕНИЮ\n\n" +
                       $"До вызова метода:\n" +
                       $"- colMonth = {colMonth}\n" +
                       $"- salaries = [{string.Join(", ", salaries)}]\n\n");

                // Вызываем метод (параметр colMonth передаётся по значению)
                decimal average = emp.AverageSalary(colMonth, salaries);

                Output(txtOutput.Text +
                       $"После вызова метода:\n" +
                       $"- colMonth = {colMonth} (НЕ изменилось! Передача по значению)\n" +
                       $"- salaries = [{string.Join(", ", salaries)}] (изменилось! Ссылочный тип)\n" +
                       $"- Средняя зарплата: {average:F2} руб.\n\n" +
                       $"ВЫВОД:\n" +
                       $"int передаётся по значению - изменения НЕ сохраняются\n" +
                       $"массив (ссылочный тип) - изменения СОХРАНЯЮТСЯ");
            }
            catch (Exception ex)
            {
                Output($"❌ ОШИБКА: {ex.Message}");
            }
        }

        /// <summary>
        /// Демонстрация передачи параметров ПО ССЫЛКЕ (ref)
        /// </summary>
        private void btnTestByRef_Click(object sender, EventArgs e)
        {
            try
            {
                // Создаём сотрудника для демонстрации
                Employee emp = new Employee("Сидоров", "Сидор", 102);
                emp.Position = "Бухгалтер";

                // Создаём массив зарплат
                decimal[] salaries = { 100m, 150m, 100m };
                int colMonth = 3;

                Output($"🔍 ПЕРЕДАЧА ПАРАМЕТРОВ ПО ССЫЛКЕ (ref)\n\n" +
                       $"До вызова метода:\n" +
                       $"- colMonth = {colMonth}\n" +
                       $"- salaries = [{string.Join(", ", salaries)}]\n\n");

                // Вызываем метод (параметр colMonth передаётся по ссылке)
                decimal average = emp.AverageSalary(ref colMonth, salaries);

                Output(txtOutput.Text +
                       $"После вызова метода:\n" +
                       $"- colMonth = {colMonth} (ИЗМЕНИЛОСЬ! Передача по ссылке ref)\n" +
                       $"- salaries = [{string.Join(", ", salaries)}]\n" +
                       $"- Средняя зарплата: {average:F2} руб.\n\n" +
                       $"ВЫВОД:\n" +
                       $"Ключевое слово ref заставляет передать параметр по ссылке\n" +
                       $"Изменения внутри метода СОХРАНЯЮТСЯ!");
            }
            catch (Exception ex)
            {
                Output($"❌ ОШИБКА: {ex.Message}");
            }
        }

        /// <summary>
        /// Демонстрация ВЫХОДНЫХ ПАРАМЕТРОВ (out)
        /// </summary>
        private void btnTestOut_Click(object sender, EventArgs e)
        {
            try
            {
                // Создаём сотрудника для демонстрации
                Employee emp = new Employee("Алексеев", "Алексей", 103);
                emp.Position = "Программист";

                // Создаём массив зарплат
                decimal[] salaries = { 80m, 120m, 150m, 90m, 110m };

                Output($"🔍 ВЫХОДНЫЕ ПАРАМЕТРЫ (out)\n\n" +
                       $"Сотрудник: {emp.GetFullName()}\n" +
                       $"Должность: {emp.Position}\n" +
                       $"Зарплаты за 5 месяцев: [{string.Join(", ", salaries)}]\n\n");

                // Вызываем метод с выходными параметрами
                decimal minSalary, maxSalary;
                decimal average = emp.AverageSalary(salaries, out minSalary, out maxSalary);

                Output(txtOutput.Text +
                       $"Результаты расчёта:\n" +
                       $"- Средняя зарплата: {average:F2} руб.\n" +
                       $"- Минимальная зарплата (out): {minSalary:F2} руб.\n" +
                       $"- Максимальная зарплата (out): {maxSalary:F2} руб.\n\n" +
                       $"ВЫВОД:\n" +
                       $"Метод возвращает ОДНО значение (return)\n" +
                       $"и ДВА значения через выходные параметры (out)\n" +
                       $"Итого: метод вернул 3 значения!");
            }
            catch (Exception ex)
            {
                Output($"❌ ОШИБКА: {ex.Message}");
            }
        }

        /// <summary>
        /// Демонстрация НЕОБЯЗАТЕЛЬНЫХ ПАРАМЕТРОВ (3 параметра)
        /// </summary>
        private void btnTestOptional_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentClient == null)
                {
                    Output("❌ ОШИБКА: Клиент не создан!");
                    return;
                }

                Output($"🔍 НЕОБЯЗАТЕЛЬНЫЕ ПАРАМЕТРЫ (3 параметра)\n\n" +
                       $"Метод: GetClientSummary(bool includeBalance = true, \n" +
                       $"                        bool includeAge = true, \n" +
                       $"                        bool includeVipStatus = false)\n\n");

                // Вызов 1: Все параметры по умолчанию
                string result1 = currentClient.GetClientSummary();
                Output(txtOutput.Text + $"Вызов 1: GetClientSummary()\n>>> {result1}\n\n");

                // Вызов 2: Указываем только первый параметр
                string result2 = currentClient.GetClientSummary(false);
                Output(txtOutput.Text + $"Вызов 2: GetClientSummary(false)\n>>> {result2}\n\n");

                // Вызов 3: Указываем два параметра
                string result3 = currentClient.GetClientSummary(true, false);
                Output(txtOutput.Text + $"Вызов 3: GetClientSummary(true, false)\n>>> {result3}\n\n");

                // Вызов 4: Указываем все три параметра
                string result4 = currentClient.GetClientSummary(true, true, true);
                Output(txtOutput.Text + $"Вызов 4: GetClientSummary(true, true, true)\n>>> {result4}\n\n");

                // Вызов 5: Именованные параметры (можно менять порядок)
                string result5 = currentClient.GetClientSummary(includeVipStatus: true, includeBalance: false);
                Output(txtOutput.Text + $"Вызов 5: GetClientSummary(includeVipStatus: true, includeBalance: false)\n>>> {result5}\n\n");

                Output(txtOutput.Text +
                       $"ВЫВОД:\n" +
                       $"Необязательные параметры позволяют вызывать метод\n" +
                       $"с разным количеством аргументов!");
            }
            catch (Exception ex)
            {
                Output($"❌ ОШИБКА: {ex.Message}");
            }
        }

        // ==================== ОБРАБОТЧИКИ ДЛЯ EMPLOYEE ====================

        /// <summary>
        /// Создание объекта Employee (демонстрация наследования)
        /// </summary>
        private void btnCreateEmployee_Click(object sender, EventArgs e)
        {
            try
            {
                // Создаём сотрудника используя данные из полей формы
                currentEmployee = new Employee(
                    currentClient.Id,
                    txtLastName.Text,
                    txtFirstName.Text,
                    "",
                    "",
                    "",
                    decimal.Parse(txtBalance.Text),
                    chkIsVip.Checked,
                    int.Parse(txtEmployeeId.Text),
                    dtpHireDate.Value,
                    txtPosition.Text
                );

                Output($"✅ ОБЪЕКТ Employee СОЗДАН!\n\n" +
                       $"Демонстрация НАСЛЕДОВАНИЯ:\n" +
                       $"Employee наследует от Client\n\n" +
                       $"Цепочка наследования:\n" +
                       $"Object → Client → Employee\n\n" +
                       $"Доступны свойства базового класса Client:\n" +
                       $"- LastName: {currentEmployee.LastName}\n" +
                       $"- FirstName: {currentEmployee.FirstName}\n" +
                       $"- Balance: {currentEmployee.Balance:F2} руб.\n\n" +
                       $"Добавлены новые свойства Employee:\n" +
                       $"- EmployeeId: {currentEmployee.EmployeeId}\n" +
                       $"- Position: {currentEmployee.Position}\n" +
                       $"- HireDate: {currentEmployee.HireDate:dd.MM.yyyy}\n" +
                       $"- Стаж работы: {currentEmployee.GetExperience()} лет");
            }
            catch (Exception ex)
            {
                Output($"❌ ОШИБКА: {ex.Message}");
            }
        }

        /// <summary>
        /// Показать полную информацию о сотруднике
        /// </summary>
        private void btnShowEmployeeInfo_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentEmployee == null)
                {
                    Output("❌ ОШИБКА: Сначала создайте сотрудника!");
                    return;
                }

                Output($"📋 ПОЛНАЯ ИНФОРМАЦИЯ О СОТРУДНИКЕ\n\n" +
                       $"{currentEmployee.GetEmployeeInfo()}\n\n" +
                       $"Демонстрация наследования методов:\n" +
                       $"- GetClientInfo() - метод базового класса Client\n" +
                       $"- GetEmployeeInfo() - метод класса Employee\n" +
                       $"- GetExperience() - новый метод класса Employee\n\n" +
                       $"Все объекты неявно наследуют от Object:\n" +
                       $"- ToString(): {currentEmployee.ToString()}\n" +
                       $"- GetType(): {currentEmployee.GetType().Name}\n" +
                       $"- GetHashCode(): {currentEmployee.GetHashCode()}");
            }
            catch (Exception ex)
            {
                Output($"❌ ОШИБКА: {ex.Message}");
            }
        }

        // ==================== ОБРАБОТЧИКИ ДЛЯ PREPOD ====================

        /// <summary>
        /// Добавление дисциплины в список
        /// </summary>
        private void btnAddDiscipline_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNewDiscipline.Text))
            {
                lstDisciplines.Items.Add(txtNewDiscipline.Text);
                txtNewDiscipline.Clear();
            }
        }

        /// <summary>
        /// Создание объекта Prepod (демонстрация цепочки наследования)
        /// </summary>
        private void btnCreatePrepod_Click(object sender, EventArgs e)
        {
            try
            {
                // Собираем список дисциплин
                List<string> disciplines = new List<string>();
                foreach (var item in lstDisciplines.Items)
                {
                    disciplines.Add(item.ToString());
                }

                // Создаём преподавателя
                currentPrepod = new Prepod(
                    txtLastName.Text,
                    txtFirstName.Text,
                    int.Parse(txtEmployeeId.Text),
                    txtDepartment.Text
                );

                // Добавляем дисциплины
                foreach (string disc in disciplines)
                {
                    currentPrepod.AddDiscipline(disc);
                }

                currentPrepod.HireDate = dtpHireDate.Value;
                currentPrepod.Balance = decimal.Parse(txtBalance.Text);

                Output($"✅ ОБЪЕКТ Prepod СОЗДАН!\n\n" +
                       $"Демонстрация ЦЕПОЧКИ НАСЛЕДОВАНИЯ:\n" +
                       $"Object → Client → Employee → Prepod\n\n" +
                       $"{currentPrepod.GetPrepodInfo()}\n\n" +
                       $"Доступны методы всех базовых классов:\n" +
                       $"- GetFullName() (от Client)\n" +
                       $"- GetAge() (от Client)\n" +
                       $"- GetExperience() (от Employee)\n" +
                       $"- GetDisciplinesList() (от Prepod)\n\n" +
                       $"Каждый класс расширяет функциональность\n" +
                       $"базового класса, добавляя новые свойства и методы!");
            }
            catch (Exception ex)
            {
                Output($"❌ ОШИБКА: {ex.Message}");
            }
        }

        // ==================== ОБРАБОТЧИКИ ДЛЯ STUDENT ====================

        /// <summary>
        /// Создание объекта Student
        /// </summary>
        private void btnCreateStudent_Click(object sender, EventArgs e)
        {
            try
            {
                currentStudent = new Student(
                    1,
                    txtLastName.Text,
                    txtFirstName.Text,
                    "",
                    "",
                    "",
                    decimal.Parse(txtBalance.Text),
                    false,
                    txtGroup.Text,
                    (int)numCourse.Value,
                    (double)numAverageGrade.Value,
                    txtSpecialty.Text
                );

                currentStudent.BirthDate = dtpBirthDate.Value;

                Output($"✅ ОБЪЕКТ Student СОЗДАН!\n\n" +
                       $"Демонстрация НАСЛЕДОВАНИЯ:\n" +
                       $"Object → Client → Student\n\n" +
                       $"{currentStudent.GetStudentInfo()}\n\n" +
                       $"Новые методы класса Student:\n" +
                       $"- IsExcellentStudent(): {currentStudent.IsExcellentStudent()}\n" +
                       $"- HasScholarship(): {currentStudent.HasScholarship()}\n" +
                       $"- GetCourseName(): {currentStudent.GetCourseName()}\n\n" +
                       $"Student сохранён для демонстрации\n" +
                       $"преобразований типов на следующей вкладке!");
            }
            catch (Exception ex)
            {
                Output($"❌ ОШИБКА: {ex.Message}");
            }
        }

        // ==================== ОБРАБОТЧИКИ ДЛЯ STUDENT DISTANT ====================

        /// <summary>
        /// Создание объекта StudentDistant (демонстрация глубокой иерархии)
        /// </summary>
        private void btnCreateStudentDistant_Click(object sender, EventArgs e)
        {
            try
            {
                currentStudentDistant = new StudentDistant(
                    txtLastName.Text,
                    txtFirstName.Text,
                    txtGroup.Text,
                    (int)numCourse.Value,
                    txtSessionCity.Text
                );

                currentStudentDistant.NextSessionDate = dtpNextSession.Value;
                currentStudentDistant.WorkPlace = txtWorkPlace.Text;
                currentStudentDistant.Balance = decimal.Parse(txtBalance.Text);
                currentStudentDistant.BirthDate = dtpBirthDate.Value;
                currentStudentDistant.AverageGrade = (double)numAverageGrade.Value;
                currentStudentDistant.Specialty = txtSpecialty.Text;

                Output($"✅ ОБЪЕКТ StudentDistant СОЗДАН!\n\n" +
                       $"Демонстрация ГЛУБОКОЙ ИЕРАРХИИ:\n" +
                       $"Object → Client → Student → StudentDistant\n\n" +
                       $"{currentStudentDistant.GetStudentDistantInfo()}\n\n" +
                       $"Новые методы класса StudentDistant:\n" +
                       $"- IsSessionSoon(): {currentStudentDistant.IsSessionSoon()}\n" +
                       $"- DaysUntilSession(): {currentStudentDistant.DaysUntilSession()} дней\n\n" +
                       $"Каждый уровень иерархии добавляет новую функциональность!\n" +
                       $"StudentDistant имеет доступ ко ВСЕМ методам\n" +
                       $"и свойствам базовых классов!");
            }
            catch (Exception ex)
            {
                Output($"❌ ОШИБКА: {ex.Message}");
            }
        }

        // ==================== ОБРАБОТЧИКИ ДЛЯ ПРЕОБРАЗОВАНИЙ ТИПОВ ====================

        /// <summary>
        /// Демонстрация UpCasting (Student → Client)
        /// </summary>
        private void btnUpCasting_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentStudent == null)
                {
                    Output("❌ ОШИБКА: Сначала создайте студента на вкладке 'Student'!");
                    return;
                }

                // UpCasting - неявное преобразование (безопасное)
                Client clientRef = currentStudent;

                Output($"🔼 UPCASTING (восходящее преобразование)\n\n" +
                       $"Student → Client (производный класс → базовый класс)\n\n" +
                       $"Код:\n" +
                       $"Client clientRef = currentStudent;\n\n" +
                       $"Это НЕЯВНОЕ (автоматическое) преобразование.\n" +
                       $"Не требует приведения типа.\n\n" +
                       $"Объект Student присвоен переменной типа Client.\n" +
                       $"Доступна только функциональность Client:\n\n" +
                       $"clientRef.GetFullName() = {clientRef.GetFullName()}\n" +
                       $"clientRef.Balance = {clientRef.Balance:F2} руб.\n" +
                       $"clientRef.GetAge() = {clientRef.GetAge()} лет\n\n" +
                       $"НЕ доступны методы Student:\n" +
                       $"clientRef.GetCourseName() - ❌ ошибка компиляции!\n" +
                       $"clientRef.Group - ❌ ошибка компиляции!\n\n" +
                       $"Фактический тип объекта:\n" +
                       $"clientRef.GetType() = {clientRef.GetType().Name}");
            }
            catch (Exception ex)
            {
                Output($"❌ ОШИБКА: {ex.Message}");
            }
        }

        /// <summary>
        /// Демонстрация DownCasting (Client → Student)
        /// </summary>
        private void btnDownCasting_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentStudent == null)
                {
                    Output("❌ ОШИБКА: Сначала создайте студента на вкладке 'Student'!");
                    return;
                }

                // Сначала делаем UpCasting
                Client clientRef = currentStudent;

                // Теперь DownCasting - ЯВНОЕ преобразование
                Student studentRef = (Student)clientRef;

                Output($"🔽 DOWNCASTING (нисходящее преобразование)\n\n" +
                       $"Client → Student (базовый класс → производный класс)\n\n" +
                       $"Код:\n" +
                       $"Client clientRef = currentStudent;  // UpCasting\n" +
                       $"Student studentRef = (Student)clientRef;  // DownCasting\n\n" +
                       $"Это ЯВНОЕ преобразование.\n" +
                       $"Требует приведения типа: (Student)\n\n" +
                       $"Теперь доступна вся функциональность Student:\n\n" +
                       $"studentRef.GetFullName() = {studentRef.GetFullName()}\n" +
                       $"studentRef.Group = {studentRef.Group}\n" +
                       $"studentRef.GetCourseName() = {studentRef.GetCourseName()}\n" +
                       $"studentRef.AverageGrade = {studentRef.AverageGrade:F2}\n" +
                       $"studentRef.IsExcellentStudent() = {studentRef.IsExcellentStudent()}\n\n" +
                       $"⚠️ ВНИМАНИЕ:\n" +
                       $"DownCasting может быть ОПАСЕН!\n" +
                       $"Если объект НЕ является Student,\n" +
                       $"произойдёт исключение InvalidCastException!");
            }
            catch (Exception ex)
            {
                Output($"❌ ОШИБКА: {ex.Message}");
            }
        }

        /// <summary>
        /// Демонстрация оператора is (проверка типа)
        /// </summary>
        private void btnTestIs_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentStudent == null)
                {
                    Output("❌ ОШИБКА: Сначала создайте студента на вкладке 'Student'!");
                    return;
                }

                Client clientRef = currentStudent;
                Client simpleClient = new Client("Тестов", "Тест");

                Output($"🔍 ОПЕРАТОР is (проверка типа)\n\n" +
                       $"Используется для БЕЗОПАСНОЙ проверки типа объекта.\n\n");

                // Проверяем разные типы
                Output(txtOutput.Text +
                       $"Проверка объекта currentStudent:\n" +
                       $"currentStudent is Client = {currentStudent is Client}\n" +
                       $"currentStudent is Student = {currentStudent is Student}\n" +
                       $"currentStudent is Employee = {currentStudent is Employee}\n" +
                       $"currentStudent is Object = {currentStudent is Object}\n\n");

                Output(txtOutput.Text +
                       $"Проверка объекта clientRef (ссылка на Student):\n" +
                       $"clientRef is Client = {clientRef is Client}\n" +
                       $"clientRef is Student = {clientRef is Student}\n" +
                       $"clientRef is Employee = {clientRef is Employee}\n\n");

                Output(txtOutput.Text +
                       $"Проверка объекта simpleClient (обычный Client):\n" +
                       $"simpleClient is Client = {simpleClient is Client}\n" +
                       $"simpleClient is Student = {simpleClient is Student}\n" +
                       $"simpleClient is Employee = {simpleClient is Employee}\n\n");

                Output(txtOutput.Text +
                       $"ПРАКТИЧЕСКОЕ ПРИМЕНЕНИЕ:\n" +
                       $"Безопасное приведение типов:\n\n" +
                       $"if (clientRef is Student)\n" +
                       $"{{\n" +
                       $"    Student s = (Student)clientRef;\n" +
                       $"    // Можно безопасно использовать методы Student\n" +
                       $"}}");
            }
            catch (Exception ex)
            {
                Output($"❌ ОШИБКА: {ex.Message}");
            }
        }

        /// <summary>
        /// Демонстрация оператора as (безопасное приведение)
        /// </summary>
        private void btnTestAs_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentStudent == null)
                {
                    Output("❌ ОШИБКА: Сначала создайте студента на вкладке 'Student'!");
                    return;
                }

                Client clientRef = currentStudent;
                Client simpleClient = new Client("Тестов", "Тест");

                Output($"🛡️ ОПЕРАТОР as (безопасное приведение)\n\n" +
                       $"Оператор as пытается привести тип.\n" +
                       $"Если приведение невозможно, возвращает null\n" +
                       $"(вместо исключения InvalidCastException).\n\n");

                // Пробуем привести к Student
                Student s1 = clientRef as Student;
                Student s2 = simpleClient as Student;

                Output(txtOutput.Text +
                       $"Тест 1: clientRef as Student\n" +
                       $"Результат: {(s1 != null ? "✅ Успешно! " + s1.GetFullName() : "❌ null")}\n\n");

                Output(txtOutput.Text +
                       $"Тест 2: simpleClient as Student\n" +
                       $"Результат: {(s2 != null ? "✅ Успешно! " + s2.GetFullName() : "❌ null (объект не является Student)")}\n\n");

                Output(txtOutput.Text +
                       $"ПРАКТИЧЕСКОЕ ПРИМЕНЕНИЕ:\n\n" +
                       $"Student student = clientRef as Student;\n" +
                       $"if (student != null)\n" +
                       $"{{\n" +
                       $"    // Безопасно используем методы Student\n" +
                       $"    string group = student.Group;\n" +
                       $"}}\n" +
                       $"else\n" +
                       $"{{\n" +
                       $"    // Объект не является Student\n" +
                       $"}}\n\n");

                Output(txtOutput.Text +
                       $"СРАВНЕНИЕ:\n" +
                       $"(Student)obj - бросает исключение при неудаче\n" +
                       $"obj as Student - возвращает null при неудаче\n\n" +
                       $"Используйте as когда:\n" +
                       $"- Не уверены в типе объекта\n" +
                       $"- Хотите избежать исключений\n" +
                       $"- Планируете проверить результат на null");
            }
            catch (Exception ex)
            {
                Output($"❌ ОШИБКА: {ex.Message}");
            }
        }
    }
}
