using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using openVote.VotingMachine.Booth.States;

namespace openVote.VotingMachine.Booth.PageViewModels
{
	public class NavigableViewModel<T> : ViewModelBase where T : IState
	{
		protected T _state;

		public virtual void SetState(T state)
		{
			_state = state;
		}
	}
}
