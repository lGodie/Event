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
    class CandidatesViewModel : MvxViewModel
    {
        private List<Candidate> candidates;
        private readonly IApiService apiService;
        private readonly IDialogService dialogService;





    }
}
