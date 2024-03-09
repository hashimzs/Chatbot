using Chatbot.Api.Data;
using Chatbot.Api.Entities;

namespace Chatbot.Api.Repositories
{
    public class ChatRepository
    {
        private readonly AppDbContext appDbContext;

        public ChatRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public IQueryable<ChatMessage> GetChatMessages(int ChatId)
        {
            return this.appDbContext.ChatMessages.Where(v=>v.ChatId== ChatId).AsQueryable();
        }

        public IQueryable<Chat> GetUserChats(int UserId)
        {
            return this.appDbContext.Chats.Where(v => v.UserId == UserId).AsQueryable();
        }
    }
}
