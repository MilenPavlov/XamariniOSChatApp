using System;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;
using XamChat.Core;
using XamChat.Core.ViewModels;

namespace XamChat.iOS
{
	public partial class FriendsController : UITableViewController
	{
	    private readonly FriendViewModel friendViewModel = ServiceContainer.Resolve<FriendViewModel>();
		public FriendsController (IntPtr handle) : base (handle)
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

	        try
	        {
	            await friendViewModel.GetFriends();

                TableView.ReloadData();
	        }
	        catch (Exception ex)
	        {
	            
	            new UIAlertView("Oops!", ex.Message, null, "Ok").Show();
	        }
	    }

	    class TableSource : UITableViewSource
        {
            private const string cellName = "FriendCell";
            private readonly FriendViewModel friendViewModel = ServiceContainer.Resolve<FriendViewModel>();
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var friend = friendViewModel.Friends[indexPath.Row];
                var cell = tableView.DequeueReusableCell(cellName);
                if (cell == null)
                {
                    cell = new UITableViewCell(UITableViewCellStyle.Default, cellName);
                    cell.AccessoryView = UIButton.FromType(UIButtonType.ContactAdd);
                    cell.AccessoryView.UserInteractionEnabled = false;
                }

                cell.TextLabel.Text = friend.Username;

                return cell;
            }

            public override nint RowsInSection(UITableView tableview, nint section)
            {
                return friendViewModel.Friends == null ? 0 : friendViewModel.Friends.Length;
            }
        }
	}

   
}
