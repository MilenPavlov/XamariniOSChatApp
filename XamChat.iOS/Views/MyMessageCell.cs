using System;
using Foundation;
using UIKit;
using XamChat.Core.Models;

namespace XamChat.iOS.Views
{
	partial class MyMessageCell : BaseMessageCell
	{

		public MyMessageCell (IntPtr handle) : base (handle)
		{
		}
        public override void Update(Message message)
        {
            this.message.Text = message.Text;
            //this.date.Text = message.Date.ToString("MM/dd/yy H:mm");
        }

	}
}
