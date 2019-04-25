namespace Event.UIForms.ViewModels
{

    using Event.Common.Models;
    using Event.UIForms.Views;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;
    public class CandidateItemViewModel : Candidate
    {
        public ICommand SelectCandidateCommand => new RelayCommand(this.SelectCandidate);

        private async void SelectCandidate()
        {
            MainViewModel.GetInstance().Votes = new VoteViewModel(this);
            await App.Navigator.PushAsync(new VotePage());
        }
    }
}
