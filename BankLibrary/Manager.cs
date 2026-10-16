using System;

namespace BankLibrary
{
    /// <summary>
    /// ИЗОЛИРОВАННЫЙ КЛАСС (sealed class) - Менеджер банка.
    /// От этого класса НЕЛЬЗЯ наследоваться.
    /// Используется для демонстрации модификатора sealed в ЛР4.
    /// </summary>
    public sealed class Manager : Employee
    {
        /// <summary>
        /// Отдел, в котором работает менеджер.
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Количество подчиненных.
        /// </summary>
        public int SubordinatesCount { get; set; }

        // КОНСТРУКТОРЫ

        /// <summary>
        /// Конструктор по умолчанию.
        /// </summary>
        public Manager() : base()
        {
            DepartmentName = "";
            SubordinatesCount = 0;
            Position = "Менеджер";
        }

        /// <summary>
        /// Конструктор с параметрами.
        /// </summary>
        public Manager(string lastName, string firstName, int employeeId, string departmentName)
            : base(lastName, firstName, employeeId)
        {
            this.DepartmentName = departmentName;
            this.SubordinatesCount = 0;
            this.Position = "Менеджер";
        }

        // ==================== РЕАЛИЗАЦИЯ АБСТРАКТНЫХ МЕТОДОВ ====================

        /// <summary>
        /// РЕАЛИЗАЦИЯ АБСТРАКТНОГО МЕТОДА: Получение текста с именем менеджера.
        /// </summary>
        public override string NameText()
        {
            return $"Менеджер {GetFullName()}, отдел: {DepartmentName}";
        }

        /// <summary>
        /// РЕАЛИЗАЦИЯ АБСТРАКТНОГО СВОЙСТВА: Краткое описание роли менеджера.
        /// </summary>
        public override string RoleDescription
        {
            get
            {
                return $"Менеджер отдела {DepartmentName}, в подчинении {SubordinatesCount} человек";
            }
        }

        // ==================== ПЕРЕОПРЕДЕЛЕНИЕ ВИРТУАЛЬНЫХ МЕТОДОВ ====================

        /// <summary>
        /// ПЕРЕОПРЕДЕЛЕНИЕ ВИРТУАЛЬНОГО МЕТОДА: Отображение информации о менеджере.
        /// </summary>
        public override string ShowInfo()
        {
            string baseInfo = base.ShowInfo();
            return baseInfo +
                   $"\nОтдел: {DepartmentName}" +
                   $"\nПодчиненных: {SubordinatesCount}" +
                   $"\n\n⚠️ Это SEALED CLASS - от этого класса НЕЛЬЗЯ наследоваться!";
        }

        /// <summary>
        /// Дополнительный метод для демонстрации функциональности менеджера.
        /// </summary>
        public string GetManagementInfo()
        {
            return $"Менеджер {GetFullName()}\n" +
                   $"Отдел: {DepartmentName}\n" +
                   $"Табельный номер: {EmployeeId}\n" +
                   $"Подчиненных: {SubordinatesCount}\n" +
                   $"Стаж: {GetExperience()} лет";
        }
    }
}
