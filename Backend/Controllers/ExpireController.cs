using System.Collections.Generic;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExpireController : ControllerBase
    {
        private readonly IBackendRepo _repository;

        public ExpireController(IBackendRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Expire>> GetAllExpires()
        {
            var item = _repository.GetAllExpires();
            return Ok(item);
        }

        [HttpGet("{id}")]
        public ActionResult<Expire> GetExpireById(int id)
        {
            var item = _repository.GetExpireById(id);
            if (item != null)
            {
                return Ok(item);
            }
            else return NotFound();
        }
    }
}