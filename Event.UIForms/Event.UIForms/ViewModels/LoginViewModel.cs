namespace Event.UIForms.ViewModels
{
    using Event.UIForms.Views;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class LoginViewModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand LoginCommand => new RelayCommand(Login);

        public LoginViewModel()
        {
            this.Email = "diegozapata1345z@gmail.com";
            this.Password = "123456";
        }
        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter an email.",
                    "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    "You must enter a password.",
                    "Accept");
                return;
            }

            if (!this.Email.Equals("diegozapata1345z@gmail.com") || !this.Password.Equals("123456"))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", 
                    "Incorrect user or password", 
                    "Accept");
                return;
            }

            //await Application.Current.MainPage.DisplayAlert(
            //    "Ok",
            //    "Funciono ",
            //    "Accept");

            MainViewModel.GetInstance().Votings = new VotingsViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new VotingsPage());


        }
    }
}
