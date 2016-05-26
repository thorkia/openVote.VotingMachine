using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.Security.ExchangeActiveSyncProvisioning;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using MetroLog;
using openVote.VotingMachine.Booth.Events;
using openVote.VotingMachine.Booth.States;
using openVote.VotingMachine.DataAccess;
using openVote.VotingMachine.DataAccess.Models;

namespace openVote.VotingMachine.Booth
{
	public class StateManager
	{
		private readonly ILogger _logger = LogManagerFactory.DefaultLogManager.GetLogger<StateManager>();
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

			var eas = new EasClientDeviceInformation();
			vote.MachineName = eas.FriendlyName;

			//The machine may not have an active IP or Network Information.
			try
			{
				var ipConnection = NetworkInformation.GetHostNames().FirstOrDefault(h => h.Type == HostNameType.Ipv4);

				vote.MachineIPAddress = ipConnection.RawName;
				vote.MachineMACAddress = ipConnection.IPInformation.NetworkAdapter.NetworkAdapterId.ToString();
			}
			catch (Exception ex)
			{
				string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "errors.log");
				using (var appender = File.AppendText(path))
				{
					appender.WriteLine($"{DateTime.Now}||{ex}");
				}

				//If no IP or network is active, use the local device ID as the MAC Address
				vote.MachineMACAddress = eas.Id.ToString();
			}
			
			return vote;
		}
	}
}
