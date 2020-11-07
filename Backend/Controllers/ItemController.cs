using System.Collections.Generic;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IBackendRepo _repository;

        public ItemController(IBackendRepo repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Item>> GetAllItems()
        {
            var item = _repository.GetAllItems();
            return Ok(item);
        }

        [HttpGet("{id}")]
        public ActionResult<Expire> GetItemById(int id)
        {
            var item = _repository.GetItemById(id);
            if (item != null)
            {
                return Ok(item);
            }
            else return NotFound();
        }
    }
}