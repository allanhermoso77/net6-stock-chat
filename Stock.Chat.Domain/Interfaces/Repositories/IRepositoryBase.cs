﻿using Stock.Chat.CrossCutting.Models;
using System.Linq.Expressions;

namespace Stock.Chat.Domain.Interfaces.Repositories
{
    public interface IRepositoryBase<T> where T : EntityBase
    {
        void Add(T entity);
        Task AddAsync(T entity,CancellationToken cancellationToken);
        void Update(T entity);
        void Remove(T entity);
        IQueryable<T> GetAll();
        IQueryable<T> GetByExpression(Expression<Func<T, bool>> predicate);
        T GetById(Guid id);
    }
}
