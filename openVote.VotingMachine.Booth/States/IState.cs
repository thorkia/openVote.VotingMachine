using System;

namespace openVote.VotingMachine.Booth.States
{
	public interface IState
	{
		string PageName { get; }

		string GetLogString();
	}
}