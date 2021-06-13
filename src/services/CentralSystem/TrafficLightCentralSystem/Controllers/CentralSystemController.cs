using AutoMapper;
using EventBus.Messages;
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
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]       
        public async Task<ActionResult> Post(IEnumerable<TrafficLighBoundRequest> initSetting)
        {
           
            return Ok();
        }

        [HttpPost("commands")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public async Task<ActionResult> Post(CommandRequest command)
        {
 
            var signalManager = new SignalManager(command, _publishEndpoint);
            switch (command.Command)
            {
                case ProccessCommand.Run:
                    // record command in repository
                    signalManager.Run();
                    break;
                case ProccessCommand.Stop:
                    // record command in repository

                    break;
                case ProccessCommand.Reset:
                    // record command in repository
                    signalManager.Reset();
                    break;
                default:
                    // record command in repository
                    _logger.LogError($"There is no {command.Command}");
                    break;
            }

            return Ok();
        }

    }
}
