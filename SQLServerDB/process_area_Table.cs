using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

#if __RCC_COMMENT__
using System.Windows.Forms;
#endif

namespace SQLServerDB
{
    public class process_area_Table
    {
        public const string theTable = "T_process_area";

        public List<process_area> itemList = new List<process_area>();

        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ReadItemListFromDatabase - read all records from "theTable" insert them into this.itemList
        ///</summary>
        ///<input>List<process_area>  itemList - an ordinary List<> of type pa_tree_node, will be cleared if not already empty </input>
        ///<output>List<process_area> itemList - an ordinary List<> of type pa_tree_node, extracted from the database </output>
        public void ReadItemListFromDatabase()
        {
            itemList.Clear();  // ensure itemList is empty

            string sQuery = "SELECT * FROM " + theTable;
            DataSet dsObj = DBUtils.ExecuteSqlQuery(sQuery);

            if (dsObj != null && dsObj.Tables[0].Rows.Count > 0)
            {
                DataTable dtObj = dsObj.Tables[0]; //get the DataTable reference once

                foreach (DataRow dr in dtObj.Rows)
                {
                    //extract all fields of the current row
                    int intID = Convert.ToInt32(dr["Id"]);
                    int processAreaId = Convert.ToInt32(dr["processAreaId"]);
                    int projectId = Convert.ToInt32(dr["projectId"]);
                    string name = dr["paName"].ToString();
                    string text = dr["text"].ToString();
                    bool boolIsActive = Convert.ToBoolean(dr["active"]);
                    bool hasArtifacts = Convert.ToBoolean(dr["hasArtifact"]);
                    bool hasAffirmations = Convert.ToBoolean(dr["hasAffirmation"]);
                    string strRating = (dr["rating"]).ToString();
                    bool boolCoverage = Convert.ToBoolean(dr["coverage"]);

                    //fill the itemList
                    process_area newRec = new process_area();
                    newRec.ID = intID;
                    newRec.processAreaId = processAreaId;
                    newRec.projectId = projectId;
                    newRec.paName = name;
                    newRec.text = text;
                    newRec.active = boolIsActive;
                    newRec.canContainArtifact = hasArtifacts;
                    newRec.canContainAffirmation = hasAffirmations;
                    newRec.rating = strRating;
                    newRec.coverage = boolCoverage;

                    itemList.Add(newRec);
                }//foreach
            }
        }//ReadItemListFromDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ReadItemListFromDatabase_ByProjectID - read all records from "theTable" insert them into this.itemList, filter by projectID
        /// 1) Erase the current itemList in memory first
        /// 2) Read records from SQLServer, filling the itemList
        ///</summary>
        ///<input>List<process_area>  itemList - an ordinary List<> of type process_area, will be cleared if not already empty </input>
        ///<output>List<process_area> itemList - an ordinary List<> of type process_area, extracted from the database </output>
        ///<param name="projectId"></param>
        public void ReadItemListFromDatabase_ByProjectID(int projectId)
        {
            itemList.Clear();  //First, empty the existing list contents

            string sQuery = "SELECT * FROM " + theTable +
                " WHERE projectId = " + "'" + projectId + "'";

            DataSet dsObj = DBUtils.ExecuteSqlQuery(sQuery);

            if (dsObj != null && dsObj.Tables[0].Rows.Count > 0)
            {
                DataTable dtObj = dsObj.Tables[0]; //get the DataTable reference once

                foreach (DataRow dr in dtObj.Rows)
                {
                    //extract all fields of the current row
                    int intID = Convert.ToInt32(dr["Id"]);
                    int intProcessAreaId = Convert.ToInt32(dr["processAreaId"]);
                    int intProjectId = Convert.ToInt32(dr["projectId"]);
                    string strname = dr["paName"].ToString();
                    string strtext = dr["text"].ToString();
                    bool boolIsActive = Convert.ToBoolean(dr["active"]);
                    bool boolHasArtifacts = Convert.ToBoolean(dr["hasArtifact"]);
                    bool boolHasAffirmations = Convert.ToBoolean(dr["hasAffirmation"]);
                    string strRating = (dr["rating"]).ToString();
                    bool boolCoverage = Convert.ToBoolean(dr["coverage"]);

                    //fill the itemList
                    process_area newRec = new process_area();
                    newRec.ID = intID;
                    newRec.processAreaId = intProcessAreaId;
                    newRec.projectId = intProjectId;
                    newRec.paName = strname;
                    newRec.text = strtext;
                    newRec.active = boolIsActive;
                    newRec.canContainArtifact = boolHasArtifacts;
                    newRec.canContainAffirmation = boolHasAffirmations;
                    newRec.rating = strRating;
                    newRec.coverage = boolCoverage;

                    itemList.Add(newRec);
                }//for
            }
        }//ReadItemListFromDatabase_ByProjectID

