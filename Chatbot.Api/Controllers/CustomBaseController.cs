using Chatbot.Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Chatbot.Api.Controllers
{
    public class CustomBaseController:ControllerBase
    {
        protected User GetUser()
        {
            return (User)this.HttpContext.Items["User"]!;
        }

    }
}
