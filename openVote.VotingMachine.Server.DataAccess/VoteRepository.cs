using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using openVote.VotingMachine.Core.Api;
using openVote.VotingMachine.Server.DataAccess.Models;
using SQLite.Net;

namespace openVote.VotingMachine.Server.DataAccess
{
	public class VoteRepository : BaseSqlRepository<ServerRecordedVote>
	{
		private readonly SQLiteConnection _connection;

		public VoteRepository(SQLiteConnection connection) : base(connection)
		{
		}
	}
}
