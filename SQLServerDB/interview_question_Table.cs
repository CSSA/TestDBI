using System;
using System.Collections.Generic;

using System.Data; //DataSet
using System.Data.SqlClient; //SQLConnection, SQLCommand

namespace SQLServerDB
{
    public partial class interview_question_Table
    {
        public const string theTable = "T_interview_question";

        public List<interview_question> itemList = new List<interview_question>();


        ///<summary>
        /// ReadItemListFromDatabase - read all records from "theTable" insert them into this.itemList
        ///</summary>
        ///<input>List<interview_question> itemList - an ordinary List<> of type interview_question, will be cleared if not already empty </input>
        ///<output>List<interview_question>  itemList - an ordinary List<> of type interview_question, extracted from the database </output>
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
                    String strinterviewQuestions = dr["interviewQuestion"].ToString();
                    String strquestionNotes = dr["questionNotes"].ToString();
                    int intsessionId = Convert.ToInt32(dr["sessionId"]);


                    //fill the itemList
                    interview_question newRec = new interview_question();
                    newRec.ID = intID;
                    newRec.interviewQuestions = strinterviewQuestions;
                    newRec.questionNotes = strquestionNotes;
                    newRec.sessionId = intsessionId;
                
                    itemList.Add(newRec);
                }//for
            }
        }//ReadItemListFromDatabase


        ///<summary>
        /// ReadItemListFromDatabase_BySessionId - read all records from "theTable" insert them into this.itemList, filter by projectID
        /// 1) Erase the current itemList in memory first
        /// 2) Read records from SQLServer, filling the itemList
        ///</summary>
        ///<input>List<interview_question> itemList -  an ordinary List<> of type interview_question, will be cleared if not already empty </input>
        ///<output>List<interview_question> itemList- an ordinary List<> of type interview_question, extracted from the database </output>
        ///<param name="projectId"></param>
        public void ReadItemListFromDatabase_BySessionId(int sessionId)
        {
            itemList.Clear();  //First, empty the existing list contents

            string sQuery = "SELECT * FROM " + theTable +
            " WHERE sessionId=" + sessionId.ToString();

            DataSet dsObj = DBUtils.ExecuteSqlQuery(sQuery);

            if (dsObj != null && dsObj.Tables[0].Rows.Count > 0)
            {
                DataTable dtObj = dsObj.Tables[0]; //get the DataTable reference once

                foreach (DataRow dr in dtObj.Rows)
                {
                    //extract data
                    int intID = Convert.ToInt32(dr["ID"]);
                    String strinterviewQuestions = dr["interviewQuestion"].ToString();
                    String strquestionNotes = dr["questionNotes"].ToString();
                    int intsessionId = Convert.ToInt32(dr["sessionId"]);


                    //fill the itemList
                    interview_question newRec = new interview_question();
                    newRec.ID = intID;
                    newRec.interviewQuestions = strinterviewQuestions;
                    newRec.questionNotes = strquestionNotes;
                    newRec.sessionId = sessionId;

                    itemList.Add(newRec);
                }//for
            }
        }//ReadItemListFromDatabase_BySessionId



        ///<summary>
        /// WriteItemListToDatabase - read all records from this.itemList and write to "theTable" 
        /// 1) provide better security;
        /// 2) provide code that is easier to write/read/maintain.
        /// New records are added using the "INSERT INTO" SQL operation
        ///</summary>
        ///<input> String theTable - the table name</input>
        ///<output>List<interview_question> itemList - an ordinary List<> of type interview_question, output to the "theTable" in the database </output>
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



        ///<summary>
        /// WriteItemToDatabase - read all records from this.itemList and write to "theTable"
        ///</summary>
        ///<output>interview_question r - output one interview_question object to the "theTable" in the database </output>
        public void WriteItemToDatabase(interview_question r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; interview_question_Table.cs:WriteItemToDatabase.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "INSERT INTO  " + theTable +
                " (interviewQuestions, questionNotes, sessionId)" +
                "VALUES ( @interviewQuestions, @questionNotes, @sessionId);";

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            // myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot asign to a field having IDENTITY semantics
            myCommand.Parameters.AddWithValue("@interviewQuestions", r.interviewQuestions);
            myCommand.Parameters.AddWithValue("@questionNotes", r.questionNotes);
            myCommand.Parameters.AddWithValue("@sessionId", r.sessionId);

            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }//WriteItemToDatabase


        ///<summary>
        /// UpdateItemListToDatabase - read all records from this.itemList and update matching recordsin "theTable"
        ///</summary>
        ///<input>List<interview_question> itemList - an ordinary List<T> of type interview_question, updated within the "theTable" in the database </input>
        public void UpdateItemListToDatabase()
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; interview_question_Table:UpdateItemListToDatabase.GetNewSqlConnection()");
                return;
            }

            foreach (var r in itemList)
            {
                UpdateItemToDatabase(r);
            }//foreach

            myConnection.Close();
        }//UpdateItemListToDatabase


        ///<summary>
        /// UpdateItemToDatabase - update one record within "theTable" in the database
        ///</summary>
        ///<input>interview_question r -  one item to be updated within the "theTable" in the database </input>
        ///<input> r.currentProject - the projectId to match with one database record</input>
        public void UpdateItemToDatabase(interview_question r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; in interview_question_Table.cs:UpdateItemToDatabase.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "UPDATE  " + theTable +
                " SET " +
                " interviewQuestions=@interviewQuestions," +
                " questionNotes=@questionNotes," +
                " WHERE " +
                " sessionId=@sessionId";  // <<<---- match on the sessionId

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            //WARNING: myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot assign/modify a field having IDENTITY semantics
            //myCommand.Parameters.AddWithValue("@interview_questionId", r.interview_questionId);  <<---this was the record selection parameter
            myCommand.Parameters.AddWithValue("@interviewQuestions", r.interviewQuestions);
            myCommand.Parameters.AddWithValue("@questionNotes", r.questionNotes);
            myCommand.Parameters.AddWithValue("@sessionId", r.sessionId);

            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }//UpdateItemToDatabase


        /// <summary>
        /// Clear_Database_Table - delete all records from the table "interview_question"  database
        /// <input> theTable - the table name</input>
        /// </summary>
        public void Clear_Database_Table()
        {
            string strCommand = "DELETE FROM " + theTable;
            if (!DBUtils.ExecuteSqlNonQuery(strCommand))
                LogManager.writeToLog("ExecuteSqlNonQuery returned: FALSE; in interview_question_Table.cs:Clear_Database_Table. ExecuteSqlNonQuery()");
        }

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clear_Database_Table_By_sessionId- delete interview_question table records by sessionId
        /// </summary>
        /// <param name="sessionId"></param>
        public void Clear_Database_Table_By_sessionId(int sessionId)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                Console.WriteLine("New connection failed to open; interview_question_Table.cs:SQLServer_Clear_Database_Table_By_sessionId.GetNewSqlConnection()");
                return;
            }

            string strQuery = "DELETE FROM  " + theTable +
               " WHERE " +
               " sessionId=@sessionId";
            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            myCommand.Parameters.AddWithValue("@sessionId", sessionId);

            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }// Clear_Database_Table_By_sessionId

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// CountRows - count how many rows in the table
        /// </summary>
        /// <returns></returns>
        public int CountRows()
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; interview_question_Table.cs: CountRows.GetNewSqlConnection()");
                return -1;
            }
            string strQuery = "SELECT COUNT(*)  FROM " + theTable;
            return DBUtils.ExecuteSqlQueryScalar(strQuery, myConnection);
        }//CountRows

    
    }
}
