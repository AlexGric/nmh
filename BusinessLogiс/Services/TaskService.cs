using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Responses;
using BusinessLogic.ViewModels;
using DataAccess.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class TaskService
    {
        private ITaskRepository _taskRepository { set; get; }
        IMapper mapper;
        public UserService UserService { set; get; }

        public TaskService(ITaskRepository taskRepository, UserService UserService)
        {
            this._taskRepository = taskRepository;
            this.UserService = UserService;
            this.mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<DataAccess.Models.Task, ViewModels.Task>();
                cfg.CreateMap<DataAccess.Models.Comment, ViewModels.Comment>();
            }).CreateMapper();
        }

        public async Task<Response<ViewModels.Task>> CreateAsync(ViewModels.Task task)
        {
            try
            {
                var result = await _taskRepository.CreateAsync(mapper.Map<DataAccess.Models.Task>(task));
                return new Response<ViewModels.Task>(mapper.Map<ViewModels.Task>(result));
            }
            catch (Exception e)
            {
                return new Response<ViewModels.Task>(e.InnerException.Message);
            }
        }
        public async Task<IReadOnlyCollection<ViewModels.Task>> FindByConditionAsync(Expression<Func<DataAccess.Models.Task, bool>> predicat)
        {

            return await mapper.Map<Task<IReadOnlyCollection<DataAccess.Models.Task>>, Task<IReadOnlyCollection<ViewModels.Task>>>((Task<IReadOnlyCollection<DataAccess.Models.Task>>)await _taskRepository.FindByConditionAsync(predicat));
        }
        public async Task<IReadOnlyCollection<ViewModels.Task>> GetAllAsync()
        {            

           var taskCollections =  mapper.Map<IReadOnlyCollection<DataAccess.Models.Task>, IReadOnlyCollection<ViewModels.Task>>(await _taskRepository.FindAllTasksAllIncludedAsync());
            foreach(ViewModels.Task task in taskCollections)
            {
                task.Executor = await UserService.GetSecureUserById(task.ExecutorId);
                task.Manager = await UserService.GetSecureUserById(task.ManagerId);
            }
            return taskCollections;
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            await _taskRepository.Delete(id);
        }
        public async System.Threading.Tasks.Task Update(ViewModels.Task task)
        {

            await _taskRepository.Update(mapper.Map<ViewModels.Task, DataAccess.Models.Task>(task));

        }

        public async Task<ViewModels.Task> GetByIdAsync(int id)
        {

            return mapper.Map<ViewModels.Task>(await _taskRepository.GetByIdAsync(id));
        }

    }
}
