using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using openVote.VotingMachine.DataAccess.Api;
using openVote.VotingMachine.DataAccess.Models;

namespace openVote.VotingMachine.Booth.Database
{
	public class TestBallotLoader : IBallotLoader
	{
		public List<Ballot> LoadBallots()
		{
			return new List<Ballot>()
			{
				new Ballot() { Id =1, Title = "test 1", Description = "Figure it out!", Choices = new List<string>() { "A", "B", "Longer Name", "Only Four This time" }},
				new Ballot() { Id =2, Title = "test 2", Description = "Figure it out!", Choices = new List<string>() { "Android", "iOS", "Windows Mobile", "Blackberry" }},
			};
		}
	}
}
