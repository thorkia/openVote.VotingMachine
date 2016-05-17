using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using openVote.VotingMachine.Booth.Events;
using openVote.VotingMachine.Booth.States;
using openVote.VotingMachine.DataAccess;
using openVote.VotingMachine.DataAccess.Models;

namespace openVote.VotingMachine.Booth
{
	public class StateManager
	{
		private INavigationService _navigationService;
		private BallotRepository _ballotRepository;

		private IState _initialState;
		private IState _currentState;

		private IList<Ballot> _ballots;
		private int _ballotLocation = 0;

		public StateManager(INavigationService navigationService, BallotRepository ballotRepository)
		{
			_ballotRepository = ballotRepository;
			_ballots = _ballotRepository.Ballots.ToList();

			_initialState = new TitleState();
			_currentState = _initialState;
			_navigationService = navigationService;

			//Register Events
			Messenger.Default.Register<NextEvent>(this, Next);			
		}


		private void Next(NextEvent param)
		{
			var newState = GetNextState() ?? _initialState;
			//Move state to the next item

			_currentState = newState;

			NavigateToStatePage();
		}

		//TODO: add all state changes
		private IState GetNextState()
		{
			if (_currentState.GetType() == typeof (TitleState))
			{
				if (_ballots.Count > 0)
				{
					return new VoteState(_ballots[_ballotLocation]);
				}
				else
				{
					//TODO: error state.  For now, the title
					return null;
				}
				
			}

			//TODO: Rework this to go to a confirmation page before the next ballot
			if (_currentState.GetType() == typeof (VoteState))
			{
				_ballotLocation++;

				if (_ballots.Count > _ballotLocation)
				{
					return new VoteState(_ballots[_ballotLocation]);
				}
				else
				{
					//TODO: suammary state - then back to title
					return null;
				}
			}

			return null;
		}

		//TODO: Pass parameters into here - the current vote, etc
		private void NavigateToStatePage()
		{
			_navigationService.NavigateTo(_currentState.PageName, _currentState);
		}
	}
}
