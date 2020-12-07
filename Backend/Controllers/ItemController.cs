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
    public class ItemController : ControllerBase
    {
        private readonly IBackendRepo _repository;
        private readonly IMapper _mapper;

        public ItemController(IBackendRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemReadDto>> GetAllItems()
        {
            var item = _repository.GetAllItems();
            if (item != null)
            {
                return Ok(_mapper.Map<IEnumerable<ItemReadDto>>(item));
            }
            else return NotFound();
        }

        [HttpGet("{id}", Name = "GetItemById")]
        public ActionResult<ItemReadDto> GetItemById(int id)
        {
            var item = _repository.GetItemById(id);
            if (item != null)
            {
                return Ok(_mapper.Map<ItemReadDto>(item));
            }
            else return NotFound();
        }

        [HttpPost]
        public ActionResult<ItemReadDto> CreateItem(ItemCreateDto itemCreateDto)
        {
            var model = new Item()
            {
                IdCategory = _repository.GetCategoryById(itemCreateDto.CategoryId),
                ItemName = itemCreateDto.ItemName,
            };
            _repository.CreateItem(model);
            _repository.SaveChanges();

            var readDto = _mapper.Map<ItemReadDto>(model);

            return CreatedAtRoute(nameof(GetItemById), new { Id = readDto.ItemId }, readDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateItem(int id, ItemUpdateDto itemUpdateDto)
        {
            var modelFromRepo = _repository.GetItemById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(itemUpdateDto, modelFromRepo);
            _repository.UpdateItem(modelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateItem(int id, JsonPatchDocument<ItemUpdateDto> patchDocument)
        {
            var modelFromRepo = _repository.GetItemById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }

            var toPatch = _mapper.Map<ItemUpdateDto>(modelFromRepo);
            patchDocument.ApplyTo(toPatch, ModelState);
            if (!TryValidateModel(toPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(toPatch, modelFromRepo);
            _repository.UpdateItem(modelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteItem(int id)
        {
            var modelFromRepo = _repository.GetItemById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteItem(modelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}