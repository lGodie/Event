namespace Event.UIForms.ViewModels
{
    using Event.Common.Models;
    using Event.UIForms.Helpers;
    using Event.UIForms.Views;
    using GalaSoft.MvvmLight.Command;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class VotingItemViewModel : Voting
    {

        public ICommand SelectVoteCommand => new RelayCommand(this.SelectVote);
        private async void SelectVote()
        {
            if (DateTime.Now >= this.DateTimeStart && DateTime.Now <= this.DateTimeEnd)
            {
                MainViewModel.GetInstance().Candidates = new CandidateViewModel(this);
                await App.Navigator.PushAsync(new CandidatePage());
        }
            else
            {
                
                await Application.Current.MainPage.DisplayAlert(
                Languages.Error,
               Languages.VoteClosed,
                Languages.Accept);
                
            }

}

    }
}

//private async void GotoCandidate()
//{

//    MainViewModel.GetInstance().Candidates = new CandidateViewModel(this);
//    await App.Navigator.PushAsync(new CandidatePage());

//DateTime now = DateTime.Now;
//if (now >= this.DateTimeStart && now <= this.DateTimeEnd)
//{
//    MainViewModel.GetInstance().SelectedVoting = new CandidateViewModel();
//    await App.Navigator.PushAsync(new CandidatePage());
//}
//else
//{
//    await Application.Current.MainPage.DisplayAlert(
//    "Error",
//   "Ud le pone el error listo?",
//    "Acecept");
//}

//        }

//    }
//}
