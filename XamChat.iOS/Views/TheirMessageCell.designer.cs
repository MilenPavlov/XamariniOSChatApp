// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using Foundation;
using UIKit;
using System.CodeDom.Compiler;

namespace XamChat.iOS.Views
{
	[Register ("TheirMessageCell")]
	partial class TheirMessageCell
	{
		[Outlet]
		UILabel message { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UILabel date { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (date != null) {
				date.Dispose ();
				date = null;
			}
		}
	}
}
