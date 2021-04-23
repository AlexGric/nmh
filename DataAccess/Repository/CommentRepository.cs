using DataAccess.Context;
using DataAccess.Models;
using DataAccess.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {

        public CommentRepository(TaskManagerContext context) : base(context)
        {
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            Comment comment = await GetByIdAsync(id);
            if (comment != null) TaskManagerContext.Remove(comment);
            await TaskManagerContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyCollection<Comment>> FindAllCommentsAllIncludedAsync()
        {
            return await Entities.ToListAsync().ConfigureAwait(false);
        }

        public async Task<IReadOnlyCollection<Comment>> FindCommentByConditionAllIncludedAsync(Expression<Func<Comment, bool>> commentPredicate)
        {
            return await Entities.Where(commentPredicate).ToListAsync().ConfigureAwait(false);
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await TaskManagerContext.Comments.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Comment> GetCommentAllIncludedAsync(Expression<Func<Comment, bool>> commentPredicate)
        {
            return await TaskManagerContext.Comments.Where(commentPredicate).FirstOrDefaultAsync();
        }

        public async System.Threading.Tasks.Task Update(Comment comment)
        {
            TaskManagerContext.Update(comment);
            await TaskManagerContext.SaveChangesAsync();
        }
    }
}
