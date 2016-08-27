using Vnap.Entity;

namespace Vnap.Service.Requests.Plant
{
    public class GetPostsRq : BaseRq
    {
        public PostType PostType { get; set; }
    }
}
