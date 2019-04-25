namespace Event.UIForms.ViewModels
{
    using Event.Common.Models;
    using Event.Common.Services;
    using Event.UIForms.Helpers;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;

    public class VotingsViewModel : BaseViewModel
    {
        private readonly ApiService apiService;
        private ObservableCollection<VotingItemViewModel> votings;
        private bool isRefreshing;

        public ObservableCollection<VotingItemViewModel> Votings
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
            this.LoadsVoting();
        }

        private async void LoadsVoting()
        {
            this.IsRefreshing = true;
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var response = await this.apiService.GetListAsync<Voting>(
                url,
                "/api",
                "/Candidate",
                "bearer",
                MainViewModel.GetInstance().Token.Token);

            this.IsRefreshing = false;


            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert(
                    Languages.Error,
                    response.Message,
                    Languages.Accept);

                return;
            }
            var myVotings = (List<Voting>)response.Result;
            var myCandidates = new List<Candidate>();
            var mapsVotes = myVotings.Select(vote => new VotingItemViewModel
            {
                Id = vote.Id,
                Description = vote.Description,
                DateTimeStart = vote.DateTimeStart,
                DateTimeEnd = vote.DateTimeEnd,
                User = vote.User,
                Candidates = vote.Candidates,
            }
            );
            this.Votings = new ObservableCollection<VotingItemViewModel>(mapsVotes);

        }
    }
}
