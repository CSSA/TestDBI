using System;
using System.Collections.Generic;

using System.Data; //DataSet
using System.Data.SqlClient; //SQLConnection, SQLCommand

namespace SQLServerDB
{
    /// <summary>
    /// class affirmation_note_Table: Define the SQLServer-based support for affirmation_note_Table 
    /// </summary>
    public partial class affirmation_note_Table
    {
        public const string theTable = "T_affirmation_note";

        public List<affirmation_note> itemList = new List<affirmation_note>();



        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ReadItemListFromDatabase - read all records from "theTable" insert them into this.itemList
        ///</summary>
        ///<input>List<affirmation_note> itemList - an ordinary List<> of type affirmation_note, will be cleared if not already empty </input>
        ///<output>List<affirmation_note>  itemList - an ordinary List<> of type affirmation_note, extracted from the database </output>
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
                    int intaffirmationId = Convert.ToInt32(dr["affirmationID"]);
                    String strnotes = dr["notes"].ToString();


                    //fill the itemList
                    affirmation_note newRec = new affirmation_note();
                    newRec.ID = intID;
                    newRec.affirmationId = intaffirmationId;
                    newRec.notes = strnotes;

                    itemList.Add(newRec);
                }//for
            }

        }//ReadItemListFromDatabase
        
      

        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemListToDatabase - read all records from this.itemList and write to "theTable" 
        /// 1) provide better security;
        /// 2) provide code that is easier to write/read/maintain.
        /// New records are added using the "INSERT INTO" SQL operation
        ///</summary>
        ///<input> String theTable - the table name</input>
        ///<output>List<affirmation_note> itemList - an ordinary List<> of type affirmation_note, output to the "theTable" in the database </output>
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
            Console.WriteLine("DONE: WriteItemListToDatabase:" + theTable);
        }//WriteItemListToDatabase



        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemToDatabase -  write the specified affirmation_note to "theTable"
        ///</summary>
        ///<output>affirmation_note r - output one affirmation_note object to the "theTable" in the database </output>
        public void WriteItemToDatabase(affirmation_note r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; affirmation_note_Table.cs:WriteItemToDatabase.GetNewSqlConnection()");
                return;
            }

            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "INSERT INTO  " + theTable +
                " (affirmationId, notes) " +
                "VALUES ( @affirmationId, @notes);";

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);

            //DEPRECATED: myCommand.Parameters.Add(...),  INSTEAD USE myCommand.Parameters.AddWithValue(...)
            // myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot asign to a field having IDENTITY semantics
            myCommand.Parameters.AddWithValue("@affirmationId", r.affirmationId);
            myCommand.Parameters.AddWithValue("@notes", r.notes);

            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }//WriteItemToDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// UpdateItemListToDatabase - read all records from this.itemList and update matching recordsin "theTable"
        ///</summary>
        ///<input>List<affirmation_note> itemList - an ordinary List<T> of type affirmation_note, updated within the "theTable" in the database </input>
        public void UpdateItemListToDatabase()
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; in affirmation_note_Table.cs: UpdateItemListToDatabase.GetNewSqlConnection()");
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
        ///<input>affirmation_note r -  one item to be updated within the "theTable" in the database </input>
        ///<input> r.currentProject - the projectId to match with one database record</input>
        public void UpdateItemToDatabase(affirmation_note r)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                LogManager.writeToLog("New connection failed to open; in affirmation_note_Table.cs: UpdateItemListToDatabase.GetNewSqlConnection()");
                return;
            }




            //WARNING: A field, like "ID", defined with "IDENTITY" semantics, cannot be assigned a value since it Auto-Increments
            string strQuery = "UPDATE  " + theTable +
                "  SET " +
                "  notes=@notes" +
                "  WHERE " +
                "  affirmationId=@affirmationId";  // <<<---- match on the Primary Key

            SqlCommand myCommand = new SqlCommand(strQuery, myConnection);


            //WARNING: myCommand.Parameters.AddWithValue("@ID", r.ID); //cannot assign/modify a field having IDENTITY semantics
            myCommand.Parameters.AddWithValue("@affirmationId", r.affirmationId);   /// <<---this is  the record selection parameter
            myCommand.Parameters.AddWithValue("@notes", r.notes);

            myCommand.ExecuteNonQuery();

            myConnection.Close();
        }//UpdateItemToDatabase


        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Clear_Database_Table - delete all records from the table "affirmation_note"  database
        /// <input> theTable - the table name</input>
        /// </summary>
        public void Clear_Database_Table()
        {
            if ( CountRows() == 0 )
                return;

            string strCommand = "DELETE FROM " + theTable;
            if (!DBUtils.ExecuteSqlNonQuery(strCommand))
                LogManager.writeToLog(" ExecuteSqlNonQuery returned: FALSE; in affirmation_note_Table.cs:Clear_Database_Table. ExecuteSqlNonQuery");
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
                Console.WriteLine("New connection failed to open, affirmation_Table.cs: SQLServer_Clear_Database_Table_By_affirmationId.GetNewSqlConnection()");
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
        /// Clear_Database_Table_By_projectId - delete affirmation_note table records by projectId
        /// </summary>
        /// <param name="projectId"></param>
        public void Clear_Database_Table_By_projectId(int projectId)
        {
            SqlConnection myConnection = DBUtils.GetNewSqlConnection();
            if (myConnection == null)
            {
                Console.WriteLine("New connection failed to open, affirmation_Table.cs: SQLServer_Clear_Database_Table_By_projectId.GetNewSqlConnection()");
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
                LogManager.writeToLog("New connection failed to open, affirmation_Table.cs: SQLServer_CountRows.GetNewSqlConnection()");
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
