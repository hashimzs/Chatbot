using Chatbot.Api.Entities;
using Chatbot.Api.Extensions;
using Chatbot.Api.Middleware;
using Chatbot.Api.Repositories;
using Chatbot.Api.ReqDto;
using Chatbot.Application.Shared;
using Chatbot.Application.Shared.ReqDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Json;

namespace Chatbot.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : CustomBaseController
    {
        private readonly ChatRepository chatRepository;

        public ChatController(ChatRepository chatRepository)
        {
            this.chatRepository = chatRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetUserChats([FromQuery] PaginationParams paginationParams)
        {
            var user = this.GetUser();

            var chatQ = this.chatRepository.GetUserChats(user.Id);

            var chatDto = chatQ.ToChatDto();

            return Ok(await chatDto.ToPagedList(paginationParams.PageNumber, paginationParams.PageSize));
        }
        [HttpGet("{Id:int}")]
        public async Task<ActionResult> GetChatMessages(int Id, [FromQuery] PaginationParams paginationParams)
        {
            var user = this.GetUser();

            var MessageQ = this.chatRepository.GetChatMessages(Id).OrderByDescending(v => v.Id);

            var MessageDto = MessageQ.ToMessageDto();

            return Ok(await MessageDto.ToPagedList(paginationParams.PageNumber, paginationParams.PageSize));
        }
        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> DeleteChat(int Id)
        {
            await this.chatRepository.DeleteChat(Id);

            return Ok();
        }


        [HttpPost]
        public async Task<ActionResult> GetReply([FromBody] QuesReqDto quesReqDto)
        {

            GenerateAiRequestDto requestDto = new(quesReqDto.Message);

            var transaction = this.chatRepository.GetTransaction();

            Chat? NewChat = null;

            if (quesReqDto.ChatId == 0)
            {
                NewChat = await this.chatRepository.AddChat(this.GetUser().Id);
                quesReqDto.ChatId = NewChat.Id;
            }
            else
                requestDto.History = await this.chatRepository.GetChatMessages(quesReqDto.ChatId)
                    .OrderByDescending(v => v.Id)
                    .Take(10)
                    .Select(c => c.Message)
                    .ToListAsync();

            var UserMessage = new ChatMessage
            {
                Id = 0,
                ChatId = quesReqDto.ChatId,
                Message = quesReqDto.Message,
                IsUser = true
            };

            await this.chatRepository.AddMessage(UserMessage);

            var reply = await this.GetAiReply(requestDto);

            var AiMessage = new ChatMessage
            {
                Id = 0,
                IsUser = false,
                ChatId = quesReqDto.ChatId,
                Message = reply
            };

            var result = await this.chatRepository.AddMessage(AiMessage);

            await transaction.CommitAsync();

            var chatDto = result.ToMessageDto();

            var AiResponseDto = new AiResponseDto
            {
                ChatMessage = chatDto,
            };

            if (NewChat != null)
                AiResponseDto.ChatInfo = NewChat.ToChatDto();

            return Ok(AiResponseDto);
        }

        private async Task<string> GetAiReply(GenerateAiRequestDto requestDto)
        {
            try
            {
                string url = "http://localhost:105"; // Replace with your desired URL

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = await client.PostAsJsonAsync<GenerateAiRequestDto>(url, requestDto);

                    if (response.IsSuccessStatusCode)
                        return await response.Content.ReadAsStringAsync();
                    else
                        throw new Exception();
                }
            }
            catch (Exception)
            {
                throw new Exception("an error occurer when trying to generate response");
            }
        }

    }
}
