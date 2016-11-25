using System;
using System.Collections.Generic;

using System.Data; //DataSet
using System.Data.SqlClient; //SQLConnection, SQLCommand

namespace SQLServerDB
{
     public partial class improvement_opportunity_Table
    {
        public const string theTable = "T_improvement_opportunity";

        public List<improvement_opportunity> itemList = new List<improvement_opportunity>();


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ReadItemListFromDatabase - read all records from "theTable" insert them into this.itemList
        ///</summary>
        ///<input>List<improvement_opportunity> itemList - an ordinary List<> of type improvement_opportunity, will be cleared if not already empty </input>
        ///<output>List<improvement_opportunity>  itemList - an ordinary List<> of type improvement_opportunity, extracted from the database </output>
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
                    String strnotes = dr["notes"].ToString();
                    String strspecificGoal = dr["specificGoal"].ToString();
                    String strspecificPractice = dr["specificPractice"].ToString();
                    String strgenericGoal = dr["genericGoal"].ToString();
                    String strgenericPractice = dr["genericPractice"].ToString();
                     int intprojectId = Convert.ToInt32(dr["projectId"]);
                    String strprocessArea = dr["processArea"].ToString();


                    //fill the itemList
                    improvement_opportunity newRec = new improvement_opportunity();
                    newRec.ID = intID;
                    newRec.notes = strnotes;
                    newRec.specificGoal = strspecificGoal;
                    newRec.specificPractice = strspecificPractice;
                    newRec.genericGoal = strgenericGoal;
                    newRec.genericPractice = strgenericPractice;
                    newRec.projectId = intprojectId;
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
        ///<input>List<improvement_opportunity> itemList -  an ordinary List<> of type improvement_opportunity, will be cleared if not already empty </input>
        ///<output>List<improvement_opportunity> itemList- an ordinary List<> of type improvement_opportunity, extracted from the database </output>
        ///<param name="projectId"></param>
        public void ReadItemListFromDatabase_ByProjectID(int projectId)
        {
            itemList.Clear();  //First, empty the existing list contents

            string sQuery = "SELECT * FROM " + theTable +
            " WHERE projectId=" + projectId.ToString();


            DataSet dsObj = DBUtils.ExecuteSqlQuery(sQuery);

            if (dsObj != null && dsObj.Tables[0].Rows.Count > 0)
            {
                DataTable dtObj = dsObj.Tables[0]; //get the DataTable reference once

                foreach (DataRow dr in dtObj.Rows)
                {
                    //extract data
                    int intID = Convert.ToInt32(dr["ID"]);
                    String strnotes = dr["notes"].ToString();
                    String strprocessArea = dr["processArea"].ToString();
                    String strspecificGoal = dr["specificGoal"].ToString();
                    String strspecificPractice = dr["specificPractice"].ToString();
                    String strgenericGoal = dr["genericGoal"].ToString();
                    String strgenericPractice = dr["genericPractice"].ToString();

                    //fill the itemList
                    improvement_opportunity newRec = new improvement_opportunity();
                    newRec.ID = intID;
                    newRec.notes = strnotes;
                    newRec.processArea = strprocessArea;
                    newRec.specificGoal = strspecificGoal;
                    newRec.specificPractice = strspecificPractice;
                    newRec.genericGoal = strgenericGoal;
                    newRec.genericPractice = strgenericPractice;
                    newRec.projectId = projectId;

                    itemList.Add(newRec);
                }//for
            }
        }//ReadItemListFromDatabase_ByProjectID


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ReadItemListFromDatabase_ByProjectID - read all records from "theTable" insert them into this.itemList, filter by projectID
        /// 1) Erase the current itemList in memory first
        /// 2) Read records from SQLServer, filling the itemList
        ///</summary>
        ///<input>List<improvement_opportunity> itemList -  an ordinary List<> of type improvement_opportunity, will be cleared if not already empty </input>
        ///<output>List<improvement_opportunity> itemList- an ordinary List<> of type improvement_opportunity, extracted from the database </output>
        ///<param name="projectId"></param>
        public void ReadItemListFromDatabase_ByProcessArea(String v_processArea)
        {
            itemList.Clear();  //First, empty the existing list contents

            string sQuery = "SELECT * FROM " + theTable +
            " WHERE " + 
             " processArea  LIKE" + "@processArea";


            DataSet dsObj = DBUtils.ExecuteSqlQuery(sQuery);

            if (dsObj != null && dsObj.Tables[0].Rows.Count > 0)
            {
                DataTable dtObj = dsObj.Tables[0]; //get the DataTable reference once

                foreach (DataRow dr in dtObj.Rows)
                {
                    //extract data
                    int intID = Convert.ToInt32(dr["ID"]);
                    String strnotes = dr["notes"].ToString();
                    String strspecificGoal = dr["specificGoal"].ToString();
                    String strspecificPractice = dr["specificPractice"].ToString();
                    String strgenericGoal = dr["genericGoal"].ToString();
                    String strgenericPractice = dr["genericPractice"].ToString();
                    int int_projectId = Convert.ToInt32(dr["projectId"]);
                    String strprocessArea = dr["processArea"].ToString();


                    //fill the itemList
                    improvement_opportunity newRec = new improvement_opportunity();
                    newRec.ID = intID;
                    newRec.notes = strnotes;
                    newRec.specificGoal = strspecificGoal;
                    newRec.specificPractice = strspecificPractice;
                    newRec.genericGoal = strgenericGoal;
                    newRec.genericPractice = strgenericPractice;
                    newRec.projectId = int_projectId;
                    newRec.processArea = strprocessArea;

                    itemList.Add(newRec);
                }//for
            }
        }//ReadItemListFromDatabase_ByProcessArea



        

        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemListToDatabase - read all records from this.itemList and write to "theTable" 
        /// 1) provide better security;
        /// 2) provide code that is easier to write/read/maintain.
        /// New records are added using the "INSERT INTO" SQL operation
        ///</summary>
        ///<input> String theTable - the table name</input>
        ///<output>List<improvement_opportunity> itemList - an ordinary List<> of type improvement_opportunity, output to the "theTable" in the database </output>
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
                WriteItemToDatabased(r);
            }//foreach

