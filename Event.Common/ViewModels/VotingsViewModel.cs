namespace Event.Common.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using Services;

    public class VotingsViewModel : MvxViewModel
    {
        private List<Voting> votings;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;

        public List<Voting> Votings
        {
            get => this.votings;
            set => this.SetProperty(ref this.votings, value);
        }

        public VotingsViewModel(
            IApiService apiService,
            IDialogService dialogService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.LoadVotings();
        }

        private async void LoadVotings()
        {
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var response = await this.apiService.GetListAsync<Voting>(
                "https://diegozapataeventweb.azurewebsites.net",
                "/api",
                "/Votings",
                "bearer",
                token.Token);

            if (!response.IsSuccess)
            {
                this.dialogService.Alert("Error", response.Message, "Accept");
                return;
            }

            this.Votings = (List<Voting>)response.Result;
            this.Votings = this.Votings.OrderBy(p=>p.DateTimeStart).ToList();
        }
    }


}
