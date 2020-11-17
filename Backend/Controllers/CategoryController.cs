using System.Collections.Generic;
using AutoMapper;
using Backend.Data;
using Backend.Dtos;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IBackendRepo _repository;
        private readonly IMapper _mapper;

        public CategoryController(IBackendRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAllCategories()
        {
            var item = _repository.GetAllCategories();
            return Ok(item);
        }

        [HttpGet("{id}")]
        public ActionResult<CategoryReadDto> GetCategoryById(int id)
        {
            var item = _repository.GetCategoryById(id);
            if (item != null)
            {
                return Ok(_mapper.Map<CategoryReadDto>(item));
            }
            else return NotFound();
        }
    }
}