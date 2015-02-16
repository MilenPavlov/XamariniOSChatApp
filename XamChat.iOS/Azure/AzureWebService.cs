using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using XamChat.Core.Abstract;
using XamChat.Core.Models;

namespace XamChat.iOS.Azure
{
    public class AzureWebService : IWebService
    {

        MobileServiceClient client = new MobileServiceClient(
            "https://milenpavlov-xamcharjs.azure-mobile.net/",
            "BoLTMTiRQJzaDAGlmUSTHuUkqwxzgS90");

        public AzureWebService()
        {
            CurrentPlatform.Init();
        }
        public async Task<User> Login(string username, string password)
        {
            var user = new User
            {
                Username = username,
                Password = password
            };

            await client.GetTable<User>().InsertAsync(user);

            return user;
        }

        public Task<User> Register(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User[]> GetFriends(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> AddFriend(int userId, string username)
        {
            throw new NotImplementedException();
        }

        public Task<Conversation[]> GetConversations(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<Message[]> GetMessages(int conversationId)
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
