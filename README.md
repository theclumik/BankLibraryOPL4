# Лабораторная работа №4 - Переопределение свойств и методов (Полиморфизм)

## 📋 Описание проекта

Приложение для управления сотрудниками, студентами и преподавателями, демонстрирующее **полиморфизм** - ключевую концепцию ООП. Включает абстрактные классы, виртуальные методы, переопределение и сокрытие членов класса.

## 🎯 Цели лабораторной работы

1. Работа с **абстрактными классами** и методами
2. Реализация **виртуальных методов** (virtual/override)
3. Демонстрация **полиморфизма** в действии
4. Применение **сокрытия методов** (new)
5. Использование **sealed override** (запрет переопределения)
6. Создание **изолированного класса** (sealed class)

## 🏗️ Иерархия классов (обновлённая в ЛР4)

```
Object
  └── Client
      └── Employee (АБСТРАКТНЫЙ КЛАСС) ⭐
          ├── Prepod (Преподаватель)
          ├── Student (Студент)
          │   └── StudentDistant (Студент-заочник)
          └── Manager (SEALED CLASS) ⭐ NEW
```

### Ключевые изменения в ЛР4:

**1. Employee - абстрактный класс:**
- Нельзя создать `new Employee()`
- Содержит абстрактные методы для реализации в наследниках
- Содержит виртуальные методы для переопределения

**2. Новый класс Manager:**
- Изолированный класс (sealed) - от него нельзя наследоваться

## 📚 Новая функциональность

### 1. Абстрактный класс Employee

```csharp
public abstract class Employee : Client
{
    // АБСТРАКТНЫЕ МЕТОДЫ - обязательны для реализации
    public abstract string NameText();
    public abstract string RoleDescription { get; }
    
    // ВИРТУАЛЬНЫЕ МЕТОДЫ - могут быть переопределены
    public virtual string ShowInfo() { ... }
    public virtual DateTime DateOfBirth { get; set; }
}
```

### 2. Реализация в классах-наследниках

**Класс Prepod:**
```csharp
public class Prepod : Employee
{
    // Реализация абстрактного метода
    public override string NameText()
    {
        return $"Преподаватель {GetFullName()}, кафедра {Department}";
    }
    
    // Sealed override - запрет дальнейшего переопределения
    public sealed override string ShowInfo()
    {
        return base.ShowInfo() + $"\nКафедра: {Department}";
    }
}
```

**Класс Student:**
```csharp
public class Student : Employee
{
    // Реализация абстрактного метода
    public override string NameText()
    {
        return $"Студент {GetFullName()}, группа {Group}";
    }
    
    // Переопределение свойства с проверкой
    public override DateTime DateOfBirth
    {
        get { return base.DateOfBirth; }
        set
        {
            int age = Client.CalculateAge(value);
            if (age < 16 || age > 35)
                throw new ArgumentException("Возраст 16-35 лет!");
            base.DateOfBirth = value;
        }
    }
    
    // Сокрытие метода (new) - НЕ переопределение
    public new void WriteToFile(string path)
    {
        // Новая реализация
    }
}
```

**Класс Manager (sealed class):**
```csharp
public sealed class Manager : Employee
{
    // От этого класса НЕЛЬЗЯ наследоваться
    
    public override string NameText()
    {
        return $"Менеджер {GetFullName()}, отдел: {DepartmentName}";
    }
}
```

## 🔑 Ключевые концепции

### Override vs New

| Аспект | override | new |
|--------|----------|-----|
| Полиморфизм | ✅ Да | ❌ Нет |
| Виртуальный вызов | ✅ Да | ❌ Нет |
| Требует virtual в базовом классе | ✅ Да | ❌ Нет |

**Пример:**
```csharp
Student student = new Student();
Employee empRef = student;

// С override:
empRef.ShowInfo();  // Вызовется Student.ShowInfo()

// С new:
empRef.WriteToFile("file.txt");  // Вызовется Employee.WriteToFile()
```

### Полиморфизм в действии

```csharp
Employee[] employees = {
    new Prepod("Иванов", "Иван", 101, "Информатики"),
    new Student("Петров", "Петр", "ПИ-101", 2),
    new Manager("Сидоров", "Сидор", 201, "Продаж")
};

foreach (Employee emp in employees)
{
    Console.WriteLine(emp.NameText());  // Полиморфизм!
}
```

## 📊 Выполненные требования ЛР4

✅ Абстрактный класс Employee с абстрактными методами  
✅ Виртуальные методы и свойства  
✅ Переопределение методов (override)  
✅ Переопределение свойств с валидацией  
✅ Запрет переопределения (sealed override)  
✅ Сокрытие методов (new)  
✅ Изолированный класс Manager (sealed class)  

## 🚀 Запуск проекта

1. Откройте `BankLibrary.slnx` в Visual Studio
2. Постройте решение (Build → Build Solution)
3. Запустите BankTestApp (F5)

## 💡 Отличия от ЛР3

| Аспект | ЛР3 | ЛР4 |
|--------|-----|-----|
| Employee | Обычный класс | Абстрактный класс |
| Виртуальные методы | ❌ | ✅ |
| Sealed override | ❌ | ✅ |
| Sealed class | ❌ | ✅ (Manager) |
| Сокрытие методов | ❌ | ✅ |

## 👨‍💻 Автор

Лабораторная работа №4 по ООП  
Октябрь 2026

## 🔗 Связанные репозитории

- **ЛР3:** https://github.com/theclumik/BankLibraryOPL3 (Наследование)
- **ЛР4:** Этот репозиторий (Полиморфизм)
