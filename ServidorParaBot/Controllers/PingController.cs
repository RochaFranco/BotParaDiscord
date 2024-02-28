using Microsoft.AspNetCore.Mvc;

namespace ServidorParaBot.Controllers
{
    [ApiController]
    [Route("Ping")]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Pong";
        }
    }
}
