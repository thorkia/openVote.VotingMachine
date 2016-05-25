using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MetroLog;
using openVote.VotingMachine.Booth.Events;
using openVote.VotingMachine.Booth.Settings;
using openVote.VotingMachine.Booth.States;

namespace openVote.VotingMachine.Booth.PageViewModels
{
	public class ConfirmVoteViewModel : NavigableViewModel<ConfirmVoteState>
	{
		private RelayCommand _confirmVoteCommand;

		private RelayCommand _changeVoteCommand;


		public string Title => _state?.Ballot?.Title;
		public string Description => _state?.Ballot?.Description;

		public string Choice => _state?.Choice;


		public RelayCommand ConfirmVoteCommand
		{
			get
			{
				return _confirmVoteCommand
							 ?? (_confirmVoteCommand = new RelayCommand(
									 () =>
									 {
										 _logger.Info(LogStatements.UserClickedButtonLog("Confirm Vote"));
										 _state.Confirmed = true;
										 Messenger.Default.Send<NextStateEvent>(new NextStateEvent());
									 }));
			}
		}

		public RelayCommand ChangeVoteCommand
		{
			get
			{
				return _changeVoteCommand
							 ?? (_changeVoteCommand = new RelayCommand(
									 () =>
									 {
										 _logger.Info(LogStatements.UserClickedButtonLog("Change Vote"));
										 _state.Confirmed = false;
										 Messenger.Default.Send<NextStateEvent>(new NextStateEvent());
									 }));
			}
		}

		public ConfirmVoteViewModel() : base(LogManagerFactory.DefaultLogManager.GetLogger<ConfirmVoteViewModel>())
		{
			
		}

		public override void SetState(ConfirmVoteState state)
		{
			_logger.Trace(LogStatements.ViewModelReceivedStateLog(state.GetLogString()));			
			base.SetState(state);
			
			RaisePropertyChanged(() => Title);
			RaisePropertyChanged(() => Description);
			RaisePropertyChanged(() => Choice);
		}
	}
}
