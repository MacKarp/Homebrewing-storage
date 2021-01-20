using System.Collections.Generic;
using System.Linq;
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
    public class ExpireController : ControllerBase
    {
        private readonly IBackendRepo _repository;
        private readonly IMapper _mapper;

        public ExpireController(IBackendRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public ActionResult<IEnumerable<ExpireReadDto>> GetAllExpires()
        {
            var item = _repository.GetAllExpires();
            if (item != null)
            {
                return Ok(_mapper.Map<IEnumerable<ExpireReadDto>>(item));
            }
            else return NotFound();
        }

        [HttpGet("{id}", Name = "GetExpireById")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public ActionResult<ExpireReadDto> GetExpireById(int id)
        {
            var item = _repository.GetExpireById(id);
            if (item != null)
            {
                return Ok(_mapper.Map<ExpireReadDto>(item));
            }
            else return NotFound();
        }

        [HttpPost]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public ActionResult<ExpireReadDto> CreateExpire(ExpireCreateDto expireCreateDto)
        {
            var model = new Expire()
            {
                ExpirationDate = expireCreateDto.ExpirationDate.Date,
                IdUser = _repository.GetUserById(expireCreateDto.UserId),
                IdItem = _repository.GetItemById(expireCreateDto.IdItem),
                IdStorage = _repository.GetStorageById(expireCreateDto.IdStorage),
            };
            _repository.CreateExpire(model);
            _repository.SaveChanges();

            var readDto = _mapper.Map<ExpireReadDto>(model);

            return CreatedAtRoute(nameof(GetExpireById), new { Id = readDto.ExpireId }, readDto);
        }

        [HttpPut("{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public ActionResult UpdateExpire(int id, ExpireUpdateDto expireUpdateDto)
        {
            var modelFromRepo = _repository.GetExpireById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }

            modelFromRepo.IdUser = _repository.GetUserById(expireUpdateDto.UserId);
            modelFromRepo.IdStorage = _repository.GetStorageById(expireUpdateDto.IdStorage);
            modelFromRepo.IdItem = _repository.GetItemById(expireUpdateDto.IdItem);
            modelFromRepo.ExpirationDate = expireUpdateDto.ExpirationDate.Date;
            _repository.UpdateExpire(modelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpPatch("{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public ActionResult PartialUpdateExpire(int id, JsonPatchDocument<ExpireUpdateDto> patchDocument)
        {
            var modelFromRepo = _repository.GetExpireById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }

            var toPatch = _mapper.Map<ExpireUpdateDto>(modelFromRepo);
            patchDocument.ApplyTo(toPatch, ModelState);
            if (!TryValidateModel(toPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(toPatch, modelFromRepo);
            _repository.UpdateExpire(modelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public ActionResult DeleteExpire(int id)
        {
            var modelFromRepo = _repository.GetExpireById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteExpire(modelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        [HttpGet("byUserId/{id}", Name = "GetExpireByUserId")]
        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public ActionResult<IEnumerable<ExpireReadDto>> GetExpireByUserId(string id)
        {
            var item = _repository.GetExpiresByUserId(id);
            if (item.Count() != 0)
            {
                return Ok(_mapper.Map<IEnumerable<ExpireReadDto>>(item));
            }
            else return NotFound();
        }
    }
}