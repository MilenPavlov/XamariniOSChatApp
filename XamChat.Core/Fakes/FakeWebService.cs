using System;
using System.Threading.Tasks;
using XamChat.Core.Abstract;
using XamChat.Core.Models;

namespace XamChat.Core.Fakes
{
    public class FakeWebService :IWebService
    {
        public int SleepDuration { get; set; }

        public FakeWebService()
        {
            SleepDuration = 1;
        }

        private Task Sleep()
        {
            return Task.Delay(SleepDuration);
        }

        #region IWebService implementation
        public async Task<User> Login(string username, string password)
        {
            await Sleep();

            return new User
            {
                Id = "1",
                Username = username
            };
        }

        public async Task<User> Register(User user)
        {
            await Sleep();

            return user;
        }

        public async Task<User[]> GetFriends(string userId)
        {
            await Sleep();

            return new User[]
            {
                new User {Id = "2", Username = "bobama"},
                new User {Id = "3", Username = "bobloblaw"},
                new User {Id = "4", Username = "gmichael"}
            };
        }

        public async Task<User> AddFriend(string userId, string username)
        {
            await Sleep();

            return new User {Id = "4", Username = username};
        }

        public async Task<Conversation[]> GetConversations(string userId)
        {
            await Sleep();

            return new Conversation[]
            {
            new Conversation { Id = "1", UserId = "2", Username = "bobama", LastMessage = "Hey!" },
                new Conversation { Id = "2", UserId = "3", Username = "bobloblaw", LastMessage = "Have you seen that new movie?" },
                new Conversation { Id = "3", UserId = "4", Username = "gmichael", LastMessage = "What?" },
            };
        }

        public async Task<Message[]> GetMessages(string conversationId)
        {
            await Sleep();

            return new[]
            {
                //new Message { Id = 1, ConversationId = conversationId, UserId = 2, Username = "bobama", Text = "Hey" },
                //new Message { Id = 2, ConversationId = conversationId, UserId = 1, Username = "testuser", Text = "What's up?" },
                //new Message { Id = 1, ConversationId = conversationId, UserId = 2, Username = "bobama", Text = "Have you seen that new movie?" },
                //new Message { Id = 2, ConversationId = conversationId, UserId = 1, Username = "testuser", Text = "It's great!" },
                new Message { Id = "2", ConversationId = conversationId, UserId = "1", Username = "testuser", Text = "What's up?", Date = DateTime.Now.AddDays(-1) },
                new Message { Id = "3", ConversationId = conversationId, UserId = "2", Username = "bobama", Text = "Have you seen that new movie?", Date = DateTime.Now.AddMinutes(-1) },
                new Message { Id = "4", ConversationId = conversationId, UserId = "1", Username = "testuser", Text = "It's great!", Date = DateTime.Now.AddSeconds(-30) },
                new Message { Id = "5", ConversationId = conversationId, UserId = "2", Username = "bobama", Text = "Cool", Date = DateTime.Now.AddSeconds(-15) },
            };

            
        }

        public async Task<Message> SendMessage(Message message)
        {
            await Sleep();

            return message;
        }

        public async Task RefisterPush(string userId, string deviceToken)
        {
            await Sleep();
        }

        #endregion
    }
}
