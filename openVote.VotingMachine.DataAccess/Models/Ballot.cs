using System.Collections.Generic;

namespace openVote.VotingMachine.DataAccess.Models
{
	public class Ballot
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public List<string> Choices { get; set; }

		public Ballot()
		{
			Choices = new List<string>();
		}
	}
}
