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
    public class ItemController : ControllerBase
    {
        private readonly IBackendRepo _repository;
        private readonly IMapper _mapper;

        public ItemController(IBackendRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ItemReadDto>> GetAllItems()
        {
            var item = _repository.GetAllItems();
            if (item != null)
            {
                return Ok(_mapper.Map<IEnumerable<ItemReadDto>>(item));
            }
            else return NotFound();
        }

        [HttpGet("{id}", Name = "GetItemById")]
        public ActionResult<ItemReadDto> GetItemById(int id)
        {
            var item = _repository.GetItemById(id);
            if (item != null)
            {
                return Ok(_mapper.Map<ItemReadDto>(item));
            }
            else return NotFound();
        }

        [HttpPost]
        public ActionResult<ItemReadDto> CreateItem(ItemCreateDto itemCreateDto)
        {
            var model = _mapper.Map<Item>(itemCreateDto);
            _repository.CreateItem(model);
            _repository.SaveChanges();

            var readDto = _mapper.Map<ItemReadDto>(model);

            return CreatedAtRoute(nameof(GetItemById), new { Id = readDto.ItemId }, readDto);
        }
    }
}