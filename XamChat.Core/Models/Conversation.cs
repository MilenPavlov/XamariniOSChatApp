namespace XamChat.Core.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }

        public string LastMessage { get; set; }
    }

}
