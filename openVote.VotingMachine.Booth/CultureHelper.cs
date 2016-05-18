using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace openVote.VotingMachine.Booth
{
	using Windows.Globalization.DateTimeFormatting;

	public class CultureInfoHelper
	{
		public static CultureInfo GetCurrentCulture()
		{
			var cultureName = new DateTimeFormatter("longdate", new[] { "US" }).ResolvedLanguage;

			return new CultureInfo(cultureName);
		}
	}
}
