using Android.App;

[assembly: Permission(Name = XamChat.Droid.GCM.PushConstants.BundleId + ".permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = XamChat.Droid.GCM.PushConstants.BundleId + ".permission.C2D_MESSAGE")]
[assembly: UsesPermission(Name = "com.google.android.c2dm.permission.RECEIVE")]
[assembly: UsesPermission(Name = "android.permission.GET_ACCOUNTS")]
[assembly: UsesPermission(Name = "android.permission.INTERNET")]
[assembly: UsesPermission(Name = "android.permission.WAKE_LOCK")]


namespace XamChat.Droid.GCM
{
    public static class PushConstants
    {
        public const string BundleId = "com.devsolo.xamchatdroid";
        public const string ProjectNumber = "172080753052";
    }
}