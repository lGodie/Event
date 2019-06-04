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

    public class RecoverPasswordViewModel : MvxViewModel
    {

        private readonly IApiService apiService;
        private readonly IMvxNavigationService navigationService;
        private readonly IDialogService dialogService;
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
        }

        public bool IsLoading
        {
            get => this.isLoading;
            set => this.SetProperty(ref this.isLoading, value);
        }

        public string Email { get; set; }

        public ICommand ChangePasswordCommand
        {
            get
            {
                this.ChangePasswordCommand = this.ChangePasswordCommand ?? new MvxCommand(this.RegisterUser);
                return this.ChangePasswordCommand;
            }
        }


    }
}