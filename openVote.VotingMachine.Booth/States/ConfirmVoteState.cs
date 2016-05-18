using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using openVote.VotingMachine.Booth.States;
using openVote.VotingMachine.DataAccess.Models;

namespace openVote.VotingMachine.Booth.States
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
	}
}
