using Chatbot.Api.Extensions;
using Chatbot.Api.Middleware;
using Chatbot.Api.Repositories;
using Chatbot.Application.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

            return Ok(chatDto.ToPagedList(paginationParams.PageNumber, paginationParams.PageSize));
        }
        [HttpGet("Id:int")]
        public async Task<ActionResult> GetChatMessages(int Id,[FromQuery] PaginationParams paginationParams)
        {
            var user = this.GetUser();

            var MessageQ = this.chatRepository.GetChatMessages(Id).OrderByDescending(v=>v.Id);

            var MessageDto = MessageQ.ToMessageDto();

            return Ok(MessageDto.ToPagedList(paginationParams.PageNumber, paginationParams.PageSize));
        }
    }
}
