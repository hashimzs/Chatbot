using Chatbot.Application.Shared;

namespace chatbot2.Services
{
    public class ChatService
    {

        public Action<int> OnChatDeleted { get; set; }
        public async Task<ChatInfoDto> CreateNewChat(string Message)
        {
            return PlaceHolderData.Chats.First();
        }

        public async Task<PagedList<ChatInfoDto>> GetChats(PaginationParams paginationParams)
        {
            return new PagedList<ChatInfoDto>(PlaceHolderData.Chats, PlaceHolderData.Chats.Count, paginationParams.PageNumber, paginationParams.PageSize);
        }

        public async Task<ChatMessage> GetBotRespose(ChatMessage chatMessage)
        {
            await Task.Delay(10000);

            return new ChatMessage
            {
                IsUser=false,
                Message="its ok brother we can talk about it"
            };
        }

        public async Task DeleteChat(int Id)
        {
            if(OnChatDeleted != null) 
                OnChatDeleted.Invoke(Id);
        }

    }

    public static class PlaceHolderData
    {
        public static List<ChatInfoDto> Chats = new List<ChatInfoDto>
        {
            new ChatInfoDto
            {
                Id = 1,
                DateCreated = DateTime.Now,
                FirstMessage = "what is the difference between a blazor web assembly and blazor server",
            },
            new ChatInfoDto
            {
                Id = 2,
                DateCreated = DateTime.Now,
                FirstMessage = "what is the difference between a blazor web assembly and blazor server",
            },
            new ChatInfoDto
            {
                Id = 3,
                DateCreated = DateTime.Now,
                FirstMessage = "what is the difference between a blazor web assembly and blazor server",
            },
            new ChatInfoDto
            {
                Id = 4,
                DateCreated = DateTime.Now,
                FirstMessage = "what is the difference between a blazor web assembly and blazor server",
            },
            new ChatInfoDto
            {
                Id = 5,
                DateCreated = DateTime.Now,
                FirstMessage = "what is the difference between a blazor web assembly and blazor server",
            },
            new ChatInfoDto
            {
                Id = 6,
                DateCreated = DateTime.Now,
                FirstMessage = "what is the difference between a blazor web assembly and blazor server",
            },
            new ChatInfoDto
            {
                Id = 7,
                DateCreated = DateTime.Now,
                FirstMessage = "what is the difference between a blazor web assembly and blazor server",
            },
            new ChatInfoDto
            {
                Id = 8,
                DateCreated = DateTime.Now,
                FirstMessage = "what is the difference between a blazor web assembly and blazor server",

            },

        };
    }

}
