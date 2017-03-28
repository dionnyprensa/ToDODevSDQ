using FluentValidation;
using System.Collections.Generic;
using ToDO.Application.Concrete;
using ToDO.Domain.Entities;
using ToDO.Domain.Interfaces;

namespace ToDO.Application.Interfaces
{
    public interface IAppService<T> : IRepository<T> where T : class
    {
        bool IsValid(T entity);

        IList<ErrorMessage> Errors(T entity);

        IList<ErrorMessage> Errors(T entity, AbstractValidator<User> validator);
    }
}