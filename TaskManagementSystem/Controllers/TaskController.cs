using AutoMapper;
using BusinessLogic.Responses;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : Controller
    {
        private TaskService _taskService { get; set; }
        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IReadOnlyCollection<BusinessLogic.ViewModels.Task> tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            BusinessLogic.ViewModels.Task task = await _taskService.GetByIdAsync(id);
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BusinessLogic.ViewModels.Task task)
        {
            if (task == null)
            {
                return BadRequest();
            }

            await _taskService.CreateAsync(task);

            return Ok();
        }

        
        [HttpDelete]
        public async Task Delete(int id)
        {
            await _taskService.Delete(id);
        }
        [HttpPut]
        public async Task Update(BusinessLogic.ViewModels.Task task)
        {
            await _taskService.Update(task);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
