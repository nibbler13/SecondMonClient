using System;
using System.Data;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

namespace SecondMonClient {
	class FBClient {
		private FbConnection connection;

		public FBClient(string ipAddress, string baseName) {
			FbConnectionStringBuilder cs = new FbConnectionStringBuilder();
			cs.DataSource = ipAddress;
			cs.Database = baseName;
			cs.UserID = "SYSDBA";
			cs.Password = "masterkey";
			cs.Charset = "NONE";
			cs.Pooling = false;

			connection = new FbConnection(cs.ToString());
		}

		public DataTable GetDataTable(string query, Dictionary<string, string> parameters) {
			Console.WriteLine("=== GetDataTable");
			DataTable dataTable = new DataTable();

			try {
				Console.WriteLine("==== GetDataTable connection.Open");
				connection.Open();
				FbCommand command = new FbCommand(query, connection);

				if (parameters.Count > 0)
					foreach (KeyValuePair<string, string> parameter in parameters)
						command.Parameters.AddWithValue(parameter.Key, parameter.Value);

				FbDataAdapter fbDataAdapter = new FbDataAdapter(command);
				fbDataAdapter.Fill(dataTable);
			} catch (Exception e) {
				Console.WriteLine(e.Message + " @ " + e.StackTrace);
			} finally {
				Console.WriteLine("==== GetDataTable connection.Close");
				connection.Close();
			}

			Console.WriteLine("=== return GetDataTable");
			return dataTable;
		}

		public bool ExecuteUpdateQuery(string query, Dictionary<string, string> parameters) {
			Console.WriteLine("=== ExecuteUpdateQuery");

			bool updated = false;
			try {
				Console.WriteLine("==== ExecuteUpdateQuery connection.Open");
				connection.Open();
				FbCommand update = new FbCommand(query, connection);

				if (parameters.Count > 0)
					foreach (KeyValuePair<string, string> parameter in parameters)
						update.Parameters.AddWithValue(parameter.Key, parameter.Value);

				updated = update.ExecuteNonQuery() > 0 ? true : false;
			} catch (Exception e) {
				Console.WriteLine(e.Message + " @ " + e.StackTrace);
			} finally {
				Console.WriteLine("==== ExecuteUpdateQuery connection.Close");
				connection.Close();
			}

			Console.WriteLine("=== return ExecuteUpdateQuery");
			return updated;
		}
	}
}
