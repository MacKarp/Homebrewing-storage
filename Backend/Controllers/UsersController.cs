using AutoMapper;
using Backend.Data;
using Backend.Dtos;
using Backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<UsersController> _logger;
        private readonly IConfiguration _configuration;

        public UsersController(
            IBackendRepo repository, 
            IMapper mapper, 
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<UsersController> logger,
            IConfiguration configuration
            )
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet("roles")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public ActionResult<List<string>> GetRoles()
        {
            var roles = _repository.GetRoles();
            return roles. Select(x => x.Name).ToList();        
        }

        [HttpPost("AssignRole")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> AssignRole(EditRoleDto editRoleDto)
        {
            var user = await _userManager.FindByIdAsync(editRoleDto.UserId);
            if (user == null)
            {
                return NotFound();
            }
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, editRoleDto.RoleName));
            return NoContent();
        }

        [HttpPost("RemoveRole")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public async Task<ActionResult> RemoveRole(EditRoleDto editRoleDto)
        {
            var user = await _userManager.FindByIdAsync(editRoleDto.UserId);
            if (user == null)
            {
                return NotFound();
            }
            await _userManager.RemoveClaimAsync(user, new Claim(ClaimTypes.Role, editRoleDto.RoleName));
            return NoContent();
        }

        [HttpGet] //GET api/users
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
        public ActionResult<IEnumerable<UserReadDto>> GetAllUsers()
        {
            var users = _repository.GetAllUsers();
            if (users != null)
            {
                _logger.LogInformation($"Getting users list by {HttpContext.User.Identity.Name} - {DateTime.UtcNow}");
                return Ok(_mapper.Map<IEnumerable<UserReadDto>>(users));
            }
            else return NotFound();
        }

        //GET api/users/{id}
        [HttpGet("{id}", Name = "GetUserById")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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
        [HttpGet("GetActiveUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public ActionResult<UserReadDto> GetActiveUser()
        {
            string emailAdress = HttpContext.User.Identity.Name;
            var user = _repository.GetAllUsers().Where(user => user.Email == emailAdress).FirstOrDefault();
            if (user != null)
            {
                return Ok(_mapper.Map<UserReadDto>(user));
            }
            else return NotFound();
        }

        //POST api/users     ---------------- 1st CREATE USER METHOD (with token build)
        [HttpPost("Create")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserInfo model)
        {
            var user = new IdentityUser { UserName = model.EmailAddress, Email = model.EmailAddress };
            var result = await _userManager.CreateAsync(user, model.Password);
                        
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "User"));
            if (result.Succeeded)
            {
                
                return await BuildToken(model);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        //POST api/users     ---------------- Temporary method --- to be deleted
        [HttpPost("CreateAdmin")]
        public async Task<ActionResult<UserToken>> CreateAdmin([FromBody] UserInfo model)
        {
            var user = new IdentityUser { UserName = model.EmailAddress, Email = model.EmailAddress };
            var result = await _userManager.CreateAsync(user, model.Password);
            
            //await _userManager.AddToRoleAsync(user, "Admin");
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, "Admin"));
            if (result.Succeeded)
            {
                return await BuildToken(model);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        //POST api/users     ---------------- 2nd CREATE USER METHOD (without token)
        //[HttpPost]
        //public ActionResult<UserReadDto> CreateUser([FromBody] UserCreateDto user)
        //{
        //    var model = _mapper.Map<IdentityUser>(user);

        //    model.PasswordHash = _userManager.PasswordHasher.HashPassword(model, user.UserPassword);
        //    _repository.CreateUser(model);
        //    _repository.SaveChanges();

        //    var readDto = _mapper.Map<UserReadDto>(model);

        //    return CreatedAtRoute(nameof(GetUserById), new { Id = readDto.UserId }, readDto);
        //}

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
                return await BuildToken(model);
            }
            else
            {
                return BadRequest("Invalid login attempt");
            }
        }

        [HttpPost("RenewToken")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
        public async Task<ActionResult<UserToken>> Renew()
        {
            var userInfo = new UserInfo
            {
                EmailAddress = HttpContext.User.Identity.Name
            };
            return await BuildToken(userInfo);
        }

        //POST api/users/{id}
        [HttpPut("{id}", Name = "PutUpdateUser")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin,User")]
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

               

        private async Task<UserToken> BuildToken(UserInfo userInfo)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userInfo.EmailAddress),
                new Claim(ClaimTypes.Email, userInfo.EmailAddress)
            };

            var identityUser = await _userManager.FindByEmailAsync(userInfo.EmailAddress);
            var claimsDB = await _userManager.GetClaimsAsync(identityUser);
            claims.AddRange(claimsDB);

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
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Admin")]
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
