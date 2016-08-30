using Vnap.Entity;

namespace Vnap.Service.Requests.Post
{
    public class GetPostsRq : BaseRq
    {
        public PostType PostType { get; set; }
    }
}
