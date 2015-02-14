using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Bluetooth;
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
    public class FriendsAdapter: BaseAdapter<User>
    {
        private LayoutInflater inflater;
        private readonly FriendViewModel friendViewModel = ServiceContainer.Resolve<FriendViewModel>();
        public FriendsAdapter(Context context)
        {
            inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
        }
        public override long GetItemId(int position)
        {
            return friendViewModel.Friends[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            if (convertView == null)
            {
                convertView = inflater.Inflate(Resource.Layout.FriendsListItem, null);
            }

            var friend = friendViewModel.Friends[position];

            var friendName = convertView.FindViewById<TextView>(Resource.Id.textView_friendName);
            friendName.Text = friend.Username;

            return convertView;
        }

        public override int Count
        {
            get { return friendViewModel.Friends==null ? 0 : friendViewModel.Friends.Length; }
        }

        public override User this[int position]
        {
            get {return friendViewModel.Friends[position]; }
        }
    }
}