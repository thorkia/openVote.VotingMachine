using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Security.ExchangeActiveSyncProvisioning;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MetroLog;
using openVote.VotingMachine.Core.Api;
using openVote.VotingMachine.Core.Events;
using openVote.VotingMachine.Core.Models;
using openVote.VotingMachine.Core.States;

namespace openVote.VotingMachine.Core
{
	public class StateManager
	{
		private readonly ILogger _logger = LogManagerFactory.DefaultLogManager.GetLogger<StateManager>();
		private readonly INavigationService _navigationService;
		private readonly BaseSqlRepository<Vote> _voteRepository;
		private readonly Controller _controller;

		private readonly IState _initialState;
		private IState _currentState;

		private readonly IList<Ballot> _ballots;
		private int _ballotLocation = 0;

		private readonly List<Vote> _votes;

		public StateManager(INavigationService navigationService, Controller controller, BaseSqlRepository<Vote> voteRepository)
		{
			_controller = controller;
			_voteRepository = voteRepository;
			_ballots = controller.Ballots.ToList();

			_initialState = new TitleState();
			_currentState = _initialState;
			_navigationService = navigationService;

			_votes = new List<Vote>();

			//Register Events
			Messenger.Default.Register<NextStateEvent>(this, Next);			
		}


		private void Next(NextStateEvent param)
		{
			_logger.Trace($"Navigation message received");
			_logger.Trace($"Changing state from [{_currentState.GetType().Name}]");
			_logger.Trace($"Current State [{_currentState}]");

			var newState = GetNextState() ?? _initialState;
			//Move state to the next item			
			_currentState = newState;

			_logger.Trace($"State changed to [{_currentState.GetType().Name}]");
			_logger.Trace($"New State [{_currentState}]");

			NavigateToStatePage();
		}

		//TODO: add all state changes
		private IState GetNextState()
		{
			//Changing state from the title screen
			//Only valid state is to go to the first ballot
			if (_currentState.GetType() == typeof(TitleState))
			{
				if (_ballots.Count > 0)
				{
					return new VoteState(_ballots[_ballotLocation]);
				}

				//TODO: error state.  For now, the title				
				return GetStartState();
			}

			//From a vote on a ballot, only place to go is the confirmation screen
			if (_currentState.GetType() == typeof(VoteState))
			{
				var voteState = (VoteState)_currentState;

				return new ConfirmVoteState(voteState.Ballot) { Choice = voteState.Choice };
			}


			//From the confirmation screen we can go the the next ballot is the is confirmed
			//Or back to the vote screen
			if (_currentState.GetType() == typeof(ConfirmVoteState))
			{
				var voteState = (ConfirmVoteState)_currentState;

				//If the vote has been confirmed, move to the next ballot
				if (voteState.Confirmed)
				{
					_votes.Add(CreateVoteFromConfirmation(voteState));
					_ballotLocation++;
				}

				if (_ballotLocation < _ballots.Count)
				{
					return new VoteState(_ballots[_ballotLocation]);
				}

				return new SummaryState(_votes, _ballots);
			}

			if (_currentState.GetType() == typeof(SummaryState))
			{
				var summaryState = (SummaryState)_currentState;

				if (summaryState.SummaryAction == SummaryAction.Reset)
				{
					_votes.Clear();
					_ballotLocation = 0;
					return new VoteState(_ballots[_ballotLocation]);
				}

				//This is a confirmed state
				//Save all the votes - then got the summary page
				var saved = _votes.TrueForAll(v => _voteRepository.Save(v));
				if (!saved)
				{
					//TODO: Exception handling here
				}

				return new LockState();
			}

			if (_currentState.GetType() == typeof (LockState))
			{
				_ballotLocation = 0;
				return GetStartState();
			}

			return GetStartState();
		}

		private IState GetStartState()
		{
			
			_votes.Clear();
			return _initialState;
		}
		
		private void NavigateToStatePage()
		{
			_logger.Trace($"Navigating to Page [{_currentState.PageName}]");
			_navigationService.NavigateTo(_currentState.PageName, _currentState);
		}

		private Vote CreateVoteFromConfirmation(ConfirmVoteState state)
		{
			var vote = new Vote();
			vote.BallotId = state.Ballot.Id;
			vote.RecordedTime = DateTime.Now;
			vote.VoteOption = state.Choice;


			vote.MachineId = _controller.MachineId;
			vote.MachineIPAddress = _controller.MachineIP;			
			vote.ServerRegsiteredMachinedId = _controller.MachineId;

			var eas = new EasClientDeviceInformation();
			vote.MachineName = eas.FriendlyName;
			
			return vote;
		}
	}
}
