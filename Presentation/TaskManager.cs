using TaskManager.Application.Interfaces;
using TaskManager.Application.Services;
using TaskManager.Infrastructure.Repositories;

namespace TaskManager.Presentation;

public static class TaskManager
{
    public static void Start()
    {
        TaskRepository repo = new TaskRepository("Server=localhost;Database=TaskManager;Trusted_Connection=true;TrustServerCertificate=true;");
        CommandHandler commands = new CommandHandler(repo);
        bool closeApp = false;
        int choice;

        Console.WriteLine("Вас приветствует приложение TaskManager!");
        
        do
        {
            Console.WriteLine("Выберите действие:\n" +
                              "1) Добавить новую задачу\n" +
                              "2) Просмотреть все задачи\n" +
                              "3) Обновить статус задачи на \"Выполнена\"\n" +
                              "4) Удалить задачу\n" +
                              "5) Выйти из программы");

            try
            {
                if (!Int32.TryParse(Console.ReadLine(), out choice))
                    throw new ArgumentException("Неверный тип! Необходимо ввести число (1-5).");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
                continue;
            }

            switch (choice)
            {
                case 1:
                    commands.AddNewTask();
                    break;
                case 2:
                    commands.ShowAllTasks();
                    break;
                case 3:
                    commands.UpdateTasksStateToCompleted();
                    break;
                case 4:
                    commands.DeleteTask();
                    break;
                case 5:
                    closeApp = true;
                    break;
                default:
                    Console.WriteLine("Необходимо ввести число (1-5).");
                    break;
            }
        }
        while(!closeApp);
    }
}