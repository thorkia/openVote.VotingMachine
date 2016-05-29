using openVote.VotingMachine.Core.Models;

namespace openVote.VotingMachine.Core.States
{
	public class VoteState : IState
	{
		public string PageName => "PlaceVote";

		public Ballot Ballot { get; }

		public string Choice { get; set; }

		public VoteState(Ballot ballot)
		{
			Ballot = ballot;
		}

		public string GetLogString()
		{
			string ballotString = Ballot != null ? Ballot.ToString() : string.Empty;
			return $"Ballot={ballotString} | Choice={Choice ?? string.Empty}";
		}
	}
}
