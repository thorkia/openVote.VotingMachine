namespace openVote.VotingMachine.Core.Api
{
	public interface IConfig
	{
		string BallotServer { get; set; }

		string LoadBallotPath { get; set; }

		string SaveBallotPath { get; set; }

		string RegisterMachinePath { get; set; }

		string UnlockPath { get; set; }

	}
}