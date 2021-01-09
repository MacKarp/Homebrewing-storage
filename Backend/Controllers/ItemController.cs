using System.Collections.Generic;
using AutoMapper;
using Backend.Data;
using Backend.Dtos;
using Backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IBackendRepo _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ItemController(IBackendRepo repository, IMapper mapper, ILogger logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public ActionResult<ItemReadDto> GetItemById(int id)
        {
            var item = _repository.GetItemById(id);
            if (item != null)
            {
                return Ok(_mapper.Map<ItemReadDto>(item));
            }
            else
            {
                _logger.LogWarning("{item} with ID: {Itemid} is null", item, id);
                return NotFound();
            }
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public ActionResult<ItemReadDto> CreateItem(ItemCreateDto itemCreateDto)
        {
            var model = new Item()
            {
                IdCategory = _repository.GetCategoryById(itemCreateDto.CategoryId),
                ItemName = itemCreateDto.ItemName,
            };
            
            try
            {
                _repository.CreateItem(model);
                _repository.SaveChanges();
                _logger.LogInformation("Item created by {HttpUser}", HttpContext.User.Identity.Name);
            }
            catch (System.Exception ex)
            {
                _logger.LogInformation("Item creation failed. Exception:", ex.Message);
            }
            

            var readDto = _mapper.Map<ItemReadDto>(model);

            return CreatedAtRoute(nameof(GetItemById), new { Id = readDto.ItemId }, readDto);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public ActionResult UpdateItem(int id, ItemUpdateDto itemUpdateDto)
        {
            var modelFromRepo = _repository.GetItemById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }

            modelFromRepo.IdCategory = _repository.GetCategoryById(itemUpdateDto.CategoryId);
            modelFromRepo.ItemName = itemUpdateDto.ItemName;

            _repository.UpdateItem(modelFromRepo);
            _repository.SaveChanges();
            _logger.LogInformation("Item with ID: {id} UPDATED by {HttpUser} ", id, HttpContext.User.Identity.Name);

            return NoContent();
        }

        [HttpPatch("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
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
            _logger.LogInformation("Item with ID: {id} UPDATED by {HttpUser} ", id, HttpContext.User.Identity.Name);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public ActionResult DeleteItem(int id)
        {
            var modelFromRepo = _repository.GetItemById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteItem(modelFromRepo);
            _repository.SaveChanges();
            _logger.LogInformation("Item with ID: {id} DELETED by {HttpUser} ", id, HttpContext.User.Identity.Name);
            return NoContent();
        }
    }
}