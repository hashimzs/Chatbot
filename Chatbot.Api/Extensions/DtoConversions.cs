using Chatbot.Api.Entities;
using Chatbot.Application.Shared;
using Microsoft.EntityFrameworkCore;

namespace Chatbot.Api.Extensions
{
    public static class DtoConversions
    {

        public static IQueryable<ChatInfoDto> ToChatDto(this IQueryable<Chat> chats)
        {
            return chats.Select(v => new ChatInfoDto
            {
                Id = v.Id,
                DateCreated = v.Created,
                FirstMessage = v.Messages.FirstOrDefault()!.Message ?? string.Empty
            });
        }
        public static IQueryable<ChatMessageDto> ToMessageDto(this IQueryable<ChatMessage> messages)
        {
            return messages.Select(v => new ChatMessageDto
            {
                Id = v.Id,
                ChatId = v.ChatId,
                Message = v.Message,
                IsUser = v.IsUser,
            });
        }
        public static async Task<PagedList<T>> ToPagedList<T>(this IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source
              .Skip((pageNumber - 1) * pageSize)
              .Take(pageSize).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
