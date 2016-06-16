using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Devkoes.Restup.WebServer.Http;
using Devkoes.Restup.WebServer.Rest;
using openVote.VotingMachine.Server.Server.Controllers;
using openVote.VotingMachine.Server.Server.Controllers.vAlpha;

namespace openVote.VotingMachine.Server.Server
{
	public class WebServer
	{
		private readonly HttpServer _server;
		public WebServer()
		{
			_server = new HttpServer(8081);

			var versionRouteHandler = new RestRouteHandler();
			versionRouteHandler.RegisterController<VersionController>();
			_server.RegisterRoute("version", versionRouteHandler);


			var alphaRouterHandler = new RestRouteHandler();
			alphaRouterHandler.RegisterController<AlphaController>();
			_server.RegisterRoute("vAlpha", alphaRouterHandler);
		}

		public async void StartServer()
		{
			await _server.StartServerAsync();
		}

		public void StopServer()
		{
			_server.StopServer();
		}		
	}
}
