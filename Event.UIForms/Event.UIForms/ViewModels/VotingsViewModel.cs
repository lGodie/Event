namespace Event.UIForms.ViewModels
{
    using Event.Common.Models;
    using Event.Common.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class VotingsViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        private ObservableCollection<Voting> votings;
        private bool isRefreshing;
        public ObservableCollection<Voting> Votings
        {
            get => this.votings;
            set => this.SetValue(ref this.votings, value);
        }

        public bool IsRefreshing
        {
            get => this.isRefreshing;
            set => this.SetValue(ref this.isRefreshing, value);
        }
        public VotingsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadVotings();
        }

        private async void LoadVotings()
        {
            this.IsRefreshing = true;
            var response = await this.apiService.GetListAsync<Voting>(
                "https://eventwebdiegoz.azurewebsites.net",
                "/api",
                "/Votings");

            this.IsRefreshing = false;

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
                return;
            }

            var myVotings = (List<Voting>)response.Result;
            this.Votings = new ObservableCollection<Voting>(myVotings);
        }
    }
}
