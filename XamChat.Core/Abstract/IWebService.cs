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

        Task<User[]> GetFriends(int userId);

        Task<User> AddFriend(int userId, string username);

        Task<Conversation[]> GetConversations(int userId);
        Task<Message[]> GetMessages(int conversationId);
        Task<Message> SendMessage(Message message);
    }
}
