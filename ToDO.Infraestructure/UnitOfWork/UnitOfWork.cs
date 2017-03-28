using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using ToDO.Infraestructure.Context;
using ToDO.Infraestructure.DataModel;
using ToDO.Infraestructure.Repositories;

namespace ToDO.Infraestructure.UnitOfWork
{
    public class UnitOfWork : IDisposable
    {
        private readonly ToDoContext _context;
        private bool _disposed;
        private Repository<TaskDataModel> _taskRepository;
        private Repository<UserDataModel> _userRepository;

        public UnitOfWork(ToDoContext context)
        {
            //
            //   public UnitOfWork(ToDoContext context)
            //{
            //_context = new ToDoContext();

            _context = context;
        }

        public UnitOfWork()
        {
            _context = new ToDoContext();
        }

        public Repository<UserDataModel> UserRepository
            => _userRepository ?? (_userRepository = new Repository<UserDataModel>(_context));

        public Repository<TaskDataModel> TaskRepository
            => _taskRepository ?? (_taskRepository = new Repository<TaskDataModel>(_context));

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void SaveChanges()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var outputLines = new List<string>();
                foreach (var eve in e.EntityValidationErrors)
                {
                    outputLines.Add(
                        $"{DateTime.Now}: Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation errors:");
                    outputLines.AddRange(
                        eve.ValidationErrors.Select(
                            ve => $"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\""));
                }
                File.AppendAllLines(@"C:\errors.txt", outputLines);

                throw;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                {
                    Debug.WriteLine("UnitOfWork is being disposed");
                    _context.Dispose();
                }
            _disposed = true;
        }
    }
}