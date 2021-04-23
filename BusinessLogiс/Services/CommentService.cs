using AutoMapper;
using BusinessLogic.Responses;
using BusinessLogic.ViewModels;
using DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace BusinessLogic.Services
{
    public class CommentService
    {
        private ICommentRepository _commentRepository { set; get; }
        IMapper mapper;

        public CommentService(ICommentRepository repository)
        {
            _commentRepository = repository;
            this.mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<DataAccess.Models.Comment, Comment>();
            }).CreateMapper();
        }
        public async System.Threading.Tasks.Task Delete(int id)
        {
            await _commentRepository.Delete(id);
        }

        public async System.Threading.Tasks.Task Update(Comment comment)
        {
            await _commentRepository.Update(mapper.Map<Comment, DataAccess.Models.Comment>(comment));
        }

        public async System.Threading.Tasks.Task<Comment> GetByIdAsync(int id)
        {
            return mapper.Map<Comment>(await _commentRepository.GetByIdAsync(id));
        }

        public async System.Threading.Tasks.Task<IReadOnlyCollection<Comment>> GetAllAsync()
        {
            return (IReadOnlyCollection<Comment>)await _commentRepository.GetAllAsync();
        }
        public async System.Threading.Tasks.Task<IReadOnlyCollection<Comment>> FindByConditionAsync(Expression<Func<DataAccess.Models.Comment, bool>> predicat)
        {
            return (IReadOnlyCollection<Comment>)await _commentRepository.FindByConditionAsync(predicat);
        }

        public async System.Threading.Tasks.Task<Response<Comment>> CreateAsync(Comment comment)
        {
            try
            {
                var result = await _commentRepository.CreateAsync(mapper.Map<DataAccess.Models.Comment>(comment));
                return new Response<Comment>(mapper.Map<Comment>(result));
            }
            catch (Exception e)
            {
                return new Response<Comment>(e.InnerException.Message);
            }
        }

    }
}
