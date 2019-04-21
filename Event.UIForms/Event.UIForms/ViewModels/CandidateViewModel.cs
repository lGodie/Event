namespace Event.UIForms.ViewModels
{
    using Event.Common.Models;
    using Event.Common.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Xamarin.Forms;
    public class CandidateViewModel : BaseViewModel
    {
        private readonly ApiService apiService;

        public Voting Voting { get; set; }
        public CandidateViewModel(Voting votingItem)
        {
            this.Voting = votingItem;
            this.apiService = new ApiService();
        }

    }
}
