using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using openVote.VotingMachine.DataAccess;
using openVote.VotingMachine.DataAccess.Api;
using openVote.VotingMachine.DataAccess.Models;

namespace openVote.VotingMachine.Booth.PageViewModels
{
	public class PlaceVoteViewModel : ViewModelBase
	{
		private static IEnumerable<Ballot> _ballots;


		private BallotRepository _ballotLoader;

		private ObservableCollection<string> _choices = new ObservableCollection<string>();
		private string _selectedChoice = null;
		private Ballot _currentBallot;

		private RelayCommand _nextCommand;

		public string Title => _currentBallot?.Title;
		public string Description => _currentBallot?.Description;

		public ObservableCollection<string> Choices => _choices;

		public string SelectedChoice
		{
			get
			{
				return _selectedChoice;
			}
			set
			{
				Set(() => SelectedChoice, ref _selectedChoice, value);
			}
		}

		public RelayCommand NextCommand
		{
			get
			{
				return _nextCommand
							 ?? (_nextCommand = new RelayCommand(
									 () =>
									 {
										 var navigationService = ServiceLocator.Current.GetInstance<INavigationService>();
										 navigationService.NavigateTo("PlaceVote", _currentBallot.Id + 1);
									 }));
			}
		}

		public PlaceVoteViewModel(BallotRepository ballotRepository)
		{
			_ballotLoader = ballotRepository;			
		}

		public void SetCurrentBallot(int ballotId)
		{			
			if (_ballots == null)
			{
				_ballots = _ballotLoader.Ballots.ToList();
			}

			//Get the first Ballot with the correct Id, or the first ballot present
			_currentBallot = _ballots.FirstOrDefault(x => x.Id == ballotId) ?? _ballots.First();

			RaisePropertyChanged(() => Title);
			RaisePropertyChanged(() => Description);
			SelectedChoice = null;

			Choices.Clear();
			_currentBallot.Choices.ForEach( c => Choices.Add(c));

		}
	}
}
