using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Windows;

public class SQL_Connector : IDisposable
{

    /// <summary>
    /// The connection object
    /// </summary>
    public SqlConnection Connection;
    /// <summary>
    /// The connection status
    /// </summary>
    public ConnectionState ConnectionStatus;
    /// <summary>
    /// The last excecuted error
    /// </summary>
    public String Error;

    /// <summary>
    /// Initializes a new instance of the <see cref="SQL_Connector"/> class.
    /// </summary>
    /// <param name="connectionString">The connection string.</param>
    public SQL_Connector(String connectionString)
    {
        try
        {
            this.Connection = new SqlConnection(connectionString);
            this.Connection.Open();
            this.ConnectionStatus = this.Connection.State;
        }
        catch (Exception)
        {
            
        }
    }
    /// <summary>
    /// Selects the specified query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <param name="needle">El caracter que separa las columnas de las filas</param>
    /// <param name="result">Como parámetro de salida el resultado del query</param>
    public Boolean Select(String query, out List<String> result, char needle)
    {
        result = new List<string>();
        try
        {
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, this.Connection);
            DataSet ds = new DataSet();
            sqlAdapter.Fill(ds);
            String row;
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                row = String.Empty;
                for (int j = 0; j < ds.Tables[0].Rows[i].ItemArray.Length; j++)
                {
                    row += ds.Tables[0].Rows[i].ItemArray[j].ToString();
                    if (j != ds.Tables[0].Rows[i].ItemArray.Length - 1)
                        row += needle;
                }
                result.Add(row);
            }
            return true;
        }
        catch (Exception exc)
        {
            Error = exc.Message;
        }
        return false;
    }

    /// <summary>
    /// Enviar comando directo a base de datos SQL Server
    /// </summary>
    /// <param name="query"></param>
    /// <returns></returns>
    public int Command(string query, Dictionary<string, object> parameters, bool isInsert = false)
    {
        try
        {
            int affected = 0;

            SqlCommand newCommand = new SqlCommand(query, this.Connection);           

            foreach(KeyValuePair<string, object> keyParam in parameters)
            {
                if (newCommand.Parameters.Contains(keyParam.Key))
                    newCommand.Parameters.Remove(keyParam.Key);

                newCommand.Parameters.AddWithValue(keyParam.Key, keyParam.Value);
            }

            if (isInsert)
            {
                object returnObj = newCommand.ExecuteScalar();

                if (returnObj != null)
                {
                    int.TryParse(returnObj.ToString(), out affected);
                }
            }
            else
                affected = newCommand.ExecuteNonQuery();

            return affected;
        }
        catch (Exception exc)
        {
            Error = exc.Message;
        }
        return 0;
    }

    public int spMachotes(string spName, Dictionary<string, object> parameters, out string returnMsg)
    {
        returnMsg = "";
        int affected = 0;
        try
        {                      
            SqlCommand newCommand = new SqlCommand(spName, this.Connection);
            newCommand.CommandType = CommandType.StoredProcedure;

            foreach (KeyValuePair<string, object> keyParam in parameters)
            {
                if (newCommand.Parameters.Contains(keyParam.Key))
                    newCommand.Parameters[keyParam.Key].Value = keyParam.Value;
                else
                    newCommand.Parameters.AddWithValue(keyParam.Key, keyParam.Value);
            }

            SqlParameter outMsgParam = new SqlParameter("@MsgRetorno", SqlDbType.NVarChar,50);
            outMsgParam.Direction = ParameterDirection.Output;
            newCommand.Parameters.Add(outMsgParam);

            affected = newCommand.ExecuteNonQuery();

            returnMsg = newCommand.Parameters["@MsgRetorno"].Value.ToString();

            return affected;

        }
        catch (Exception exc)
        {
            Error = exc.Message;
        }

        return 0;
    }
    /// <summary>
    /// Selects the specified query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <param name="needle">El caracter que separa las columnas de las filas</param>
    /// <param name="result">Como parámetro de salida el resultado del query</param>
    public DataSet SelectTables(string query)
    {
        //Inicializo
        DataSet dtSet = new DataSet();
        try
        {
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, this.Connection);
            sqlAdapter.Fill(dtSet);
        }
        catch (Exception exc)
        {
            Error = exc.Message;
        }
        return dtSet;
    }

    public DataSet SelectTables(string query, Dictionary<string,object> parameters)
    {
        //Inicializo
        DataSet dtSet = new DataSet();
        try
        {
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, this.Connection);

            foreach (KeyValuePair<string, object> keyParam in parameters)
            {
                sqlAdapter.SelectCommand.Parameters.AddWithValue(keyParam.Key, keyParam.Value);
            }

            sqlAdapter.Fill(dtSet);
        }
        catch (Exception exc)
        {
            Error = exc.Message;
        }
        return dtSet;
    }

    public Boolean SelectOne(String query, out object[] result)
    {
        result = new object[0];
        try
        {
            SqlDataAdapter sqlAdapter = new SqlDataAdapter(query, this.Connection);
            DataSet ds = new DataSet();
            sqlAdapter.Fill(ds);
            result = ds.Tables[0].Rows[0].ItemArray;
            return true;
        }
        catch (Exception exc)
        {
            Error = exc.Message;
        }
        return false;
    }



    /// <summary>
    /// Runs the specified query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>Verdadero si funciona el comando</returns>
    public bool Run(string query)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = this.Connection;
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception exc)
        {
            Error = exc.Message;           
        }
        return false;
    }


    /// <summary>
    /// Runs the specified query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns>Verdadero si funciona el comando</returns>
    public Boolean Run(SqlCommand cmd)
    {
        try
        {
            cmd.Connection = this.Connection;
            cmd.ExecuteNonQuery();
            return true;
        }
        catch (Exception exc)
        {
            Error = exc.Message;
        }
        return false;
    }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        this.Connection.Close();
        this.Connection.Dispose();
    }
    /// <summary>
    /// Formats the specified value.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static string Format(String value)
    {
        return value.Replace("'", "''");
    }

}

