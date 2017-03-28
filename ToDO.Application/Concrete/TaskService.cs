using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using ToDO.Application.Interfaces;
using ToDO.Application.Validators;
using ToDO.Domain.Entities;
using ToDO.Infraestructure.DataModel;
using ToDO.Infraestructure.UnitOfWork;

namespace ToDO.Application.Concrete
{
    public class TaskService : ITaskService
    {
        private readonly UnitOfWork _unitOfWork;

        public TaskService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public Task GetTaskById(int taskId)
        {
            var task = _unitOfWork.TaskRepository.GetById(taskId);

            if (task == null) return null;

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<TaskDataModel, Task>(); });
            var mapper = config.CreateMapper();
            var taskModel = mapper.Map<TaskDataModel, Task>(task);
            return taskModel;
        }

        public IEnumerable<Task> GetAllTasks()
        {
            var task = _unitOfWork.TaskRepository.GetAll().ToList();

            if (task.Any()) return null;

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<TaskDataModel, Task>(); });
            var mapper = config.CreateMapper();
            var tasksModel = mapper.Map<List<TaskDataModel>, List<Task>>(task);
            return tasksModel;
        }

        public int CreateTask(Task taskEntity, User user)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserDataModel>(); });
            var mapper = config.CreateMapper();
            var userModel = mapper.Map<User, UserDataModel>(user);

            using (var scope = new TransactionScope())
            {
                var task = new TaskDataModel
                {
                    Title = taskEntity.Title,
                    Description = taskEntity.Description,
                    IsCompleted = taskEntity.IsCompleted,
                    CreatedAt = taskEntity.CreatedAt,
                    User = userModel
                };
                _unitOfWork.TaskRepository.Insert(task);
                _unitOfWork.SaveChanges();
                scope.Complete();
                return task.Id;
            }
        }

        public bool UpdateTask(int taskId, Task taskEntity)
        {
            if (taskEntity == null) return false;

            using (var scope = new TransactionScope())
            {
                var task = _unitOfWork.TaskRepository.GetById(taskId);

                if (task == null) return false;

                task.Title = taskEntity.Title;
                task.Description = taskEntity.Description;
                task.IsCompleted = taskEntity.IsCompleted;
                task.LastModified = DateTime.Now;

                _unitOfWork.TaskRepository.Update(task);
                _unitOfWork.SaveChanges();
                scope.Complete();
            }
            return true;
        }

        public bool DeleteTask(int taskId)
        {
            if (taskId <= 0) return false;
            using (var scope = new TransactionScope())
            {
                var task = _unitOfWork.TaskRepository.GetById(taskId);
                if (task == null) return false;
                _unitOfWork.TaskRepository.Delete(task);
                _unitOfWork.SaveChanges();
                scope.Complete();
            }
            return true;
        }

        public bool IsValid(Task entity)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Task, TaskDataModel>(); });
            var mapper = config.CreateMapper();
            var taskModel = mapper.Map<Task, TaskDataModel>(entity);

            var validator = new TaskValidator();
            var result = validator.Validate(taskModel);

            return result.IsValid;
        }

        public IList<ErrorMessage> Errors(Task entity)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<Task, TaskDataModel>(); });
            var mapper = config.CreateMapper();
            var taskModel = mapper.Map<Task, TaskDataModel>(entity);

            var validator = new TaskValidator();
            var result = validator.Validate(taskModel);

            return
                result.Errors.Select(item => new ErrorMessage {Key = item.PropertyName, Error = item.ErrorMessage})
                    .ToList();
        }

        public IList<ErrorMessage> Errors(Task entity, AbstractValidator<Task> validator)
        {
            throw new NotImplementedException();
        }

        /*
        private readonly IRepository<Task> _repository;

        public TaskService(IRepository<Task> repository)
        {
            _repository = repository;
        }

        public void Insert(Task entity)
        {
            _repository.Insert(entity);
        }

        public void Update(Task entity)
        {
            _repository.Update(entity);
        }

        public int SaveChanges()
        {
            return _repository.SaveChanges();
        }

        public void DeleteHard(Task entity)
        {
            _repository.DeleteHard(entity);
        }

        public void DeleteSoft(Task entity)
        {
            _repository.DeleteSoft(entity);
        }

        public Task Find(Task entity)
        {
            return _repository.Find(entity);
        }

        public Task GetById(int id)
        {
            return _repository.GetById(id);
        }

        public bool IsValid(Task entity)
        {
            var validator = new TaskValidator();
            var result = validator.Validate(entity);

            return result.IsValid;
        }

        public IList<ErrorMessage> Errors(Task entity)
        {
            var validator = new TaskValidator();
            var result = validator.Validate(entity);

            return
                result.Errors.Select(item => new ErrorMessage {Key = item.PropertyName, Error = item.ErrorMessage})
                    .ToList();
        }

        public ICollection<Task> ToList()
        {
            return _repository.Get();
        }

        public IList<ErrorMessage> Errors(Task entity, AbstractValidator<User> validator)
        {
            throw new NotImplementedException();
        }

        public ICollection<Task> ContainsWord(string word)
        {
            return ToList()
                .Where(x => x.Title.Contains(word) || x.Description.Contains(word))
                .ToList();
        }

        public Task FindByUserName(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            return _repository.SaveChangesAsync();
        }

        public Task<Task> FindAsync(Task entity)
        {
            return _repository.FindAsync(entity);
        }

        public Task<Task> FindByIdAsync(int id)
        {
            return _repository.FindByIdAsync(id);
        }

        public void Dispose()
        {
            _repository.Dispose();;
        }
        */
    }
}