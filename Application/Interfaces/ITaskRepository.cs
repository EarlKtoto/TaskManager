using TaskManager.Domain.Entities;

namespace TaskManager.Application.Interfaces;

public interface ITaskRepository
{
    public void Create(Tasks task);
    public Tasks? GetTask(int id);
    public List<Tasks> GetTasks();
    public void Update(Tasks task);
    public void Delete(int id);
}



