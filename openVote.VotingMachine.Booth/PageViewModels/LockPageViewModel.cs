using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using openVote.VotingMachine.Booth.Events;
using openVote.VotingMachine.Booth.States;

namespace openVote.VotingMachine.Booth.PageViewModels
{
	public class LockPageViewModel : NavigableViewModel<LockState>
	{
		public LockPageViewModel()
		{
			Messenger.Default.Register<UnlockEvent>(this, UnlockMessageReceived);
		}

		private void UnlockMessageReceived(UnlockEvent eventargs)
		{
			Messenger.Default.Send<NextStateEvent>(new NextStateEvent());
		}
	}
}
