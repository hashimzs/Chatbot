using Chatbot.Api.Repositories;

namespace Chatbot.Api.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;
        //private readonly IUserRepository userRepository;

        public JwtMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, UserRepository userRepository, TokenManager tokenManager)
        {
            var token = context.Request.Headers["token"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await AttachUserToContext(context, token, userRepository, tokenManager);
            }

            await next(context);
        }

        public async Task AttachUserToContext(HttpContext context, string token, UserRepository userRepository,TokenManager tokenManager)
        {
            try
            {
                var userId = tokenManager.ValidateToken(token);

                var user = await userRepository.GetUserById(userId);

                context.Items["User"] = user;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
