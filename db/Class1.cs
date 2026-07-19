using System.Data;
using System.Data.SqlClient;

namespace IncidentReportSystem;

internal class Class1
{
	private string sqlConString;

	public int rowAffected;

	public string SqlConString { get; set; }

	public Class1(string server_address, string database, string username, string password)
	{
		sqlConString = "Server = " + server_address + "; Database = " + database + "; User Id = " + username + "; Password = " + password;
	}

	public DataTable GetData(string sql)
	{
		SqlConnection Sqlcon = new SqlConnection(sqlConString);
		if (Sqlcon.State == ConnectionState.Closed)
		{
			Sqlcon.Open();
		}
		SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(new SqlCommand(sql, Sqlcon));
		DataSet ds = new DataSet();
		sqlDataAdapter.Fill(ds);
		return ds.Tables[0];
	}

	public void executeSQL(string sql)
	{
		SqlConnection Sqlcon = new SqlConnection(sqlConString);
		if (Sqlcon.State == ConnectionState.Closed)
		{
			Sqlcon.Open();
		}
		SqlCommand SQLcom = new SqlCommand(sql, Sqlcon);
		rowAffected = SQLcom.ExecuteNonQuery();
	}
}
