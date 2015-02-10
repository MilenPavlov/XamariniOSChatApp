using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using XamChat.Core.Models;

namespace XamChat.Core.ViewModels
{
    public class RegisterViewModel: BaseViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public async Task Register()
        {
            if (string.IsNullOrEmpty(Username))
            {
                throw new Exception("Username is blank");
            }

            if (string.IsNullOrEmpty(Password))
            {
                throw new Exception("Password is blank");
            }

            if (Password != ConfirmPassword)
            {
                throw new Exception("Passwords do not match");
            }

            IsBusy = true;

            try
            {
                settings.User = await service.Register(new User
                {
                    Username = Username,
                    Password = Password
                });

                settings.Save();
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
