using System;
using System.Collections.Generic;

using System.Data; //DataSet
using System.Data.SqlClient; //SQLConnection, SQLCommand

namespace SQLServerDB
{
    public class weakness_Table
    {
        public const string theTable = "T_weakness";

        public List<weakness> itemList = new List<weakness>();


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ReadItemListFromDatabase - read all records from "theTable" insert them into this.itemList
        ///</summary>
        ///<input>List<weakness> itemList - an ordinary List<> of type weakness, will be cleared if not already empty </input>
        ///<output>List<weakness>  itemList - an ordinary List<> of type weakness, extracted from the database </output>
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
                    String strprocessArea = dr["processArea"].ToString();
                    String strspecificGoal = dr["specificGoal"].ToString();
                    String strspecificPractice = dr["specificPractice"].ToString();
                    String strgenericGoal = dr["genericGoal"].ToString();
                    String strgenericPractice = dr["genericPractice"].ToString();
                    int intprojectId = Convert.ToInt32(dr["projectId"]);

                    //fill the itemList
                    weakness newRec = new weakness();
                    newRec.ID = intID;
                    newRec.notes = strnotes;
                    newRec.processArea = strprocessArea;
                    newRec.specificGoal = strspecificGoal;
                    newRec.specificPractice = strspecificPractice;
                    newRec.genericGoal = strgenericGoal;
                    newRec.genericPractice = strgenericPractice;
                    newRec.projectId = intprojectId;

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
        ///<input>List<weakness> itemList -  an ordinary List<> of type weakness, will be cleared if not already empty </input>
        ///<output>List<weakness> itemList- an ordinary List<> of type weakness, extracted from the database </output>
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
                    weakness newRec = new weakness();
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
        /// WriteItemListToDatabase- read all records from this.itemList and write to "theTable" 
        /// New records are added using the "INSERT INTO" SQL operation
        ///</summary>
        ///<input> String theTable - the table name</input>
        ///<output>List<weakness> itemList - an ordinary List<> of type weakness, output to the "theTable" in the database </output>
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
        ///<output>weakness r - output one weakness object to the "theTable" in the database </output>
        public void WriteItemToDatabase(weakness r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                Console.WriteLine("New connection failed to open; WriteItemToDatabase.GetNewSqlConnection()");
                LogManager.writeToLog("New connection failed to open; WriteItemToDatabase.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "INSERT INTO  " + theTable +
                           " (notes, processArea, specificGoal, specificPractice, genericGoal, genericPractice, projectId ) " +
                "VALUES ( @notes, @processArea, @specificGoal, @specificPractice, @genericGoal, @genericPractice, @projectId);";

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            //DEPRECATED: myCommand.Parameters.Add(...),  INSTEAD USE myCommand.Parameters.AddWithValue(...)
            // myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot asign to a field having IDENTITY semantics
            myCommand.Parameters.AddWithValue("@notes", r.notes);
            myCommand.Parameters.AddWithValue("@processArea", r.processArea);
            myCommand.Parameters.AddWithValue("@specificGoal", r.specificGoal);
            myCommand.Parameters.AddWithValue("@specificPractice", r.specificPractice);
            myCommand.Parameters.AddWithValue("@genericGoal", r.genericGoal);
            myCommand.Parameters.AddWithValue("@genericPractice", r.genericPractice);
            myCommand.Parameters.AddWithValue("@projectId", r.projectId);
            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }//WriteItemToDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// UpdateItemListToDatabase- read all records from this.itemList and update matching recordsin "theTable"
        ///</summary>
        ///<input>List<weakness> itemList - an ordinary List<T> of type weakness, updated within the "theTable" in the database </input>
        public void UpdateItemListToDatabase()
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; weakness_Table:UpdateItemListToDatabase.GetNewSqlConnection()");
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
        ///<input>weakness r -  one item to be updated within the "theTable" in the database </input>
        ///<input> r.currentProject - the projectId to match with one database record</input>
        public void UpdateItemToDatabase(weakness r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; in weakness_Table.cs: UpdateItemToDatabase.GetNewSqlConnection()");
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
                " genericPractice=@genericPractice" +
                " WHERE " +
                " projectId=@projectId";  // <<<---- match on the Primary Key  !!! TBD:  This is probably NOT going to work!  may keys will match on projectId????

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);


            //WARNING: myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot assign/modify a field having IDENTITY semantics
            myCommand.Parameters.AddWithValue("@notes", r.notes);
            myCommand.Parameters.AddWithValue("@processArea", r.processArea);
            myCommand.Parameters.AddWithValue("@specificGoal", r.specificGoal);
            myCommand.Parameters.AddWithValue("@specificPractice", r.specificPractice);
            myCommand.Parameters.AddWithValue("@genericGoal", r.genericGoal);
            myCommand.Parameters.AddWithValue("@genericPractice", r.genericPractice);
            myCommand.Parameters.AddWithValue("@projectId", r.projectId);
            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }//UpdateItemToDatabase


        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clear_Database_Table - delete all records from the table "weakness"  database
        /// <input> theTable - the table name</input>
        /// </summary>
        public void Clear_Database_Table()
        {
            if (CountRows() == 0)
                return;

            string strCommand = "DELETE FROM " + theTable;
            if (!DBUtils.ExecuteSqlNonQuery(strCommand))
                LogManager.writeToLog(" ExecuteSqlNonQuery returned: FALSE; in weakness_Table.cs:Clear_Database_Table. ExecuteSqlNonQuery");
        }

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clear_Database_Table_By_processArea- delete weakness table records by processArea
        /// </summary>
        /// <param name="processArea"></param>
        public void Clear_Database_Table_By_processArea(String processArea)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                Console.WriteLine("New connection failed to open; weakness_Table.cs: Clear_Database_Table_By_processArea.GetNewSqlConnection()");
                return;
            }

