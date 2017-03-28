using FluentValidation;
using System.Collections.Generic;
using ToDO.Application.Concrete;
using ToDO.Domain.Entities;

namespace ToDO.Application.Interfaces
{
    public interface ITaskService /*: IAppService<TaskEntity>*/
    {
        Task GetTaskById(int taskId);

        IEnumerable<Task> GetAllTasks();

        int CreateTask(Task taskEntity, User user);

        bool UpdateTask(int taskId, Task taskEntity);

        bool DeleteTask(int taskId);

        bool IsValid(Task entity);

        IList<ErrorMessage> Errors(Task entity);

        IList<ErrorMessage> Errors(Task entity, AbstractValidator<Task> validator);
    }
}