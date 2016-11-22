#if __RCC_COMMENT__
using cssa_appraisal_partner_version3.CustomEventArgs;
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace SQLServerDB
{
    public class DBUtils
    {

        public static SqlConnection GetNewSqlConnection()
        {
            // Create a new connection and try to open it
            SqlConnection Connection = new SqlConnection(Connection_String.SQLServer_ConnectionString);
            try {
                Connection.Open();
            } catch (Exception) {
                Connection = null;  // Opening connection failed; return null
            }
            return Connection;
        }

        public static DataSet ExecuteSqlQuery(string strSQL)
        {
            using (SqlConnection conn = new SqlConnection(Connection_String.SQLServer_ConnectionString)) {
                conn.Open();
                // Call the overload that takes a connection in place of a connection string
                return ExecuteSqlQuery(strSQL, conn);
            }
        }


        public static DataSet ExecuteSqlQuery(string strSQL, String project_Instantiation, String project_Name)
        {
            DataSet dsObject = new DataSet();
            //using (SqlDataAdapter daObject = new SqlDataAdapter(strSQL, GetNewSqlConnection()))
            //{
            //    try
            //    {
            //        daObject.SelectCommand.Parameters.Add(new SqlParameter
            //        {
            //            ParameterName = "@instantiation",
            //            Value = project.Instantiation,
            //            SqlDbType = SqlDbType.VarChar,
            //            Size = 50
            //        });
            //        daObject.SelectCommand.Parameters.Add(new SqlParameter
            //        {
            //            ParameterName = "@nodename",
            //            Value = project.NodeName,
            //            SqlDbType = SqlDbType.VarChar,
            //            Size = 50
            //        });
            //        daObject.Fill(dsObject);
            //    }
            //    catch (Exception ex)
            //    {
            //        LogManager.writeLog("DBUtils.ExecuteSqlQuery: Exception > [{0}]", ex.Message);
            //    }
            //}
            return dsObject;
        }



        public static DataSet ExecuteSqlQuery(string strSQL, SqlConnection conn)
        {
            DataSet dsObject = new DataSet();
            using (SqlDataAdapter daObject = new SqlDataAdapter(strSQL, conn)) {
                try {
                    daObject.Fill(dsObject);
                } catch (Exception ex) {
                    LogManager.writeLog("DBUtils.ExecuteSqlQuery: Exception > [{0}]", ex.Message);
                }
            }
            return dsObject;
        }

        /// <summary>
        /// Specify a table name when creating the Dataset.
        /// </summary>
        /// <param name="strSQL"></param>
        /// <param name="conn"></param>
        /// <param name="tableName"></param>
        /// <returns>DataSet dsObject</returns>
        public static DataSet ExecuteSqlQuery(string strSQL, SqlConnection conn, string tableName)
        {
            DataSet dsObject = new DataSet();
            using (SqlDataAdapter daObject = new SqlDataAdapter(strSQL, conn))
            {
                try
                {
                    daObject.Fill(dsObject, tableName);
                }
                catch (Exception ex)
                {
                    LogManager.writeLog("DBUtils.ExecuteSqlQuery: Exception > [{0}]", ex.Message);
                }
            }
            return dsObject;
        }


        public static bool ExecuteSqlNonQuery(string strSql)
        {
            string strConn = Connection_String.SQLServer_ConnectionString;

            // pass through the call providing the default for the database to query (default database connection string is pulled from the app config file)
            return ExecuteSqlNonQuery(strSql, strConn);
        }

        public static bool ExecuteSqlNonQuery(string strSQL, string strConn)
        {
            using (SqlConnection conn = new SqlConnection(strConn)) {
                conn.Open();
                // Call the overload that takes a connection in place of a connection string
                return ExecuteSqlNonQuery(strSQL, conn);
            }
        }

        public static bool ExecuteSqlNonQuery(string strSQL, SqlConnection conn)
        {
            bool status = false;
            using (SqlCommand cmd = new SqlCommand(strSQL, conn)) {
                try {
                    status = (cmd.ExecuteNonQuery() > 0);
                } catch (Exception ex) {
                    LogManager.writeLog("DBUtils.ExecuteSqlQuery: Exception > [{0}]", ex.Message);
                }
            }
            return status;
        }

        /// <summary>
        /// Used when parameterization is needed.
        /// </summary>
        /// <param name="cmd"></param>
        /// <returns></returns>
        public static bool ExecuteSqlNonQuery(SqlCommand cmd)
        {
            bool status = false;
            try
            {
                status = (cmd.ExecuteNonQuery() > 0);
            }
            catch (Exception ex)
            {
                LogManager.writeLog("DBUtils.ExecuteSqlQuery: Exception > [{0}]", ex.Message);
            }
            return status;
        }

        public static DataSet ExecuteStoredProcedureQuery(string strProcedure, params string[] args)
        {
            DataSet dsObject = new DataSet();
            try {
                dsObject = ExecuteStoredProcedureQuery(strProcedure + argsToString(args));
            } catch (Exception ex) {
                LogManager.writeLog("DBUtils.ExecuteStoredProcedure: Exception > [{0}] > strProcedure [{1}]", ex.Message, strProcedure);
            }
            return dsObject;
        }

        public static DataSet ExecuteStoredProcedureQuery(string strProcedure)
        {
            string strConn = Connection_String.SQLServer_ConnectionString;
            DataSet dsObject = new DataSet();
            SqlDataAdapter daObject = new SqlDataAdapter(strProcedure, strConn);

            try {
                daObject.Fill(dsObject);
            } catch (Exception ex) {
                LogManager.writeLog("DBUtils.ExecuteStoredProcedureQuery: Exception > [{0}]", ex.Message);
            } finally { daObject.Dispose(); }
            return dsObject;
        }

        public static bool ExecuteStoredProcedureNonQuery(string strProcedure, params string[] args)
        {
            bool status = false;
            try {
                status = ExecuteStoredProcedureNonQuery(strProcedure + argsToString(args));
            } catch (Exception ex) {
                LogManager.writeLog("DBUtils.ExecuteStoredProcedureNonQuery: Exception [{0}] strProcedure [{1}]", ex.Message, strProcedure);
            }
            return status;
        }

        public static bool ExecuteStoredProcedureNonQuery(string strProcedure)
        {
            string strConn = Connection_String.SQLServer_ConnectionString;
            bool status = false;
            SqlConnection conn = new SqlConnection(strConn);
            SqlCommand cmd = new SqlCommand(strProcedure, conn);
            try {
                cmd.Connection.Open();
                status = (cmd.ExecuteNonQuery() > 0);
            } catch (Exception ex) {
                LogManager.writeLog("DBUtils.ExecuteStoredProcedureNonQuery: Exception > [{0}]", ex.Message);
            } finally {
                cmd.Connection.Close();
                cmd.Dispose();
            }
            return status;
        }

        public static int ExecuteSqlQueryScalar(string strSQL)
        {
            string strConn = Connection_String.SQLServer_ConnectionString;
            using (SqlConnection conn = new SqlConnection(strConn)) {
                conn.Open();
                return ExecuteSqlQueryScalar(strSQL, conn);
            }
        }

        public static int ExecuteSqlQueryScalar(string strSQL, SqlConnection conn)
        {
            int retValue = 0;

            using (SqlCommand cmd = new SqlCommand(strSQL, conn)) {
                using (SqlDataReader reader = cmd.ExecuteReader()) {
                    try {
                        reader.Read();
                        retValue = Convert.ToInt32(reader[0]);
                    } catch (Exception ex) {
                        LogManager.writeLog("DBUtils.ExecuteSqlQueryScalar: Exception > [{0}]", ex.Message);
                        return 0;
                    }
                } // using reader
            } // using cmd
            return retValue;
        }

        private static string argsToString(params string[] args)
        {
            StringBuilder query = new StringBuilder();
            string strFormatter;

            for (int i = 0; i < args.Length; i++) {
                if (i > 0) strFormatter = ",";
                else strFormatter = " ";

                if (args[i].Length > 0) strFormatter += "'{0}'";
                else strFormatter += "NULL";

                query.AppendFormat(strFormatter, args[i].Replace("'", "''"));
            }
            return query.ToString();
        }

    }
}
