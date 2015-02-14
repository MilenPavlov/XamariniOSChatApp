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
using XamChat.Core;
using XamChat.Core.Models;
using XamChat.Core.ViewModels;

namespace XamChat.Droid.Adapters
{
    public class ConversationsAdapter: BaseAdapter<Conversation>
    {
        private readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();
        private readonly LayoutInflater inflater;

        public ConversationsAdapter(Context context)
        {
            inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
        }
        public override long GetItemId(int position)
        {
            return messageViewModel.Conversations[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = inflater.Inflate(Resource.Layout.ConversationsListItem, null);
            }

            var convesation = this[position];
            var username = convertView.FindViewById<TextView>(Resource.Id.textView_conversationUserName);
            var lastMessage = convertView.FindViewById<TextView>(Resource.Id.textView_conversationLastMessage);

            username.Text = convesation.Username;
            lastMessage.Text = convesation.LastMessage;

            return convertView;
        }

        public override int Count
        {
            get { return messageViewModel.Conversations == null ? 0 : messageViewModel.Conversations.Length; }
        }

        public override Conversation this[int position]
        {
            get { return  messageViewModel.Conversations[position]; }
        }
    }
}