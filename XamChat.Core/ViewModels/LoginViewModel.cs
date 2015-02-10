using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamChat.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public async Task Login()
        {
            if (string.IsNullOrEmpty(Username))
            {
                throw new Exception("Username is blank");    
            }

            if (string.IsNullOrEmpty(Password))
            {
                throw new Exception("Password is blank");
            }

            IsBusy = true;
            try
            {
                settings.User = await service.Login(Username, Password);
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
