using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ServidorParaBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InteractionsController : ControllerBase
    {
        [HttpPost]
        public ActionResult Post([FromHeader(Name = "X-Signature-Ed25519")] string? signature, [FromHeader(Name = "X-Signature-Timestamp")] string? timestamp)
        {
            if (String.IsNullOrWhiteSpace(signature) || String.IsNullOrWhiteSpace(timestamp))
            {
                return Unauthorized();
            }

            return Ok(new Interactions(1));

        }

        

    }

}
