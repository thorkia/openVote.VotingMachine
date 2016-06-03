using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net.Attributes;

namespace openVote.VotingMachine.Server.DataAccess.Models
{
	//TODO: Change this to store the public key for the server as well - so that responses can be encrypted
	public class RegisteredMachine
	{
		[PrimaryKey]
		public string UniqueId { get; set; }

		public string MachineName { get; set; }

		public string MachineIPAddress { get; set; }

		public string MachineId { get; set; }

		public string Version { get; set; }


		public string GetLogString()
		{
			return $"UniqueId={UniqueId} | MachineName={MachineName} | MachineIPAddress={MachineIPAddress} | MachineId={MachineId} | Version={Version}";
		}
	}
}
