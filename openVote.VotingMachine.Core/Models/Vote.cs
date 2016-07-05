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

		public string GetLogString()
		{
			return $"BallotId={BallotId} | VoteOption={VoteOption} | RecordedTime={RecordedTime} | MachineName={MachineName} | MachineIPAddress={MachineIPAddress} | MachineId={MachineId} | ServerRegsiteredMachinedId={ServerRegsiteredMachinedId}";
		}
	}
}
