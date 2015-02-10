using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamChat.Core.Models;

namespace XamChat.Core.ViewModels
{
    public class FriendViewModel: BaseViewModel
    {
        public User[] Friends { get; set; }
        public string Username { get; set; }

        public async Task GetFriends()
        {
            if (settings.User == null)
            {
                throw new Exception("Not logged in.");
            }

            IsBusy = true;
            try
            {
                Friends = await service.GetFriends(settings.User.Id);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async Task AddFriend()
        {
            if (settings.User == null)
            {
                throw new Exception("Not logged in.");
            }

            if (string.IsNullOrEmpty(Username))
            {
                throw new Exception("Username is blank");
            }

            IsBusy = true;
            try
            {
                var friend = await service.AddFriend(settings.User.Id, Username);
                var friends = new List<User>();
                if (Friends != null)
                {
                    friends.AddRange(Friends);
                }
                friends.Add(friend);

                Friends = friends
                    .OrderBy(x => x.Username)
                    .ToArray();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
