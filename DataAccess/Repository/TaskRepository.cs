using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Repository
{
    public class TaskRepository :BaseRepository<Task>, ITaskRepository
    {
        public TaskRepository(TaskManagerContext context): base(context) { }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            Task task = await GetByIdAsync(id);
            if (task != null) TaskManagerContext.Remove(task);
            await TaskManagerContext.SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task<IReadOnlyCollection<Task>> FindAllTasksAllIncludedAsync()
        {
          var a = await Entities.ToListAsync().ConfigureAwait(false);
            return a;
        }

        public async System.Threading.Tasks.Task<IReadOnlyCollection<Task>> FindTaskByConditionAllIncludedAsync(Expression<Func<Task, bool>> taskPredicate)
        {
            return await Entities.Where(taskPredicate).ToListAsync().ConfigureAwait(false);
        }

        public async System.Threading.Tasks.Task<Task> GetByIdAsync(int id)
        {
            return await TaskManagerContext.Tasks.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async System.Threading.Tasks.Task<Task> GetTaskAllIncludedAsync(Expression<Func<Task, bool>> taskPredicate)
        {
            return await TaskManagerContext.Tasks.Where(taskPredicate).FirstOrDefaultAsync();
        }

        public async System.Threading.Tasks.Task Update(Task task)
        {
            TaskManagerContext.Update(task);
            await TaskManagerContext.SaveChangesAsync();
        }
    }
}
