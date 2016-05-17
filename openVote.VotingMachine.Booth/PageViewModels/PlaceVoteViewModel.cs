using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using openVote.VotingMachine.Booth.Events;
using openVote.VotingMachine.Booth.States;
using openVote.VotingMachine.DataAccess;
using openVote.VotingMachine.DataAccess.Api;
using openVote.VotingMachine.DataAccess.Models;

namespace openVote.VotingMachine.Booth.PageViewModels
{
	public class PlaceVoteViewModel : ViewModelBase
	{
		private VoteState _voteState;

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
										 _voteState.Choice = SelectedChoice;
										 Messenger.Default.Send<NextEvent>( new NextEvent());										 
									 }));
			}
		}


		public void SetCurrentBallot(VoteState voteState)
		{
			_voteState = voteState;
			_currentBallot = voteState.Ballot;		
			
			RaisePropertyChanged(() => Title);
			RaisePropertyChanged(() => Description);
			SelectedChoice = null;

			Choices.Clear();
			_currentBallot.Choices.ForEach( c => Choices.Add(c));

		}
	}
}
