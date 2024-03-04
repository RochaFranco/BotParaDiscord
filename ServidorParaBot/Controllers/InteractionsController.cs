using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Signers;
using Org.BouncyCastle.Utilities.Encoders;
using System;
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
                            var dolarBlue = await PrecioDolarBlue();
                            var dolarOficial = await PrecioDolarOficial();
                            return Ok(new InteractionsResponse(dolarBlue.ToString() + "\n" + dolarOficial.ToString()));
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

        public static async Task<DolarBlue> PrecioDolarBlue()
        {
            string url = "https://dolarapi.com/v1/dolares/blue";

            HttpClient client = new HttpClient();

            var httpResponde = await client.GetAsync(url);

            if (httpResponde.IsSuccessStatusCode)
            {
                var content = await httpResponde.Content.ReadAsStringAsync();

                DolarBlue dolar = JsonSerializer.Deserialize<DolarBlue>(content);
                

                return dolar;
            }
            else
            {
                return null;
            }
        }

        public static async Task<DolarOficial> PrecioDolarOficial()
        {
            string url = "https://dolarapi.com/v1/dolares/oficial";

            HttpClient client = new HttpClient();

            var httpResponde = await client.GetAsync(url);

            if (httpResponde.IsSuccessStatusCode)
            {
                var content = await httpResponde.Content.ReadAsStringAsync();

                DolarOficial dolar = JsonSerializer.Deserialize<DolarOficial>(content);


                return dolar;
            }
            else
            {
                return null;
            }
        }



    }

    public class DolarBlue
    {

        public string nombre {  get; set; }
        public int compra { get; set; }
        public int venta { get; set; }

        public override string ToString()
        {
            var texto = "DOLAR BLUE \n El precio de compra es de: " + compra + "\n El precio de venta es de: " + venta;

            return string.Join("",texto);
        }

    }

    public class DolarOficial
    {
        public string nombre { get; set; }
        public int compra { get; set; }
        public int venta { get; set; }

        public override string ToString()
        {
            var texto = "DOLAR OFICIAL \n El precio de compra es de: " + compra + "\n El precio de venta es de: " + venta;

            return string.Join("", texto);
        }
    }



}



