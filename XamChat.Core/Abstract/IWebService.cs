using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamChat.Core.Models;

namespace XamChat.Core.Abstract
{
    public interface IWebService
    {
        Task<User> Login(string username, string password);
        Task<User> Register(User user);

        Task<User[]> GetFriends(string userId);

        Task<User> AddFriend(string userId, string username);

        Task<Conversation[]> GetConversations(string userId);
        Task<Message[]> GetMessages(int conversationId);
        Task<Message> SendMessage(Message message);
    }
}
