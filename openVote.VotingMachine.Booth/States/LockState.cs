﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openVote.VotingMachine.Booth.States
{
	public class LockState : IState
	{
		public string PageName => "LockScreen";

		public string GetLogString()
		{
			return "LockState";
		}
	}
}
