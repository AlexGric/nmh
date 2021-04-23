using AutoMapper;
using AutoMapper.QueryableExtensions;
using BusinessLogic.Responses;
using BusinessLogic.ViewModels;
using DataAccess.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Task = System.Threading.Tasks.Task;

namespace BusinessLogic.Services
{
    public class UserService
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger<UserService> logger;
        private readonly IMapper mapper;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;

            mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<DataAccess.Models.User, SecureUser>();
                cfg.CreateMap<User, DataAccess.Models.User>().ReverseMap();
            }).CreateMapper();
        }

        public async Task<User> GetUserById(int id)
        {
            return mapper.Map<User>(await userRepository.GetByIdAsync(id));
        }

        public async Task<SecureUser> GetSecureUserById(int id)
        {
            return mapper.Map<SecureUser>(await userRepository.GetByIdAsync(id));
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return (await userRepository.GetAllAsync()).AsQueryable().ProjectTo<User>(mapper.ConfigurationProvider);
        }

        public async Task<IEnumerable<SecureUser>> GetAllSecureUsersAsync()
        {
            return (await userRepository.GetAllAsync()).AsQueryable().ProjectTo<SecureUser>(mapper.ConfigurationProvider);
        }

        public async Task<IReadOnlyCollection<SecureUser>> FindByConditionAsync(Expression<Func<DataAccess.Models.User, bool>> predicate)
        {
            return (IReadOnlyCollection<SecureUser>)await userRepository.FindByConditionAsync(predicate);
        }        

        public async Task<Response<SecureUser>> Create(User user)
        {
            IReadOnlyCollection<DataAccess.Models.User> users = await userRepository.GetAllAsync();

            if (users.Any(existingUser => existingUser.Login == user.Login))
            {
                string errorMessage = "User with this login already exists";
                return new Response<SecureUser>(errorMessage);
            }

            if (users.Any(existingUser => existingUser.Email == user.Email))
            {
                string errorMessage = "User with this email already exists";
                return new Response<SecureUser>(errorMessage);
            }

            DataAccess.Models.User u = mapper.Map<User, DataAccess.Models.User>(user);
            u.Password = BCrypt.Net.BCrypt.HashPassword(u.Password);

            try
            {
                SecureUser newUser = mapper.Map<DataAccess.Models.User, SecureUser>(await userRepository.CreateAsync(u));
                return new Response<SecureUser>(newUser);
            }
            catch (Exception e)
            {
                logger.LogError(e.InnerException.Message);
                string errorMessage = "An error occured while creating new user";
                return new Response<SecureUser>(errorMessage);
            }
        }

        public async Task Update(User user)
        {
            await userRepository.Update(mapper.Map<User, DataAccess.Models.User>(user));
        }

        public async Task Delete(int id)
        {
            await userRepository.Delete(id);
        }
    }
}