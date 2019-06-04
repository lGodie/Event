
using Android.App;
using Android.OS;
using Android.Views;
using MvvmCross.Droid.Support.V7.AppCompat;
using Event.Common.ViewModels;
using Toolbar = Android.Support.V7.Widget.Toolbar;
namespace Event.UICross.Android.Views
{

    [Activity(Label = "@string/app_name")]
    public class CandidateView : MvxAppCompatActivity<CandidatesViewModel>
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.SetContentView(Resource.Layout.CandidatesPage);
            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var actionBar = SupportActionBar;
            if (actionBar != null)
            {
                actionBar.SetDisplayHomeAsUpEnabled(true);
            }
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == global::Android.Resource.Id.Home) { OnBackPressed(); }
            return base.OnOptionsItemSelected(item);
        }
    }



}