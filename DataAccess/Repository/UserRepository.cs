using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace DataAccess.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(TaskManagerContext context) : base(context) { }

        public async Task<IReadOnlyCollection<User>> FindAllUsersAllIncludedAsync()
        {
            return await Entities.AsNoTracking().ToListAsync().ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<User>> FindUserByConditionAllIncludedAsync(Expression<Func<User, bool>> taskPredicate)
        {
            return await Entities.AsNoTracking().Where(taskPredicate).ToListAsync().ConfigureAwait(false);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await TaskManagerContext.Users.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> GetUserAllIncludedAsync(Expression<Func<User, bool>> taskPredicate)
        {
            return await TaskManagerContext.Users.AsNoTracking().Where(taskPredicate).FirstOrDefaultAsync();
        }

        public override async Task<IReadOnlyCollection<User>> GetAllAsync()
        {
            return await Entities.AsNoTracking().ToListAsync();
        }

        public override async Task<User> CreateAsync(User user)
        {
            await base.CreateAsync(user);
            await TaskManagerContext.SaveChangesAsync();
            return user;
        }

        public async Task Update(User user)
        {
            TaskManagerContext.Update(user);
            await TaskManagerContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            User user = await GetByIdAsync(id);

            if (user != null)
                TaskManagerContext.Remove(user);

            await TaskManagerContext.SaveChangesAsync();
        }
    }
}