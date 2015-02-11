using System;
using Foundation;
using ObjCRuntime;
using UIKit;
using System.CodeDom.Compiler;
using XamChat.Core;
using XamChat.Core.Abstract;
using XamChat.Core.ViewModels;
using XamChat.iOS.Views;

namespace XamChat.iOS
{
	partial class MessagesController : UITableViewController
	{
	    private readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();
		public MessagesController (IntPtr handle) : base (handle)
		{           
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TableView.Source = new TableSource();
        }

        public async override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
            Title = messageViewModel.Conversation.Username;
            try
            {
                await messageViewModel.GetMessages();
                TableView.ReloadData();
            }
            catch (Exception ex)
            {
                
                new UIAlertView("Oops!", ex.Message, null, "Ok").Show();
            }
            
        }

        class TableSource : UITableViewSource
        {
            private const string MyCellName = "MyCell";
            private const string TheirCellName = "TheirCell";
            private readonly MessageViewModel messageViewModel = ServiceContainer.Resolve<MessageViewModel>();
            private readonly ISettings settings = ServiceContainer.Resolve<ISettings>();

            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var message = messageViewModel.Messages[indexPath.Row];
                var isMyMessage = message.UserId == settings.User.Id;

                var cell = tableView.DequeueReusableCell(isMyMessage ? MyCellName : TheirCellName) as BaseMessageCell;
                cell.Update(message);

                return cell;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return messageViewModel.Messages == null ? 0 : messageViewModel.Messages.Length;
            }
        }
	}
}
