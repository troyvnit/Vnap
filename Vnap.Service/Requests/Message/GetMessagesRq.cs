using Vnap.Entity;

namespace Vnap.Service.Requests.Message
{
    public class GetMessagesRq : BaseRq
    {
        public string ConversationName { get; set; }
    }
}
