using System;
using System.Collections.Generic;

using System.Data; //DataSet
using System.Data.SqlClient; //SQLConnection, SQLCommand


namespace SQLServerDB
{
    public partial class goal_practice_Table
    {
        public const string theTable = "T_goal_practice";

        public List<goal_practice> itemList = new List<goal_practice>();

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
                    int nodeId = Convert.ToInt32(dr["nodeId"]);
                    int processAreaId = Convert.ToInt32(dr["processAreaId"]);
                    int projectId = Convert.ToInt32(dr["projectId"]);
                    string name = dr["name"].ToString();
                    bool boolIsGoal = Convert.ToBoolean(dr["isGoal"]);
                    bool boolIsPractice = Convert.ToBoolean(dr["isPractice"]);
                    string strRating = (dr["rating"]).ToString();
                    bool boolcoverage = Convert.ToBoolean(dr["coverage"]);
                    

                    //fill the itemList
                    goal_practice newRec = new goal_practice();
                    newRec.ID = intID;
                    newRec.nodeId = nodeId;
                    newRec.processAreaId = processAreaId;
                    newRec.projectId = projectId;
                    newRec.name = name;
                    newRec.isGoal = boolIsGoal;
                    newRec.isPractice = boolIsPractice;
                    newRec.rating = strRating;
                    newRec.coverage = boolcoverage;        

