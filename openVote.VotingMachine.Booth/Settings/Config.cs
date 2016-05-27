using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace openVote.VotingMachine.Booth.Settings
{
	public class Config
	{
		public string BallotServer { get; set; }

		public string LoadBallotPath { get; set; }

		public string SaveBallotPath { get; set; }

		public string RegisterMachinePath { get; set; }

		public string UnlockPath { get; set; }
	}
}
