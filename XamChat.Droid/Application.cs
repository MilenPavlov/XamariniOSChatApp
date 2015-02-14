using System;
using Android.App;
using Android.Runtime;
using XamChat.Core;
using XamChat.Core.Abstract;
using XamChat.Core.Fakes;
using XamChat.Core.ViewModels;

namespace XamChat.Droid
{
    [Application(Theme = "@android:style/Theme.Holo.Light")]
    public class Application: Android.App.Application
    {
        public Application(IntPtr javaReference, JniHandleOwnership transfer):base(javaReference, transfer)
        {}

        public override void OnCreate()
        {
           //view models

            ServiceContainer.Register<LoginViewModel>(()=> new LoginViewModel());
            ServiceContainer.Register<FriendViewModel>(() => new FriendViewModel());
            ServiceContainer.Register<MessageViewModel>(() => new MessageViewModel());
            ServiceContainer.Register<RegisterViewModel>(() => new RegisterViewModel());

            ServiceContainer.Register<ISettings>(() => new FakeSettings());
            ServiceContainer.Register<IWebService>(() => new FakeWebService());
        }
    }
}