namespace Chatbot.Api.Entities
{
    public class Chat
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public virtual List<ChatMessage> Messages { get; set; }=new List<ChatMessage>();
        public virtual User User { get; set; } = new User();
    }
}
