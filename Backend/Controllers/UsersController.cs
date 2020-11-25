using AutoMapper;
using Backend.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

       
    }
}
