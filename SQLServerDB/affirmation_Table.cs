using System;
using System.Collections.Generic;

using System.Data; //DataSet
using System.Data.SqlClient; //SQLConnection, SQLCommand



namespace SQLServerDB
{
    /// <summary>
    /// class affirmation_Table: Define the SQLServer-based support for affirmation_Table 
    /// </summary>
    public partial class affirmation_Table
    {
        public const string theTable = "T_affirmation";

        public List<affirmation> itemList = new List<affirmation>();

        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ReadItemListFromDatabase - read all records from "theTable" insert them into this.itemList
        ///</summary>
        ///<input>List<affirmation> itemList - an ordinary List<> of type affirmation, will be cleared if not already empty </input>
        ///<output>List<affirmation>  itemList - an ordinary List<> of type affirmation, extracted from the database </output>
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
                    int intaffirmationId = Convert.ToInt32(dr["affirmationId"]);
                    String straffirmationName = dr["affirmationName"].ToString();
                    String straffirmationType = dr["affirmationType"].ToString();
                    String strspecificGoal = dr["specificGoal"].ToString();
                    String strspecificPractice = dr["specificPractice"].ToString();
                    String strgenericGoal = dr["genericGoal"].ToString();
                    String strgenericPractice = dr["genericPractice"].ToString();
                    String strprocessArea = dr["processArea"].ToString();
                    int intprojectId = Convert.ToInt32(dr["projectId"]);

                    //fill the itemList
                    affirmation newRec = new affirmation();
                    newRec.ID = intID;
                    newRec.affirmationId = intaffirmationId;
                    newRec.affirmationName = straffirmationName;
                    newRec.affirmationType = straffirmationType;
                    newRec.specificGoal = strspecificGoal;
                    newRec.specificPractice = strspecificPractice;
                    newRec.genericGoal = strgenericGoal;
                    newRec.genericPractice = strgenericPractice;
                    newRec.processArea = strprocessArea;
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
        ///<input>List<affirmation> itemList -  an ordinary List<> of type affirmation, will be cleared if not already empty </input>
        ///<output>List<affirmation> itemList- an ordinary List<> of type affirmation, extracted from the database </output>
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
                    int intaffirmationId = Convert.ToInt32(dr["affirmationId"]);
                    String straffirmationName = dr["affirmationName"].ToString();
                    String straffirmationType = dr["affirmationType"].ToString();
                    String strspecificGoal = dr["specificGoal"].ToString();
                    String strspecificPractice = dr["specificPractice"].ToString();
                    String strgenericGoal = dr["genericGoal"].ToString();
                    String strgenericPractice = dr["genericPractice"].ToString();
                    String strprocessArea = dr["processArea"].ToString();


                    //fill the itemList
                    affirmation newRec = new affirmation();
                    newRec.ID = intID;
                    newRec.affirmationId = intaffirmationId;
                    newRec.affirmationName = straffirmationName;
                    newRec.affirmationType = straffirmationType;
                    newRec.specificGoal = strspecificGoal;
                    newRec.specificPractice = strspecificPractice;
                    newRec.genericGoal = strgenericGoal;
                    newRec.genericPractice = strgenericPractice;
                    newRec.processArea = strprocessArea;
                    newRec.projectId = projectId;

                    itemList.Add(newRec);
                }//for
            }
        }//ReadItemListFromDatabase_ByProjectID


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ReadItemListFromDatabase_By_processArea - read all records from "theTable" insert them into this.itemList, filter by processArea
        /// 1) Erase the current itemList in memory first
        /// 2) Read records from SQLServer, filling the itemList
        ///</summary>
        ///<input>List<affirmation> itemList -  an ordinary List<> of type affirmation, will be cleared if not already empty </input>
        ///<output>List<affirmation> itemList- an ordinary List<> of type affirmation, extracted from the database </output>
        ///<param name="processArea"></param>
        public void ReadItemListFromDatabase_By_processArea(String processArea)
        {
            String singleQuote = "'";

            itemList.Clear();  //First, empty the existing list contents

            string sQuery = "SELECT * FROM " + theTable +
            " WHERE processArea  LIKE " + singleQuote + processArea + singleQuote;

            DataSet dsObj = DBUtils.ExecuteSqlQuery(sQuery);

            if (dsObj != null && dsObj.Tables[0].Rows.Count > 0)
            {
                DataTable dtObj = dsObj.Tables[0]; //get the DataTable reference once

                foreach (DataRow dr in dtObj.Rows)
                {
                    //extract data
                    int intID = Convert.ToInt32(dr["ID"]);
                    int intaffirmationId = Convert.ToInt32(dr["affirmationId"]);
                    String straffirmationName = dr["affirmationName"].ToString();
                    String straffirmationType = dr["affirmationType"].ToString();
                    String strspecificGoal = dr["specificGoal"].ToString();
                    String strspecificPractice = dr["specificPractice"].ToString();
                    String strgenericGoal = dr["genericGoal"].ToString();
                    String strgenericPractice = dr["genericPractice"].ToString();

                    int intprojectId = Convert.ToInt32(dr["projectId"]);


                    //fill the itemList
                    affirmation newRec = new affirmation();
                    newRec.ID = intID;
                    newRec.affirmationId = intaffirmationId;
                    newRec.affirmationName = straffirmationName;
                    newRec.affirmationType = straffirmationType;
                    newRec.specificGoal = strspecificGoal;
                    newRec.specificPractice = strspecificPractice;
                    newRec.genericGoal = strgenericGoal;
                    newRec.genericPractice = strgenericPractice;
                    newRec.processArea = processArea;
                    newRec.projectId = intprojectId;

                    itemList.Add(newRec);
                }//for
            }
        }//ReadItemListFromDatabase_By_processArea

