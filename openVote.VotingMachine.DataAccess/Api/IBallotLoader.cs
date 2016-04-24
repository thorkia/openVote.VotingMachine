using System.Collections.Generic;
using System.Threading.Tasks;
using openVote.VotingMachine.DataAccess.Models;

namespace openVote.VotingMachine.DataAccess.Api
{
	public interface IBallotLoader
	{
		Task<List<Ballot>> LoadBallotsAsync();
	}
}
