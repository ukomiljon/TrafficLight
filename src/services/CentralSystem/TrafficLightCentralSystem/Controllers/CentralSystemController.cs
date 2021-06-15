using AutoMapper;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TrafficLightCentralSystem.Model.DTO;
using TrafficLightCentralSystem.Repositories;
using TrafficLightCentralSystem.Usecases;

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
        private readonly SignalManager _signalManager;

        public CentralSystemController(
            ILogger<CentralSystemController> logger,
            IEventRepository eventRepository,
            IMapper mapper,
            IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _eventRepository = eventRepository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;

            _signalManager = new SignalManager(_publishEndpoint);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> Post(TrafficLightIntersection initSetting)
        {
            await _eventRepository.Create(initSetting);
            return Ok();
        }

        [HttpGet("{intersectionName}")]
        public async Task<ActionResult<TrafficLightIntersection>> Get(string intersectionName)
        {
            return Ok(await _eventRepository.Get(intersectionName));
        }

        [HttpGet]
        public ActionResult<IEnumerable<TrafficLightIntersection>> GetAll()
        {
            return Ok(_eventRepository.GetAll());
        }

        [HttpPost("commands/{intersectionName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public async Task<ActionResult> Post(string intersectionName, CommandRequest command)
        {
            var trafficLightIntersection = await _eventRepository.Get(intersectionName);

            switch (command.Command)
            {
                case ProccessCommandRequest.Run:
                    _signalManager.Stop();// stop any if running
                    _signalManager.Run(trafficLightIntersection, command);
                    break;
                case ProccessCommandRequest.Stop:
                    _signalManager.Stop();// stop any if running
                    break;
                case ProccessCommandRequest.Reset:
                    _signalManager.Stop();// stop any if running
                    _signalManager.Run(trafficLightIntersection, command);
                    break;
                default:
                    _logger.LogError($"There is no {command.Command}");
                    break;
            }

            return Ok();
        }

    }
}
