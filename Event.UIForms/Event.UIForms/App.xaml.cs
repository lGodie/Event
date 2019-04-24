using Event.Common.Helpers;
using Event.Common.Models;
using Event.UIForms.ViewModels;
using Event.UIForms.Views;
using Newtonsoft.Json;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Event.UIForms
{
    public partial class App : Application
    {
        public static NavigationPage Navigator { get; internal set; }
        public static MasterPage Master { get; internal set; }

        

        public App()
        {
            InitializeComponent();

            if (Settings.IsRemember)
            {
                var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
                var user = JsonConvert.DeserializeObject<User>(Settings.User);
                

                if (token.Expiration > DateTime.Now)
                {
                    var mainViewModel = MainViewModel.GetInstance();
                    mainViewModel.User = user;
                    mainViewModel.Token = token;
                    mainViewModel.UserEmail = Settings.UserEmail;
                    mainViewModel.UserPassword = Settings.UserPassword;
                    mainViewModel.Votings = new VotingsViewModel();
                    mainViewModel.Candidates = new CandidateViewModel();
                    this.MainPage = new MasterPage();
                    return;
                }
            }

            MainViewModel.GetInstance().Login = new LoginViewModel();
            this.MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