                    itemList.Add(newRec);
                }//for
            }
        }//ReadItemListFromDatabase


        public void ReadItemListFromDatabase_ByProcessAreaId( int paID)
        {
            itemList.Clear();  //First, empty the existing list contents

            string sQuery = "SELECT * FROM " + theTable +
                " WHERE  "+ 
                " processAreaId   LIKE " + "'" + paID + "'";

            DataSet dsObj = DBUtils.ExecuteSqlQuery(sQuery);

            if (dsObj != null && dsObj.Tables[0].Rows.Count > 0)
            {
                DataTable dtObj = dsObj.Tables[0]; //get the DataTable reference once

                foreach (DataRow dr in dtObj.Rows)
                {
                    //extract all fields of the current row
                    int intID = Convert.ToInt32(dr["Id"]);
                    int intNodeId = Convert.ToInt32(dr["nodeId"]);
                    int intProcessAreaId = Convert.ToInt32(dr["processAreaId"]);
                    string strnodename = dr["name"].ToString();
                    bool boolIsGoal = Convert.ToBoolean(dr["isGoal"]);
                    bool boolIsPractice = Convert.ToBoolean(dr["isPractice"]);
                    string strRating = (dr["rating"]).ToString();
                    bool boolCoverage = Convert.ToBoolean(dr["coverage"]);

                    //fill the itemList
                    goal_practice newRec = new goal_practice();
                    newRec.ID = intID;
                    newRec.nodeId = intNodeId;
                    newRec.processAreaId = intProcessAreaId;
                    newRec.name = strnodename;
                    newRec.isGoal = boolIsGoal;
                    newRec.isPractice = boolIsPractice;
                    newRec.rating = strRating;
                    newRec.coverage = boolCoverage;

                    itemList.Add(newRec);
                }//foreach
            }//if
        }//ReadItemListFromDatabase_ByProcessAreaId


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
                " WHERE projectId = " + projectId.ToString();

            DataSet dsObj = DBUtils.ExecuteSqlQuery(sQuery);

            if (dsObj != null && dsObj.Tables[0].Rows.Count > 0)
            {
                DataTable dtObj = dsObj.Tables[0]; //get the DataTable reference once

                foreach (DataRow dr in dtObj.Rows)
                {
                    //extract all fields of the current row
                    int intID = Convert.ToInt32(dr["Id"]);
                    int intNodeId = Convert.ToInt32(dr["nodeId"]);
                    int intProcessAreaId = Convert.ToInt32(dr["processAreaId"]);
                    string strnodename = dr["name"].ToString();
                    bool boolIsGoal = Convert.ToBoolean(dr["isGoal"]);
                    bool boolIsPractice = Convert.ToBoolean(dr["isPractice"]);
                    string strRating = (dr["rating"]).ToString();
                    bool boolCoverage = Convert.ToBoolean(dr["coverage"]);

                    //fill the itemList
                    goal_practice newRec = new goal_practice();
                    newRec.ID = intID;
                    newRec.nodeId = intNodeId;
                    newRec.processAreaId = intProcessAreaId;
                    newRec.name = strnodename;
                    newRec.isGoal = boolIsGoal;
                    newRec.isPractice = boolIsPractice;
                    newRec.rating = strRating;
                    newRec.coverage = boolCoverage;

                    itemList.Add(newRec);
                }//foreach
            }//if
        }//ReadItemListFromDatabase_ByProjectID


        public void ReadItemListFromDatabase_ByProjectIDAndProcessAreaId(int projectId, int paID)
        {
            itemList.Clear();  //First, empty the existing list contents

            string sQuery = "SELECT * FROM " + theTable +
                " WHERE projectId = " + projectId.ToString() +
                " AND processAreaId = " + "'" + paID + "'";

            DataSet dsObj = DBUtils.ExecuteSqlQuery(sQuery);

            if (dsObj != null && dsObj.Tables[0].Rows.Count > 0)
            {
                DataTable dtObj = dsObj.Tables[0]; //get the DataTable reference once

                foreach (DataRow dr in dtObj.Rows)
                {
                    //extract all fields of the current row
                    int intID = Convert.ToInt32(dr["Id"]);
                    int intNodeId = Convert.ToInt32(dr["nodeId"]);
                    int intProcessAreaId = Convert.ToInt32(dr["processAreaId"]);
                    string strnodename = dr["name"].ToString();
                    bool boolIsGoal = Convert.ToBoolean(dr["isGoal"]);
                    bool boolIsPractice = Convert.ToBoolean(dr["isPractice"]);
                    string strRating = (dr["rating"]).ToString();
                    bool boolCoverage = Convert.ToBoolean(dr["coverage"]);

                    //fill the itemList
                    goal_practice newRec = new goal_practice();
                    newRec.ID = intID;
                    newRec.nodeId = intNodeId;
                    newRec.processAreaId = intProcessAreaId;
                    newRec.name = strnodename;
                    newRec.isGoal = boolIsGoal;
                    newRec.isPractice = boolIsPractice;
                    newRec.rating = strRating;
                    newRec.coverage = boolCoverage;

                    itemList.Add(newRec);
                }//foreach
            }//if
        }// ReadItemListFromDatabase_ByProjectIDAndProcessAreaId


        /// <summary>
        /// Retrieve a specific node from the goal_practice table and put it in the item list.
        /// </summary>
        /// <param name="nodeName"></param>
        public void ReadItemFromDatabase_ByProjectIDAndNodeName(int projectId, string nodeName)
        {
            itemList.Clear();

            string sQuery = "SELECT * FROM " + theTable +
                " WHERE  " +
                " projectId  =  " + "'" + projectId + "'" +
                " AND name  LIKE " + "'" + nodeName + "'";

            DataSet dsObj = DBUtils.ExecuteSqlQuery(sQuery);

            if (dsObj != null && dsObj.Tables[0].Rows.Count > 0)
            {
                DataTable dtObj = dsObj.Tables[0]; //get the DataTable reference once

                foreach (DataRow dr in dtObj.Rows)
                {
                    //extract all fields of the current row
                    int intID = Convert.ToInt32(dr["Id"]);
                    int intNodeId = Convert.ToInt32(dr["nodeId"]);
                    int intProcessAreaId = Convert.ToInt32(dr["processAreaId"]);
                    string strnodename = dr["name"].ToString();
                    bool boolIsGoal = Convert.ToBoolean(dr["isGoal"]);
                    bool boolIsPractice = Convert.ToBoolean(dr["isPractice"]);
                    string strRating = (dr["rating"]).ToString();
                    bool boolCoverage = Convert.ToBoolean(dr["coverage"]);

                    //fill the itemList
                    goal_practice newRec = new goal_practice();
                    newRec.ID = intID;
                    newRec.nodeId = intNodeId;
                    newRec.processAreaId = intProcessAreaId;
                    newRec.name = strnodename;
                    newRec.isGoal = boolIsGoal;
                    newRec.isPractice = boolIsPractice;
                    newRec.rating = strRating;
                    newRec.coverage = boolCoverage;

                    itemList.Add(newRec);
                }//foreach
            }//if
        }//ReadItemFromDatabase_ByProjectIDAndNodeName


        ///<summary>
        /// WriteItemListToDatabase - read all records from this.itemList and write to "theTable"
        ///</summary>
        ///<output>List<process_area>  itemList - an ordinary List<> of type process_area, output to the "theTable" in the database </output>
        ///<reference> WriteItemToDatabase(process_area r)</reference>
        public void WriteItemListToDatabase()
        {
            foreach (var r in itemList)
            {
                WriteItemToDatabase(r);
            }
        }//WriteItemListToDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemToDatabase - read all records from this.itemList and write to "theTable"
        ///</summary>
        ///<output>process_area r - output one process_area object to the "theTable" in the database </output>
        public void WriteItemToDatabase(goal_practice r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("new connection failed to open@WriteItemToDatabase.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "INSERT INTO  " + theTable +
                " (nodeId, processAreaId, projectId, name, isGoal, isPractice, rating, coverage) " +
                "VALUES ( @nodeId, @processAreaId, @projectId, @name, @isGoal, @isPractice, @rating, @coverage);";

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            // myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot asign to a field having IDENTITY semantics
            myCommand.Parameters.AddWithValue("@nodeId", r.nodeId);
            myCommand.Parameters.AddWithValue("@processAreaId", r.processAreaId);
            myCommand.Parameters.AddWithValue("@projectId", r.projectId);
            myCommand.Parameters.AddWithValue("@name", r.name);
            myCommand.Parameters.AddWithValue("@isGoal", r.isGoal);
            myCommand.Parameters.AddWithValue("@isPractice", r.isPractice);
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
                LogManager.writeToLog("new connection failed to open, goal_practice_Table:UpdateItemListToDatabase");
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
        /// match a record based on the projectId AND name
        ///</summary>
        ///<input>process_area r -  one item to be updated within the "theTable" in the database </input>
        ///<input> r.currentProject - the projectId to match with one database record</input>
        public void UpdateItemToDatabase(goal_practice r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("new connection failed to open; in goal_practice_Table.cs: UpdateItemToDatabase.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "UPDATE  " + theTable +
                " SET " +
                " rating=@rating," +
                " coverage=@coverage" +
                " WHERE " +
                     " projectId=" + "@projectId"  + 
                     " AND name  LIKE  " + "@name";

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            //WARNING: myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot assign/modify a field having IDENTITY semantics
            myCommand.Parameters.AddWithValue("@rating", r.rating);
            myCommand.Parameters.AddWithValue("@coverage", r.coverage);
            myCommand.Parameters.AddWithValue("@projectId", r.projectId);
            myCommand.Parameters.AddWithValue("@name", r.name);

            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }//UpdateItemToDatabas

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clear_Database_Table - delete all records from the table "process_area" in the database
        /// <input> theTable - the table name</input>
        /// </summary>
        public void Clear_Database_Table()
        {
            if (CountRows() == 0)
                return;

            string strCommand = "DELETE from " + theTable;
            if (!DBUtils.ExecuteSqlNonQuery(strCommand))
                LogManager.writeToLog(" ExecuteSqlNonQuery returned: FALSE; in goal_practice_Table.cs:Clear_Database_Table. ExecuteSqlNonQuery");
        }


        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// SQLServer_Clear_Database_Table_By_processArea- delete affirmation table records by processArea
        /// </summary>
        /// <param name="nodeName"></param>
        public void Clear_Database_Table_By_processAreaId(int paId)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                Console.WriteLine("new connection failed to open, goal_practice_Table.cs: SQLServer_Clear_Database_Table_By_appraisalName.GetNewSqlConnection");
                return;
            }

            string strQuery = "DELETE  FROM  " + theTable +
               " WHERE " +
               " processAreaId=@processAreaId";
            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            myCommand.Parameters.AddWithValue("@processAreaId", paId);

            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }//Clear_Database_Table_By_processAreaId

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// SQLServer_Clear_Database_Table_By_prjojectD- delete affirmation table records by projectId
        /// </summary>
        /// <param name="projectId"></param>
        public void Clear_Database_Table_By_projectId(int projectId)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                Console.WriteLine("new connection failed to open, goal_practice_Table.cs: SQLServer_Clear_Database_Table_By_appraisalName.GetNewSqlConnection");
                return;
            }

            string strQuery = "DELETE  FROM  " + theTable +
               " WHERE " +
               " projectId=  " + "'"+  "@projectId" + "'";
            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            myCommand.Parameters.AddWithValue("@projectId", projectId);

            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }// Clear_Database_Table_By_projectId

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
                LogManager.writeToLog("new connection failed to open, goal_practice_Table.cs: SQLServer_CountRows.GetNewSqlConnection");
                return -1;
            }
            string strQuery = "SELECT COUNT(*)  FROM " + theTable;
            return DBUtils.ExecuteSqlQueryScalar(strQuery, myConnection);
        }// CountRows


        //----------------------------------------------------------------------------------
        public void Show()
        {
            Console.WriteLine("Table (" + theTable + ") contents");
            foreach (var r in itemList)
            {
                r.Show();
            }
        }

    }// class goal_practice_Table
}//namespace
