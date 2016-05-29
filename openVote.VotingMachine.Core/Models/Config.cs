using openVote.VotingMachine.Core.Api;

namespace openVote.VotingMachine.Core.Models
{
	public class Config : IConfig
	{
		public string BallotServer { get; set; }

		public string LoadBallotPath { get; set; }

		public string SaveBallotPath { get; set; }

		public string RegisterMachinePath { get; set; }

		public string UnlockPath { get; set; }
	}
}
