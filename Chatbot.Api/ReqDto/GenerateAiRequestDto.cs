namespace Chatbot.Api.ReqDto
{
    public class GenerateAiRequestDto
    {
        public string Query { get; set; } = string.Empty;
        public List<string> History { get; set; } = new();

        public GenerateAiRequestDto(string Query)
        {
            this.Query = Query;
        }
    }
}
