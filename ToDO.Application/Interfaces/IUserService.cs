using FluentValidation;
using System.Collections.Generic;
using ToDO.Application.Concrete;
using ToDO.Domain.Entities;

namespace ToDO.Application.Interfaces
{
    public interface IUserService /*: IAppService<User>*/
    {
        User GetUserById(int userId);

        IEnumerable<User> GetAllUsers();

        int CreateUser(User userEntity);

        bool UpdateUser(int userId, User userEntity);

        bool DeleteUser(int userId);

        User SignIn(string username, string pass);

        bool SignInn(string username, string pass);

        string Encrypt(string pass);

        bool IsValidLogin(User entity);

        bool IsValid(User entity);

        IList<ErrorMessage> Errors(User entity);

        IList<ErrorMessage> Errors(User entity, AbstractValidator<User> validator);
    }
}