using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace DataAccess.Repository.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<IReadOnlyCollection<User>> FindAllUsersAllIncludedAsync();
        public Task<IReadOnlyCollection<User>> FindUserByConditionAllIncludedAsync(Expression<Func<User, bool>> userPredicate);
        public Task<User> GetUserAllIncludedAsync(Expression<Func<User, bool>> userPredicate);
        public Task<User> GetByIdAsync(int id);
        public Task Update(User user);
        public Task Delete(int userId);
    }
}