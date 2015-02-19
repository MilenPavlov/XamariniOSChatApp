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
            "tPZMmYCVPILaVuDmygNkzxxevpioQw83");

        public AzureWebService()
        {
            CurrentPlatform.Init();
        }

        public async Task LoadData()
        {
            var users = client.GetTable<User>();
            var friends = client.GetTable<Friend>();
            var conversations = client.GetTable<Conversation>();
            var messages = client.GetTable<Message>();

            var me = new User()
            {
                Username = "milenpavlov",
                Password = "password"
            };

            var friend = new User()
            {
                Username = "chucknorris",
                Password = "password"
            };
          
            try
            {
                await users.InsertAsync(me);
                await users.InsertAsync(friend);
                var f1 = new Friend()
                {
                    MyId = me.Id,
                    Username = friend.Username,
                    //UserId = "2",
                 
                };
                var f2 = new Friend()
                {
                    MyId = friend.Id,
                    Username = me.Username,
                   // UserId = "1",
                  
                };

                await friends.InsertAsync(f1);

                await friends.InsertAsync(f2);

                var conversation = new Conversation { MyId = me.Id, UserId = friend.Id, Username = friend.Username, LastMessage = "HEY!" };

                await conversations.InsertAsync(conversation);
                await messages.InsertAsync(new Message
                {
                    ConversationId = conversation.Id,
                    UserId = friend.Id,
                    Username = friend.Username,
                    Text = "What's up?",
                    Date = DateTime.Now.AddSeconds(-60),
                    ToId = me.Id
                });
                await messages.InsertAsync(new Message
                {
                    ConversationId = conversation.Id,
                    UserId = me.Id,
                    Username = me.Username,
                    Text = "Not much",
                    Date = DateTime.Now.AddSeconds(-30),
                    ToId = friend.Id
                });
                await messages.InsertAsync(new Message
                {
                    ConversationId = conversation.Id,
                    UserId = friend.Id,
                    Username = friend.Username,
                    Text = "HEY!",
                    Date = DateTime.Now,
                    ToId = me.Id
                });
                
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
          
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

        public Task<Message[]> GetMessages(string conversationId)
        {
            throw new NotImplementedException();
        }

        public Task<Message> SendMessage(Message message)
        {
            throw new NotImplementedException();
        }
    }
}
