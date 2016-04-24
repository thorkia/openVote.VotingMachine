using System.IO;
using SQLite.Net;

namespace openVote.VotingMachine.Booth.Database
{
	public class Database
	{		
		public static SQLiteConnection Connection
		{
			get
			{
				string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");
				return new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path, true);
			}
		}
	}
}
