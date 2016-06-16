using openVote.VotingMachine.Core.Api;
using openVote.VotingMachine.Core.Models;
using SQLite.Net;

namespace openVote.VotingMachine.Booth.DataAccess
{
	public class VoteRepository : BaseSqlRepository<Vote>
	{
		private readonly SQLiteConnection _connection;

		public VoteRepository(SQLiteConnection connection) : base (connection)
		{
		}
	}
}
