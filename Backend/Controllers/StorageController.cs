using System.Collections.Generic;
using AutoMapper;
using Backend.Data;
using Backend.Dtos;
using Backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public ActionResult UpdateStorage(int id, StorageUpdateDto storageUpdateDto)
        {
            var modelFromRepo = _repository.GetStorageById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }

            modelFromRepo.IdUser = _repository.GetUserById(storageUpdateDto.UserId);
            modelFromRepo.StorageName = storageUpdateDto.StorageName;

            _repository.UpdateStorage(modelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
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
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
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