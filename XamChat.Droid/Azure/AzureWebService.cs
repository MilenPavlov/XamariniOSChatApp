using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using XamChat.Core.Abstract;
using XamChat.Core.Azure;
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
                };
                var f2 = new Friend()
                {
                    MyId = friend.Id,
                    Username = me.Username,                 
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

        public async Task<User> Register(User user)
        {
            await client.GetTable<User>().InsertAsync(user);
            return user;
        }

        public async Task<User[]> GetFriends(string userId)
        {
            var myFriendsList = await client.GetTable<Friend>()
                .Where(x => x.MyId == userId).ToListAsync();

            return myFriendsList.Select(x => new User
            {
                Id = x.UserId, Username = x.Username
            }).ToArray();

        }

        public async Task<User> AddFriend(string userId, string username)
        {
            var friend = new Friend
            {
                MyId = userId,
                Username = username
            };

            await client.GetTable<Friend>().InsertAsync(friend);

            return new User{Id = friend.UserId, Username = friend.Username};
        }

        public async Task<Conversation[]> GetConversations(string userId)
        {
            var myConversationsList = await client.GetTable<Conversation>()
                .Where(x => x.MyId == userId).ToListAsync();

            return myConversationsList.ToArray();
        }

        public async Task<Message[]> GetMessages(string conversationId)
        {
            var myMessagesList = await client.GetTable<Message>()
                .Where(x => x.ConversationId == conversationId).ToListAsync();

            return myMessagesList.ToArray();
        }

        public async Task<Message> SendMessage(Message message)
        {
            await client.GetTable<Message>().InsertAsync(message);

            return message;
        }

        public async Task RefisterPush(string userId, string deviceToken)
        {
            await client.GetTable<Device>()
                .InsertAsync(new Device
                {
                    UserId = userId,
                    DeviceToken = deviceToken
                });
        }
    }
}
