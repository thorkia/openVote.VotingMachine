using System;
using openVote.VotingMachine.Core.Models;

namespace openVote.VotingMachine.Server.DataAccess.Models
{
	public class ServerRecordedVote : Vote
	{
		/// <summary>
		/// This is the time the server recieved the Vote
		/// </summary>
		public DateTime ServerRecordedTime { get; set; }

		/// <summary>
		/// This is the ID that the client used to save the vote
		/// </summary>
		public int ClientId { get; set; }
	}
}
