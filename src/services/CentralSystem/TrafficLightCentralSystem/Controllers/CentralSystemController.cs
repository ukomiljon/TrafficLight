using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrafficLightCentralSystem.Model.DTO;
using TrafficLightCentralSystem.Repositories;

namespace TrafficLightCentralSystem.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CentralSystemController : Controller
    {
        private readonly ILogger<CentralSystemController> _logger;
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public CentralSystemController(ILogger<CentralSystemController> logger, IEventRepository eventRepository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _eventRepository = eventRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]       
        public async Task<ActionResult> Post(IEnumerable<TrafficLighBoundRequest> command)
        {
           
            return Ok();
        }

        [HttpPost("commands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public async Task<ActionResult> Post(CommandRequest command)
        {

            return Ok();
        }

    }
}
