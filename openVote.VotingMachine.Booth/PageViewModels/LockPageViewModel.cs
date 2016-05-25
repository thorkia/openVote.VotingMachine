using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using MetroLog;
using openVote.VotingMachine.Booth.Events;
using openVote.VotingMachine.Booth.Settings;
using openVote.VotingMachine.Booth.States;

namespace openVote.VotingMachine.Booth.PageViewModels
{
	public class LockPageViewModel : NavigableViewModel<LockState>
	{
		public LockPageViewModel() : base(LogManagerFactory.DefaultLogManager.GetLogger<LockPageViewModel>())
		{
			Messenger.Default.Register<UnlockEvent>(this, UnlockMessageReceived);
		}

		private void UnlockMessageReceived(UnlockEvent eventargs)
		{
			_logger.Trace("Received Unlock Message");
			Messenger.Default.Send<NextStateEvent>(new NextStateEvent());
		}
	}
}
