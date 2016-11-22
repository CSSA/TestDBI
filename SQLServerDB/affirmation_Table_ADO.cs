using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ADODB;

namespace SQLServerDB
{
    /// <summary>
    /// Define the ADO-based support for affirmation_Table 
    /// Provide infrastructure for ADO-based database operations to support Database Interface (DBI) testing 
    /// </summary>
    public partial class affirmation_Table
    {

        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ReadItemListFromDatabase - read all records from "theTable" insert them into this.itemList
        ///</summary>
        ///<input>List<affirmation> itemList - an ordinary List<> of type affirmation, will be cleared if not already empty </input>
        ///<output>List<affirmation>  itemList - an ordinary List<> of type affirmation, extracted from the database </output>
        public void ADODB_ReadItemListFromDatabase()
        {
            itemList.Clear();  //First, empty the existing list contents

            ADODB.Connection CONN = new ADODB.Connection();
            ADODB.Recordset RS = new ADODB.Recordset();

            Console.WriteLine("Connection_String.ADO_ConnectionString=" + Connection_String.ADO_ConnectionString);
            //OPEN Connection
            CONN.Open(Connection_String.ADO_ConnectionString, "", "", -1);

            //OPEN RecordSet
            String strQuery = "SELECT * FROM " + theTable;
            RS.CursorLocation = ADODB.CursorLocationEnum.adUseServer;
            RS.CursorType = ADODB.CursorTypeEnum.adOpenDynamic;
            RS.LockType = ADODB.LockTypeEnum.adLockOptimistic;
            RS.Open(strQuery, CONN);

            if (!(RS.BOF & RS.EOF)) // record set is empty if both BOF and EOF are true simultaneously
            {
                while (!RS.EOF) //Read ALL records & insert into the itemList 
                {
                    //  Extract the fields from this RecordSet.row
                    int int_ID = RS.Fields["ID"].Value;
                    int int_affirmationId = RS.Fields["affirmationId"].Value;
                    String str_affirmationName = RS.Fields["affirmationName"].Value;
                    String str_affirmationType = RS.Fields["affirmationType"].Value;
                    String str_specificGoal = RS.Fields["specificGoal"].Value;
                    String str_specificPractice = RS.Fields["specificPractice"].Value;
                    String str_genericGoal = RS.Fields["genericGoal"].Value;
                    String str_genericPractice = RS.Fields["genericPractice"].Value;
                    String str_processArea = RS.Fields["processArea"].Value;
                    int int_projectId = RS.Fields["projectId"].Value;

                    affirmation newRec = new affirmation();
                    newRec.ID = int_ID;
                    newRec.affirmationId = int_affirmationId;
                    newRec.affirmationName = str_affirmationName;
                    newRec.affirmationType = str_affirmationType;
                    newRec.specificGoal = str_specificGoal;
                    newRec.specificPractice = str_specificPractice;
                    newRec.genericGoal = str_genericGoal;
                    newRec.genericPractice = str_genericPractice;
                    newRec.processArea = str_processArea;
                    newRec.projectId = int_projectId;

                    itemList.Add(newRec);

                    RS.MoveNext();
                }
            }

            RS.Close();
            CONN.Close();

            RS = null;
            CONN = null;

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
        public void ADODB_ReadItemListFromDatabase_ByProjectID(int projectId)
        {
            itemList.Clear();  //First, empty the existing list contents

            ADODB.Connection CONN = new ADODB.Connection();
            ADODB.Recordset RS = new ADODB.Recordset();

            //OPEN Connection
            CONN.Open(Connection_String.ADO_ConnectionString, "", "", -1);

            //OPEN RecordSet
            string strQuery = "SELECT * FROM " + theTable +
            " WHERE projectId=" + projectId.ToString();

            RS.CursorLocation = ADODB.CursorLocationEnum.adUseServer;
            RS.CursorType = ADODB.CursorTypeEnum.adOpenDynamic;
            RS.LockType = ADODB.LockTypeEnum.adLockOptimistic;
            RS.Open(strQuery, CONN);

            if (!(RS.BOF & RS.EOF)) // record set is empty if both BOF and EOF are true simultaneously
            {
                while (!RS.EOF) //Read ALL records & insert into the itemList 
                {
                    //  Extract the fields from this RecordSet.row
                    int int_ID = RS.Fields["ID"].Value;
                    int int_affirmationId = RS.Fields["affirmationId"].Value;
                    String str_affirmationName = RS.Fields["affirmationName"].Value;
                    String str_affirmationType = RS.Fields["affirmationType"].Value;
                    String str_specificGoal = RS.Fields["specificGoal"].Value;
                    String str_specificPractice = RS.Fields["specificPractice"].Value;
                    String str_genericGoal = RS.Fields["genericGoal"].Value;
                    String str_genericPractice = RS.Fields["genericPractice"].Value;
                    String str_processArea = RS.Fields["processArea"].Value;
                    int int_projectId = RS.Fields["projectId"].Value;

                    affirmation newRec = new affirmation();
                    newRec.ID = int_ID;
                    newRec.affirmationId = int_affirmationId;
                    newRec.affirmationName = str_affirmationName;
                    newRec.affirmationType = str_affirmationType;
                    newRec.specificGoal = str_specificGoal;
                    newRec.specificPractice = str_specificPractice;
                    newRec.genericGoal = str_genericGoal;
                    newRec.genericPractice = str_genericPractice;
                    newRec.processArea = str_processArea;
                    newRec.projectId = int_projectId;

                    itemList.Add(newRec);

                    RS.MoveNext();
                }
            }

            RS.Close();
            CONN.Close();

            RS = null;
            CONN = null;
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
        public void ADODB_ReadItemListFromDatabase_By_processArea(String processArea)
        {
            itemList.Clear();  //First, empty the existing list contents

            ADODB.Connection CONN = new ADODB.Connection();
            ADODB.Recordset RS = new ADODB.Recordset();

            //OPEN Connection
            CONN.Open(Connection_String.ADO_ConnectionString, "", "", -1);

            //OPEN RecordSet
            string strQuery = "SELECT * FROM " + theTable +
            " WHERE processArea=" + processArea;

            RS.CursorLocation = ADODB.CursorLocationEnum.adUseServer;
            RS.CursorType = ADODB.CursorTypeEnum.adOpenDynamic;
            RS.LockType = ADODB.LockTypeEnum.adLockOptimistic;
            RS.Open(strQuery, CONN);

            if (!(RS.BOF & RS.EOF)) // record set is empty if both BOF and EOF are true simultaneously
            {
                while (!RS.EOF) //Read ALL records & insert into the itemList 
                {
                    //  Extract the fields from this RecordSet.row
                    int int_ID = RS.Fields["ID"].Value;
                    int int_affirmationId = RS.Fields["affirmationId"].Value;
                    String str_affirmationName = RS.Fields["affirmationName"].Value;
                    String str_affirmationType = RS.Fields["affirmationType"].Value;
                    String str_specificGoal = RS.Fields["specificGoal"].Value;
                    String str_specificPractice = RS.Fields["specificPractice"].Value;
                    String str_genericGoal = RS.Fields["genericGoal"].Value;
                    String str_genericPractice = RS.Fields["genericPractice"].Value;
                    String str_processArea = RS.Fields["processArea"].Value;
                    int int_projectId = RS.Fields["projectId"].Value;

                    affirmation newRec = new affirmation();
                    newRec.ID = int_ID;
                    newRec.affirmationId = int_affirmationId;
                    newRec.affirmationName = str_affirmationName;
                    newRec.affirmationType = str_affirmationType;
                    newRec.specificGoal = str_specificGoal;
                    newRec.specificPractice = str_specificPractice;
                    newRec.genericGoal = str_genericGoal;
                    newRec.genericPractice = str_genericPractice;
                    newRec.processArea = str_processArea;
                    newRec.projectId = int_projectId;

                    itemList.Add(newRec);

                    RS.MoveNext();
                }
            }

            RS.Close();
            CONN.Close();

            RS = null;
            CONN = null;
        }//ReadItemListFromDatabase_By_processArea

        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemListToDatabase - read all records from this.itemList and write to "theTable"
        ///</summary>
        ///<output>List<affirmation> itemList - an ordinary List<> of type affirmation, output to the "theTable" in the database </output>
        ///<reference> WriteItemToDatabase(affirmation r)</reference>
        public void ADODB_WriteItemListToDatabase()
        {
            foreach (var r in itemList)
            {
                ADODB_WriteItemToDatabase(r);
            }
        }//WriteItemListToDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemToDatabase - write one affirmation record to "theTable" in the database
        ///</summary>
        ///<param name="affirmation r"></param>
        ///<output> r - output one object of type affirmation to the "theTable" in the database </output>
        public void ADODB_WriteItemToDatabase(affirmation r)
        {
            //Console.WriteLine("START: ADODB_WriteItemToDatabase:" + theTable);

            ADODB.Connection CONN = new ADODB.Connection();
            ADODB.Recordset RS = new ADODB.Recordset();

            //OPEN Connection
            CONN.Open(Connection_String.ADO_ConnectionString, "", "", -1);

            //Initialize the RecordSet object although there is no intention to use the records;  
            string strSQL = "SELECT * FROM " + theTable;
            RS.Open(strSQL, CONN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic);

            //write the content of itemList to the SQLServerDatabase    

            object[] fieldnames = { "ID", "affirmationId", "affirmationName", "affirmationType", "specificGoal", "specificPractice",
                                                  "genericGoal", "genericPractice", "processArea", "projectId"};
            object[] fieldvalues = {r.ID, r.affirmationId, r.affirmationName, r.affirmationType, r.specificGoal, r.specificPractice,
                                                  r.genericGoal, r.genericPractice, r.processArea, r.projectId};
            RS.AddNew(fieldnames, fieldvalues);
            RS.Update();

            RS.Close();
            CONN.Close();

            RS = null;
            CONN = null;
            // Console.WriteLine("DONE: ADODB_WriteItemToDatabase:" + theTable);
        }//WriteItemToDatabase



        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// ADODB_Clear_Database_Table - delete all records from the table "affirmation"  database
        /// <input> theTable - the table name</input>
        /// </summary>
        public void ADODB_Clear_Database_Table()
        {
            string strQuerySelect = "SELECT * FROM " + theTable;

            ADODB.Connection CONN = new ADODB.Connection();
            ADODB.Recordset RS = new ADODB.Recordset();

            CONN.Open(Connection_String.ADO_ConnectionString, "", "", -1);

            //Initialize the RecordSet object although there is no intention to use the records;  
            RS.Open(strQuerySelect, CONN); //Accept all of the RecordSet defaults

            if (!(RS.BOF & RS.EOF)) // record set is empty if both BOF and EOF are true simultaneously
            {
                string strQueryDelete = "DELETE * FROM " + theTable;

                Object numRecs;
                CONN.Execute(strQueryDelete, out numRecs, (int)ExecuteOptionEnum.adExecuteNoRecords);
            }
            CONN.Close();
            CONN = null;
        }



        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// ADODB_Clear_Database_Table_By_AffirmationID - delete affirmation table records by affirmationId
        /// </summary>
        /// <param name="affirmationId"></param>
        public void ADODB_Clear_Database_Table_By_AffirmationID(int affirmationId)
        {
            string strQuery = "DELETE FROM  " + theTable +
               " WHERE " + " affirmationId=" + affirmationId.ToString();

            ADODB.Connection CONN = new ADODB.Connection();
            ADODB.Recordset RS = new ADODB.Recordset();

            CONN.Open(Connection_String.ADO_ConnectionString, "", "", -1);

            //Initialize the RecordSet object although there is no intention to use the records;  
            RS.Open(strQuery, CONN); //Accept all of the RecordSet defaults

            if (!(RS.BOF & RS.EOF)) // record set is empty if both BOF and EOF are true simultaneously
            {
                Object numRecs;
                CONN.Execute(strQuery, out numRecs, (int)ExecuteOptionEnum.adExecuteNoRecords);
            }
            CONN.Close();
            CONN = null;
        }



        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// ADODB_Clear_Database_Table_By_projectD- delete affirmation table records by projectId
        /// </summary>
        /// <param name="projectId"></param>
        public void ADODB_Clear_Database_Table_By_projectD(int projectId)
        {
            string strQuery = "SELECT * FROM  " + theTable +
                   " WHERE " + " projectId=" + projectId.ToString();

            ADODB.Connection CONN = new ADODB.Connection();
            ADODB.Recordset RS = new ADODB.Recordset();

            CONN.Open(Connection_String.ADO_ConnectionString, "", "", -1);

            //Initialize the RecordSet object although there is no intention to use the records;  
            RS.Open(strQuery, CONN); //Accept all of the RecordSet defaults

            if (!(RS.BOF & RS.EOF)) // record set is empty if both BOF and EOF are true simultaneously
            {
                Object numRecs;
                CONN.Execute(strQuery, out numRecs, (int)ExecuteOptionEnum.adExecuteNoRecords);
            }

            CONN.Close();
            CONN = null;
        }

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// ADODB_CountRows - count how many rows in the table
        /// </summary>
        /// <returns>int RowCount</returns>
        public int ADODB_CountRows()
        {
            string strQuery = "SELECT * FROM  " + theTable;
            int iCount = 0;

            ADODB.Connection CONN = new ADODB.Connection();
            ADODB.Recordset RS = new ADODB.Recordset();
            RS.CursorLocation = ADODB.CursorLocationEnum.adUseClient;   //OBSCURE CRITICAL CONSTRAINT!! Use the client side cursor such that RS.RecordCount will be accurate.

            CONN.Open(Connection_String.ADO_ConnectionString, "", "", -1);

            //Initialize the RecordSet object although there is no intention to use the records;  
            RS.Open(strQuery, CONN); //Accept all of the RecordSet defaults


            if (!(RS.BOF & RS.EOF)) // record set is empty if both BOF and EOF are true simultaneously
                iCount = RS.RecordCount;

            CONN.Close();
            CONN = null;

            return (iCount);
        }

        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// ADODB_CountRows_By_affirmationId - count how many rows match the affirmationID
        /// </summary>
        /// <param name="affirmationId"></param>
        /// <returns>int RowCount</returns>
        public int ADODB_CountRows_By_affirmationId(int affirmationId)
        {
            string strQuery = "SELECT * FROM  " + theTable +
                     " WHERE " + " affirmationId=" + affirmationId.ToString();
            int iCount = 0;

            ADODB.Connection CONN = new ADODB.Connection();
            ADODB.Recordset RS = new ADODB.Recordset();
            RS.CursorLocation = ADODB.CursorLocationEnum.adUseClient;   //OBSCURE CRITICAL CONSTRAINT!! Use the client side cursor such that RS.RecordCount will be accurate.


            CONN.Open(Connection_String.ADO_ConnectionString, "", "", -1);

            //Initialize the RecordSet object although there is no intention to use the records;  
            RS.Open(strQuery, CONN); //Accept all of the RecordSet defaults

            if (!(RS.BOF & RS.EOF)) // record set is empty if both BOF and EOF are true simultaneously
                iCount = RS.RecordCount;

            CONN.Close();
            CONN = null;

            return (iCount);
        }




        /// <summary>
        /// ADODB_CountRows_By_projectId - count how many rows match the projectID
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        public int ADODB_CountRows_By_projectId(int projectId)
        {
            string strQuery = "SELECT * FROM  " + theTable +
                     " WHERE " + " projectId=" + projectId.ToString();
            int iCount = 0;

            ADODB.Connection CONN = new ADODB.Connection();
            ADODB.Recordset RS = new ADODB.Recordset();

            CONN.Open(Connection_String.ADO_ConnectionString, "", "", -1);

            //Initialize the RecordSet object although there is no intention to use the records;  
            RS.Open(strQuery, CONN); //Accept all of the RecordSet defaults

            if (!(RS.BOF & RS.EOF)) // record set is empty if both BOF and EOF are true simultaneously
                iCount = RS.RecordCount;

            CONN.Close();
            CONN = null;

            return (iCount);
        }

        //----------------------------------------------------------------------------------
        public void Show()
        {
            foreach (var r in itemList)
            {
                r.Show();
            }
        }


    }
}
