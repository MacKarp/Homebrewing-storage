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
        public ActionResult<IEnumerable<Expire>> GetAllExpires()
        {
            var item = _repository.GetAllExpires();
            return Ok(item);
        }

        [HttpGet("{id}")]
        public ActionResult<ExpireReadDto> GetExpireById(int id)
        {
            var item = _repository.GetExpireById(id);
            if (item != null)
            {
                return Ok(_mapper.Map<ExpireReadDto>(item));
            }
            else return NotFound();
        }
    }
}