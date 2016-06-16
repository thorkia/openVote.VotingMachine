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
	public class VersionController
	{
		[UriFormat("/versioncheck/{version}")]
		public IGetResponse SupportedVersion(string version)
		{
			//TODO: Add code to check client version against support versions
			return new GetResponse(GetResponse.ResponseStatus.OK, new { ApiVersion = "vAlpha"});
		}
	}
}
