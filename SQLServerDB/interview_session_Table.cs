using System;
using System.Collections.Generic;

using System.Data; //DataSet
using System.Data.SqlClient; //SQLConnection, SQLCommand

namespace SQLServerDB
{
    public class interview_session_Table
    {
        public const string theTable = "T_interview_session";

        public List<interview_session> itemList = new List<interview_session>();

        
        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ReadItemListFromDatabase - read all records from "theTable" insert them into this.itemList
        ///</summary>
        ///<input>List<interview_session> itemList - an ordinary List<> of type interview_session, will be cleared if not already empty </input>
        ///<output>List<interview_session>  itemList - an ordinary List<> of type interview_session, extracted from the database </output>
        public void ReadItemListFromDatabase()
        {
            itemList.Clear();  //First, empty the existing list contents

            string sQuery = "SELECT * FROM " + theTable;
            DataSet dsObj = DBUtils.ExecuteSqlQuery(sQuery);

            if (dsObj != null && dsObj.Tables[0].Rows.Count > 0)
            {
                DataTable dtObj = dsObj.Tables[0]; //get the DataTable reference once

                foreach (DataRow dr in dtObj.Rows)
                {
                    //extract data
                    int intID = Convert.ToInt32(dr["ID"]);
                    int intsessionId = Convert.ToInt32(dr["sessionId"]);
                    int intsessionIndex = Convert.ToInt32(dr["sessionIndex"]);
                    String strsessionName = dr["sessionName"].ToString();
        
                    int intsessionDurationHours = Convert.ToInt32(dr["sessionDurationHours"]);
                    int intsessionDurationMinutes = Convert.ToInt32(dr["sessionDurationMinutes"]);
                    string strsessionDuration = intsessionDurationHours.ToString() + ":" + intsessionDurationMinutes.ToString();

                    String strspecificGoal = dr["specificGoal"].ToString();
                    String strspecificPractice = dr["specificPractice"].ToString();
                    String strgenericGoal = dr["genericGoal"].ToString();
                    String strgenericPractice = dr["genericPractice"].ToString();

                    String strprocessArea = dr["processArea"].ToString();

                    //fill the itemList
                    interview_session newRec = new interview_session();
                    newRec.ID = intID;
                    newRec.sessionId = intsessionId;
                    newRec.sessionIndex = intsessionIndex;
                    newRec.sessionName = strsessionName;
                    newRec.sessionDurationHours = intsessionDurationHours;
                    newRec.sessionDurationMinutes = intsessionDurationMinutes;
                    newRec.sessionDuration = strsessionDuration;

                    newRec.specificGoal = strspecificGoal;
                    newRec.specificPractice = strspecificPractice;
                    newRec.genericGoal = strgenericGoal;
                    newRec.genericPractice = strgenericPractice;
                    newRec.processArea = strprocessArea;

                    itemList.Add(newRec);
                }//for
            }
        }//ReadItemListFromDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ReadItemListFromDatabase_ByProjectID - read all records from "theTable" insert them into this.itemList, filter by projectID
        /// 1) Erase the current itemList in memory first
        /// 2) Read records from SQLServer, filling the itemList
        ///</summary>
        ///<input>List<interview_session> itemList -  an ordinary List<> of type interview_session, will be cleared if not already empty </input>
        ///<output>List<interview_session> itemList- an ordinary List<> of type interview_session, extracted from the database </output>
        ///<param name="sessionId"></param>
        public void ReadItemListFromDatabase_BySessionId(int sessionId)
        {
            itemList.Clear();  //First, empty the existing list contents

            string sQuery = "SELECT * FROM " + theTable +
            " WHERE sessionId=" + "'" + sessionId + "'" ;

            DataSet dsObj = DBUtils.ExecuteSqlQuery(sQuery);

            if (dsObj != null && dsObj.Tables[0].Rows.Count > 0)
            {
                DataTable dtObj = dsObj.Tables[0]; //get the DataTable reference once

                foreach (DataRow dr in dtObj.Rows)
                {
                    //extract data
                    int intID = Convert.ToInt32(dr["ID"]);
                    int intsessionIndex = Convert.ToInt32(dr["sessionIndex"]);
                    String strsessionName = dr["sessionName"].ToString();
                    int intsessionDurationHours = Convert.ToInt32(dr["sessionDurationHours"]);
                    int intsessionDurationMinutes = Convert.ToInt32(dr["sessionDurationMinutes"]);
                    string strsessionDuration = intsessionDurationHours.ToString() + ":" + intsessionDurationMinutes.ToString();
                    String strspecificGoal = dr["specificGoal"].ToString();
                    String strspecificPractice = dr["specificPractice"].ToString();
                    String strgenericGoal = dr["genericGoal"].ToString();
                    String strgenericPractice = dr["genericPractice"].ToString();
                    String strprocessArea = dr["processArea"].ToString();

                    //fill the itemList
                    interview_session newRec = new interview_session();
                    newRec.ID = intID;
                    newRec.sessionIndex = intsessionIndex;
                    newRec.sessionName = strsessionName;
                    newRec.sessionDurationHours = intsessionDurationHours;
                    newRec.sessionDurationMinutes = intsessionDurationMinutes;
                    newRec.sessionDuration = strsessionDuration;
                    newRec.projectId = sessionId;
                    newRec.specificGoal = strspecificGoal;
                    newRec.specificPractice = strspecificPractice;
                    newRec.genericGoal = strgenericGoal;
                    newRec.genericPractice = strgenericPractice;
                    newRec.processArea = strprocessArea;                  

                    itemList.Add(newRec);
                }//for
            }
        }//ReadItemListFromDatabase_ByProjectID


       
        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemListToDatabase- read all records from this.itemList and write to "theTable" 
        /// New records are added using the "INSERT INTO" SQL operation
        ///</summary>
        ///<input> String theTable - the table name</input>
        ///<output>List<interview_session> itemList - an ordinary List<> of type interview_session, output to the "theTable" in the database </output>
        public void WriteItemListToDatabase()
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open");
                return;
            }

