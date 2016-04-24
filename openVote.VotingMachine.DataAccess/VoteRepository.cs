using openVote.VotingMachine.DataAccess.Models;
using SQLite.Net;

namespace openVote.VotingMachine.DataAccess
{
	public class VoteRepository
	{
		private readonly SQLiteConnection _connection;

		public VoteRepository(SQLiteConnection connection)
		{
			_connection = connection;

			_connection.CreateTable<Vote>();
		}

		public bool Save(Vote vote)
		{
			return _connection.InsertOrReplace(vote) > 0;			
		}
	}
}
