using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Utilities.Encoders;
using System.Net;
using System.Security;
using System.Text;
using System.Text.Json;

namespace ServidorParaBot.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InteractionsController : ControllerBase
    {

        private readonly ILogger<InteractionsController> _logger;

        public InteractionsController(ILogger<InteractionsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromHeader(Name = "X-Signature-Ed25519")] string? signature, [FromHeader(Name = "X-Signature-Timestamp")] string? timestamp)
        {

            if (String.IsNullOrWhiteSpace(signature) || String.IsNullOrWhiteSpace(timestamp))
            {
                return Unauthorized();
            }

            string body = string.Empty;
            using (var reader = new StreamReader(Request.Body,
                          encoding: Encoding.UTF8, detectEncodingFromByteOrderMarks: false))
            {
                body = await reader.ReadToEndAsync();

                if (VerifySignature(Environment.GetEnvironmentVariable("PUBLIC_KEY"), timestamp + body, signature))
                {
                    InteractionsRequest interaction = JsonSerializer.Deserialize<InteractionsRequest>(body);

                    if (interaction.type ==  RequestTypesEnum.PING)
                    {
                        return Ok(new InteractionsResponse(ResponseTypesEnum.PONG));
                    }

                    if (interaction.type == RequestTypesEnum.APPLICATION_COMMAND)
                    {
                        if (interaction.data.name == "test")
                        {
                            return Ok(new InteractionsResponse("Mi primer test uwu"));
                        }

                        if (interaction.data.name == "dolar")
                        {
                            return Ok(new InteractionsResponse("420.69"));
                        }
                    }



                    return Ok();
                }
            }
            return Unauthorized();    
        }

        public bool VerifySignature(string publicKey, string dataToVerify, string signature)
        {
            var publicKeyParam = new Ed25519PublicKeyParameters(Hex.DecodeStrict(publicKey));
            var dataToVerifyBytes = Encoding.UTF8.GetBytes(dataToVerify);
            var signatureBytes = Convert.FromHexString(signature);

            var verifier = new Ed25519Signer();
            verifier.Init(false, publicKeyParam);
            verifier.BlockUpdate(dataToVerifyBytes, 0, dataToVerifyBytes.Length);
            var isVerified = verifier.VerifySignature(signatureBytes);
            return isVerified;
        }

        

    }

}
