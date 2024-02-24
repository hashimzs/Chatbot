namespace Chatbot.Application.Shared
{
    public class ChatInfoDto
    {
        public int Id { get; set; }
        public string FirstMessage { get; set; }=string.Empty;
        public DateTime DateCreated { get; set; }
    }
}
