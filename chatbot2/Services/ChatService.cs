using Chatbot.Application.Shared;
using Chatbot.Application.Shared.ReqDtos;
using System.Net.Http.Json;

namespace chatbot2.Services
{
    public class ChatService
    {
        private readonly HttpClient httpClient;

        public ChatService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public Action<int> OnChatDeleted { get; set; }
        public Action<ChatInfoDto> OnChatStarted { get; set; }

        public async Task<PagedList<ChatInfoDto>> GetChats(PaginationParams paginationParams)
        {
            return await this.httpClient.GetFromJsonAsync<PagedList<ChatInfoDto>>($"api/chat?pagenumber={paginationParams.PageNumber}&pagesize={paginationParams.PageSize}")!;
        }

        public async Task<ChatMessageDto> GetBotRespose(ChatMessageDto chatMessage)
        {
            var req = new QuesReqDto { ChatId=chatMessage.ChatId,Message=chatMessage.Message };

            var response = await this.httpClient.PostAsJsonAsync<QuesReqDto>("api/chat",req);

            if(!response.IsSuccessStatusCode)
            {
                var content=await response.Content.ReadAsStringAsync();
                throw new Exception(content);
            }

            var ChatReply = await response.Content.ReadFromJsonAsync<AiResponseDto>();

            if (ChatReply.ChatInfo != null)
                OnChatStarted.Invoke(ChatReply.ChatInfo);

            return ChatReply.ChatMessage;
        }

        public async Task DeleteChat(int Id)
        {
            var response = await this.httpClient.DeleteAsync($"api/chat/{Id}");

            if (!response.IsSuccessStatusCode)
                return;

            if (OnChatDeleted != null)
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
                FirstMessage = "Should i buy nividia stocks",
            },

        };
    }

}
