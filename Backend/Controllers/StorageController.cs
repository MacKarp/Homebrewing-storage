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
        public ActionResult<IEnumerable<Storage>> GetAllStorages()
        {
            var item = _repository.GetAllStorages();
            return Ok(item);
        }

        [HttpGet("{id}")]
        public ActionResult<StorageReadDto> GetStorageById(int id)
        {
            var item = _repository.GetStorageById(id);
            if (item != null)
            {
                return Ok(_mapper.Map<StorageReadDto>(item));
            }
            else return NotFound();
        }
    }
}