using Microsoft.AspNetCore.Mvc;

namespace ServidorParaBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InteractionsController : ControllerBase
    {
        [HttpPost]
        public Interactions Post()
        {
            return new Interactions(1);
        }

    }

}
