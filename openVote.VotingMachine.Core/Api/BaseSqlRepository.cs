using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;

namespace openVote.VotingMachine.Core.Api
{
	public class BaseSqlRepository<T> : IRepository<T> where T : class
	{
		private readonly SQLiteConnection _connection;

		public BaseSqlRepository(SQLiteConnection connection)
		{
			_connection = connection;
		}

		public virtual bool Save(T item)
		{
			var i = _connection.Insert(item);
			return i > 0;
		}

		public virtual TableQuery<T> GetQuery() 
		{
			return _connection.Table<T>();
		}
	}
}
