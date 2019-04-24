namespace Event.UIForms.ViewModels
{
    using Event.Common.Models;
    using Event.Common.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;
    public class CandidateViewModel : BaseViewModel
    {

        private readonly ApiService apiService;
        
        public Voting Voting { get; set; }

        private ObservableCollection<Candidate> candidate;

        public ObservableCollection<Candidate> Candidates
        {
            get => this.candidate;
            set => this.SetValue(ref this.candidate, value);

        }
        public CandidateViewModel()
        {

        }

        public CandidateViewModel(Voting votingItem)
        {
            this.Voting = votingItem;
            this.Candidates = ToCandidates(votingItem.Candidates);
            this.apiService = new ApiService();
        }

        private ObservableCollection<Candidate> ToCandidates(List<Candidate> candidate)
        {
            var mycandidates = new ObservableCollection<Candidate>();
            candidate.ForEach(c => mycandidates.Add(c));
            return mycandidates;
        }

    }
}
