using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace openVote.VotingMachine.Server.DataAccess.Models
{
	public class LockModel
	{
		[PrimaryKey]
		public string MachineId { get; set; }
		public bool Locked { get; set; }
	}
}
