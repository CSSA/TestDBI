using System;
using System.Collections.Generic;

using System.Data; //DataSet
using System.Data.SqlClient; //SQLConnection, SQLCommand


namespace SQLServerDB
{
    public class project_Table
    {
        public const string theTable = "T_project";

        public List<project> itemList = new List<project>();

        ///<summary>
        /// ReadItemListFromDatabase - read all records from "theTable" insert them into this.itemList
        ///</summary>
        ///<input>List<project> itemList - an ordinary List<> of type project, will be cleared if not already empty </input>
        ///<output>List<project>  itemList - an ordinary List<> of type project, extracted from the database </output>
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
                    int intprojectId = Convert.ToInt32(dr["projectId"]);
                    int intprojectIndex = Convert.ToInt32(dr["projectIndex"]);
                    String strprojectName = dr["projectName"].ToString();
                    String strcreator = dr["creator"].ToString();
                    bool bstandardProcess = Convert.ToBoolean(dr["standardProcess"]);

                    //fill the itemList
                    project newRec = new project();
                    newRec.projectId = intprojectId;
                    newRec.projectIndex = intprojectIndex;
                    newRec.projectName = strprojectName;
                    newRec.creator = strcreator;
                    newRec.standardProcess = bstandardProcess;

                    itemList.Add(newRec);
                }//for
            }
        }//ReadItemListFromDatabase


        ///<summary>
        /// ReadItemListFromDatabase_ByProjectID - read all records from "theTable" insert them into this.itemList, filter by projectID
        /// 1) Erase the current itemList in memory first
        /// 2) Read records from SQLServer, filling the itemList
        ///</summary>
        ///<input>List<project> itemList -  an ordinary List<> of type project, will be cleared if not already empty </input>
        ///<output>List<project> itemList- an ordinary List<> of type project, extracted from the database </output>
        ///<param name="projectIndex"></param>
        public void ReadItemListFromDatabase_ByProjectID(int projectIndex)
        {
            itemList.Clear();  //First, empty the existing list contents

            string sQuery = "SELECT * FROM " + theTable +
            " WHERE projectIndex=" + projectIndex.ToString();

            DataSet dsObj = DBUtils.ExecuteSqlQuery(sQuery);

            if (dsObj != null && dsObj.Tables[0].Rows.Count > 0)
            {
                DataTable dtObj = dsObj.Tables[0]; //get the DataTable reference once

                foreach (DataRow dr in dtObj.Rows)
                {
                    //extract data
                    int intID = Convert.ToInt32(dr["ID"]);
                    int intprojectId = Convert.ToInt32(dr["projectId"]);
                    int intprojectIndex = Convert.ToInt32(dr["projectIndex"]);
                    String strprojectName = dr["projectName"].ToString();
                    String strcreator = dr["creator"].ToString();
                    bool standardProcess = Convert.ToBoolean(dr["standardProcess"]);

                    //fill the itemList
                    project newRec = new project();
                    newRec.projectId = intprojectId;
                    newRec.projectIndex = intprojectIndex;
                    newRec.projectName = strprojectName;
                    newRec.creator = strcreator;
                    newRec.standardProcess = standardProcess;

                    itemList.Add(newRec);
                }//for
            }
        }//ReadItemListFromDatabase_ByProjectID

        

        ///<summary>
        /// WriteItemListToDatabase - read all records from this.itemList and write to "theTable" 
        /// New records are added using the "INSERT INTO" SQL operation
        ///</summary>
        ///<input> String theTable - the table name</input>
        ///<output>List<project> itemList - an ordinary List<> of type project, output to the "theTable" in the database </output>
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
        ///<output>project r - output one project object to the "theTable" in the database </output>
        public void WriteItemToDatabase(project r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; project_Table.cs:WriteItemToDatabase.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "INSERT INTO  " + theTable +
                           " (projectId, projectIndex, projectName, creator, standardProcess) " +
                "VALUES ( @projectId, @projectIndex, @projectName, @creator, @standardProcess);";

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            myCommand.Parameters.AddWithValue("@projectId", r.projectId);
            myCommand.Parameters.AddWithValue("@projectIndex", r.projectIndex);
            myCommand.Parameters.AddWithValue("@projectName", r.projectName);
            myCommand.Parameters.AddWithValue("@creator", r.creator);
            myCommand.Parameters.AddWithValue("@standardProcess", r.standardProcess);

            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }//WriteItemToDatabase


        ///<summary>
        /// UpdateItemListToDatabase - read all records from this.itemList and update matching recordsin "theTable"
        ///</summary>
        ///<input>List<project> itemList - an ordinary List<T> of type project, updated within the "theTable" in the database </input>
        public void UpdateItemListToDatabase()
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; project_Table.cs:UpdateItemListToDatabase.GetNewSqlConnection()");
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
        ///<input>project r -  one item to be updated within the "theTable" in the database </input>
        ///<input> r.currentProject - the projectId to match with one database record</input>
        public void UpdateItemToDatabase(project r)
        { 
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; in project_Table.cs: UpdateItemToDatabase.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "UPDATE  " + theTable +
                " SET " +
                " projectName=@projectName," +
                " projectIndex=@projectIndex," +
                " creator=@creator," +
                " standardProcess=@standardProcess" +
                " WHERE " +
                " projectId=@projectId";  // <<<---- match on the Primary Key

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            //WARNING: myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot assign/modify a field having IDENTITY semantics
            myCommand.Parameters.AddWithValue("@projectId", r.projectId);
            myCommand.Parameters.AddWithValue("@projectIndex", r.projectIndex);
            myCommand.Parameters.AddWithValue("@projectName", r.projectName);
            myCommand.Parameters.AddWithValue("@creator", r.creator);
            myCommand.Parameters.AddWithValue("@standardProcess", r.standardProcess);

            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }//UpdateItemToDatabase


        /// <summary>
        /// Clear_Database_Table - delete all records from the table "project"  database
        /// <input> theTable - the table name</input>
        /// </summary>
        public void Clear_Database_Table()
        {
            string strCommand = "DELETE FROM " + theTable;
            if (!DBUtils.ExecuteSqlNonQuery(strCommand))
                LogManager.writeToLog(" ExecuteSqlNonQuery returned: FALSE; in project_Table.cs:Clear_Database_Table. ExecuteSqlNonQuery");
        }//Clear_Database_Table


        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clear_Database_Table_By_projectId- delete project table records by projectId
        /// </summary>
        /// <param name="projectId"></param>
        public void Clear_Database_Table_By_projectId(int projectId)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                Console.WriteLine("New connection failed to open; project_Table.cs:SQLServer_Clear_Database_Table_By_projectId.GetNewSqlConnection");
                return;
            }

            string strQuery = "DELETE FROM " + theTable +
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
                LogManager.writeToLog("New connection failed to open; project_Table.cs:SQLServer_CountRows.GetNewSqlConnection()");
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
