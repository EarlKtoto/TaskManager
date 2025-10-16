using System.Reflection;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Application.Services;

public class CommandHandler
{
    private ITaskRepository repo;

    public CommandHandler(ITaskRepository repo)
    {
        this.repo = repo;
    }

    public void AddNewTask()
    {
        Console.WriteLine("---Добавление новой задачи---");
        try
        {
            Console.Write("Введите имя задачи: ");
            string title = Console.ReadLine();
            Console.Write("Введите описание задачи: ");
            string description = Console.ReadLine();

            Tasks task = new Tasks()
            {
                Title = title,
                Description = description
            };
            
            repo.Create(task);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
            Console.WriteLine("~~~Запрос прерван из-за ошибки!~~~");
            return;
        }
        Console.WriteLine("===Запрос выполнен успешно!===");
    }

    public void ShowAllTasks()
    {
        Console.WriteLine("---Просмотр всех задач---");
        Console.WriteLine("Id\tTitle\tDescription\tIsCompleted\tCreatedAt");
        try
        {
            foreach (var task in repo.GetTasks())
            {
                Console.WriteLine(task.Id + "\t" + task.Title + "\t" + task.Description + "\t"
                                  +  task.IsCompleted + "\t" + task.CreatedAt);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
            Console.WriteLine("~~~Запрос прерван из-за ошибки!~~~");
            return;
        }
        Console.WriteLine("===Запрос выполнен успешно!===");
    }

    public void UpdateTasksStateToCompleted()
    {
        Console.WriteLine("---Изменение состояния задачи на \"Выполнена\"---");
        try
        {
            Console.Write("Введите id задачи: ");
            if (!Int32.TryParse(Console.ReadLine(), out int id)) throw new ArgumentException("Неверный тип!");
            
            if (repo.GetTask(id) == null) throw new Exception("Задача не найдена!");

            Tasks task = repo.GetTask(id);
            task.IsCompleted = true;
            
            repo.Update(task);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
            Console.WriteLine("~~~Запрос прерван из-за ошибки!~~~");
            return;
        }
        Console.WriteLine("===Запрос выполнен успешно!===");
    }

    public void DeleteTask()
    {
        Console.WriteLine("---Удаление задачи---");
        try
        {
            Console.Write("Введите id задачи: ");
            if (!Int32.TryParse(Console.ReadLine(), out int id)) throw new ArgumentException("Неверный тип!");

            if (repo.GetTask(id) == null) throw new Exception("Задача не найдена!");
            
            repo.Delete(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
            Console.WriteLine("~~~Запрос прерван из-за ошибки!~~~");
            return;
        }
        Console.WriteLine("===Запрос выполнен успешно!===");
    }
}