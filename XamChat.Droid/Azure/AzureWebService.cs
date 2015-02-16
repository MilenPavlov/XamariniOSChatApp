using System;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using XamChat.Core.Abstract;
using XamChat.Core.Models;

namespace XamChat.Droid.Azure
{
    public class AzureWebService : IWebService
    {

        MobileServiceClient client = new MobileServiceClient(
            "https://milenpavlov-xamchatjs.azure-mobile.net/",
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

            try
            {
                await client.GetTable<User>().InsertAsync(user);
            }
            catch (Exception ex)
            {
                 
            }
           

            return user;
        }

        public Task<User> Register(User user)
        {
            throw new NotImplementedException();
        }

        public Task<User[]> GetFriends(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> AddFriend(string userId, string username)
        {
            throw new NotImplementedException();
        }

        public Task<Conversation[]> GetConversations(string userId)
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
