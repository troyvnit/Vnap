using Vnap.Web.DataAccess.Entity;

namespace Vnap.Web.DataAccess.Repository
{
    public interface IConversationRepository : IRepository<Conversation>
    { }

    public class ConversationRepository : RepositoryBase<Conversation>, IConversationRepository
    {
        public ConversationRepository(IDbFactory dbFactory)
            : base(dbFactory)
        { }
    }
}
