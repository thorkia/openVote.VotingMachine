using MetroLog;
using openVote.VotingMachine.Core.Api;
using openVote.VotingMachine.Server.DataAccess.Models;
using SQLite.Net;

namespace openVote.VotingMachine.Server.DataAccess
{
	public class RegisteredMachineRepository : BaseSqlRepository<RegisteredMachine>
	{
		private readonly SQLiteConnection _connection;

		public RegisteredMachineRepository(SQLiteConnection connection) : base(connection)
		{
		}
	}
}
