namespace Event.Common.ViewModels
{
    using System.Windows.Input;
    using Interfaces;
    using Models;
    using MvvmCross.Navigation;
    using MvvmCross.Commands;
    using MvvmCross.ViewModels;
    using Services;
    using Event.Common.Helpers;
    using Newtonsoft.Json;

    public class LoginViewModel : MvxViewModel
    {
        private string email;
        private string password;
        private MvxCommand loginCommand;
        private MvxCommand registerCommand;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private readonly IMvxNavigationService navigationService;
        private bool isLoading;

        public bool IsLoading
        {
            get => this.isLoading;
            set => this.SetProperty(ref this.isLoading, value);
        }

        public string Email
        {
            get => this.email;
            set => this.SetProperty(ref this.email, value);
        }

        public string Password
        {
            get => this.password;
            set => this.SetProperty(ref this.password, value);
        }

        public ICommand LoginCommand
        {
            get
            {
                this.loginCommand = this.loginCommand ?? new MvxCommand(this.DoLoginCommand);
                return this.loginCommand; 
            }
        }

        public ICommand RegisterCommand
        {
            get
            {
                this.registerCommand = this.registerCommand ?? new MvxCommand(this.DoRegisterCommand);
                return this.registerCommand;
            }
        }


        public LoginViewModel(
            IApiService apiService,
            IDialogService dialogService,
            IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;

            this.Email = "diegozapata1345@gmail.com";
            this.Password = "123456";
            this.IsLoading = false;
        }

        private async void DoRegisterCommand()
        {
            await this.navigationService.Navigate<RegisterViewModel>();
        }

        private async void DoLoginCommand()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                this.dialogService.Alert("Error", "You must enter an email.", "Accept");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                this.dialogService.Alert("Error", "You must enter a password.", "Accept");
                return;
            }

            this.IsLoading = true;

            var request = new TokenRequest
            {
                Password = this.Password,
                Username = this.Email
            };

            var response = await this.apiService.GetTokenAsync(
                "https://diegozapataeventweb.azurewebsites.net",
                "/Account",
                "/CreateToken",
                request);

            if (!response.IsSuccess)
            {
                this.IsLoading = false;
                this.dialogService.Alert("Error", "User or password incorrect.", "Accept");
                return;
            }

            //this.IsLoading = false;
            //this.dialogService.Alert("Ok", "Fuck yeah!", "Accept");

            var token = (TokenResponse)response.Result;
            Settings.UserEmail = this.Email;
            Settings.Token = JsonConvert.SerializeObject(token);
            this.IsLoading = false;
            await this.navigationService.Navigate<VotingsViewModel>();
        }
      

    }

}
