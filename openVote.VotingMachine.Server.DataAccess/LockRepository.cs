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
	public class LockRepository : BaseSqlRepository<LockModel>
	{
		private readonly SQLiteConnection _connection;

		public LockRepository(SQLiteConnection connection) : base(connection)
		{
		}

		public bool SetState(string machineId, bool locked)
		{
			//Get the current item
			var lockState = GetQuery().FirstOrDefault(lm => lm.MachineId == machineId);

			if (lockState == null)
			{
				return Save(new LockModel() {Locked = locked, MachineId = machineId});
			}

			lockState.Locked = locked;

			return _connection.Update(lockState) > 0;
		}

		public bool GetState(string machineId)
		{
			var lockState = GetQuery().FirstOrDefault(lm => lm.MachineId == machineId);

			return lockState?.Locked ?? false;
		}
	}
}
