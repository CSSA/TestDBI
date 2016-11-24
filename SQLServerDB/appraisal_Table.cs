using System;
using System.Collections.Generic;

using System.Data; //DataSet
using System.Data.SqlClient; //SQLConnection, SQLCommand


namespace SQLServerDB
{
    public partial class appraisal_Table
    {
        public const string theTable = "T_appraisal";

        public List<appraisal> itemList = new List<appraisal>();

        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ReadItemListFromDatabase - read all records from "theTable" insert them into this.itemList
        ///</summary>
        ///<input>List<appraisal> itemList</appraisal> itemList - an ordinary List<> of type appraisal, will be cleared if not already empty </input>
        ///<output>List<appraisal> itemList</appraisal> itemList - an ordinary List<> of type appraisal, extracted from the database </output>
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
                    String strappraisalName = dr["appraisalName"].ToString();
                    String strcreator = dr["creator"].ToString();
                    int intmaturityLevel = Convert.ToInt32(dr["maturityLevel"]);
                    String strprojects = dr["projects"].ToString();
                    bool bsamSelected = Convert.ToBoolean(dr["samSelected"]);
                    bool bsdSelected = Convert.ToBoolean(dr["ssdSelected"]);


                    //fill the itemList
                    appraisal newRec = new appraisal();
                    newRec.ID = intID;
                    newRec.AppraisalName = strappraisalName;
                    newRec.Creator = strcreator;
                    newRec.MaturityLevel = intmaturityLevel;
                    newRec.Projects = strprojects;
                    newRec.SAMSelected = bsamSelected;
                    newRec.SSDSelected = bsdSelected;

