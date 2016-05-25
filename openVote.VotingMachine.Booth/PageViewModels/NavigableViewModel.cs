using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using MetroLog;
using openVote.VotingMachine.Booth.Settings;
using openVote.VotingMachine.Booth.States;

namespace openVote.VotingMachine.Booth.PageViewModels
{
	public class NavigableViewModel<T> : ViewModelBase where T : IState
	{
		protected readonly ILogger _logger;

		protected T _state;

		protected NavigableViewModel(ILogger logger)
		{
			_logger = logger;
		} 

		public virtual void SetState(T state)
		{
			_logger?.Trace(LogStatements.ViewModelReceivedStateLog(state.GetLogString()));

			_state = state;
		}
	}
}
