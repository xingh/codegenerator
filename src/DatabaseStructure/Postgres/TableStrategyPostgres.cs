using System.Data;
using iCodeGenerator.GenericDataAccess;

namespace iCodeGenerator.DatabaseStructure
{
	public class TableStrategyPostgres : TableStrategy
	{
		protected override DataSet TableSchema(DataAccessProviderFactory dataProvider, IDbConnection connection)
		{
			var ds = new DataSet();
			var sqlString = dataProvider.CreateCommand("SELECT tablename FROM pg_tables WHERE schemaname = 'public'",connection);
			sqlString.CommandType = CommandType.Text;
			var da = dataProvider.CreateDataAdapter();
			da.SelectCommand = sqlString;
			da.Fill(ds);
			return ds;
		}

		protected override DataSet ViewSchema(DataAccessProviderFactory dataAccessProvider, IDbConnection connection)
		{
			return new DataSet();
		}
		protected override Table CreateTable(Database database, DataRow row)
		{
			var table = new Table();
			table.ParentDatabase = database;
			table.Name = row["tablename"].ToString();
		    table.Schema = string.Empty;
			return table;
		}
	}
}
