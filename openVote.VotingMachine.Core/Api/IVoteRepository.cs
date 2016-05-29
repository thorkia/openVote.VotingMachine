using openVote.VotingMachine.Core.Models;

namespace openVote.VotingMachine.Core.Api
{
	public interface IVoteRepository
	{
		bool Save(Vote vote);
	}
}