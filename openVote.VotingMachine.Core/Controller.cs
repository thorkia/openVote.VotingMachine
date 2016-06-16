using System.Collections.Generic;
using System.Linq;
using Windows.Networking;
using Windows.Networking.Connectivity;
using Windows.Security.ExchangeActiveSyncProvisioning;
using openVote.VotingMachine.Core.Api;
using openVote.VotingMachine.Core.Models;

namespace openVote.VotingMachine.Core
{
	public class Controller : IController
	{
		private readonly IBallotLoader _ballotLoader;

		private List<Ballot> _ballots;
		private bool IsNetworkAcitve => NetworkInformation.GetHostNames().FirstOrDefault(h => h.Type == HostNameType.Ipv4) != null;
		
		public string MachineId { get; private set; }
		public string MachineName { get; private set; }
		public string MachineIP { get; private set; }

		
		public IEnumerable<Ballot> Ballots
		{
			get
			{
				if (_ballots == null)
				{
					_ballots = _ballotLoader.LoadBallots();
				}

				return _ballots.AsEnumerable();
			}
		}


		public Controller(IBallotLoader ballotLoader)
		{
			_ballotLoader = ballotLoader;

			RegisterMachine();
		}

		private void RegisterMachine()
		{
			if (!IsNetworkAcitve)
			{
				var eas = new EasClientDeviceInformation();
				MachineId = eas.Id.ToString();
				MachineName = eas.FriendlyName;
				return;
			}

			var ipConnection = NetworkInformation.GetHostNames().FirstOrDefault(h => h.Type == HostNameType.Ipv4);
			MachineIP = ipConnection.RawName;
			MachineId = ipConnection.IPInformation.NetworkAdapter.NetworkAdapterId.ToString();

			//TODO: Register the machine name!
			MachineName = new EasClientDeviceInformation().FriendlyName;
		}
	}
}
