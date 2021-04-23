using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interfaces
{
    public interface ITaskRepository : IRepository<Models.Task>
    {
        Task<IReadOnlyCollection<Models.Task>> FindAllTasksAllIncludedAsync();
        Task<IReadOnlyCollection<Models.Task>> FindTaskByConditionAllIncludedAsync(Expression<Func<Models.Task, bool>> taskPredicate);
        Task<Models.Task> GetTaskAllIncludedAsync(Expression<Func<Models.Task, bool>> taskPredicate);
        Task<Models.Task> GetByIdAsync(int id);
        Task Delete(int id);
        Task Update(Models.Task task);
    }
}
