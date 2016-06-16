using System;
using System.Linq;
using Devkoes.Restup.WebServer.Attributes;
using Devkoes.Restup.WebServer.Models.Schemas;
using Devkoes.Restup.WebServer.Rest.Models.Contracts;
using MetroLog;
using Microsoft.Practices.ServiceLocation;
using openVote.VotingMachine.Core.Models;
using openVote.VotingMachine.Core.WebApi.Requests;
using openVote.VotingMachine.Server.DataAccess;
using openVote.VotingMachine.Server.DataAccess.Models;

namespace openVote.VotingMachine.Server.Server.Controllers.vAlpha
{
	[RestController(InstanceCreationType.Singleton)]
	public class AlphaController
	{
		private readonly ILogger _logger = LogManagerFactory.DefaultLogManager.GetLogger<AlphaController>();
		private static RegisteredMachineRepository _registerRepository;
		private static BallotRepository _ballotRepository;
		private static VoteRepository _voteRepository;

		private RegisteredMachineRepository RegisterRepository
		{
			get
			{
				return _registerRepository
							 ?? (_registerRepository = ServiceLocator.Current.GetInstance<RegisteredMachineRepository>());
			}
		}

		private BallotRepository BallotRepository
		{
			get
			{
				return _ballotRepository
							 ?? (_ballotRepository = ServiceLocator.Current.GetInstance<BallotRepository>());
			}
		}

		private VoteRepository VoteRepository
		{
			get
			{
				return _voteRepository
							 ?? (_voteRepository = ServiceLocator.Current.GetInstance<VoteRepository>());
			}
		}


		[UriFormat("/register")]
		public IPostResponse PostRegisterMachine([FromContent]RegisterMachineRequest request)
		{
			_logger.Trace($"Request to register {request.GetLogString()}");

			string registerdId;

			//Check if the machine already exists
			var registered = RegisterRepository.GetQuery().FirstOrDefault(r => r.MachineId == request.MachineId);

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

				if (!RegisterRepository.Save(machine))
				{
					_logger.Trace("Unable to register machine. Error saving the machine to the database");
					return new PostResponse(PostResponse.ResponseStatus.Conflict, "", new { Error = "Unable to register machine for voting"});
				}

				_logger.Trace($"Machine registered as {machine.GetLogString()}");
				registerdId = machine.UniqueId;
			}
			
			return new PostResponse(PostResponse.ResponseStatus.Created, "", new { ID = registerdId });
		}

		[UriFormat("/ballots")]
		public IGetResponse GetBallots()
		{
			var ballots = BallotRepository.Ballots;

			return new GetResponse(GetResponse.ResponseStatus.OK, ballots);
		}

		[UriFormat("/vote")]
		public IPostResponse PlaceVote([FromContent]Vote vote)
		{
			var serverVote = new ServerRecordedVote()
			{
				ClientId = vote.Id,
				BallotId = vote.BallotId,
				MachineIPAddress = vote.MachineIPAddress,
				MachineId = vote.MachineId,
				MachineName = vote.MachineName,
				RecordedTime = vote.RecordedTime,
				ServerRegsiteredMachinedId = vote.ServerRegsiteredMachinedId,
				VoteOption = vote.VoteOption,
				ServerRecordedTime = DateTime.Now			
			};

			var success = VoteRepository.Save(serverVote);

			return success ? 
				new PostResponse(PostResponse.ResponseStatus.Created, "", new {ID = serverVote.Id}) 
				: new PostResponse(PostResponse.ResponseStatus.Conflict, "", new { ID = -1, Error = "Unable to save vote, please try again" });
		}
	}
}