            string strQuery = "DELETE FROM  " + theTable +
               " WHERE " +
               " processArea=@processArea";
            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            myCommand.Parameters.AddWithValue("@processArea", processArea);

            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }//Clear_Database_Table_By_processArea

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clear_Database_Table_By_projectId- delete weakness table records by projectId
        /// </summary>
        /// <param name="projectId"></param>
        public void Clear_Database_Table_By_projectId(int projectId)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                Console.WriteLine("New connection failed to open; weakness_Table.cs: Clear_Database_Table_By_projectId.GetNewSqlConnection()");
                return;
            }

            string strQuery = "DELETE FROM  " + theTable +
               " WHERE " +
               " projectId=@projectId";
            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            myCommand.Parameters.AddWithValue("@projectId", projectId);

            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }//Clear_Database_Table_By_projectId

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
                LogManager.writeToLog("New connection failed to open; weakness_Table.cs: SQLServer_CountRows.GetNewSqlConnection()");
                return -1;
            }
            string strQuery = "SELECT COUNT(*)  FROM " + theTable;
            return DBUtils.ExecuteSqlQueryScalar(strQuery, myConnection);
        }//CountRows

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// CountRows_By_processAre - count how many rows match the processAre
        /// </summary>
        /// <param name="processAre"></param>
        /// <returns></returns>
        public int CountRows_By_processAre(String processAre)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; weakness_Table.cs: SQLServer_CountRows_By_processAre.GetNewSqlConnection");
                return -1;
            }
            string strQuery = "SELECT COUNT(*)  FROM " + theTable +
             "  WHERE " + "processAre=" + processAre;

            return DBUtils.ExecuteSqlQueryScalar(strQuery, myConnection);
        }//CountRows_By_processAre


        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// CountRows_By_projectId - count how many rows match the projectId
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public int CountRows_By_projectId(int projectId)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; weakness_Table.cs: SQLServer_CountRows_By_projectId.GetNewSqlConnection()");
                return -1;
            }
            string strQuery = "SELECT COUNT(*)  FROM " + theTable +
             "  WHERE " + "projectId=" + projectId.ToString();

            return DBUtils.ExecuteSqlQueryScalar(strQuery, myConnection);
        }//CountRows_By_projectId

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
