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

namespace XamChat.iOS
{
	[Register ("LoginController")]
	partial class LoginController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIButton button_Login { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UIActivityIndicatorView indicator { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField textField_Password { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		UITextField textField_Username { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (button_Login != null) {
				button_Login.Dispose ();
				button_Login = null;
			}
			if (indicator != null) {
				indicator.Dispose ();
				indicator = null;
			}
			if (textField_Password != null) {
				textField_Password.Dispose ();
				textField_Password = null;
			}
			if (textField_Username != null) {
				textField_Username.Dispose ();
				textField_Username = null;
			}
		}
	}
}
