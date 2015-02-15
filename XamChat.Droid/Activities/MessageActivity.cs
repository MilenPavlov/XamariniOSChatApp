using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    [Activity(Label = "MessageActivity")]
    public class MessageActivity : BaseActivity<MessageViewModel>
    {
        private ListView messageListView;
        private EditText messageText;
        private Button sendButton;
        private MessagesAdapter messagesAdapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Messages);

            AttachControls();
        }

        private void AttachControls()
        {
            messageListView = FindViewById<ListView>(Resource.Id.listView_messageList);
            messageText = FindViewById<EditText>(Resource.Id.messageText);
            sendButton = FindViewById<Button>(Resource.Id.sendButton);

            messageListView.Adapter = messagesAdapter = new MessagesAdapter(this);
            sendButton.Click += async (sender, args) =>
            {
                viewModel.Text = messageText.Text;
                try
                {
                    await viewModel.SendMessage();
                    messageText.Text = string.Empty;
                    messagesAdapter.NotifyDataSetInvalidated();
                    messageListView.SetSelection(messagesAdapter.Count);
                }
                catch (Exception ex)
                {
                    DisplayError(ex);
                }
            };
        }

        protected async override void OnResume()
        {
            base.OnResume();
            try
            {
                await viewModel.GetMessages();
                messagesAdapter.NotifyDataSetInvalidated();
                messageListView.SetSelection(messagesAdapter.Count);
            }
            catch (Exception ex)
            {
                DisplayError(ex);
            }
        }
    }
}