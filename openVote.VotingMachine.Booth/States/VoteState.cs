using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using openVote.VotingMachine.DataAccess.Models;

namespace openVote.VotingMachine.Booth.States
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
