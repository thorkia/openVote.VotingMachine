using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MetroLog;
using openVote.VotingMachine.Booth.Events;
using openVote.VotingMachine.Booth.Settings;
using openVote.VotingMachine.Booth.States;

namespace openVote.VotingMachine.Booth.PageViewModels
{
	public class SummaryPageViewModel : NavigableViewModel<SummaryState>
	{
		private RelayCommand _resetChoicesCommand;
		private RelayCommand _confirmChoicesCommand;

		private ObservableCollection<VoteSummary> _summaries = new ObservableCollection<VoteSummary>();
		
		public RelayCommand ResetChoicesCommand
		{
			get
			{
				return _resetChoicesCommand
							 ?? (_resetChoicesCommand = new RelayCommand(
									 () =>
									 {
										 _logger.Info(LogStatements.UserClickedButtonLog("Cancel and Restart"));
										 _state.SummaryAction = SummaryAction.Reset;
										 Messenger.Default.Send<NextStateEvent>(new NextStateEvent());
									 }));
			}
		}

		public RelayCommand ConfirmChoicesCommand
		{
			get
			{
				return _confirmChoicesCommand
							 ?? (_confirmChoicesCommand = new RelayCommand(
									 () =>
									 {
										 _logger.Info(LogStatements.UserClickedButtonLog("Place Votes"));
										 _state.SummaryAction = SummaryAction.Confirm;
										 Messenger.Default.Send<NextStateEvent>(new NextStateEvent());
									 }));
			}
		}

		public ObservableCollection<VoteSummary> Choices => _summaries;

		public SummaryPageViewModel() : base(LogManagerFactory.DefaultLogManager.GetLogger<SummaryPageViewModel>())
		{

		}

		public override void SetState(SummaryState state)
		{
			base.SetState(state);
			Choices.Clear();

			state.VoteSummaries.ForEach( vs => Choices.Add(vs));			
		}
	}
}
