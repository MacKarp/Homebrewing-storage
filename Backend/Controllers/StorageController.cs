using System.Collections.Generic;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageController : ControllerBase
    {
        private IBackendRepo _repository;

        public StorageController(IBackendRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Storage>> GetAllStorages()
        {
            var item = _repository.GetAllStorages();
            return Ok(item);
        }

        [HttpGet("{id}")]
        public ActionResult<Expire> GetStorageById(int id)
        {
            var item = _repository.GetStorageById(id);
            if (item != null)
            {
                return Ok(item);
            }
            else return NotFound();
        }
    }
}