using Android.Content;
using Android.Views;
using Android.Widget;
using XamChat.Core;
using XamChat.Core.Abstract;
using XamChat.Core.Models;
using XamChat.Core.ViewModels;

namespace XamChat.Droid.Adapters
{
    public class MessagesAdapter : BaseAdapter<Message>
    {
        private readonly LayoutInflater inflater;
        private readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();
        private readonly ISettings setting = ServiceContainer.Resolve<ISettings>();
        private const int MyMessageType = 0, TheirMessageType = 1;

        public MessagesAdapter(Context context)
        {
            inflater = (LayoutInflater) context.GetSystemService(Context.LayoutInflaterService);
        }
        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var message = messageViewModel.Messages[position];
            int type = GetItemViewType(position);

            if (convertView == null)
            {
                if (type == MyMessageType)
                {
                    convertView = inflater.Inflate(Resource.Layout.MyMessageListItem, null);
                }
                else
                {
                    convertView = inflater.Inflate(Resource.Layout.TheirMessageListItem, null);
                }            
            }

            TextView messageText, dateText;

            if (type == MyMessageType)
            {
                messageText = convertView.FindViewById<TextView>(Resource.Id.textView_myMessageText);
                dateText = convertView.FindViewById<TextView>(Resource.Id.textView_myMessageDate);
            }
            else
            {
                messageText = convertView.FindViewById<TextView>(Resource.Id.textView_theirMessageText);
                dateText = convertView.FindViewById<TextView>(Resource.Id.textView_theirMessageDate);
            }

            messageText.Text = message.Text;
            dateText.Text = message.Date.ToString("MM/dd/yy HH:mm");

            return convertView;
        }

        public override int Count
        {
            get { return messageViewModel.Messages == null ? 0 : messageViewModel.Messages.Length; }
        }

        public override Message this[int position]
        {
            get { return messageViewModel.Messages[position]; }
        }

        public override int ViewTypeCount
        {
            get { return 2; }
        }

        public override int GetItemViewType(int position)
        {
            var message = this[position];
            return message.UserId == setting.User.Id ? MyMessageType : TheirMessageType;
        }
    }
}