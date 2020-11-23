using System.Collections.Generic;
using AutoMapper;
using Backend.Data;
using Backend.Dtos;
using Backend.Models;
using Microsoft.AspNetCore.JsonPatch;
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
        public ActionResult<IEnumerable<CategoryReadDto>> GetAllCategories()
        {
            var item = _repository.GetAllCategories();
            if (item != null)
            {
                return Ok(_mapper.Map<IEnumerable<CategoryReadDto>>(item));
            }
            else return NotFound();
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public ActionResult<CategoryReadDto> GetCategoryById(int id)
        {
            var item = _repository.GetCategoryById(id);
            if (item != null)
            {
                return Ok(_mapper.Map<CategoryReadDto>(item));
            }
            else return NotFound();
        }

        [HttpPost]
        public ActionResult<CategoryReadDto> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            var model = _mapper.Map<Category>(categoryCreateDto);
            _repository.CreateCategory(model);
            _repository.SaveChanges();

            var readDto = _mapper.Map<CategoryReadDto>(model);

            return CreatedAtRoute(nameof(GetCategoryById), new { Id = readDto.CategoryId }, readDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCategory(int id, CategoryUpdateDto categoryUpdateDto)
        {
            var modelFromRepo = _repository.GetCategoryById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(categoryUpdateDto, modelFromRepo);
            _repository.UpdateCategory(modelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateCategory(int id, JsonPatchDocument<CategoryUpdateDto> patchDocument)
        {
            var modelFromRepo = _repository.GetCategoryById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }

            var toPatch = _mapper.Map<CategoryUpdateDto>(modelFromRepo);
            patchDocument.ApplyTo(toPatch, ModelState);
            if (!TryValidateModel(toPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(toPatch, modelFromRepo);
            _repository.UpdateCategory(modelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteCategory(int id)
        {
            var modelFromRepo = _repository.GetCategoryById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteCategory(modelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}