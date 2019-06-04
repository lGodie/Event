using Android.App;
using Android.OS;
using MvvmCross.Droid.Support.V7.AppCompat;
using Event.Common.ViewModels;
using Toolbar = global::Android.Support.V7.Widget.Toolbar;

namespace Event.UICross.Android.Views
{
    [Activity(Label = "@string/votings")]
    public class VotingsView : MvxAppCompatActivity<VotingsViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.VotingsPage);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
        }
    }
}