namespace Event.Common.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using Helpers;
    using Interfaces;
    using Models;
    using MvvmCross.Commands;
    using MvvmCross.Navigation;
    using MvvmCross.ViewModels;
    using Newtonsoft.Json;
    using Services;
    public class CandidatesViewModel : MvxViewModel<NavigationArgs>
    {
        private List<Candidate> candidates; 
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;
        private readonly IMvxNavigationService navigationService;
        private Candidate candidate;
        private MvxCommand voteCommand;
        private Voting Voting;
        private bool isLoading;
        //private MvxCommand voteCommand;

        public CandidatesViewModel(
            IApiService apiService,
            IDialogService dialogService,
            IMvxNavigationService navigationService)
        {
            this.apiService = apiService;
            this.dialogService = dialogService;
            this.navigationService = navigationService;
            this.IsLoading = false;
        }

        public bool IsLoading
        {
            get => this.isLoading;
            set => this.SetProperty(ref this.isLoading, value);
        }

        public ICommand VoteCommand
        {
            get
            {
                this.voteCommand = this.voteCommand ?? new MvxCommand(this.Vote);
                return this.voteCommand;
            }
        }

        private void Vote()
        {

            this.dialogService.Alert("OK", "Do you want vote for  this candidate", "Accept");
            
                
        }

        public List<Candidate> Candidates 
        {
            get => this.Voting.Candidates;
            set => this.SetProperty(ref this.candidates, value);
        }

        public Voting voting
        {
            get => this.voting;
            set => this.SetProperty(ref this.Voting, value);
        }

        public override void Prepare(NavigationArgs parameter)
        {
            this.Voting = parameter.Voting;
            this.candidate = parameter.Candidate; 
        }
    }
}
