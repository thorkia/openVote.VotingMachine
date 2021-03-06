﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.UI.Xaml;

namespace openVote.VotingMachine.Booth
{
	public class AssemblyFunctions
	{
		public static string Version
		{
			get
			{
				Package package = Package.Current;
				PackageId packageId = package.Id;
				PackageVersion version = packageId.Version;

				return $"{version.Major}.{version.Minor}.{version.Build}.{version.Revision}";
			}
		}

		//TODO: Find a way to get the public key token for display
		public static string Secured
		{
			get
			{
				Package package = Package.Current;
				var status = package.Status;

				return (!status.Tampered) ? "Yes" : "No";
			}
		}

		public static string InstalledDate
		{
			get
			{
				Package package = Package.Current;

				var culture = CultureInfoHelper.GetCurrentCulture();
				return package.InstalledDate.ToLocalTime().Date.ToString("D", culture);
			}
		}
	}
}
