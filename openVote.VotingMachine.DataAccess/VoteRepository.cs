using openVote.VotingMachine.Core.Api;
using openVote.VotingMachine.Core.Models;
using SQLite.Net;

namespace openVote.VotingMachine.DataAccess
{
	public class VoteRepository : IVoteRepository
	{
		private readonly SQLiteConnection _connection;

		public VoteRepository(SQLiteConnection connection)
		{
			_connection = connection;

			_connection.CreateTable<Vote>();
		}

		public bool Save(Vote vote)
		{
			var i = _connection.Insert(vote);
			return i > 0;
			//return _connection.InsertOrReplace(vote) > 0;			
		}
	}
}
