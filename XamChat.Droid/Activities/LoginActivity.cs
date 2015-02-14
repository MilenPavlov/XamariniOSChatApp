using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using XamChat.Core.ViewModels;

namespace XamChat.Droid.Activities
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true)]
    public class LoginActivity: BaseActivity<LoginViewModel>
    {
        private EditText userName,password;
        private Button login;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Login);

            AttachControlls();
        }

        protected override void OnResume()
        {
            base.OnResume();
            userName.Text = password.Text = string.Empty;
        }


        private void AttachControlls()
        {
            userName = FindViewById<EditText>(Resource.Id.username);
            password = FindViewById<EditText>(Resource.Id.password);
            login = FindViewById<Button>(Resource.Id.button_Login);

            login.Click += LoginOnClick;
        }

        private async void LoginOnClick(object sender, EventArgs eventArgs)
        {
            viewModel.Username = userName.Text;
            viewModel.Password = password.Text;
            try
            {
                await viewModel.Login();
                
                StartActivity(typeof(ConversationsActivity));
            }
            catch (Exception ex)
            {
                DisplayError(ex);               
            }
        }
    }
}