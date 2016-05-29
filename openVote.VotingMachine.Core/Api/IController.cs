namespace openVote.VotingMachine.Core.Api
{
	public interface IController
	{
		//The local network MAC Address or Machine EAS ID
		string MachineId { get; }
		//The name given to this machine from registration service
		string MachineName { get; }
		//IP address of the machine running the software
		string MachineIP { get; }
	}
}