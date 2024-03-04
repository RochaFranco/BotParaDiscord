using Org.BouncyCastle.Asn1.Ocsp;
using ServidorParaBot.Controllers;

namespace ServidorParaBot
{
    public class InteractionsResponse
    {
        public ResponseTypesEnum type { get; set; }
        public DataResponse? data { get; set; }
        public Task<DolarBlue> Task { get; set; }
        public DolarBlue DolarBlue { get; set; }

        public InteractionsResponse(ResponseTypesEnum type)
        {
            this.type = type;
        }

        public InteractionsResponse(string message)
        {
            this.type = ResponseTypesEnum.CHANNEL_MESSAGE_WITH_SOURCE;
            this.data = new DataResponse(message);
        }

        public InteractionsResponse(Task<DolarBlue> task)
        {
            Task = task;
        }

        public InteractionsResponse(DolarBlue dolarBlue)
        {
            DolarBlue = dolarBlue;
        }
    }

    public enum ResponseTypesEnum
    {
        PONG = 1,
        CHANNEL_MESSAGE_WITH_SOURCE = 4,
        DEFERRED_CHANNEL_MESSAGE_WITH_SOURCE = 5,
        DEFERRED_UPDATE_MESSAGE = 6,
        UPDATE_MESSAGE = 7,
        APPLICATION_COMMAND_AUTOCOMPLETE_RESULT = 8,
        MODAL = 9,
        PREMIUM_REQUIRED = 10
    }

    public class DataResponse
    {
        public string? content {  get; set; }

        public DataResponse(string content)
        {
            this.content = content;
        }

    }

}


