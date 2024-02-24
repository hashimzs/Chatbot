using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Application.Shared
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string Message { get; set; } = string.Empty;
        //whether the message is from the bot or the user
        public bool IsUser { get; set; }
    }
}