                    itemList.Add(newRec);
                }//for
            }
        }//ReadItemListFromDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ReadItemListFromDatabase_ByProjectID - read all records from "theTable" insert them into this.itemList, filter by appraisalName
        /// 1) Erase the current itemList in memory first
        /// 2) Read records from SQLServer, filling the itemList
        ///</summary>
        ///<input>List<appraisal> itemList</appraisal> itemList - an ordinary List<> of type appraisal, will be cleared if not already empty </input>
        ///<output>List<appraisal> itemList</appraisal> itemList - an ordinary List<> of type appraisal, extracted from the database </output>
        ///<param name="appraisalName"></param>
        public void ReadItemListFromDatabase_ByAppraisalName(string appraisalName)
        {
            itemList.Clear();  //First, empty the existing list contents

            string sQuery = "SELECT * FROM " + theTable +
                " WHERE appraisalName=" + appraisalName;

            DataSet dsObj = DBUtils.ExecuteSqlQuery(sQuery);

            if (dsObj != null && dsObj.Tables[0].Rows.Count > 0)
            {
                DataTable dtObj = dsObj.Tables[0]; //get the DataTable reference once

                foreach (DataRow dr in dtObj.Rows)
                {
                    //extract data
                    int intID = Convert.ToInt32(dr["Id"]);
                    String strappraisalName = dr["appraisalName"].ToString();
                    String strcreator = dr["creator"].ToString();
                    int intmaturityLevel = Convert.ToInt32(dr["maturityLevel"]);
                    String strprojects = dr["projects"].ToString();
                    bool bsamSelected = Convert.ToBoolean(dr["samSelected"]);
                    bool bssdSelected = Convert.ToBoolean(dr["ssdSelected"]);

                    //fill the itemList
                    appraisal newRec = new appraisal();
                    newRec.ID = intID;
                    newRec.AppraisalName = strappraisalName;
                    newRec.Creator = strcreator;
                    newRec.MaturityLevel = intmaturityLevel;
                    newRec.Projects = strprojects;
                    newRec.SAMSelected = bsamSelected;
                    newRec.SSDSelected = bssdSelected;

                    itemList.Add(newRec);
                }//for
            }
        }//ReadItemListFromDatabase_ByProjectID


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemListToDatabase - read all records from this.itemList and write to "theTable"
        /// New records are added using the "INSERT INTO" SQL operation
        ///</summary>
        ///<input> String theTable - the table name</input>
        ///<output>List<appraisal> itemList</appraisal> itemList - an ordinary List<> of type appraisal, output to the "theTable" in the database </output>
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
        ///<output>appraisal r - output one appraisal object to the "theTable" in the database </output>
        public void WriteItemToDatabase(appraisal r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; appraisal_Table.cs:WriteItemToDatabase.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "INSERT INTO  " + theTable +
                " (appraisalName, creator, maturityLevel, projects, samSelected, ssdSelected) " +
                "VALUES ( @appraisalName, @creator, @maturityLevel, @projects, @samSelected, @ssdSelected);";

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            // myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot asign to a field having IDENTITY semantics
            myCommand.Parameters.AddWithValue("@appraisalName", r.AppraisalName);
            myCommand.Parameters.AddWithValue("@creator", r.Creator);
            myCommand.Parameters.AddWithValue("@maturityLevel", r.MaturityLevel);
            myCommand.Parameters.AddWithValue("@projects", r.Projects);
            myCommand.Parameters.AddWithValue("@samSelected", r.SAMSelected);
            myCommand.Parameters.AddWithValue("@ssdSelected", r.SSDSelected);

            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }//WriteItemToDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// UpdateItemListToDatabase - read all records from this.itemList and update matching recordsin "theTable"
        ///</summary>
        ///<input>List<appraisal> itemList - an ordinary List<T> of type appraisal, updated within the "theTable" in the database </input>
        public void UpdateItemListToDatabase()
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; appraisal_Table:UpdateItemListToDatabase.GetNewSqlConnection()");
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
        ///<input>appraisal r -  one item to be updated within the "theTable" in the database </input>
        ///<input> r.currentProject - the projectId to match with one database record</input>
        public void UpdateItemToDatabase(appraisal r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; appraisal_Table.cs:UpdateItemToDatabase.GetNewSqlConnection()");
                return;
            }



            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "UPDATE  " + theTable +
                " SET " +
                 " creator=@creator," +
                " maturityLevel=@maturityLevel," +
                " samSelected=@samSelected," +
                " ssdSelected=@ssdSelected," +
                " projects=@projects"  + 
            " WHERE " +
                " appraisalName  LIKE " + "@appraisalName" ;  // <<<---- match on the Primary Key


            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            //WARNING: myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot assign/modify a field having IDENTITY semantics
            myCommand.Parameters.AddWithValue("@appraisalName", r.AppraisalName);
            myCommand.Parameters.AddWithValue("@creator", r.Creator);
            myCommand.Parameters.AddWithValue("@maturityLevel", r.MaturityLevel);
            myCommand.Parameters.AddWithValue("@samSelected", r.SAMSelected);
            myCommand.Parameters.AddWithValue("@ssdSelected", r.SSDSelected);
            myCommand.Parameters.AddWithValue("@projects", r.Projects);

            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }//UpdateItemToDatabase

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clear_Database_Table - delete all records from the table "appraisal" in the database
        /// <input> theTable - the table name</input>
        /// </summary>
        public void Clear_Database_Table()
        {
            if (CountRows() == 0)
                return;

            string strCommand = "DELETE FROM " + theTable;
            if (!DBUtils.ExecuteSqlNonQuery(strCommand))
                LogManager.writeToLog("ExecuteSqlNonQuery returned: FALSE; in appraisal_Table.cs:Clear_Database_Table.ExecuteSqlNonQuery()");
        }

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// SQLServer_Clear_Database_Table_By_appraisalName - delete appraisal table records by appraisalName
        /// </summary>
        /// <param name="appraisalName"></param>
        public void SQLServer_Clear_Database_Table_By_appraisalName(String appraisalName)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
               LogManager.writeToLog("New connection failed to open; appraisal_Table.cs:SQLServer_Clear_Database_Table_By_appraisalName.GetNewSqlConnection()");
                return;
            }

            string strQuery = "DELETE FROM  " + theTable +
               " WHERE " +
               " appraisalName=@appraisalName";
            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            myCommand.Parameters.AddWithValue("@appraisalName", appraisalName);

            myCommand.ExecuteNonQuery();
            myConnection.Close();
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
                LogManager.writeToLog("New connection failed to open; appraisal_Table.cs: SQLServer_CountRows.GetNewSqlConnection");
                return -1;
            }
            string strQuery = "SELECT COUNT(*)  FROM " + theTable;
            return DBUtils.ExecuteSqlQueryScalar(strQuery, myConnection);
        }//CountRows


        //----------------------------------------------------------------------------------
        public void Show()
        {
            Console.WriteLine("Table (" + theTable + ") contents");
            foreach (var r in itemList)
            {
                r.Show();
            }
        }

    }//class appraisal_Table
}
