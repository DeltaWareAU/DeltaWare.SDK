using DeltaWare.SDK.MessageBroker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Testing.Messages;

namespace Testing.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceBusController : ControllerBase
    {
        private readonly ILogger<ServiceBusController> _logger;

        private readonly IMessageBroker _messageBroker;

        public ServiceBusController(ILogger<ServiceBusController> logger, IMessageBroker messageBroker)
        {
            _logger = logger;
            _messageBroker = messageBroker;
        }

        [HttpPost("SendMessage/Direct")]
        public async Task<IActionResult> SendDirectMessage([FromBody] DirectMessage message)
        {
            await _messageBroker.SendAsync(message);

            return Ok();
        }

        [HttpPost("SendMessage/Topic")]
        public async Task<IActionResult> SendDirectMessage([FromBody] TopicMessage message)
        {
            await _messageBroker.SendAsync(message);

            return Ok();
        }
    }
}
