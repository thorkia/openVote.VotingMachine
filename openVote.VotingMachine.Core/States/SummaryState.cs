using System.Collections.Generic;
using System.Linq;
using System.Text;
using openVote.VotingMachine.Core.Models;

namespace openVote.VotingMachine.Core.States
{
	public class SummaryState : IState
	{
		public string PageName => "Summary";

		public SummaryAction SummaryAction { get; set; }
		
		public List<VoteSummary> VoteSummaries { get; }

		public SummaryState(IEnumerable<Vote> votes, IEnumerable<Ballot> ballots)
		{
			//Construct a new object for the vote
			SummaryAction = SummaryAction.None;
			VoteSummaries = votes.Select( v => new VoteSummary { Choice =  v.VoteOption, Ballot = ballots.First(b => b.Id == v.BallotId).Title}).ToList();
		}

		public string GetLogString()
		{
			StringBuilder log = new StringBuilder();

			log.Append($"SummaryAction={SummaryAction} | ");
			foreach (var voteSummary in VoteSummaries)
			{
				log.Append($"{voteSummary} | ");
			}

			return log.ToString();
		}
	}

	public class VoteSummary
	{
		public string Ballot { get; set; }
		public string Choice { get; set; }

		public override string ToString()
		{
			return $"Ballot={Ballot} Choice={Choice}";
		}
	}

	public enum SummaryAction
	{
		None,
		Reset,
		Confirm
	}
}
