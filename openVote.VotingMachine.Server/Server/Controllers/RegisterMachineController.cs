using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using Devkoes.Restup.WebServer.Rest.Models.Contracts;
using MetroLog;
using Microsoft.Practices.ServiceLocation;
using openVote.VotingMachine.Core.WebApi.Requests;
using openVote.VotingMachine.Server.DataAccess;
using openVote.VotingMachine.Server.DataAccess.Models;

namespace openVote.VotingMachine.Server.Server.Controllers
{
	[RestController(InstanceCreationType.Singleton)]
	public class RegisterMachineController
	{
		private readonly ILogger _logger = LogManagerFactory.DefaultLogManager.GetLogger<RegisterMachineController>();
		private static RegisteredMachineRepository _repository;

		private RegisteredMachineRepository Repository
		{
			get
			{
				return _repository
							 ?? (_repository = ServiceLocator.Current.GetInstance<RegisteredMachineRepository>());
			}
		}
					
		[UriFormat("/register")]
		public IPostResponse PostRegisterMachine([FromContent]RegisterMachineRequest request)
		{
			_logger.Trace($"Request to register {request.GetLogString()}");

			string registerdId;

			//Check if the machine already exists
			var registered = Repository.GetQuery().FirstOrDefault(r => r.MachineId == request.MachineId);

			if (registered != null)
			{
				_logger.Trace($"Machine already Registered as {registered.GetLogString()}");
				registerdId = registered.UniqueId;
			}
			else
			{
				_logger.Trace("Attempting to register machine");
				var machine = new RegisteredMachine
				{
					MachineId = request.MachineId,
					MachineIPAddress = request.MachineIPAddress,
					MachineName = request.MachineName,
					Version = request.Version,
					UniqueId = Guid.NewGuid().ToString()
				};

				if (!Repository.Save(machine))
				{
					_logger.Trace("Unable to register machine. Error saving the machine to the database");
					return new PostResponse(PostResponse.ResponseStatus.Conflict, "", new { Error = "Unable to register machine for voting"});
				}

				_logger.Trace($"Machine registered as {machine.GetLogString()}");
				registerdId = machine.UniqueId;
			}
			
			return new PostResponse(PostResponse.ResponseStatus.Created, "", new { ID = registerdId });
		}
	}
}
