using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using Devkoes.Restup.WebServer.Rest.Models.Contracts;

namespace openVote.VotingMachine.Server.Server.Controllers
{
	[RestController(InstanceCreationType.Singleton)]
	public class RegisterMachineController
	{
		private static int _machineCount = 0;

		[UriFormat("/")]
		public IGetResponse GetRegisterMachine()
		{
			//TODO: Register the machine and return the ID
			_machineCount++;

			return new GetResponse( GetResponse.ResponseStatus.OK, new { ID = _machineCount});
		}
	}
}
