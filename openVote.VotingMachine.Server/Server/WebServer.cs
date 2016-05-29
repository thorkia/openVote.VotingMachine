using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Devkoes.Restup.WebServer.Http;
using Devkoes.Restup.WebServer.Rest;
using openVote.VotingMachine.Server.Server.Controllers;

namespace openVote.VotingMachine.Server.Server
{
	public class WebServer
	{
		private readonly HttpServer _server;
		public WebServer()
		{
			_server = new HttpServer(8081);

			var registerRouterHandler = new RestRouteHandler();
			registerRouterHandler.RegisterController<RegisterMachineController>();

			_server.RegisterRoute("/api/register", registerRouterHandler);			
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
