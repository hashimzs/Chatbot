using Chatbot.Api.Data;
using Chatbot.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;

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
            return this.appDbContext.ChatMessages.Where(v => v.ChatId == ChatId).AsQueryable();
        }

        public IQueryable<Chat> GetUserChats(int UserId)
        {
            return this.appDbContext.Chats.Where(v => v.UserId == UserId).AsQueryable();
        }

        public IDbContextTransaction GetTransaction()
        {
            return this.appDbContext.Database.BeginTransaction();
        }

        public async Task<Chat> AddChat(int userId)
        {
            var Chat = new Chat
            {
                UserId = userId,
                Created = DateTime.Now,
            };

            var result = await this.appDbContext.Chats.AddAsync(Chat);

            await this.appDbContext.SaveChangesAsync();

            return result.Entity;
        }

        public async Task<ChatMessage> AddMessage(ChatMessage chatMessage)
        {
            var result = await this.appDbContext.ChatMessages.AddAsync(chatMessage);

            await this.appDbContext.SaveChangesAsync();

            return result.Entity;
        }
        public async Task DeleteChat(int Id)
        {
            var Chat = await this.appDbContext.Chats.FirstOrDefaultAsync(v => v.Id == Id);

            if (Chat == null) return;

            this.appDbContext.Chats.Remove(Chat);

            await this.appDbContext.SaveChangesAsync();
        }

    }
}
