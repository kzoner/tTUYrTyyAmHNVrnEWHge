using System;
using System.Data;
using System.Data.SqlClient;

namespace Inside.DataProviders
{
    public class SQLDatabase
    {
        private SqlConnection connection;
        private string m_ConnectionString;
        const int defaultTimeOut = 120;


        #region SQLDatabase constructors

        public SQLDatabase()
        {
            m_ConnectionString = string.Empty;
            connection = new SqlConnection(m_ConnectionString);
        }

        public SQLDatabase(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new SQLException("Connection string can not null or empty");

            m_ConnectionString = connectionString;
            connection = new SqlConnection(m_ConnectionString);
        }        

        #endregion


        #region SQLDatabase properties

        public string ConnectionString
        {
            get
            {
                return m_ConnectionString;
            }
            set
            {
                m_ConnectionString = value;
            }
        }

        #endregion


        #region Parameters assigment functions

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        protected void AssignParameters(SqlCommand command, SqlParameter[] parameters)
        {
            if (parameters == null) return;

            foreach (SqlParameter p in parameters)
            {
                command.Parameters.Add(p);
            }
        }        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameters"></param>
        protected void AssignParameters(SqlCommand command, object[] values)
        {
            int index;

            if (command.Parameters.Count - 1 != values.Length)
            {
                throw new Exception("Parameters was not matched");
            }

            for (index = 0; index < command.Parameters.Count; index++)
            {
                if (command.Parameters[index].Direction != ParameterDirection.Output && command.Parameters[index].Direction != ParameterDirection.ReturnValue)
                {
                    command.Parameters[index].Value = values[index];
                }
            }            
        }        

        #endregion


