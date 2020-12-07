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
    public class StorageController : ControllerBase
    {
        private readonly IBackendRepo _repository;
        private readonly IMapper _mapper;

        public StorageController(IBackendRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StorageReadDto>> GetAllStorages()
        {
            var item = _repository.GetAllStorages();
            if (item != null)
            {
                return Ok(_mapper.Map<IEnumerable<StorageReadDto>>(item));
            }
            else return NotFound();
        }

        [HttpGet("{id}", Name = "GetStorageById")]
        public ActionResult<StorageReadDto> GetStorageById(int id)
        {
            var item = _repository.GetStorageById(id);
            if (item != null)
            {
                return Ok(_mapper.Map<StorageReadDto>(item));
            }
            else return NotFound();
        }

        [HttpPost]
        public ActionResult<StorageReadDto> CreateStorage(StorageCreateDto storageCreateDto)
        {
            var model = new Storage()
            {
                IdUser = _repository.GetUserById(storageCreateDto.UserId),
                StorageName = storageCreateDto.StorageName,
            };
            _repository.CreateStorage(model);
            _repository.SaveChanges();

            var readDto = _mapper.Map<StorageReadDto>(model);

            return CreatedAtRoute(nameof(GetStorageById), new { Id = readDto.StorageId }, readDto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateStorage(int id, StorageUpdateDto storageUpdateDto)
        {
            var modelFromRepo = _repository.GetStorageById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(storageUpdateDto, modelFromRepo);
            _repository.UpdateStorage(modelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public ActionResult PartialUpdateStorage(int id, JsonPatchDocument<StorageUpdateDto> patchDocument)
        {
            var modelFromRepo = _repository.GetStorageById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }

            var toPatch = _mapper.Map<StorageUpdateDto>(modelFromRepo);
            patchDocument.ApplyTo(toPatch, ModelState);
            if (!TryValidateModel(toPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(toPatch, modelFromRepo);
            _repository.UpdateStorage(modelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStorage(int id)
        {
            var modelFromRepo = _repository.GetStorageById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteStorage(modelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}