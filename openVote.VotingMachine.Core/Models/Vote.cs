using System;
using SQLite.Net.Attributes;

namespace openVote.VotingMachine.Core.Models
{
	public class Vote
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public int BallotId { get; set; }

		public string VoteOption { get; set; }

		public DateTime RecordedTime { get; set; }


		//Add machine identifiers
		public string MachineName { get; set; }

		public string MachineIPAddress { get; set; }

		public string MachineId { get; set; }

		//This is the registered MachineId Generated from the registration
		public string ServerRegsiteredMachinedId { get; set; }
	}
}
