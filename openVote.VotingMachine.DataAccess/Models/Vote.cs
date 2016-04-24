using System;
using SQLite.Net.Attributes;

namespace openVote.VotingMachine.DataAccess.Models
{
	public class Vote
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }

		public int BallotNumber { get; set; }

		public string VoteOption { get; set; }

		public DateTime RecordedTime { get; set; }


		//Add machine identifiers
		public string MachineName { get; set; }
		public string MachineIPAddress { get; set; }
		public string MachineMACAddress { get; set; }
	}
}
