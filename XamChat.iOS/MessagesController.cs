using System;
using System.Drawing;
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
	    private UIToolbar toolbar;
	    private UITextField message;
	    private UIBarButtonItem send;
        private NSObject willShowObserver, willHideObserver;


		public MessagesController (IntPtr handle) : base (handle)
		{           
		}

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //text field
            message = new UITextField(new RectangleF(0,0,240,32))
            {
                BorderStyle = UITextBorderStyle.RoundedRect,
                ReturnKeyType = UIReturnKeyType.Send,
                ShouldReturn = _ =>
                {
                    Send();
                    return false;
                }
            };

            //bar button item 
            send = new UIBarButtonItem("Send", UIBarButtonItemStyle.Plain, (sender,e) => Send());

            //toolbar
            toolbar = new UIToolbar(new RectangleF(0, (float)(TableView.Frame.Height - 44), (float)TableView.Frame.Width, 44));


            toolbar.Items = new UIBarButtonItem[]
            {
                new UIBarButtonItem(message),
                send
            };

            NavigationController.View.AddSubview(toolbar);
            TableView.Source = new TableSource();
            TableView.TableFooterView = new UIView(new RectangleF(0, 0, (float)TableView.Frame.Width, 44))
            {
                BackgroundColor = UIColor.Clear
            };
        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);

            //Unsubcribe notifications
            if (willShowObserver != null)
            {
                willShowObserver.Dispose();
                willShowObserver = null;
            }
            if (willHideObserver != null)
            {
                willHideObserver.Dispose();
                willHideObserver = null;
            }

            //IsBusy
            messageViewModel.IsBusyChanged -= OnIsBusyChanged;
        }

        void OnIsBusyChanged(object sender, EventArgs e)
        {
            message.Enabled =
                send.Enabled = !messageViewModel.IsBusy;
        }

        async void Send()
        {
            //Just hide the keyboard if they didn’t type anything
            if (string.IsNullOrEmpty(message.Text))
            {
                message.ResignFirstResponder();
                return;
            }

            //Set the text, send the message
            messageViewModel.Text = message.Text;
            await messageViewModel.SendMessage();

            //Clear the text field & view model
            message.Text =
                messageViewModel.Text = string.Empty;
            //Reload the table
            TableView.ReloadData();
            //Hide the keyboard
            message.ResignFirstResponder();
            //Scroll to end, to see the new message
            ScrollToEnd();
        }

        void ScrollToEnd()
        {
            TableView.ContentOffset = new PointF(0, (float)(TableView.ContentSize.Height - TableView.Frame.Height));
        }

        void OnKeyboardNotification(UIKeyboardEventArgs e)
        {
            //Check if the keyboard is becoming visible
            bool willShow = e.Notification.Name == UIKeyboard.WillShowNotification;

            //Start an animation, using values from the keyboard
            UIView.BeginAnimations("AnimateForKeyboard");
            UIView.SetAnimationDuration(e.AnimationDuration);
            UIView.SetAnimationCurve(e.AnimationCurve);

            //Calculate keyboard height, etc.
            if (willShow)
            {
                var keyboardFrame = e.FrameEnd;

                var frame = TableView.Frame;
                frame.Height -= keyboardFrame.Height;
                TableView.Frame = frame;

                frame = toolbar.Frame;
                frame.Y -= keyboardFrame.Height;
                toolbar.Frame = frame;
            }
            else
            {
                var keyboardFrame = e.FrameBegin;

                var frame = TableView.Frame;
                frame.Height += keyboardFrame.Height;
                TableView.Frame = frame;

                frame = toolbar.Frame;
                frame.Y += keyboardFrame.Height;
                toolbar.Frame = frame;
            }

            //Commit the animation
            UIView.CommitAnimations();
            ScrollToEnd();
        }

        public async override void ViewWillAppear(bool animated)
        {

            Title = messageViewModel.Conversation.Username;

            //Keyboard notifications
            willShowObserver = UIKeyboard.Notifications.ObserveWillShow((sender, e) => OnKeyboardNotification(e));
            willHideObserver = UIKeyboard.Notifications.ObserveWillHide((sender, e) => OnKeyboardNotification(e));

            //IsBusy
            messageViewModel.IsBusyChanged += OnIsBusyChanged;

            try
            {
                await messageViewModel.GetMessages();

                TableView.ReloadData();
                message.BecomeFirstResponder();
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
