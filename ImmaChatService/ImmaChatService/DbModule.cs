using Microsoft.CSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Xml;
using System.IO;
using System.Configuration;
using System.Text;

public class DBAccessClass
{
    public static string DbConnStr = System.Configuration.ConfigurationManager.ConnectionStrings["imma_str"].ToString();
    public ArrayList SqlStatements = new ArrayList();
    public int Count;
    public SqlDataAdapter DbAdpt = new SqlDataAdapter();
    public SqlDataReader DbRdr;
    public string ExecutionResults;
    public DataSet Dbdset = new DataSet();
    public DataTable DbTables = new DataTable();
    public SqlCommand DbCommand = new SqlCommand();
    public SqlTransaction DbTransact;

    public SqlConnection DbConn = new SqlConnection();
    public string ClassError;
    
    public string SerialCode;

    public ConnectionState DbServerConnect()
    {
        ClassError = "";
        try
        {
            if (DbConn.State != ConnectionState.Connecting)
            {
                DbConn.ConnectionString = DbConnStr;
            }
            if (DbConn.State != ConnectionState.Closed)
            {
                DbConn.Close();
            }
            DbConn.Open();
        }
        catch (Exception ex)
        {
            ClassError = ex.Message;
        }
        return DbConn.State;
    }
    public void DbServerDisconnect()
    {
        ClassError = "";
        try
        {
            DbConn.Close();
        }
        catch (Exception ex)
        {
            ClassError = ex.Message;
        }
    }
    public void AddSqlStatements(string SqlStatement)
    {
        ClassError = "";
        try
        {
            SqlStatements.Add(SqlStatement);
        }
        catch (Exception ex)
        {
            ClassError = ex.Message;
        }
    }
    public DataSet SearchSqlStatements(string SqlStatement)
    {
        ClassError = "";

        DbServerConnect();
        Dbdset.Tables.Clear();
        DbCommand.CommandText = SqlStatement;
        DbCommand.Connection = DbConn;
        DbCommand.Prepare();
        DbAdpt.SelectCommand = DbCommand;
        try
        {
            DbAdpt.Fill(Dbdset, "dbTable");
        }
        catch (Exception ex)
        {
            ClassError = ex.Message;            
        }
        DbConn.Close();
        return Dbdset;
    }
    public string ExecuteSqlStatements()
    {
        ClassError = "";
        ExecutionResults = "Not Successful";
        DbServerConnect();
        DbCommand.Connection = DbConn;
        try
        {
            if (SqlStatements.Count > 0)
            {
                Count = 0;
                for (Count = 0; Count <= SqlStatements.Count - 1; Count++)
                {
                    DbCommand.CommandText = SqlStatements[Count].ToString();
                    DbCommand.ExecuteNonQuery();
                }
                ExecutionResults = "Successful";
            }
            else
            {
                ExecutionResults = "No SqlStatements";
            }
            DbConn.Close();
        }
        catch (Exception ex)
        {
            ClassError = ex.Message;
            DbConn.Close();
            ExecutionResults = "Error";
            ClearSqStatements();            
        }
        ClearSqStatements();
        DbConn.Close();
        return ExecutionResults;
    }
    public DataSet ExecuteStoredProc(string sp_name)
    {

        DataSet Return_dset = null;
        DbCommand = new SqlCommand();
        try
        {

            DbConn.Close();
            DbConn.Dispose();
            DbServerConnect();
            DbCommand.CommandType = CommandType.StoredProcedure;
            DbCommand.Connection = DbConn;
            DbCommand.CommandText = sp_name;

            switch (sp_name.ToLower())
            {                
                case "generate_serials":
                    {
                        DbCommand.Parameters.AddWithValue("@code", SerialCode);
                    } break;                
            }
            DbAdpt = new SqlDataAdapter(DbCommand);

            if (DbConn.State == ConnectionState.Open)
            {
                DbAdpt.Fill(Dbdset, "dbTable");
                Return_dset = Dbdset;
            }
            DbConn.Close();
            DbCommand.Dispose();
            return Return_dset;
        }
        catch (Exception ex)
        {
            ClassError = ex.Message;            
            DbConn.Close();
            DbConn.Dispose();
            return null;
        }
    }
    public void ClearSqStatements()
    {
        ClassError = "";
        try
        {
            SqlStatements.Clear();
        }
        catch (Exception ex)
        {
            ClassError = ex.Message;
        }
    }
}