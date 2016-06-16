using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MetroLog;
using openVote.VotingMachine.Core.Models;

namespace openVote.VotingMachine.Server.DataAccess
{
	public class BallotRepository
	{
		private readonly ILogger _logger = LogManagerFactory.DefaultLogManager.GetLogger<BallotRepository>();

		private readonly Dictionary<int, Ballot> _ballots;
		private readonly string _ballotPath;

		public IEnumerable<Ballot> Ballots => _ballots.Values;

		public BallotRepository()
		{
			_ballots = new Dictionary<int, Ballot>();
			
			//Load Ballots from XML here
			_ballotPath = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "ballots.xml");
			LoadBallots();
		}

		private void LoadBallots()
		{
			XDocument doc = XDocument.Load( new FileStream(_ballotPath, FileMode.Open), LoadOptions.None);

			//Parse Document here
			var ballots = doc.Descendants().Where(e => e.Name == "Ballot");
						
			foreach (var ballotElement in ballots)
			{
				Ballot ballot = new Ballot
				{
					Id = int.Parse(ballotElement.Attribute("Id").Value),
					Title = ballotElement.Element("Title")?.Value,
					Description = ballotElement.Element("Description")?.Value
				};
				
				var choices = ballotElement.Element("Choices");
				foreach (var choiceElement in choices.Elements())
				{
					ballot.Choices.Add(choiceElement?.Value);
				}

				if (_ballots.ContainsKey(ballot.Id))
				{
					_logger.Info($"Duplicate Ballot of Id [{ballot.Id}] with title [{ballot.Title}] found.  Ballot will not be processed");
				}
				else
				{
					_ballots[ballot.Id] = ballot;
					_logger.Info($"Ballot with Id [{ballot.Id}] and Title [{ballot.Title}] loaded.");
				}
			}

		}
	}
}
