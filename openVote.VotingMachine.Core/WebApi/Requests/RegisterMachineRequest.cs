namespace openVote.VotingMachine.Core.WebApi.Requests
{
	//TODO: Add alter this to include the public key when encryption is enabled
	public class RegisterMachineRequest
	{
		public string MachineName { get; set; }

		public string MachineIPAddress { get; set; }

		public string MachineId { get; set; }

		public string Version { get; set; }

		public string GetLogString()
		{
			return $"MachineName={MachineName} | MachineIPAddress={MachineIPAddress} | MachineId={MachineId} | Version={Version}";
		}
	}
}
