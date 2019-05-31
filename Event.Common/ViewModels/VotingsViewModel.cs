﻿namespace Event.Common.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Input;
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using Services;

    public class VotingsViewModel : MvxViewModel
    {
        private List<Voting> votings;
        private MvxCommand<Voting> candidateCommand;
        private readonly IMvxNavigationService navigationService;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;

        public List<Voting> Votings
        {
            get => this.votings;
            set => this.SetProperty(ref this.votings, value);
        }

        public VotingsViewModel(
            IMvxNavigationService navigationService,
            IApiService apiService,
            IDialogService dialogService)
        {
            this.apiService = apiService;
            this.navigationService = navigationService;
            this.dialogService = dialogService;
            this.LoadVotings();
        }

        public ICommand CandidateCommand
        {
            get
            {
                this.candidateCommand = this.candidateCommand ?? new MvxCommand<Voting>(this.DoCandidateCommand);
                return this.candidateCommand;
            }
        }

        private async void DoCandidateCommand(Voting voting)
        {
            if (DateTime.Now >= voting.DateTimeStart && DateTime.Now <= voting.DateTimeEnd)
            {
               
                await this.navigationService.Navigate<RegisterViewModel>();
            }
            else
            {

                this.dialogService.Alert("Error", "The vote is closed", "Accept");

            }
        }
        private async void LoadVotings()
        {
            var token = JsonConvert.DeserializeObject<TokenResponse>(Settings.Token);
            var response = await this.apiService.GetListAsync<Voting>(
                "https://diegozapataeventweb.azurewebsites.net",
                "/api",
                "/Candidate",
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
