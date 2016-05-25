using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openVote.VotingMachine.Booth.Settings
{
	//TODO: Move much of this into resources for localization
	public class LogStatements
	{
		public static string NavigatedFromAction = "from";
		public static string NavigatedToAction = "to";

		public static string NavigatedLog(string action, string page)
		{
			return $"Navigated {action} page [{page}]";
		}

		public static string UserClickedButtonLog(string button)
		{
			return $"Button [{button}] tapped";
		}

		public static string ViewModelReceivedStateLog(string state)
		{
			return $"ViewModel received state [{state}]";
		}
	}
}
