namespace openVote.VotingMachine.Core.States
{
	public class TitleState : IState
	{
		public string PageName => "Title";

		public string GetLogString()
		{
			return "TitleState";
		}
	}
}