        #region Data manipulation functions 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText, CommandType commandType)
        {
            try
            {
                connection.ConnectionString = ConnectionString;
                using (connection)
                {
                    connection.Open();

                    //using (SqlTransaction transaction = connection.BeginTransaction())
                    //{
                    //using (SqlCommand command = new SqlCommand(commandText, connection, transaction))
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = commandType;
                        command.CommandTimeout = defaultTimeOut;

                        int affectedRows = command.ExecuteNonQuery();

                        //transaction.Commit();

                        return affectedRows;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if(connection.State == ConnectionState.Open)
                    connection.Close();
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            try
            {
                connection.ConnectionString = ConnectionString;
                using (connection)
                {
                    connection.Open();

                    //using (SqlTransaction transaction = connection.BeginTransaction())
                    //{
                    //using (SqlCommand command = new SqlCommand(commandText, connection, transaction))
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = commandType;
                        command.CommandTimeout = defaultTimeOut;

                        // Add command's parameters
                        AssignParameters(command, parameters);

                        int affectedRows = command.ExecuteNonQuery();

                        // Commit transaction
                        //        transaction.Commit();

                        // return affected rows
                        return affectedRows;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string procedureName, params object[] values)
        {
            try
            {
                connection.ConnectionString = ConnectionString;
                using (connection)
                {
                    connection.Open();

                    //using (SqlTransaction transaction = connection.BeginTransaction())
                    //{
                    //using (SqlCommand command = new SqlCommand(procedureName, connection, transaction))
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = defaultTimeOut;

                        SqlCommandBuilder.DeriveParameters(command);

                        AssignParameters(command, values);

                        int affectedRows = command.ExecuteNonQuery();

                        //transaction.Commit();

                        return affectedRows;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="returnValue"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string procedureName, ref int returnValue, params object[] values)
        {
            try
            {
                connection.ConnectionString = ConnectionString;
                using (connection)
                {
                    connection.Open();

                    //using (SqlTransaction transaction = connection.BeginTransaction())
                    //{
                    //using (SqlCommand command = new SqlCommand(procedureName, connection, transaction))
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = defaultTimeOut;

                        SqlCommandBuilder.DeriveParameters(command);

                        AssignParameters(command, values);

                        int affectedRows = command.ExecuteNonQuery();

                        returnValue = int.Parse(command.Parameters[0].Value.ToString());

                        //transaction.Commit();

                        return affectedRows;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText, CommandType commandType)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    //using (SqlTransaction transaction = connection.BeginTransaction())
                    //{
                    //using (SqlCommand command = new SqlCommand(commandText, connection, transaction))
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = commandType;
                        command.CommandTimeout = defaultTimeOut;

                        object returnValue = command.ExecuteScalar();

                        //transaction.Commit();

                        return returnValue;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public object ExecuteScalar(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    //using (SqlTransaction transaction = connection.BeginTransaction())
                    //{
                    //using (SqlCommand command = new SqlCommand(commandText, connection, transaction))
                    using (SqlCommand command = new SqlCommand(commandText, connection))
                    {
                        command.CommandType = commandType;
                        command.CommandTimeout = defaultTimeOut;

                        // Add command's parameters
                        AssignParameters(command, parameters);

                        object returnObject = command.ExecuteScalar();

                        // Commit transaction
                        //     transaction.Commit();

                        // return affected rows
                        return returnObject;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public object ExecuteScalar(string procedureName, params object[] values)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    //using (SqlTransaction transaction = connection.BeginTransaction())
                    //{
                    //using (SqlCommand command = new SqlCommand(procedureName, connection, transaction))
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = defaultTimeOut;

                        SqlCommandBuilder.DeriveParameters(command);

                        AssignParameters(command, values);

                        object returnValue = command.ExecuteScalar();

                        //     transaction.Commit();

                        return returnValue;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string commandText, CommandType commandType)
        {
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(commandText, connection);
                command.CommandType = commandType;
                command.CommandTimeout = defaultTimeOut;

                IDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

                return dr;
            }
            catch (Exception ex)
            {
                throw new SQLException(ex.Message, ex.InnerException);
            }
        
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(commandText, connection);
                command.CommandType = commandType;
                command.CommandTimeout = defaultTimeOut;

                AssignParameters(command, parameters);

                IDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

                return dr;
            }
            catch (Exception ex)
            {
                throw new SQLException(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string procedureName, params object[] values)
        {
            try
            {
                connection.Open();
                
                SqlCommand command = new SqlCommand(procedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = defaultTimeOut;

                SqlCommandBuilder.DeriveParameters(command);
                AssignParameters(command, values);

                IDataReader dr = command.ExecuteReader(CommandBehavior.CloseConnection);

                return dr;
            }
            catch (Exception ex)
            {
                throw new SQLException(ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        public DataSet FillDataSet(string commandText, CommandType commandType)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        using (SqlCommand command = new SqlCommand(commandText, connection, transaction))
                        {
                            command.CommandType = commandType;
                            command.CommandTimeout = defaultTimeOut;

                            SqlDataAdapter adapter = new SqlDataAdapter(command);

                            DataSet ds = new DataSet();

                            adapter.Fill(ds);

                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandText"></param>
        /// <param name="commandType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public DataSet FillDataSet(string commandText, CommandType commandType, params SqlParameter[] parameters)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        using (SqlCommand command = new SqlCommand(commandText, connection, transaction))
                        {
                            command.CommandType = commandType;
                            command.CommandTimeout = defaultTimeOut;

                            AssignParameters(command, parameters);

                            SqlDataAdapter adapter = new SqlDataAdapter(command);

                            DataSet ds = new DataSet();

                            adapter.Fill(ds);

                            return ds;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public DataSet FillDataSet(string procedureName, params object[] values)
        {
            try
            {
                using (connection)
                {
                    connection.Open();

                    //using (SqlTransaction transaction = connection.BeginTransaction())
                    //{
                    //using (SqlCommand command = new SqlCommand(procedureName, connection, transaction))
                    using (SqlCommand command = new SqlCommand(procedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandTimeout = defaultTimeOut;

                        SqlCommandBuilder.DeriveParameters(command);

                        AssignParameters(command, values);

                        SqlDataAdapter adapter = new SqlDataAdapter(command);

                        DataSet ds = new DataSet();

                        adapter.Fill(ds);

                        return ds;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="insertCommand"></param>
        /// <param name="updateCommand"></param>
        /// <param name="deleteCommand"></param>
        /// <param name="ds"></param>
        /// <param name="sourceTable"></param>
        /// <returns></returns>
        public int ExecuteDataSet(SqlCommand insertCommand, SqlCommand updateCommand, SqlCommand deleteCommand, DataSet dataSet, string sourceTable)
        {
            try
            {
                using (connection)
                {
                    connection.Open();
                    //using (SqlTransaction transaction = connection.BeginTransaction())
                    //{
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        if (insertCommand != null)
                        {
                            adapter.InsertCommand = insertCommand;
                            adapter.InsertCommand.Connection = connection;
                        }

                        if (updateCommand != null)
                        {

                            adapter.UpdateCommand = updateCommand;
                            adapter.UpdateCommand.Connection = connection;
                        }

                        if (deleteCommand != null)
                        {
                            adapter.DeleteCommand = deleteCommand;
                            adapter.DeleteCommand.Connection = connection;
                        }

                        int affectedRows = adapter.Update(dataSet, sourceTable);

                        //transaction.Commit();

                        return affectedRows;
                    }
                    // }
                }            
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }            
        }

        #endregion

        public void CloseConn()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

    }
}
