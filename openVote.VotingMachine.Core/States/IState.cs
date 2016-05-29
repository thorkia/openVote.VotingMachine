namespace openVote.VotingMachine.Core.States
{
	public interface IState
	{
		string PageName { get; }

		string GetLogString();
	}
}