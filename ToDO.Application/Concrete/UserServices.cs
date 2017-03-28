using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using AutoMapper;
using FluentValidation;
using ToDO.Application.Interfaces;
using ToDO.Application.Validators;
using ToDO.Domain.Entities;
using ToDO.Infraestructure.DataModel;
using ToDO.Infraestructure.UnitOfWork;

namespace ToDO.Application.Concrete
{
    public class UserServices : IUserService
    {
        private readonly UnitOfWork _unitOfWork;

        public UserServices()
        {
            _unitOfWork = new UnitOfWork();
        }

        public User GetUserById(int userId)
        {
            var user = _unitOfWork.UserRepository.GetById(userId);

            if (user == null) return null;

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<UserDataModel, User>(); });
            var mapper = config.CreateMapper();
            var userModel = mapper.Map<UserDataModel, User>(user);
            return userModel;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = _unitOfWork.UserRepository.GetAll().ToList();

            if (users.Any()) return null;

            var config = new MapperConfiguration(cfg => { cfg.CreateMap<UserDataModel, User>(); });
            var mapper = config.CreateMapper();
            var usersModel = mapper.Map<List<UserDataModel>, List<User>>(users);
            return usersModel;
        }

        public int CreateUser(User userEntity)
        {
            using (var scope = new TransactionScope())
            {
                var user = new UserDataModel
                {
                    UserName = userEntity.UserName,
                    Password = userEntity.Password,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    RememberMe = userEntity.RememberMe,
                    CreatedAt = DateTime.Now,
                    SoftDelete = userEntity.SoftDelete
                };
                _unitOfWork.UserRepository.Insert(user);
                _unitOfWork.SaveChanges();
                scope.Complete();
                return user.Id;
            }
        }

        public bool UpdateUser(int userId, User userEntity)
        {
            if (userEntity == null) return false;

            using (var scope = new TransactionScope())
            {
                var user = _unitOfWork.UserRepository.GetById(userId);

                if (user == null) return false;

                user.UserName = userEntity.UserName;
                user.Password = userEntity.Password;
                user.FirstName = userEntity.FirstName;
                user.LastName = userEntity.LastName;
                user.RememberMe = userEntity.RememberMe;
                user.SoftDelete = userEntity.SoftDelete;

                _unitOfWork.UserRepository.Update(user);
                _unitOfWork.SaveChanges();
                scope.Complete();
            }
            return true;
        }

        public bool DeleteUser(int userId)
        {
            if (userId <= 0) return false;
            using (var scope = new TransactionScope())
            {
                var user = _unitOfWork.UserRepository.GetById(userId);
                if (user == null) return false;
                _unitOfWork.UserRepository.Delete(user);
                _unitOfWork.SaveChanges();
                scope.Complete();
            }
            return true;
        }

        public IList<ErrorMessage> Errors(User entity)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserDataModel>(); });
            var mapper = config.CreateMapper();
            var userModel = mapper.Map<User, UserDataModel>(entity);

            var validator = new UserValidator();
            var result = validator.Validate(userModel);

            return
                result.Errors.Select(item => new ErrorMessage {Key = item.PropertyName, Error = item.ErrorMessage})
                    .ToList();
        }

        public IList<ErrorMessage> Errors(User entity, AbstractValidator<User> validator)
        {
            var result = validator.Validate(entity);

            return
                result.Errors.Select(item => new ErrorMessage
                    {
                        Key = item.PropertyName,
                        Error = item.ErrorMessage
                    })
                    .ToList();
        }

        public bool IsValid(User entity)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<User, UserDataModel>(); });
            var mapper = config.CreateMapper();
            var userModel = mapper.Map<User, UserDataModel>(entity);

            var validator = new UserValidator();
            var result = validator.Validate(userModel);

            return result.IsValid;
        }

        public bool IsValidLogin(User entity)
        {
            var validator = new LoginValidator(this);
            var result = validator.Validate(entity);

            return result.IsValid;
        }

        public User SignIn(string username, string pass)
        {
            return !GetAllUsers().Any() ? null : GetAllUsers().FirstOrDefault(x => x.UserName == username);
        }

        public bool SignInn(string username, string pass)
        {
            if (!GetAllUsers().Any())
                return false;

            var user = GetAllUsers().FirstOrDefault(x => x.UserName == username);

            return user != null && Encrypt(pass).Equals(user.Password);
        }

        public string Encrypt(string pass)
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

        #region Validations

        /*
        public bool IsValid(User entity)
        {
            var validator = new UserValidator();
            var result = validator.Validate(entity);

            return result.IsValid;
        }

        public bool IsValidLogin(User entity)
        {
            var validator = new LoginValidator(this);
            var result = validator.Validate(entity);

            return result.IsValid;
        }

        public IList<ErrorMessage> Errors(User entity)
        {
            var validator = new UserValidator();
            var result = validator.Validate(entity);

            return
                result.Errors.Select(item => new ErrorMessage {Key = item.PropertyName, Error = item.ErrorMessage})
                    .ToList();
        }

        public IList<ErrorMessage> Errors(User entity, AbstractValidator<User> validator)
        {
            var result = validator.Validate(entity);

            return
                result.Errors.Select(item => new ErrorMessage {Key = item.PropertyName, Error = item.ErrorMessage})
                    .ToList();
        }
        */

        #endregion Validations

        #region Login

        /*
        public User SignIn(string username, string pass)
        {
            var users = ToList().ToList();

            return users.Count <= 0
                ? null
                : users
                    .Where(user => user.UserName == username)
                    .FirstOrDefault(user => user.Password.Equals(Encrypt(pass)));
        }

        public bool SignInn(string username, string pass)
        {
            var user = ToList().FirstOrDefault(x => x.UserName == username);

            return user != null && Encrypt(pass).Equals(user.Password);
        }

        public string Encrypt(string pass)
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

        public bool Decrypt(string passLogin, string passDb)
        {
            /*
            byte[] DecryptBytes = Convert.FromBase64String(passDb);
            string DecryptedText = Encoding.UTF8.GetString(DecryptBytes);

            DecryptedText = DecryptedText.Replace("A", "+");
            DecryptedText = DecryptedText.Replace("B", @"\");
            DecryptedText = DecryptedText.Replace("B", @"/");

            return DecryptedText == Encrypt(passLogin) ? true : false;
             /

            return passDb == Encrypt(passLogin);
        }
        */

        #endregion Login

        /*
        private readonly IRepository<User> _repository;

        public UserService(IRepository<User> repository)
        {
            _repository = repository;
        }

        public ICollection<User> ToList()
        {
            return _repository.Get();
        }

        public Task<int> SaveChangesAsync()
        {
            return _repository.SaveChangesAsync();
        }

        public Task<User> FindAsync(User entity)
        {
            return _repository.FindAsync(entity);
        }

        public Task<User> FindByIdAsync(int id)
        {
            return _repository.FindByIdAsync(id);
        }

        public void Dispose()
        {
            _repository.Dispose(); ;
        }
        */

        #region CRUD

        /*
        public void Insert(User entity)
        {
            entity.Password = Encrypt(entity.Password);
            _repository.Insert(entity);
        }

        public void Update(User entity)
        {
            entity.Password = Encrypt(entity.Password);
            _repository.Update(entity);
        }

        public int SaveChanges()
        {
            return _repository.SaveChanges();
        }

        public void DeleteHard(User entity)
        {
            _repository.DeleteHard(entity);
        }

        public void DeleteSoft(User entity)
        {
            _repository.DeleteSoft(entity);
        }

        public User Find(User entity)
        {
            return _repository.Find(entity);
        }

        public User GetById(int id)
        {
            return _repository.GetById(id);
        }
        */

        #endregion CRUD
    }
}