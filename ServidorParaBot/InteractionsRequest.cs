namespace ServidorParaBot
{
    public class InteractionsRequest
    {
        public RequestTypesEnum type { get; set; }
        public DataRequest data {  get; set; }
    }

    public enum RequestTypesEnum
    {
        PING = 1,
        APPLICATION_COMMAND = 2,
        MESSAGE_COMPONENT = 3,
        APPLICATION_COMMAND_AUTOCOMPLETE = 4,
        MODAL_SUBMITE = 5
    }

    public class DataRequest
    {
        public string name { get; set; }
    }


}




