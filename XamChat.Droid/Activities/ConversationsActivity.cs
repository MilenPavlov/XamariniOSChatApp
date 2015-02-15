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
using XamChat.Droid.Adapters;

namespace XamChat.Droid.Activities
{
    [Activity(Label = "Conversations")]
    public class ConversationsActivity : BaseActivity<MessageViewModel>
    {
        private ListView conversationsListView;
        private ConversationsAdapter conversationsAdapter;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Conversations);


            AttachControls();
        }

        private void AttachControls()
        {
            conversationsListView = FindViewById<ListView>(Resource.Id.listView_conversations);
            conversationsListView.Adapter = conversationsAdapter = new ConversationsAdapter(this);
            conversationsListView.ItemClick += (sender, args) =>
            {
                viewModel.Conversation = conversationsAdapter[args.Position];
                StartActivity(typeof(MessageActivity));
            };
        }

        protected async override void OnResume()
        {
            base.OnResume();
            try
            {
                await viewModel.GetConversations();
                conversationsAdapter.NotifyDataSetInvalidated();
            }
            catch (Exception ex)
            {
               DisplayError(ex);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.ConversationsMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == Resource.Id.addFriendMenu)
            {
                StartActivity(typeof(FriendsActivity));
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}