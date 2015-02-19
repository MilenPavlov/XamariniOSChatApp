using System;
using System.Threading.Tasks;
using Android.App;
using Android.Runtime;
using XamChat.Core;
using XamChat.Core.Abstract;
using XamChat.Core.Fakes;
using XamChat.Core.ViewModels;
using XamChat.Droid.Azure;

namespace XamChat.Droid
{
    [Application(Theme = "@android:style/Theme.Holo.Light")]
    public class Application: Android.App.Application
    {
        public Application(IntPtr javaReference, JniHandleOwnership transfer):base(javaReference, transfer)
        {}

        public async override void OnCreate()
        {
           //view models

            ServiceContainer.Register<LoginViewModel>(()=> new LoginViewModel());
            ServiceContainer.Register<FriendViewModel>(() => new FriendViewModel());
            ServiceContainer.Register<MessageViewModel>(() => new MessageViewModel());
            ServiceContainer.Register<RegisterViewModel>(() => new RegisterViewModel());

            ServiceContainer.Register<ISettings>(() => new FakeSettings());
            ServiceContainer.Register<IWebService>(() => new AzureWebService());

            await LoadData();
        }

        private async Task LoadData()
        {
            var service = ServiceContainer.Resolve<IWebService>() as AzureWebService;
            if (service != null)
                await service.LoadData();
        }
    }
}