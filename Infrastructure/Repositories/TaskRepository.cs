using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private string connectionString;

    public TaskRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public Tasks? GetTask(int id)
    {
        using (SqlConnection dbConnection = new SqlConnection(connectionString))
        {
            return dbConnection.Query<Tasks>("SELECT * FROM Tasks WHERE Id = @id", new { id }).FirstOrDefault();
        }
    }
    
    public List<Tasks> GetTasks()
    {
        using (SqlConnection dbConnection = new SqlConnection(connectionString))
        {
            return dbConnection.Query<Tasks>("Select * from Tasks").ToList();    
        }
    }

    public void Create(Tasks task)
    {
        using (SqlConnection dbConnection = new SqlConnection(connectionString))
        {
            var sqlQuery = "Insert Into Tasks (Title, Description) Values (@Title, @Description)";
            dbConnection.Execute(sqlQuery, task);
        }
    }

    public void Update(Tasks task)
    {
        using (SqlConnection dbConnection = new SqlConnection(connectionString))
        {
            var sqlQuery = "Update Tasks Set Title = @title, Description = @description, IsCompleted = @isCompleted Where Id = @id";
            dbConnection.Execute(sqlQuery, new { task.Title, task.Description, task.IsCompleted, task.Id });
        }
    }

    public void Delete(int id)
    {
        using (SqlConnection dbConnection = new SqlConnection(connectionString))
        {
            var sqlQuery = "Delete From Tasks Where Id = @id";
            dbConnection.Execute(sqlQuery, new { id });
        }
    }
}