#if __OBSOLETE__
        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemListToDatabase - read all records from this.itemList and write to "theTable"
        ///</summary>
        ///<output>List<affirmation> itemList - an ordinary List<> of type affirmation, output to the "theTable" in the database </output>
        ///<reference> WriteItemToDatabase(affirmation r)</reference>
        public void WriteItemListToDatabase()
        {
            foreach (var r in itemList)
            {
                WriteItemToDatabase(r);
            }
        }//WriteItemListToDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemToDatabase - write one affirmation record to "theTable" in the database
        ///</summary>
        ///<param name="affirmation r"></param>
        ///<output> r - output one object of type affirmation to the "theTable" in the database </output>
        public void WriteItemToDatabase(affirmation r)
        {
            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strValues = String.Format("'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}'",
                 r.affirmationId, r.affirmationName, r.affirmationType, r.specificGoal, r.specificPractice, r.genericGoal,
                 r.genericPractice, r.processArea, r.projectId);

            string strCommand =
                "INSERT INTO " + theTable +
                " (affirmationId, affirmationName, affirmationType,specificGoal,specificPractice,genericGoal, genericPractice,processArea, projectId) " +
                "Values (" + strValues + ")";

            Console.WriteLine("command: " + strCommand);

            if (!DBUtils.ExecuteSqlNonQuery(strCommand))
                LogManager.writeToLog("ExecuteSqlNonQuery returned: FALSE in affirmation_Table.cs: WriteItemToDatabase()");
        }//WriteItemToDatabase

