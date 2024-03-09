﻿using Chatbot.Application.Shared;

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

        public async Task<ChatMessageDto> GetBotRespose(ChatMessageDto chatMessage)
        {
            await Task.Delay(10000);

            return new ChatMessageDto
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
                FirstMessage = "Should i buy nividia stocks",
            },

        };
    }

}