            foreach (var r in itemList)
            {
                WriteItemToDatabase(r);
            }//foreach

            myConnection.Close();
        }//WriteItemListToDatabase



        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemToDatabase - read all records from this.itemList and write to "theTable"
        ///</summary>
        ///<output>interview_session r - output one interview_session object to the "theTable" in the database </output>
        public void WriteItemToDatabase(interview_session r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; interview_session_Table.cs:WriteItemToDatabase.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "INSERT INTO  " + theTable +
                " (sessionId, sessionIndex, sessionName, sessionDurationHours,sessionDurationMinutes," +
                "specificGoal, specificPractice, genericGoal, genericPractice, processArea) " +
                "VALUES ( @sessionId, @sessionIndex, @sessionName, @sessionDurationHours, @sessionDurationMinutes, " +
                "@specificGoal, @specificPractice, @genericGoal, @genericPractice, @processArea);";

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            // myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot asign to a field having IDENTITY semantics
            myCommand.Parameters.AddWithValue("@sessionId", r.sessionId);
            myCommand.Parameters.AddWithValue("@sessionIndex", r.sessionIndex);
            myCommand.Parameters.AddWithValue("@sessionName", r.sessionName);
            myCommand.Parameters.AddWithValue("@sessionDurationHours", r.sessionDurationHours);
            myCommand.Parameters.AddWithValue("@sessionDurationMinutes", r.sessionDurationMinutes);
            myCommand.Parameters.AddWithValue("@specificGoal", r.specificGoal);
            myCommand.Parameters.AddWithValue("@specificPractice", r.specificPractice);
            myCommand.Parameters.AddWithValue("@genericGoal", r.genericGoal);
            myCommand.Parameters.AddWithValue("@genericPractice", r.genericPractice);
            myCommand.Parameters.AddWithValue("@processArea", r.processArea);
            
            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }//WriteItemToDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// UpdateItemListToDatabase - read all records from this.itemList and update matching recordsin "theTable"
        ///</summary>
        ///<input>List<interview_session> itemList - an ordinary List<T> of type interview_session, updated within the "theTable" in the database </input>
        public void UpdateItemListToDatabase()
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; interview_session_Table:UpdateItemListToDatabase.GetNewSqlConnection()");
                return;
            }

            foreach (var r in itemList)
            {
                UpdateItemToDatabase(r);
            }//foreach

            myConnection.Close();
        }//UpdateItemListToDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// UpdateItemToDatabase - update one record within "theTable" in the database
        ///</summary>
        ///<input>interview_session r -  one item to be updated within the "theTable" in the database </input>
        ///<input> r.currentProject - the projectId to match with one database record</input>
        public void UpdateItemToDatabase(interview_session r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; interview_session_Table.cs:UpdateItemToDatabase.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "UPDATE  " + theTable +
                " SET " +
                "sessionIndex=@sessionIndex,"+
                " sessionName=@sessionName," +
                " sessionDurationHours=@sessionDurationHours," +
                " sessionDurationMinutes=@sessionDurationMinutes," +
                " specificGoal=@specificGoal," +
                " specificPractice=@specificPractice," +
                " genericGoal=@genericGoal," +
                " genericPractice=@genericPractice," +
                " processArea=@processArea" +
                " WHERE " +
                " sessionId=@sessionId";  // <<<---- match on the Primary Key

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            //WARNING: myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot assign/modify a field having IDENTITY semantics
            //myCommand.Parameters.AddWithValue("@interview_sessionId", r.interview_sessionId);  <<---this was the record selection parameter
            myCommand.Parameters.AddWithValue("@sessionIndex", r.sessionIndex);
            myCommand.Parameters.AddWithValue("@sessionName", r.sessionName);
            myCommand.Parameters.AddWithValue("@sessionDurationHours", r.sessionDurationHours);
            myCommand.Parameters.AddWithValue("@sessionDurationMinutes", r.sessionDurationMinutes);
            myCommand.Parameters.AddWithValue("@specificGoal", r.specificGoal);
            myCommand.Parameters.AddWithValue("@specificPractice", r.specificPractice);
            myCommand.Parameters.AddWithValue("@genericGoal", r.genericGoal);
            myCommand.Parameters.AddWithValue("@genericPractice", r.genericPractice);
            myCommand.Parameters.AddWithValue("@processArea", r.processArea);
            myCommand.Parameters.AddWithValue("@sessionId", r.sessionId);

            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }//UpdateItemToDatabase

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clear_Database_Table - delete all records from the table "interview_session"  database
        /// <input> theTable - the table name</input>
        /// </summary>
        public void Clear_Database_Table()
        {
            if (CountRows() == 0)
                return;

            string strCommand = "DELETE FROM " + theTable;
            if (!DBUtils.ExecuteSqlNonQuery(strCommand))
                LogManager.writeToLog("ExecuteSqlNonQuery returned: FALSE; in interview_session_Table.cs:Clear_Database_Table.ExecuteSqlNonQuery()");
        }

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clear_Database_Table_By_sessionId- delete interview_session table records by sessionId
        /// </summary>
        /// <param name="sessionId"></param>
        public void Clear_Database_Table_By_sessionId(int sessionId)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                Console.WriteLine("New connection failed to open; interview_session_Table.cs:SQLServer_Clear_Database_Table_By_sessionId.GetNewSqlConnection()");
                return;
            }

            string strQuery = "DELETE FROM " + theTable +
               " WHERE " +
               " sessionId=@sessionId";
            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            myCommand.Parameters.AddWithValue("@sessionId", sessionId);

            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// CountRows - count how many rows in the table
        /// </summary>
        /// <returns>int </returns>
        public int CountRows()
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; interview_session_Table.cs: SQLServer_CountRows.GetNewSqlConnection");
                return -1;
            }
            string strQuery = "SELECT COUNT(*)  FROM " + theTable;
            return DBUtils.ExecuteSqlQueryScalar(strQuery, myConnection);
        }

#if TESTDBI
        //----------------------------------------------------------------------------------
        /// <summary>
        /// Show - if TESTDBI is defined in the build, enable the Show Table feature for Console output
        /// </summary>
        public void Show()
        {
            Console.WriteLine("Table (" + theTable + ") contents");
            foreach (var r in itemList)
            {
                r.Show();
            }
        }//Show
#endif
    }
}