#endif
        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemListToDatabase - read all records from this.itemList and write to "theTable" 
        /// New records are added using the "INSERT INTO" SQL operation
        ///</summary>
        ///<input> String theTable - the table name</input>
        ///<output>List<affirmation> itemList - an ordinary List<> of type affirmation, output to the "theTable" in the database </output>
        public void WriteItemListToDatabase()
        {
            //Console.WriteLine("START : WriteItemListToDatabase:" + theTable);
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
            //Console.WriteLine("DONE: WriteItemListToDatabase:" + theTable);
        }//SQLServer_WriteToDatabas



        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemToDatabase - write the given affirmation record to "theTable"
        ///</summary>
        ///<output>affirmation r - output one affirmation object to the "theTable" in the database </output>
        public void WriteItemToDatabase(affirmation r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; affirmation_Table.cs:WriteItemToDatabase.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "INSERT INTO  " + theTable +
                " (affirmationId, affirmationName, affirmationType, specificGoal, specificPractice, genericGoal, genericPractice, processArea, projectId) " +
                "VALUES ( @affirmationId, @affirmationName, @affirmationType, @specificGoal, @specificPractice, @genericGoal, @genericPractice, @processArea, @projectId);";

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            // myCommand.Parameters.AddWithValue("@ID", r.ID); //Warning: We cannot asign to a field having IDENTITY semantics
            myCommand.Parameters.AddWithValue("@affirmationId", r.affirmationId);
            myCommand.Parameters.AddWithValue("@affirmationName", r.affirmationName);
            myCommand.Parameters.AddWithValue("@affirmationType", r.affirmationType);
            myCommand.Parameters.AddWithValue("@specificGoal", r.specificGoal);
            myCommand.Parameters.AddWithValue("@specificPractice", r.specificPractice);
            myCommand.Parameters.AddWithValue("@genericGoal", r.genericGoal);
            myCommand.Parameters.AddWithValue("@genericPractice", r.genericPractice);
            myCommand.Parameters.AddWithValue("@processArea", r.processArea);
            myCommand.Parameters.AddWithValue("@projectId", r.projectId);

            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }//WriteItemToDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// UpdateItemListToDatabase - read all records from this.itemList and update matching recordsin "theTable"
        ///</summary>
        ///<input>List<affirmation> itemList - an ordinary List<T> of type affirmation, updated within the "theTable" in the database </input>
        public void UpdateItemListToDatabase()
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; affirmation_Table.cs:UpdateItemListToDatabase.GetNewSqlConnection()");
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
        ///<input>affirmation r -  one item to be updated within the "theTable" in the database </input>
        ///<input> r.currentProject - the projectId to match with one database record</input>
        public void UpdateItemToDatabase(affirmation r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; in affirmation_Table.cs: UpdateItemToDatabase.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "UPDATE  " + theTable +
                " SET " +
                " affirmationName=@affirmationName," +
                " affirmationType=@affirmationType," +
                " specificGoal=@specificGoal," +
                " specificPractice=@specificPractice," +
                " genericGoal=@genericGoal," +
                " genericPractice=@genericPractice," +
                " processArea=@processArea," +
                 "projectId=@projectId" +
                 " WHERE " +
                " affirmationId=@affirmationId";  // <<<---- match on the Primary Key

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            //WARNING: myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot assign/modify a field having IDENTITY semantics
            myCommand.Parameters.AddWithValue("@affirmationId", r.affirmationId);     /// <<---this is the record selection parameter
            myCommand.Parameters.AddWithValue("@affirmationName", r.affirmationName);
            myCommand.Parameters.AddWithValue("@affirmationType", r.affirmationType);

            myCommand.Parameters.AddWithValue("@specificGoal", r.specificGoal);
            myCommand.Parameters.AddWithValue("@specificPractice", r.specificPractice);
            myCommand.Parameters.AddWithValue("@genericGoal", r.genericGoal);
            myCommand.Parameters.AddWithValue("@genericPractice", r.genericPractice);
            myCommand.Parameters.AddWithValue("@processArea", r.processArea);
            myCommand.Parameters.AddWithValue("@projectId", r.projectId);

            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }//UpdateItemToDatabase


        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clear_Database_Table - delete all records from the table "affirmation"  database
        /// <input> theTable - the table name</input>
        /// </summary>
        public void Clear_Database_Table()
        {
            if (CountRows() == 0)
                return;

            string strCommand = "DELETE FROM " + theTable; // Delete all rows from TABLE
            if (!DBUtils.ExecuteSqlNonQuery(strCommand))
                LogManager.writeToLog("ExecuteSqlNonQuery returned: FALSE; in affirmation_Table.cs:Clear_Database_Table.ExecuteSqlNonQuery()");
        }//Clear_Database_Table

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clear_Database_Table_By_AffirmationID - delete affirmation table records by affirmationId
        /// </summary>
        /// <param name="affirmationId"></param>
        public void Clear_Database_Table_By_AffirmationID(int affirmationId)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                Console.WriteLine("New connection failed to open, affirmation_Table.cs:SQLServer_Clear_Database_Table_By_appraisalName.GetNewSqlConnection()");
                return;
            }

            string strQuery = "DELETE FROM  " + theTable +
               " WHERE " +
               " affirmationId=@affirmationId";
            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            myCommand.Parameters.AddWithValue("@affirmationId", affirmationId);

            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }//Clear_Database_Table_By_AffirmationID

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// SQLServer_Clear_Database_Table_By_prjojectD- delete affirmation table records by projectId
        /// </summary>
        /// <param name="projectId"></param>
        public void Clear_Database_Table_By_projectD(int projectId)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                Console.WriteLine("New connection failed to open; affirmation_Table.cs:SQLServer_Clear_Database_Table_By_projectD.GetNewSqlConnection()");
                return;
            }

            string strQuery = "DELETE FROM  " + theTable +
               " WHERE " +
               " projectId=@projectId";
            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            myCommand.Parameters.AddWithValue("@projectId", projectId);

            myCommand.ExecuteNonQuery();
            myConnection.Close();
        }//Clear_Database_Table_By_projectD

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
                LogManager.writeToLog("New connection failed to open; affirmation_Table.cs: SQLServer_CountRows.GetNewSqlConnection()");
                return -1;
            }
            string strQuery = "SELECT COUNT(*)  FROM " + theTable;
            return DBUtils.ExecuteSqlQueryScalar(strQuery, myConnection);
        }//CountRows

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// CountRows_By_projectId - count how many rows match the projectID
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public int CountRows_By_projectId(int projectId)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; affirmation_Table.cs:SQLServer_CountRows_By_projectId.GetNewSqlConnection()");
                return -1;
            }
            string strQuery = "SELECT COUNT(*)  FROM " + theTable +
             "  WHERE " + "projectId=" + projectId.ToString();

            return DBUtils.ExecuteSqlQueryScalar(strQuery, myConnection);
        }//CountRows_By_projectId




    }
}
