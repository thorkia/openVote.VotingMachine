namespace openVote.VotingMachine.Core.States
{
	public class LockState : IState
	{
		public string PageName => "LockScreen";

		public string GetLogString()
		{
			return "LockState";
		}
	}
}
