using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Application.Shared
{
    public class AiResponseDto
    {
        public ChatInfoDto? ChatInfo { get; set; }
        public ChatMessageDto ChatMessage { get; set; } =new ChatMessageDto();

    }
}
