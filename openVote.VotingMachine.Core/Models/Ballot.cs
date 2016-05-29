using System.Collections.Generic;

namespace openVote.VotingMachine.Core.Models
{
	public class Ballot
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		public List<string> Choices { get; set; }

		public Ballot()
		{
			Choices = new List<string>();
		}

		public override string ToString()
		{
			return $"{Id} - {Title} - {Description}";
		}
	}
}
