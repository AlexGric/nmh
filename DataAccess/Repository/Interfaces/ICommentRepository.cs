using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repository.Interfaces
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IReadOnlyCollection<Comment>> FindAllCommentsAllIncludedAsync();
        Task<IReadOnlyCollection<Comment>> FindCommentByConditionAllIncludedAsync(Expression<Func<Comment, bool>> commentPredicate);
        Task<Comment> GetCommentAllIncludedAsync(Expression<Func<Comment, bool>> commentPredicate);
        Task<Comment> GetByIdAsync(int id);
        System.Threading.Tasks.Task Delete(int id);
        System.Threading.Tasks.Task Update(Comment comment);
    }
}
