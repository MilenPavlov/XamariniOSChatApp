using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using XamChat.Core.ViewModels;
using XamChat.Core;
using System.Threading.Tasks;

namespace XamChat.iOS
{
	partial class LoginController : UIViewController
	{
		private readonly LoginViewModel loginViewModel = ServiceContainer.Resolve<LoginViewModel>();
		public LoginController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad ();
            
		    button_Login.TouchUpInside += async (sender, args) =>
		    {
                loginViewModel.Username = textField_Username.Text;
                loginViewModel.Password = textField_Password.Text;

                try
                {
                    await loginViewModel.Login();
                    PerformSegue("OnLogin", this);
                }

                catch (Exception ex)
                {
                    new UIAlertView("Oops", ex.Message, null, "Ok").Show();
                }
		    };

		}

		void OnIsBusyChanged (object sender, EventArgs e)
		{
			textField_Username.Enabled =
				textField_Password.Enabled =
					button_Login.Enabled =
						indicator.Hidden = !loginViewModel.IsBusy;
		}

	    public override void ViewWillAppear(bool animated)
	    {
	        base.ViewWillAppear(animated);
            loginViewModel.IsBusyChanged += OnIsBusyChanged;
	    }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
            loginViewModel.IsBusyChanged -= OnIsBusyChanged;
        }
	}
}
