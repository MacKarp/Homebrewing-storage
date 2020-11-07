using System.Collections.Generic;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IBackendRepo _repository;

        public CategoryController(IBackendRepo repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            var item = _repository.GetAllCategories();
            return Ok(item);
        }

        [HttpGet("{id}")]
        public ActionResult<Category> GetCategoryById(int id)
        {
            var item = _repository.GetCategoryById(id);
            if (item != null)
            {
                return Ok(item);
            }
            else return NotFound();
        }
    }
}