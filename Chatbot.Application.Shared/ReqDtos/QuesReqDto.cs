using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chatbot.Application.Shared.ReqDtos
{
    public class QuesReqDto
    {
        public string Message { get; set; } = string.Empty;
        public int ChatId { get; set; }
    }
}
