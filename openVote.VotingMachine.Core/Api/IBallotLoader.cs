using System.Collections.Generic;
using openVote.VotingMachine.Core.Models;

namespace openVote.VotingMachine.Core.Api
{
	public interface IBallotLoader
	{
		List<Ballot> LoadBallots();
	}
}
