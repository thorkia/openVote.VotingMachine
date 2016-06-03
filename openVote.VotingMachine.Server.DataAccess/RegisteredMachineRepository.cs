using openVote.VotingMachine.Server.DataAccess.Models;
using SQLite.Net;

namespace openVote.VotingMachine.Server.DataAccess
{
	public class RegisteredMachineRepository
	{
		private readonly SQLiteConnection _connection;

		public RegisteredMachineRepository(SQLiteConnection connection)
		{
			_connection = connection;

			_connection.CreateTable<RegisteredMachine>();
		}

		public bool Save(RegisteredMachine machine)
		{
			var i = _connection.Insert(machine);
			return i > 0;			
		}

		public TableQuery<RegisteredMachine> GetQuery()
		{
			return _connection.Table<RegisteredMachine>();
		}
	}
}