        public void ReadItemListFromDatabase_ByProjectIDAndName(int projectId, string paName)
        {
            itemList.Clear();  //First, empty the existing list contents

            string sQuery = "SELECT * FROM " + theTable +
                " WHERE projectId = " + projectId.ToString() +
                " AND paName = " + "'"+paName+"'";

            DataSet dsObj = DBUtils.ExecuteSqlQuery(sQuery);

            if (dsObj == null)
                  LogManager.writeToLog("The dataset is null in process_area_Table.ReadItemListFromDatabase_ByProjectIDAndName()");

            if (dsObj != null && dsObj.Tables[0].Rows.Count > 0)
            {
                DataTable dtObj = dsObj.Tables[0]; //get the DataTable reference once

                foreach (DataRow dr in dtObj.Rows)
                {
                    //extract all fields of the current row
                    int intID = Convert.ToInt32(dr["Id"]);
                    int intProcessAreaId = Convert.ToInt32(dr["processAreaId"]);
                    int intProjectId = Convert.ToInt32(dr["projectId"]);
                    string strname = dr["paName"].ToString();
                    string strtext = dr["text"].ToString();
                    bool boolIsActive = Convert.ToBoolean(dr["active"]);
                    bool boolHasArtifacts = Convert.ToBoolean(dr["hasArtifact"]);
                    bool boolHasAffirmations = Convert.ToBoolean(dr["hasAffirmation"]);
                    string strRating = (dr["rating"]).ToString();
                    bool boolCoverage = Convert.ToBoolean(dr["coverage"]);

                    //fill the itemList
                    process_area newRec = new process_area();
                    newRec.ID = intID;
                    newRec.processAreaId = intProcessAreaId;
                    newRec.projectId = intProjectId;
                    newRec.paName = strname;
                    newRec.text = strtext;
                    newRec.active = boolIsActive;
                    newRec.canContainArtifact = boolHasArtifacts;
                    newRec.canContainAffirmation = boolHasAffirmations;
                    newRec.rating = strRating;
                    newRec.coverage = boolCoverage;

                    itemList.Add(newRec);
                }//for
            }
        }
        
        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemListToDatabase - read all records from this.itemList and write to "theTable" 
        /// New records are added using the "INSERT INTO" SQL operation
        ///</summary>
        ///<input> String theTable - the table name</input>
        ///<output>List<process_area>  itemList - an ordinary List<> of type process_area, output to the "theTable" in the database </output>
        public void WriteItemListToDatabase()
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("new connection failed to open");
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
        ///<output>process_area r - output one process_area object to the "theTable" in the database </output>
        public void WriteItemToDatabase(process_area r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("new connection failed to open@WriteItemToDatabase.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "INSERT INTO  " + theTable +
                " (processAreaId, projectId, paName, text, active, hasArtifact, hasAffirmation, rating, coverage) " +
                "VALUES ( @processAreaId, @projectId, @paName, @text, @active, @hasArtifact, @hasAffirmation, @rating, @coverage);";

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            // myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot asign to a field having IDENTITY semantics
            myCommand.Parameters.AddWithValue("@processAreaId", r.processAreaId);
            myCommand.Parameters.AddWithValue("@projectId", r.projectId);
            myCommand.Parameters.AddWithValue("@paName", r.paName);
            myCommand.Parameters.AddWithValue("@text", r.text);
            myCommand.Parameters.AddWithValue("@active", r.active);
            myCommand.Parameters.AddWithValue("@hasArtifact", r.canContainArtifact);
            myCommand.Parameters.AddWithValue("@hasAffirmation", r.canContainAffirmation);
            myCommand.Parameters.AddWithValue("@rating", r.rating);
            myCommand.Parameters.AddWithValue("@coverage", r.coverage);

            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }//WriteItemToDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// UpdateItemListToDatabase - read all records from this.itemList and update matching recordsin "theTable"
        ///</summary>
        ///<input>List<process_area> itemList - an ordinary List<T> of type process_area, updated within the "theTable" in the database </input>
        public void UpdateItemListToDatabase()
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("new connection failed to open, process_area_Table:UpdateItemListToDatabase");
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
        /// UpdateItemToDatabase - update one record within "theTable" in the database;
        /// match a record based on the projectId
        ///</summary>
        ///<input>process_area r -  one item to be updated within the "theTable" in the database </input>
        ///<input> r.currentProject - the projectId to match with one database record</input>
        public void UpdateItemToDatabase(process_area r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("new connection failed to open; in process_area_Table.cs: UpdateItemToDatabase.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "UPDATE  " + theTable +
                " SET " +
                " paName=@paName," +
                " text=@text," +
                " active=@active," +
                " hasArtifact=@hasArtifact," +
                " hasAffirmation=@hasAffirmation," +
                " rating=@rating," +
                " coverage=@coverage" +
                " WHERE " +
                " processAreaId=@processAreaId" +
                " AND projectId=@projectId";

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            //WARNING: myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot assign/modify a field having IDENTITY semantics
            myCommand.Parameters.AddWithValue("@paName", r.paName);
            myCommand.Parameters.AddWithValue("@text", r.text);
            myCommand.Parameters.AddWithValue("@active", r.active);
            myCommand.Parameters.AddWithValue("@hasArtifact", r.canContainArtifact);
            myCommand.Parameters.AddWithValue("@hasAffirmation", r.canContainAffirmation);
            myCommand.Parameters.AddWithValue("@rating", r.rating);
            myCommand.Parameters.AddWithValue("@coverage", r.coverage);
            myCommand.Parameters.AddWithValue("@processAreaId", r.processAreaId);
            myCommand.Parameters.AddWithValue("@projectId", r.projectId);

            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }//UpdateItemToDatabase

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clear_Database_Table - delete all records from the table "process_area" in the database
        /// <input> theTable - the table name</input>
        /// </summary>
        public void Clear_Database_Table()
        {
            string strCommand = "DELETE from " + theTable;
            if (!DBUtils.ExecuteSqlNonQuery(strCommand))
                LogManager.writeToLog(" ExecuteSqlNonQuery returned: FALSE; in process_area_Table.cs:Clear_Database_Table. ExecuteSqlNonQuery");
        }


        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clear_Database_Table_By_processArea- delete affirmation table records by processArea
        /// </summary>
        /// <param name="paName"></param>
        public void Clear_Database_Table_By_processArea(String paName)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                Console.WriteLine("new connection failed to open, process_area_Table.cs: SQLServer_Clear_Database_Table_By_appraisalName.GetNewSqlConnection");
                return;
            }

            string strQuery = "DELETE  FROM  " + theTable +
               " WHERE " +
               " paName=@paName";
            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            myCommand.Parameters.AddWithValue("@paName", paName);

            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }//Clear_Database_Table_By_processArea


        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// SQLServer_CountRows - count how many rows in the table
        /// </summary>
        /// <returns></returns>
        public int CountRows()
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("new connection failed to open, process_area_Table.cs: SQLServer_CountRows.GetNewSqlConnection");
                return -1;
            }
            string strQuery = "SELECT COUNT(*)  FROM " + theTable;
            return DBUtils.ExecuteSqlQueryScalar(strQuery, myConnection);
        }//SQLServer_CountRows

        //----------------------------------------------------------------------------------
        public void Show()
        {
            Console.WriteLine("Table (" + theTable + ") contents");
            foreach (var r in itemList)
            {
                r.Show();
            }
        }
    }
}
