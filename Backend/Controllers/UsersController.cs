using AutoMapper;
using Backend.Data;
using Backend.Dtos;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IBackendRepo _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;

        public UsersController(
            IBackendRepo repository, 
            IMapper mapper, 
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration
            )
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }








        [HttpGet] //GET api/users
        [HttpGet("/users")] // GET users
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
        public ActionResult<UserReadDto> GetUserById(string id)
        {
            var user = _repository.GetUserById(id);
            if (user != null)
            {
                return Ok(_mapper.Map<UserReadDto>(user));
            }
            else return NotFound();
        }

        //GET api/users/GetUser   GETs the data of actual user on whose behalf the code is running
        [HttpGet("GetUser")]
        public ActionResult<UserReadDto> GetUser()
        {
            string emailAdress = HttpContext.User.Identity.Name;
            var user = _repository.GetAllUsers().Where(user => user.Email == emailAdress).FirstOrDefault();
            if (user != null)
            {
                return Ok(_mapper.Map<UserReadDto>(user));
            }
            else return NotFound();
        }

        //POST api/users     ---------------- 1st CREATE USER METHOD
        [HttpPost]
        public ActionResult<UserReadDto> CreateUser([FromBody] UserCreateDto user)
        {
            var model = _mapper.Map<IdentityUser>(user);
            
            model.PasswordHash = _userManager.PasswordHasher.HashPassword(model, user.UserPassword);
            _repository.CreateUser(model);
            _repository.SaveChanges();

            var readDto = _mapper.Map<UserReadDto>(model);

            return CreatedAtRoute(nameof(GetUserById), new { Id = readDto.UserId }, readDto);
        }


        //POST api/users     ---------------- 2nd CREATE USER METHOD
        [HttpPost("Create")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo model)
        {
            var user = new IdentityUser { UserName = model.EmailAddress, Email = model.EmailAddress };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return BuildToken(model);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo model)
        {
            var result = await _signInManager.PasswordSignInAsync(
                                                    model.EmailAddress,
                                                    model.Password,
                                                    isPersistent: false,
                                                    lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return BuildToken(model);
            }
            else
            {
                return BadRequest("Invalid login attempt");
            }
        }


        //POST api/users/{id}
        [HttpPut("{id}", Name = "PutUpdateUser")]
        public ActionResult UpdateUser([FromRoute]string id, UserCreateDto userUpdateDto)
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
        public ActionResult PartialUpdateUser(string id, JsonPatchDocument<UserCreateDto> patchDocument)
        {
            var modelFromRepo = _repository.GetUserById(id);
            if (modelFromRepo == null)
            {
                return NotFound();
            }

            var toPatch = _mapper.Map<UserCreateDto>(modelFromRepo);
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

               

        private UserToken BuildToken(UserInfo userInfo)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userInfo.EmailAddress),
                new Claim(ClaimTypes.Email, userInfo.EmailAddress)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwt:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddDays(7);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: expiration,
                signingCredentials: creds);


            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

        //DELETE api/users/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(string id)
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
