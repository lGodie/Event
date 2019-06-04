namespace Event.Common.ViewModels
{

    using System.Windows.Input;
    using Common.Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using Services;
    using Event.Common.Interfaces;
    using System;
    using Event.Common.Helpers;

    public class RecoverPasswordViewModel : MvxViewModel
    {

        private readonly IApiService apiService;
        private readonly IMvxNavigationService navigationService;
        private readonly IDialogService dialogService;
        private MvxCommand changePasswordCommand;
        private bool isEnabled;
        private bool isLoading;
        

        public RecoverPasswordViewModel(
           IMvxNavigationService navigationService,
           IApiService apiService,
           IDialogService dialogService)
        {
            this.apiService = apiService;
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.IsLoading = false;
            this.IsEnabled = true;
        }

        public bool IsEnabled
        {
            get => this.isEnabled;
            set => this.SetProperty(ref this.isEnabled, value);
        }

        public bool IsLoading
        {
            get => this.isLoading;
            set => this.SetProperty(ref this.isLoading, value);
        }

        public string Email { get; set; }

        public ICommand RecoverpasswordCommand
        {
            get
            {
                this.changePasswordCommand = this.changePasswordCommand ?? new MvxCommand(this.RecoverPassword);
                return this.changePasswordCommand;
            }
        }

        private async void RecoverPassword()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                this.dialogService.Alert("Error", "You must enter an email.", "Accept");
                return;
            }

            if (!RegexHelper.IsValidEmail(this.Email))
            {
                this.dialogService.Alert("Error", "You must enter a valid email.", "Accept");
                return;
            }

            this.IsLoading = true;
            this.IsEnabled = false;

            var request = new RecoverPasswordRequest
            {
                Email = this.Email
            };

            var response = await this.apiService.RecoverPasswordAsync(
                "https://diegozapataeventweb.azurewebsites.net",
                "/api",
                "/Account/RecoverPassword",
                request);

            this.IsLoading = false;
            this.IsEnabled = true;

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            this.dialogService.Alert(
                "Ok",
                "An email with instructions to change " +
                "to change the password was sent",
                "Accept",
                () => { this.navigationService.Close(this); }
            );
        }
    }
}