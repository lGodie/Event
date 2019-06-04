﻿using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Support.V7.AppCompat;
using Event.Common.ViewModels;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace Event.UICross.Android.Views
{
    [Activity(
        Label = "@string/votings",
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class VotingsView : MvxAppCompatActivity<VotingsViewModel>
    {
        private readonly string[] menuOptions = { "Edit User", "Change Password", "Close Session" };
        private ListView drawerListView;
        private DrawerLayout drawer;
        private ActionBarDrawerToggle toggle;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.VotingsPage);

            if (global::Android.Support.V4.App.ActivityCompat.CheckSelfPermission(this, global::Android.Manifest.Permission.Camera) != global::Android.Content.PM.Permission.Granted ||
                global::Android.Support.V4.App.ActivityCompat.CheckSelfPermission(this, global::Android.Manifest.Permission.WriteExternalStorage) != global::Android.Content.PM.Permission.Granted ||
                global::Android.Support.V4.App.ActivityCompat.CheckSelfPermission(this, global::Android.Manifest.Permission.ReadExternalStorage) != global::Android.Content.PM.Permission.Granted)
            {
                global::Android.Support.V4.App.ActivityCompat.RequestPermissions(this, new string[] { global::Android.Manifest.Permission.Camera, global::Android.Manifest.Permission.WriteExternalStorage, global::Android.Manifest.Permission.ReadExternalStorage }, 1);
            }

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetHomeAsUpIndicator(Resource.Drawable.menu_icon);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SupportActionBar.SetDisplayShowHomeEnabled(true);
            drawerListView = FindViewById<ListView>(Resource.Id.drawerListView);
            drawerListView.Adapter = new ArrayAdapter<string>(this, global::Android.Resource.Layout.SimpleListItem1, menuOptions);
            drawerListView.ItemClick += listView_ItemClick;
            drawer = FindViewById<DrawerLayout>(Resource.Id.drawerLayout);
            toggle = new ActionBarDrawerToggle(
                this,
                drawer,
                toolbar,
                Resource.String.navigation_drawer_open,
                Resource.String.navigation_drawer_closed);
            drawer.AddDrawerListener(toggle);
            toggle.SyncState();
        }

        private void listView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            switch (e.Position)
            {
                case 0:
                    StartActivity(typeof(EditUserView));
                    break;
                case 1:
                    StartActivity(typeof(ChangePasswordView));
                    break;
                case 2:
                    OnBackPressed();
                    break;
            }

            drawer.CloseDrawer(drawerListView);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (toggle.OnOptionsItemSelected(item))
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
    }
}