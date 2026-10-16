using System.Drawing;
using System.Windows.Forms;

namespace BankTestApp
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        // Элементы управления
        private ListBox listBoxClients;
        private Button btnAddClient;
        private Button btnStaticMethod;

        private TextBox txtLastName;
        private TextBox txtFirstName;
        private TextBox txtBalance;
        private DateTimePicker dtpBirthDate;
        private CheckBox chkIsVip;

        private PictureBox pictureBoxFoto;
        private Button btnLoadFoto;
        private OpenFileDialog openFileDialogFoto;

        private TextBox txtOutput;

        // --- НОВЫЕ ЭЛЕМЕНТЫ ДЛЯ ЛАБ. РАБОТЫ №3 ---

        // Работа с файлами
        private Button btnSaveToFile;
        private Button btnLoadFromFile;
        private SaveFileDialog saveFileDialog;
        private OpenFileDialog openFileDialog;

        // Тестирование передачи параметров
        private Button btnTestByValue;
        private Button btnTestByRef;
        private Button btnTestOut;
        private Button btnTestOptional;

        // Работа с наследованием
        private TabControl tabControl;
        private TabPage tabClient;
        private TabPage tabEmployee;
        private TabPage tabPrepod;
        private TabPage tabStudent;
        private TabPage tabStudentDistant;
        private TabPage tabTypeCasting;

        // Элементы для Employee
        private TextBox txtEmployeeId;
        private TextBox txtPosition;
        private DateTimePicker dtpHireDate;
        private Button btnCreateEmployee;
        private Button btnShowEmployeeInfo;

        // Элементы для Prepod
        private TextBox txtDepartment;
        private ListBox lstDisciplines;
        private TextBox txtNewDiscipline;
        private Button btnAddDiscipline;
        private Button btnCreatePrepod;

        // Элементы для Student
        private TextBox txtGroup;
        private NumericUpDown numCourse;
        private NumericUpDown numAverageGrade;
        private TextBox txtSpecialty;
        private Button btnCreateStudent;

        // Элементы для StudentDistant
        private TextBox txtSessionCity;
        private DateTimePicker dtpNextSession;
        private TextBox txtWorkPlace;
        private Button btnCreateStudentDistant;

        // Элементы для преобразования типов
        private Button btnUpCasting;
        private Button btnDownCasting;
        private Button btnTestIs;
        private Button btnTestAs;

        private void InitializeComponent()
        {
            this.listBoxClients = new ListBox();
            this.btnAddClient = new Button();
            this.btnStaticMethod = new Button();
            this.txtLastName = new TextBox();
            this.txtFirstName = new TextBox();
            this.txtBalance = new TextBox();
            this.dtpBirthDate = new DateTimePicker();
            this.chkIsVip = new CheckBox();
            this.pictureBoxFoto = new PictureBox();
            this.btnLoadFoto = new Button();
            this.openFileDialogFoto = new OpenFileDialog();
            this.txtOutput = new TextBox();

            // Новые элементы для ЛР№3
            this.btnSaveToFile = new Button();
            this.btnLoadFromFile = new Button();
            this.saveFileDialog = new SaveFileDialog();
            this.openFileDialog = new OpenFileDialog();
            this.btnTestByValue = new Button();
            this.btnTestByRef = new Button();
            this.btnTestOut = new Button();
            this.btnTestOptional = new Button();

            this.tabControl = new TabControl();
            this.tabClient = new TabPage();
            this.tabEmployee = new TabPage();
            this.tabPrepod = new TabPage();
            this.tabStudent = new TabPage();
            this.tabStudentDistant = new TabPage();
            this.tabTypeCasting = new TabPage();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFoto)).BeginInit();
            this.SuspendLayout();

            // --- ОСНОВНАЯ КОМПОНОВКА (TabControl) ---
            this.tabControl.Location = new Point(20, 20);
            this.tabControl.Size = new Size(1160, 600);
            this.tabControl.TabPages.Add(this.tabClient);
            this.tabControl.TabPages.Add(this.tabEmployee);
            this.tabControl.TabPages.Add(this.tabPrepod);
            this.tabControl.TabPages.Add(this.tabStudent);
            this.tabControl.TabPages.Add(this.tabStudentDistant);
            this.tabControl.TabPages.Add(this.tabTypeCasting);

            this.tabClient.Text = "Client (базовый класс)";
            this.tabEmployee.Text = "Employee (сотрудник)";
            this.tabPrepod.Text = "Prepod (преподаватель)";
            this.tabStudent.Text = "Student (студент)";
            this.tabStudentDistant.Text = "StudentDistant (заочник)";
            this.tabTypeCasting.Text = "Преобразования типов";

            // ========== TAB 1: CLIENT ==========
            InitializeClientTab();

            // ========== TAB 2: EMPLOYEE ==========
            InitializeEmployeeTab();

            // ========== TAB 3: PREPOD ==========
            InitializePrepodTab();

            // ========== TAB 4: STUDENT ==========
            InitializeStudentTab();

            // ========== TAB 5: STUDENT DISTANT ==========
            InitializeStudentDistantTab();

            // ========== TAB 6: TYPE CASTING ==========
            InitializeTypeCastingTab();

            // --- Вывод информации (общий) ---
            this.txtOutput.Location = new Point(20, 630);
            this.txtOutput.Size = new Size(1160, 150);
            this.txtOutput.Multiline = true;
            this.txtOutput.ReadOnly = true;
            this.txtOutput.ScrollBars = ScrollBars.Vertical;
            this.txtOutput.Font = new Font("Consolas", 9F);

            // --- Настройки формы ---
            this.ClientSize = new Size(1200, 800);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.txtOutput);

            this.Font = new Font("Segoe UI", 9F);
            this.Text = "Лаб. работа 3: Наследование, Файлы, Параметры";
            this.StartPosition = FormStartPosition.CenterScreen;

            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxFoto)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        // Инициализация вкладки Client
        private void InitializeClientTab()
        {
            // Левая панель: Список клиентов
            this.listBoxClients.FormattingEnabled = true;
            this.listBoxClients.Location = new Point(20, 50);
            this.listBoxClients.Size = new Size(280, 350);
            this.listBoxClients.SelectedIndexChanged += new System.EventHandler(this.listBoxClients_SelectedIndexChanged);

            Label lblClients = new Label() { Text = "Список клиентов:", Location = new Point(20, 25), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };

            this.btnAddClient.Text = "Добавить клиента";
            this.btnAddClient.Location = new Point(20, 410);
            this.btnAddClient.Size = new Size(135, 30);
            this.btnAddClient.Click += new System.EventHandler(this.btnAddClient_Click);

            this.btnStaticMethod.Text = "Статический метод";
            this.btnStaticMethod.Location = new Point(165, 410);
            this.btnStaticMethod.Size = new Size(135, 30);
            this.btnStaticMethod.Click += new System.EventHandler(this.btnStaticMethod_Click);

            // Средняя панель: Поля данных
            Label lblFields = new Label() { Text = "Данные клиента:", Location = new Point(320, 25), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };

            Label lbl1 = new Label() { Text = "Фамилия:", Location = new Point(320, 55), AutoSize = true };
            this.txtLastName.Location = new Point(320, 75); this.txtLastName.Size = new Size(200, 23);

            Label lbl2 = new Label() { Text = "Имя:", Location = new Point(320, 105), AutoSize = true };
            this.txtFirstName.Location = new Point(320, 125); this.txtFirstName.Size = new Size(200, 23);

            Label lbl3 = new Label() { Text = "Баланс:", Location = new Point(320, 155), AutoSize = true };
            this.txtBalance.Location = new Point(320, 175); this.txtBalance.Size = new Size(200, 23);

            Label lbl4 = new Label() { Text = "Дата рождения:", Location = new Point(320, 205), AutoSize = true };
            this.dtpBirthDate.Location = new Point(320, 225); this.dtpBirthDate.Size = new Size(200, 23);
            this.dtpBirthDate.Format = DateTimePickerFormat.Short;

            this.chkIsVip.Text = "VIP-клиент";
            this.chkIsVip.Location = new Point(320, 255); this.chkIsVip.AutoSize = true;

            // Кнопки работы с файлами
            Label lblFiles = new Label() { Text = "Работа с файлами:", Location = new Point(320, 295), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };

            this.btnSaveToFile.Text = "Сохранить в файл";
            this.btnSaveToFile.Location = new Point(320, 320);
            this.btnSaveToFile.Size = new Size(200, 30);
            this.btnSaveToFile.Click += new System.EventHandler(this.btnSaveToFile_Click);

            this.btnLoadFromFile.Text = "Загрузить из файла";
            this.btnLoadFromFile.Location = new Point(320, 360);
            this.btnLoadFromFile.Size = new Size(200, 30);
            this.btnLoadFromFile.Click += new System.EventHandler(this.btnLoadFromFile_Click);

            this.saveFileDialog.Filter = "Текстовые файлы|*.txt";
            this.openFileDialog.Filter = "Текстовые файлы|*.txt";

            // Кнопки тестирования параметров
            Label lblParams = new Label() { Text = "Передача параметров:", Location = new Point(320, 405), AutoSize = true, Font = new Font("Segoe UI", 9F, FontStyle.Bold) };

            this.btnTestByValue.Text = "По значению";
            this.btnTestByValue.Location = new Point(320, 430);
            this.btnTestByValue.Size = new Size(95, 28);
            this.btnTestByValue.Click += new System.EventHandler(this.btnTestByValue_Click);

            this.btnTestByRef.Text = "По ссылке (ref)";
            this.btnTestByRef.Location = new Point(425, 430);
            this.btnTestByRef.Size = new Size(95, 28);
            this.btnTestByRef.Click += new System.EventHandler(this.btnTestByRef_Click);

            this.btnTestOut.Text = "Out параметр";
            this.btnTestOut.Location = new Point(320, 468);
            this.btnTestOut.Size = new Size(95, 28);
            this.btnTestOut.Click += new System.EventHandler(this.btnTestOut_Click);

            this.btnTestOptional.Text = "Необязательные";
            this.btnTestOptional.Location = new Point(425, 468);
            this.btnTestOptional.Size = new Size(95, 28);
            this.btnTestOptional.Click += new System.EventHandler(this.btnTestOptional_Click);

            // Правая панель: Фото
            this.pictureBoxFoto.Location = new Point(560, 50);
            this.pictureBoxFoto.Size = new Size(280, 320);
            this.pictureBoxFoto.BorderStyle = BorderStyle.FixedSingle;
            this.pictureBoxFoto.SizeMode = PictureBoxSizeMode.Zoom;

            this.btnLoadFoto.Text = "Загрузить фото";
            this.btnLoadFoto.Location = new Point(560, 380);
            this.btnLoadFoto.Size = new Size(280, 30);
            this.btnLoadFoto.Click += new System.EventHandler(this.btnLoadFoto_Click);

            this.openFileDialogFoto.Filter = "Изображения|*.jpg;*.png;*.bmp";

            // Добавляем на вкладку
            this.tabClient.Controls.Add(lblClients);
            this.tabClient.Controls.Add(this.listBoxClients);
            this.tabClient.Controls.Add(this.btnAddClient);
            this.tabClient.Controls.Add(this.btnStaticMethod);

            this.tabClient.Controls.Add(lblFields);
            this.tabClient.Controls.Add(lbl1); this.tabClient.Controls.Add(this.txtLastName);
            this.tabClient.Controls.Add(lbl2); this.tabClient.Controls.Add(this.txtFirstName);
            this.tabClient.Controls.Add(lbl3); this.tabClient.Controls.Add(this.txtBalance);
            this.tabClient.Controls.Add(lbl4); this.tabClient.Controls.Add(this.dtpBirthDate);
            this.tabClient.Controls.Add(this.chkIsVip);

            this.tabClient.Controls.Add(lblFiles);
            this.tabClient.Controls.Add(this.btnSaveToFile);
            this.tabClient.Controls.Add(this.btnLoadFromFile);

            this.tabClient.Controls.Add(lblParams);
            this.tabClient.Controls.Add(this.btnTestByValue);
            this.tabClient.Controls.Add(this.btnTestByRef);
            this.tabClient.Controls.Add(this.btnTestOut);
            this.tabClient.Controls.Add(this.btnTestOptional);

            this.tabClient.Controls.Add(this.pictureBoxFoto);
            this.tabClient.Controls.Add(this.btnLoadFoto);
        }

        // Инициализация вкладки Employee
        private void InitializeEmployeeTab()
        {
            this.txtEmployeeId = new TextBox();
            this.txtPosition = new TextBox();
            this.dtpHireDate = new DateTimePicker();
            this.btnCreateEmployee = new Button();
            this.btnShowEmployeeInfo = new Button();

            Label lbl1 = new Label() { Text = "Табельный номер:", Location = new Point(30, 30), AutoSize = true };
            this.txtEmployeeId.Location = new Point(30, 50); this.txtEmployeeId.Size = new Size(200, 23);

            Label lbl2 = new Label() { Text = "Должность:", Location = new Point(30, 80), AutoSize = true };
            this.txtPosition.Location = new Point(30, 100); this.txtPosition.Size = new Size(200, 23);

            Label lbl3 = new Label() { Text = "Дата приёма на работу:", Location = new Point(30, 130), AutoSize = true };
            this.dtpHireDate.Location = new Point(30, 150); this.dtpHireDate.Size = new Size(200, 23);
            this.dtpHireDate.Format = DateTimePickerFormat.Short;

            this.btnCreateEmployee.Text = "Создать сотрудника";
            this.btnCreateEmployee.Location = new Point(30, 190);
            this.btnCreateEmployee.Size = new Size(200, 35);
            this.btnCreateEmployee.Click += new System.EventHandler(this.btnCreateEmployee_Click);

            this.btnShowEmployeeInfo.Text = "Показать информацию";
            this.btnShowEmployeeInfo.Location = new Point(30, 235);
            this.btnShowEmployeeInfo.Size = new Size(200, 35);
            this.btnShowEmployeeInfo.Click += new System.EventHandler(this.btnShowEmployeeInfo_Click);

            this.tabEmployee.Controls.Add(lbl1); this.tabEmployee.Controls.Add(this.txtEmployeeId);
            this.tabEmployee.Controls.Add(lbl2); this.tabEmployee.Controls.Add(this.txtPosition);
            this.tabEmployee.Controls.Add(lbl3); this.tabEmployee.Controls.Add(this.dtpHireDate);
            this.tabEmployee.Controls.Add(this.btnCreateEmployee);
            this.tabEmployee.Controls.Add(this.btnShowEmployeeInfo);
        }

        // Инициализация вкладки Prepod
        private void InitializePrepodTab()
        {
            this.txtDepartment = new TextBox();
            this.lstDisciplines = new ListBox();
            this.txtNewDiscipline = new TextBox();
            this.btnAddDiscipline = new Button();
            this.btnCreatePrepod = new Button();

            Label lbl1 = new Label() { Text = "Кафедра:", Location = new Point(30, 30), AutoSize = true };
            this.txtDepartment.Location = new Point(30, 50); this.txtDepartment.Size = new Size(250, 23);

            Label lbl2 = new Label() { Text = "Дисциплины:", Location = new Point(30, 90), AutoSize = true };
            this.lstDisciplines.Location = new Point(30, 110); this.lstDisciplines.Size = new Size(250, 150);

            Label lbl3 = new Label() { Text = "Новая дисциплина:", Location = new Point(30, 275), AutoSize = true };
            this.txtNewDiscipline.Location = new Point(30, 295); this.txtNewDiscipline.Size = new Size(180, 23);

            this.btnAddDiscipline.Text = "+";
            this.btnAddDiscipline.Location = new Point(220, 295);
            this.btnAddDiscipline.Size = new Size(60, 23);
            this.btnAddDiscipline.Click += new System.EventHandler(this.btnAddDiscipline_Click);

            this.btnCreatePrepod.Text = "Создать преподавателя";
            this.btnCreatePrepod.Location = new Point(30, 335);
            this.btnCreatePrepod.Size = new Size(250, 35);
            this.btnCreatePrepod.Click += new System.EventHandler(this.btnCreatePrepod_Click);

            this.tabPrepod.Controls.Add(lbl1); this.tabPrepod.Controls.Add(this.txtDepartment);
            this.tabPrepod.Controls.Add(lbl2); this.tabPrepod.Controls.Add(this.lstDisciplines);
            this.tabPrepod.Controls.Add(lbl3); this.tabPrepod.Controls.Add(this.txtNewDiscipline);
            this.tabPrepod.Controls.Add(this.btnAddDiscipline);
            this.tabPrepod.Controls.Add(this.btnCreatePrepod);
        }

        // Инициализация вкладки Student
        private void InitializeStudentTab()
        {
            this.txtGroup = new TextBox();
            this.numCourse = new NumericUpDown();
            this.numAverageGrade = new NumericUpDown();
            this.txtSpecialty = new TextBox();
            this.btnCreateStudent = new Button();

            Label lbl1 = new Label() { Text = "Группа:", Location = new Point(30, 30), AutoSize = true };
            this.txtGroup.Location = new Point(30, 50); this.txtGroup.Size = new Size(200, 23);

            Label lbl2 = new Label() { Text = "Курс (1-5):", Location = new Point(30, 80), AutoSize = true };
            this.numCourse.Location = new Point(30, 100); this.numCourse.Size = new Size(200, 23);
            this.numCourse.Minimum = 1; this.numCourse.Maximum = 5; this.numCourse.Value = 1;

            Label lbl3 = new Label() { Text = "Средний балл (2.0-5.0):", Location = new Point(30, 130), AutoSize = true };
            this.numAverageGrade.Location = new Point(30, 150); this.numAverageGrade.Size = new Size(200, 23);
            this.numAverageGrade.Minimum = 2; this.numAverageGrade.Maximum = 5; this.numAverageGrade.DecimalPlaces = 2; this.numAverageGrade.Increment = 0.1m; this.numAverageGrade.Value = 4.0m;

            Label lbl4 = new Label() { Text = "Специальность:", Location = new Point(30, 180), AutoSize = true };
            this.txtSpecialty.Location = new Point(30, 200); this.txtSpecialty.Size = new Size(300, 23);

            this.btnCreateStudent.Text = "Создать студента";
            this.btnCreateStudent.Location = new Point(30, 240);
            this.btnCreateStudent.Size = new Size(200, 35);
            this.btnCreateStudent.Click += new System.EventHandler(this.btnCreateStudent_Click);

            this.tabStudent.Controls.Add(lbl1); this.tabStudent.Controls.Add(this.txtGroup);
            this.tabStudent.Controls.Add(lbl2); this.tabStudent.Controls.Add(this.numCourse);
            this.tabStudent.Controls.Add(lbl3); this.tabStudent.Controls.Add(this.numAverageGrade);
            this.tabStudent.Controls.Add(lbl4); this.tabStudent.Controls.Add(this.txtSpecialty);
            this.tabStudent.Controls.Add(this.btnCreateStudent);
        }

        // Инициализация вкладки StudentDistant
        private void InitializeStudentDistantTab()
        {
            this.txtSessionCity = new TextBox();
            this.dtpNextSession = new DateTimePicker();
            this.txtWorkPlace = new TextBox();
            this.btnCreateStudentDistant = new Button();

            Label lbl1 = new Label() { Text = "Город проживания:", Location = new Point(30, 30), AutoSize = true };
            this.txtSessionCity.Location = new Point(30, 50); this.txtSessionCity.Size = new Size(250, 23);

            Label lbl2 = new Label() { Text = "Дата следующей сессии:", Location = new Point(30, 80), AutoSize = true };
            this.dtpNextSession.Location = new Point(30, 100); this.dtpNextSession.Size = new Size(250, 23);
            this.dtpNextSession.Format = DateTimePickerFormat.Short;

            Label lbl3 = new Label() { Text = "Место работы:", Location = new Point(30, 130), AutoSize = true };
            this.txtWorkPlace.Location = new Point(30, 150); this.txtWorkPlace.Size = new Size(300, 23);

            this.btnCreateStudentDistant.Text = "Создать студента-заочника";
            this.btnCreateStudentDistant.Location = new Point(30, 190);
            this.btnCreateStudentDistant.Size = new Size(250, 35);
            this.btnCreateStudentDistant.Click += new System.EventHandler(this.btnCreateStudentDistant_Click);

            this.tabStudentDistant.Controls.Add(lbl1); this.tabStudentDistant.Controls.Add(this.txtSessionCity);
            this.tabStudentDistant.Controls.Add(lbl2); this.tabStudentDistant.Controls.Add(this.dtpNextSession);
            this.tabStudentDistant.Controls.Add(lbl3); this.tabStudentDistant.Controls.Add(this.txtWorkPlace);
            this.tabStudentDistant.Controls.Add(this.btnCreateStudentDistant);
        }

        // Инициализация вкладки TypeCasting
        private void InitializeTypeCastingTab()
        {
            this.btnUpCasting = new Button();
            this.btnDownCasting = new Button();
            this.btnTestIs = new Button();
            this.btnTestAs = new Button();

            Label lblInfo = new Label()
            {
                Text = "Преобразования типов (UpCasting / DownCasting)",
                Location = new Point(30, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            this.btnUpCasting.Text = "UpCasting (Student → Client)";
            this.btnUpCasting.Location = new Point(30, 60);
            this.btnUpCasting.Size = new Size(300, 40);
            this.btnUpCasting.Click += new System.EventHandler(this.btnUpCasting_Click);

            this.btnDownCasting.Text = "DownCasting (Client → Student)";
            this.btnDownCasting.Location = new Point(30, 110);
            this.btnDownCasting.Size = new Size(300, 40);
            this.btnDownCasting.Click += new System.EventHandler(this.btnDownCasting_Click);

            this.btnTestIs.Text = "Проверка типа (оператор is)";
            this.btnTestIs.Location = new Point(30, 160);
            this.btnTestIs.Size = new Size(300, 40);
            this.btnTestIs.Click += new System.EventHandler(this.btnTestIs_Click);

            this.btnTestAs.Text = "Безопасное приведение (оператор as)";
            this.btnTestAs.Location = new Point(30, 210);
            this.btnTestAs.Size = new Size(300, 40);
            this.btnTestAs.Click += new System.EventHandler(this.btnTestAs_Click);

            this.tabTypeCasting.Controls.Add(lblInfo);
            this.tabTypeCasting.Controls.Add(this.btnUpCasting);
            this.tabTypeCasting.Controls.Add(this.btnDownCasting);
            this.tabTypeCasting.Controls.Add(this.btnTestIs);
            this.tabTypeCasting.Controls.Add(this.btnTestAs);
        }
    }
}