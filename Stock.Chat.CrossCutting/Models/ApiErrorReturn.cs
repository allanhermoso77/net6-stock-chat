namespace Stock.Chat.CrossCutting.Models
{
    public class ApiErrorReturn
    {
        public bool Success { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}
