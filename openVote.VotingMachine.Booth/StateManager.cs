using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Connectivity;
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
		private readonly INavigationService _navigationService;
		private readonly VoteRepository _voteRepository;

		private readonly IState _initialState;
		private IState _currentState;

		private readonly IList<Ballot> _ballots;
		private int _ballotLocation = 0;

		private readonly List<Vote> _votes;

		public StateManager(INavigationService navigationService, BallotRepository ballotRepository, VoteRepository voteRepository)
		{
			_voteRepository = voteRepository;
			_ballots = ballotRepository.Ballots.ToList();

			_initialState = new TitleState();
			_currentState = _initialState;
			_navigationService = navigationService;

			_votes = new List<Vote>();

			//Register Events
			Messenger.Default.Register<NextStateEvent>(this, Next);			
		}


		private void Next(NextStateEvent param)
		{
			var newState = GetNextState() ?? _initialState;
			//Move state to the next item

			_currentState = newState;

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
				var voteState = (VoteState)_currentState as VoteState;

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
				//TODO: Have this move to a lock page that won't move until an unlock command is recieved
				var saved = _votes.TrueForAll(v => _voteRepository.Save(v));
				if (!saved)
				{
					//TODO: Exception handling here
				}

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
			_navigationService.NavigateTo(_currentState.PageName, _currentState);
		}

		private Vote CreateVoteFromConfirmation(ConfirmVoteState state)
		{
			var vote = new Vote();
			vote.BallotId = state.Ballot.Id;
			vote.RecordedTime = DateTime.Now;
			vote.VoteOption = state.Choice;

			var hostName = NetworkInformation.GetHostNames().FirstOrDefault(h => h.Type == HostNameType.DomainName);
			var ipConnection = NetworkInformation.GetHostNames().FirstOrDefault(h => h.Type == HostNameType.Ipv4);

			vote.MachineName = hostName.RawName;
			vote.MachineIPAddress = ipConnection.RawName;
			vote.MachineMACAddress = ipConnection.IPInformation.NetworkAdapter.NetworkAdapterId.ToString();

			return vote;
		}
	}
}
