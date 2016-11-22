using System;
using System.Collections.Generic;

using System.Data; //DataSet
using System.Data.SqlClient; //SQLConnection, SQLCommand

namespace SQLServerDB
{
    public class mapping_Table
    {
        public const string theTable = "T_mapping";

        public List<mapping> itemList = new List<mapping>();



        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ReadItemListFromDatabase - read all records from "theTable" insert them into this.itemList
        ///</summary>
        ///<input>List<mapping> itemList - an ordinary List<> of type mapping, will be cleared if not already empty </input>
        ///<output>List<mapping>  itemList - an ordinary List<> of type mapping, extracted from the database </output>
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
                    int intmappingId = Convert.ToInt32(dr["mappingId"]);
                    String strmappingName = dr["mappingName"].ToString();
                    String strmappingPath = dr["mappingPath"].ToString();
                    String strmappingType = dr["mappingType"].ToString();
                    String strprocessArea = dr["processArea"].ToString();
                    String strgoal = dr["goal"].ToString();
                    String strpractice = dr["practice"].ToString();
                    int intprojectId = Convert.ToInt32(dr["projectId"]);

                    //fill the itemList
                    mapping newRec = new mapping();
                    newRec.ID = intID;
                    newRec.mappingId = intmappingId;
                    newRec.mappingName = strmappingName;
                    newRec.mappingPath = strmappingPath;
                    newRec.mappingType = strmappingType;
                    newRec.processArea = strprocessArea;
                    newRec.goal = strgoal;
                    newRec.practice = strpractice;
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
        ///<input>List<mapping> itemList -  an ordinary List<> of type mapping, will be cleared if not already empty </input>
        ///<output>List<mapping> itemList- an ordinary List<> of type mapping, extracted from the database </output>
        ///<param name="projectId"></param>
        public void ReadItemListFromDatabase_ByProjectID(int projectId)
        {
            itemList.Clear();  //First, empty the existing list contents

            string sQuery = "SELECT * FROM " + theTable +
            " WHERE projectId=" + "'" + projectId.ToString() + "'";

            DataSet dsObj = DBUtils.ExecuteSqlQuery(sQuery);

            if (dsObj != null && dsObj.Tables[0].Rows.Count > 0)
            {
                DataTable dtObj = dsObj.Tables[0]; //get the DataTable reference once

                foreach (DataRow dr in dtObj.Rows)
                {
                    //extract data
                    int intID = Convert.ToInt32(dr["ID"]);
                    int intmappingId = Convert.ToInt32(dr["mappingId"]);
                    String strmappingName = dr["mappingName"].ToString();
                    String strmappingPath = dr["mappingPath"].ToString();
                    String strmappingType = dr["mappingType"].ToString();
                    String strprocessArea = dr["processArea"].ToString();
                    String strgoal = dr["goal"].ToString();
                    String strpractice = dr["practice"].ToString();

                    //fill the itemList
                    mapping newRec = new mapping();
                    newRec.ID = intID;
                    newRec.mappingId = intmappingId;
                    newRec.mappingName = strmappingName;
                    newRec.mappingPath = strmappingPath;
                    newRec.mappingType = strmappingType;
                    newRec.processArea = strprocessArea;
                    newRec.goal = strgoal;
                    newRec.practice = strpractice;
                    newRec.projectId = projectId;

                    itemList.Add(newRec);
                }//for
            }
        }//ReadItemListFromDatabase_ByProjectID