            myConnection.Close();
        }//WriteItemListToDatabase



        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemToDatabase - read all records from this.itemList and write to "theTable"
        ///</summary>
        ///<output>improvement_opportunity r - output one improvement_opportunity object to the "theTable" in the database </output>
        public void WriteItemToDatabased(improvement_opportunity r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                Console.WriteLine("New connection failed to open; WriteItemToDatabased.GetNewSqlConnection()");
                LogManager.writeToLog("New connection failed to open; WriteItemToDatabased.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "INSERT INTO  " + theTable +
                 " (notes,  specificGoal, specificPractice, genericGoal, genericPractice, projectId, processArea) " +
                " VALUES "  +
                " ( @notes,  @specificGoal, @specificPractice, @genericGoal, @genericPractice, @projectId, @processArea);";

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            //DEPRECATED: myCommand.Parameters.Add(...),  INSTEAD USE myCommand.Parameters.AddWithValue(...)
            // myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot asign to a field having IDENTITY semantics
            myCommand.Parameters.AddWithValue("@notes", r.notes);
            myCommand.Parameters.AddWithValue("@specificGoal", r.specificGoal);
            myCommand.Parameters.AddWithValue("@specificPractice", r.specificPractice);
            myCommand.Parameters.AddWithValue("@genericGoal", r.genericGoal);
            myCommand.Parameters.AddWithValue("@genericPractice", r.genericPractice);
            myCommand.Parameters.AddWithValue("@projectId", r.projectId);
            myCommand.Parameters.AddWithValue("@processArea", r.processArea);
            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }//WriteItemToDatabased


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// UpdateItemListToDatabase - read all records from this.itemList and update matching recordsin "theTable"
        ///</summary>
        ///<input>List<improvement_opportunity> itemList - an ordinary List<T> of type improvement_opportunity, updated within the "theTable" in the database </input>
        public void UpdateItemListToDatabase()
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; improvement_opportunity_Table:UpdateItemListToDatabase.GetNewSqlConnection()");
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
        ///<input>improvement_opportunity r -  one item to be updated within the "theTable" in the database </input>
        ///<input> r.currentProject - the projectId to match with one database record</input>
        public void UpdateItemToDatabase(improvement_opportunity r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; improvement_opportunity_Table.cs: UpdateItemToDatabase.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "UPDATE  " + theTable +
                " SET " +
                " notes=@notes," +
                " processArea=@processArea," +
                " specificGoal=@specificGoal," +
                " specificPractice=@specificPractice," +
                " genericGoal=@genericGoal," +
                " genericPractice=@genericPractice," +
                " WHERE " +
                " projectId=@projectId";  

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);


            //WARNING: myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot assign/modify a field having IDENTITY semantics
            myCommand.Parameters.AddWithValue("@notes", r.notes);
            myCommand.Parameters.AddWithValue("@specificGoal", r.specificGoal);
            myCommand.Parameters.AddWithValue("@specificPractice", r.specificPractice);
            myCommand.Parameters.AddWithValue("@genericGoal", r.genericGoal);
            myCommand.Parameters.AddWithValue("@genericPractice", r.genericPractice);
            myCommand.Parameters.AddWithValue("@projectId", r.projectId);
            myCommand.Parameters.AddWithValue("@processArea", r.processArea);
            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }//UpdateItemToDatabase


        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clear_Database_Table - delete all records from the table "improvement_opportunity"  database
        /// <input> theTable - the table name</input>
        /// </summary>
        public void Clear_Database_Table()
        {
            if (CountRows() == 0)
                return;

            string strCommand = "DELETE FROM " + theTable;
            if (!DBUtils.ExecuteSqlNonQuery(strCommand))
                LogManager.writeToLog(" ExecuteSqlNonQuery returned: FALSE; in improvement_opportunity_Table.cs:Clear_Database_Table. ExecuteSqlNonQuery");
        }



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
                LogManager.writeToLog("New connection failed to open; affirmation_Table.cs:SQLServer_CountRows.GetNewSqlConnection");
                return -1;
            }
            string strQuery = "SELECT COUNT(*)  FROM " + theTable;
            return DBUtils.ExecuteSqlQueryScalar(strQuery, myConnection);
        }//CountRows

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
