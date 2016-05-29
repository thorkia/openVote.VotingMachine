using openVote.VotingMachine.Core.Models;

namespace openVote.VotingMachine.Core.States
{
	public class ConfirmVoteState : IState
	{
		public string PageName => "ConfirmVote";

		public Ballot Ballot { get; }

		public string Choice { get; set; }

		public bool Confirmed { get; set; }

		public ConfirmVoteState(Ballot ballot)
		{
			Ballot = ballot;
			Confirmed = false;
		}

		public string GetLogString()
		{
			return $"Ballot={Ballot} | Choice={Choice} | Confirmed={Confirmed}";
		}
	}
}
