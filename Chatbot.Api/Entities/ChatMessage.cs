namespace Chatbot.Api.Entities
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsUser { get; set; }
        public virtual Chat Chat { get; set; } = null!;
    }
}
