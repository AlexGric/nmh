using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private CommentService _commentService { get; set; }
        public CommentController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            IEnumerable<BusinessLogic.ViewModels.Comment> comment = await _commentService.GetAllAsync();
            return Ok(comment);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            BusinessLogic.ViewModels.Comment comment = await _commentService.GetByIdAsync(id);
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BusinessLogic.ViewModels.Comment comment)
        {
            if (comment == null)
            {
                return BadRequest();
            }

            await _commentService.CreateAsync(comment);

            return Ok();
        }


        public async Task Delete(int id)
        {
            await _commentService.Delete(id);
        }

        public async Task Update(BusinessLogic.ViewModels.Comment comment)
        {
            await _commentService.Update(comment);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
