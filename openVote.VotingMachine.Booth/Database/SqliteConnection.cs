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
				//TODO: Add code to delete the stored DB if needed as part of the reboot - receive a command: IE: Start new election
				//Maybe change it to take a name - the election it is for and use that!
				//Wrap all the ballots in an "Election", that has a name - use that for the file name
				string path = Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "db.sqlite");

				return new SQLiteConnection(new SQLite.Net.Platform.WinRT.SQLitePlatformWinRT(), path, true);
			}
		}
	}
}
