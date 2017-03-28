using System;
using System.Data.Entity;
using System.Text;
using ToDO.Infraestructure.DataModel;

namespace ToDO.Infraestructure.Context
{
    public class ToDoInitializer : CreateDatabaseIfNotExists<ToDoContext>
    {
        protected override void Seed(ToDoContext context)
        {
            var uoW = new UnitOfWork.UnitOfWork();

            var user1 = new UserDataModel
            {
                FirstName = "System",
                LastName = "System",
                UserName = "system",
                Password = Encrypt("Qwer1234"),
                RememberMe = true,
                CreatedAt = DateTime.Now
            };

            var user2 = new UserDataModel
            {
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "admin",
                Password = Encrypt("Asdf1234"),
                RememberMe = true,
                CreatedAt = DateTime.Now
            };

            var user3 = new UserDataModel
            {
                FirstName = "Sys Admin",
                LastName = "Sys Admin",
                UserName = "sysadmin",
                Password = Encrypt("Zxcv1234"),
                RememberMe = true,
                CreatedAt = DateTime.Now
            };

            uoW.UserRepository.Insert(user1);
            uoW.UserRepository.Insert(user2);
            uoW.UserRepository.Insert(user3);
            uoW.SaveChanges();

            var task1 = new TaskDataModel
            {
                Title = "Task Uno",
                Description = "Description de la tarea",
                IsCompleted = true,
                CreatedAt = DateTime.Now,
                User = user1
            };
            var task2 = new TaskDataModel
            {
                Title = "Task Dos",
                Description = "Description de la tarea",
                IsCompleted = false,
                CreatedAt = DateTime.Now,
                User = user1
            };
            var task3 = new TaskDataModel
            {
                Title = "Task Tres",
                Description = "Description de la tarea",
                IsCompleted = true,
                CreatedAt = DateTime.Now,
                User = user1
            };

            var task4 = new TaskDataModel
            {
                Title = "Task Cuatro",
                Description = "Description de la tarea",
                IsCompleted = false,
                CreatedAt = DateTime.Now,
                User = user2
            };
            var task5 = new TaskDataModel
            {
                Title = "Task Cinco",
                Description = "Description de la tarea",
                IsCompleted = false,
                CreatedAt = DateTime.Now,
                User = user2
            };
            var task6 = new TaskDataModel
            {
                Title = "Task Seis",
                Description = "Description de la tarea",
                IsCompleted = false,
                CreatedAt = DateTime.Now,
                User = user2
            };

            var task7 = new TaskDataModel
            {
                Title = "Task Siete",
                Description = "Description de la tarea",
                IsCompleted = true,
                CreatedAt = DateTime.Now,
                User = user3
            };
            var task8 = new TaskDataModel
            {
                Title = "Task Ocho",
                Description = "Description de la tarea",
                IsCompleted = false,
                CreatedAt = DateTime.Now,
                User = user3
            };
            var task9 = new TaskDataModel
            {
                Title = "Task Nueve",
                Description = "Description de la tarea",
                IsCompleted = true,
                CreatedAt = DateTime.Now,
                User = user1
            };

            uoW.TaskRepository.Insert(task1);
            uoW.TaskRepository.Insert(task2);
            uoW.TaskRepository.Insert(task3);
            uoW.TaskRepository.Insert(task4);
            uoW.TaskRepository.Insert(task5);
            uoW.TaskRepository.Insert(task6);
            uoW.TaskRepository.Insert(task7);
            uoW.TaskRepository.Insert(task8);
            uoW.TaskRepository.Insert(task9);
            uoW.SaveChanges();
        }

        private string Encrypt(string pass)
        {
            var passToBytes = Encoding.UTF8.GetBytes(pass);
            var bytesToEncrypt = Convert.ToBase64String(passToBytes);

            bytesToEncrypt = bytesToEncrypt.Replace("+", "A");
            bytesToEncrypt = bytesToEncrypt.Replace(@"\", "B");
            bytesToEncrypt = bytesToEncrypt.Replace(@"/", "C");

            passToBytes = Encoding.UTF8.GetBytes(bytesToEncrypt);
            bytesToEncrypt += Convert.ToBase64String(passToBytes);

            return bytesToEncrypt;
        }
    }
}