        public void ReadItemListFromDatabase_ByProjectIDAndNode(int projectId, string pa, string goal, string practice)
        {
            itemList.Clear();

            string sQuery = "SELECT * FROM " + theTable +
                " WHERE " +
                "projectId = " + projectId.ToString() +
                " AND processArea = " + "'" + pa + "'" + 
                " AND goal = " + "'" + goal + "'" + 
                " AND practice = " + "'" + practice + "'";

            DataSet dsObj = DBUtils.ExecuteSqlQuery(sQuery);

            if (dsObj != null && dsObj.Tables[0].Rows.Count > 0)
            {
                DataTable dtObj = dsObj.Tables[0]; //get the DataTable reference once

                foreach (DataRow dr in dtObj.Rows)
                {
                    //extract data
                    int intID = Convert.ToInt32(dr["ID"]);
                    int intmappingId = Convert.ToInt32(dr["mappingId"]);
                    String strmappingName = dr["mappingName"].ToString();
                    String strmappingPath = dr["mappingPath"].ToString();
                    String strmappingType = dr["mappingType"].ToString();
                    String strprocessArea = dr["processArea"].ToString();
                    String strgoal = dr["goal"].ToString();
                    String strpractice = dr["practice"].ToString();

                    //fill the itemList
                    mapping newRec = new mapping();
                    newRec.ID = intID;
                    newRec.mappingId = intmappingId;
                    newRec.mappingName = strmappingName;
                    newRec.mappingPath = strmappingPath;
                    newRec.mappingType = strmappingType;
                    newRec.processArea = strprocessArea;
                    newRec.goal = strgoal;
                    newRec.practice = strpractice;
                    newRec.projectId = projectId;

                    itemList.Add(newRec);
                }//for
            }
        }//ReadItemListFromDatabase_ByProjectIDAndNode

        

        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemListToDatabase - read all records from this.itemList and write to "theTable" 
        /// New records are added using the "INSERT INTO" SQL operation
        ///</summary>
        ///<input> String theTable - the table name</input>
        ///<output>List<mapping> itemList - an ordinary List<> of type mapping, output to the "theTable" in the database </output>
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
        /// WriteItemToDatabase - given one item/record, write it to "theTable"
        ///</summary>
        ///<output>mapping r - output one mapping object to the "theTable" in the database </output>
        public void WriteItemToDatabase(mapping r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; mapping_Table.cs:WriteItemToDatabase.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "INSERT INTO  " + theTable +
                " (mappingId, mappingName, mappingPath, mappingType, processArea, goal, practice, projectId) " +
                "VALUES " + 
                "( @mappingId, @mappingName, @mappingPath, @mappingType, @processArea, @goal, @practice, @projectId);";

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            //DEPRECATED: myCommand.Parameters.Add(...),  INSTEAD USE myCommand.Parameters.AddWithValue(...)
            // myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot asign to a field having IDENTITY semantics
            myCommand.Parameters.AddWithValue("@mappingId", r.mappingId);
            myCommand.Parameters.AddWithValue("@mappingName", r.mappingName);
            myCommand.Parameters.AddWithValue("@mappingPath", r.mappingPath);
            myCommand.Parameters.AddWithValue("@mappingType", r.mappingType);
            myCommand.Parameters.AddWithValue("@processArea", r.processArea);
            myCommand.Parameters.AddWithValue("@goal", r.goal);
            myCommand.Parameters.AddWithValue("@practice", r.practice);
            myCommand.Parameters.AddWithValue("@projectId", r.projectId);

            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }//WriteItemToDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// UpdateItemListToDatabase - read all records from this.itemList and update matching recordsin "theTable"
        ///</summary>
        ///<input>List<mapping> itemList - an ordinary List<T> of type mapping, updated within the "theTable" in the database </input>
        public void UpdateItemListToDatabase()
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; mapping_Table.cs:UpdateItemListToDatabase()");
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
        ///<input>mapping r -  one item to be updated within the "theTable" in the database </input>
        ///<input> r.currentProject - the projectId to match with one database record</input>
        public void UpdateItemToDatabase(mapping r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; in mapping_Table.cs:UpdateItemToDatabase.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "UPDATE  " + theTable +
                " SET " +
                " mappingName=@mappingName," +
                " mappingPath=@mappingPath," +
                " mappingType=@mappingType," +
                " goal=@goal," +
                " practice=@practice," +
                " processArea=@processArea" +
                " WHERE " +
                " mappingId=@mappingId";  // <<<---- match on the Primary Key

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            //DEPRECATED: myCommand.Parameters.Add(...),  INSTEAD USE myCommand.Parameters.AddWithValue(...)
            //WARNING: myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot assign/modify a field having IDENTITY semantics
            //myCommand.Parameters.AddWithValue("@mappingId", r.mappingId);  <<---this was the record selection parameter
            myCommand.Parameters.AddWithValue("@mappingName", r.mappingName);
            myCommand.Parameters.AddWithValue("@mappingPath", r.mappingPath);
            myCommand.Parameters.AddWithValue("@mappingType", r.mappingType);
            myCommand.Parameters.AddWithValue("@processArea", r.processArea);
            myCommand.Parameters.AddWithValue("@goal", r.goal);
            myCommand.Parameters.AddWithValue("@practice", r.practice);
            myCommand.Parameters.AddWithValue("@projectId", r.projectId);
            myCommand.Parameters.AddWithValue("@mappingId", r.mappingId);

            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }//UpdateItemToDatabase


        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clear_Database_Table - delete all records from the table "mapping"  database
        /// <input> theTable - the table name</input>
        /// </summary>
        public void Clear_Database_Table()
        {
            string strCommand = "DELETE FROM " + theTable;
            if (!DBUtils.ExecuteSqlNonQuery(strCommand))
                LogManager.writeToLog(" ExecuteSqlNonQuery returned: FALSE; in mapping_Table.cs:Clear_Database_Table. ExecuteSqlNonQuery");
        }//Clear_Database_Table

        public void Clear_Database_Table_By_Node(string pa, string goal, string practice)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                Console.WriteLine("New connection failed to open; mapping_Table.cs:SQLServer_Clear_Database_Table_By_mappingId.GetNewSqlConnection()");
                return;
            }

            string strQuery = "DELETE FROM  " + theTable +
               " WHERE " +
               " processArea=@pa" +
               " AND goal = @goal" +
               " AND practice = @practice";
            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            myCommand.Parameters.AddWithValue("@pa", pa);
            myCommand.Parameters.AddWithValue("@goal", goal);
            myCommand.Parameters.AddWithValue("practice", practice);

            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }//Clear_Database_Table_By_Node

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clear_Database_Table_By_mappingId- delete affirmation table records by mappingId
        /// </summary>
        /// <param name="mappingId"></param>
        public void Clear_Database_Table_By_mappingId(int mappingId)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                Console.WriteLine("New connection failed to open; mapping_Table.cs:SQLServer_Clear_Database_Table_By_mappingId.GetNewSqlConnection()");
                return;
            }

            string strQuery = "DELETE FROM  " + theTable +
               " WHERE " +
               " mappingId=@mappingId";
            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            myCommand.Parameters.AddWithValue("@mappingId", mappingId);

            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }//Clear_Database_Table_By_mappingId

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clear_Database_Table_By_projectId - delete mapping table records by projectId
        /// </summary>
        /// <param name="projectId"></param>
        public void Clear_Database_Table_By_projectId(int projectId)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                Console.WriteLine("New connection failed to open; mapping_Table.cs: SQLServer_Clear_Database_Table_By_projectId.GetNewSqlConnection()");
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
                LogManager.writeToLog("New connection failed to open; mapping_Table.cs:SQLServer_CountRows.GetNewSqlConnection()");
                return -1;
            }
            string strQuery = "SELECT COUNT(*)  FROM " + theTable;
            return DBUtils.ExecuteSqlQueryScalar(strQuery, myConnection);
        }//CountRows



    }
}
