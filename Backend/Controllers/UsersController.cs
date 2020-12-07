using AutoMapper;
using Backend.Data;
using Backend.Dtos;
using Backend.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IBackendRepo _repository;
        private readonly IMapper _mapper;

        public UsersController(IBackendRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        //GET api/users
        [HttpGet]
        public ActionResult<IEnumerable<UserReadDto>> GetAllUsers()
        {
            var user = _repository.GetAllUsers();
            if (user != null)
            {
                return Ok(_mapper.Map<IEnumerable<UserReadDto>>(user));
            }
            else return NotFound();
        }
        
        //GET api/users/{id}
        [HttpGet("{id}", Name = "GetUserById")]
        public ActionResult<UserReadDto> GetUserById(int id)
        {
            var user = _repository.GetUserById(id);
            if (user != null)
            {
                return Ok(_mapper.Map<UserReadDto>(user));
            }
            else return NotFound();
        }

        //GET api/users/GetUser
        [HttpGet("GetUser")]
        public ActionResult<UserReadDto>GetUser()
        {
            string emailAdress = HttpContext.User.Identity.Name;
            var user = _repository.GetAllUsers().Where(user => user.UserEmail == emailAdress);
            if (user != null)
            {
                return Ok(_mapper.Map<UserReadDto>(user));
            }
            else return NotFound();
        }
        
        //POST api/users
        [HttpPost]
        public ActionResult<UserReadDto> CreateUser(UserCreateDto userCreateDto)
        {
            var model = _mapper.Map<User>(userCreateDto);
            _repository.CreateUser(model);
            _repository.SaveChanges();

            var readDto = _mapper.Map<UserReadDto>(model);

            return CreatedAtRoute(nameof(GetUserById), new { Id = readDto.UserId }, readDto);
        }

        //POST api/users/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, UserUpdateDto userUpdateDto)
        {
            var modelFromRepo = _repository.GetUserById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(userUpdateDto, modelFromRepo);
            _repository.UpdateUser(modelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }

        //PATCH api/users/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialUpdateUser(int id, JsonPatchDocument<UserUpdateDto> patchDocument)
        {
            var modelFromRepo = _repository.GetUserById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }

            var toPatch = _mapper.Map<UserUpdateDto>(modelFromRepo);
            patchDocument.ApplyTo(toPatch, ModelState);
            if (!TryValidateModel(toPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(toPatch, modelFromRepo);
            _repository.UpdateUser(modelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
        
        //DELETE api/users/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var modelFromRepo = _repository.GetUserById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }
            _repository.DeleteUser(modelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
    }
}
