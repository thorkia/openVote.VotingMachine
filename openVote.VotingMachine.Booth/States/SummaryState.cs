using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using openVote.VotingMachine.DataAccess.Models;

namespace openVote.VotingMachine.Booth.States
{
	public class SummaryState : IState
	{
		public string PageName => "Summary";

		public SummaryAction SummaryAction { get; set; }
		//TODO: add a list of items here
		public List<VoteSummary> VoteSummaries { get; }

		public SummaryState(IEnumerable<Vote> votes, IEnumerable<Ballot> ballots)
		{
			//Construct a new object for the vote
			VoteSummaries = votes.Select( v => new VoteSummary { Choice =  v.VoteOption, Ballot = ballots.First(b => b.Id == v.BallotId).Title}).ToList();
		}
	}

	public class VoteSummary
	{
		public string Ballot { get; set; }
		public string Choice { get; set; }
	}

	public enum SummaryAction
	{
		Reset,
		Confirm
	}
}
