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
				new Ballot() { Id =1, Title = "Favourite Continent", Description = "Select your favourite continent", Choices = new List<string>() { "North America", "South America", "Europe", "Africa", "Asia", "Australia", "Antarctica" }},
				new Ballot() { Id =2, Title = "Favourite Ocean", Description = "Select your favourite ocean", Choices = new List<string>() { "Pacific", "Atlantic", "Indian", "Arctic", "Antarctic" }},
				new Ballot() { Id =3, Title = "Next Vacation Location", Description = "Which one is would like to so the most", Choices = new List<string>() { "Staycation", "Europe", "Japan", "Mexico", "Australia" }},
			};
		}
	}
}
