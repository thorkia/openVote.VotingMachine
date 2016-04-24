using System.Collections.Generic;
using System.Linq;
using openVote.VotingMachine.DataAccess.Api;
using openVote.VotingMachine.DataAccess.Models;

namespace openVote.VotingMachine.DataAccess
{
	public class BallotRepository
	{
		private readonly IBallotLoader _loader;
		private List<Ballot> _ballots;

		public IEnumerable<Ballot> Ballots
		{
			get
			{
				if (_ballots == null)
				{
					_ballots = _loader.LoadBallotsAsync().Result;
				}

				return _ballots.AsEnumerable();
			}
		}
		public BallotRepository(IBallotLoader loader)
		{
			_loader = loader;
		}

		
	}
}
