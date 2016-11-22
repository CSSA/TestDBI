using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ADODB;

namespace SQLServerDB
{
    public partial class improvement_opportunity_Table
    {

        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ADODB_ReadItemListFromDatabase - read all records from "theTable" insert them into this.itemList
        ///</summary>
        ///<input>List<improvement_opportunity> itemList - an ordinary List<> of type improvement_opportunity, will be cleared if not already empty </input>
        ///<output>List<improvement_opportunity>  itemList - an ordinary List<> of type improvement_opportunity, extracted from the database </output>
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
                    String str_notes = RS.Fields["notes"].Value;
                    String str_specificGoal = RS.Fields["specificGoal"].Value;
                    String str_specificPractice = RS.Fields["specificPractice"].Value;
                    String str_GenericGoal = RS.Fields["GenericGoal"].Value;
                    String str_GenericPractice = RS.Fields["GenericPractice"].Value;
                    int int_projectId = Convert.ToInt32(RS.Fields["projectId"]);
                    String  str_processArea = RS.Fields["processArea"].Value;

                    improvement_opportunity newRec = new improvement_opportunity();
                    newRec.ID = int_ID;
                    newRec.notes = str_notes;
                    newRec.specificGoal = str_specificGoal;
                    newRec.specificPractice = str_specificPractice;
                    newRec.genericGoal = str_GenericGoal;
                    newRec.genericPractice =str_GenericPractice;
                    newRec.projectId  = int_projectId;
                    newRec.processArea = str_processArea;

                    itemList.Add(newRec);

                    RS.MoveNext();
                }
            }

            RS.Close();
            CONN.Close();

            RS = null;
            CONN = null;

        }//ADODB_ReadItemListFromDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ADODB_ReadItemListFromDatabase_ByProjectID - read all records from "theTable" insert them into this.itemList
        ///</summary>
        ///<input>List<improvement_opportunity> itemList - an ordinary List<> of type improvement_opportunity, will be cleared if not already empty </input>
        ///<output>List<improvement_opportunity>  itemList - an ordinary List<> of type improvement_opportunity, extracted from the database </output>
        public void ADODB_ReadItemListFromDatabase_ByProjectID(int projectId)
        {
            itemList.Clear();  //First, empty the existing list contents

            ADODB.Connection CONN = new ADODB.Connection();
            ADODB.Recordset RS = new ADODB.Recordset();

            Console.WriteLine("Connection_String.ADO_ConnectionString=" + Connection_String.ADO_ConnectionString);
            //OPEN Connection
            CONN.Open(Connection_String.ADO_ConnectionString, "", "", -1);

            //OPEN RecordSet
            String strQuery = "SELECT * FROM " + theTable +
                " WHERE " +
                "projectId = " +  "'" +  projectId.ToString() + "'";

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
                    String str_notes = RS.Fields["notes"].Value;
                    String str_specificGoal = RS.Fields["specificGoal"].Value;
                    String str_specificPractice = RS.Fields["specificPractice"].Value;
                    String str_GenericGoal = RS.Fields["GenericGoal"].Value;
                    String str_GenericPractice = RS.Fields["GenericPractice"].Value;
                    int int_projectId = Convert.ToInt32(RS.Fields["projectId"]);
                    String str_processArea = RS.Fields["processArea"].Value;

                    improvement_opportunity newRec = new improvement_opportunity();
                    newRec.ID = int_ID;
                    newRec.notes = str_notes;
                    newRec.specificGoal = str_specificGoal;
                    newRec.specificPractice = str_specificPractice;
                    newRec.genericGoal = str_GenericGoal;
                    newRec.genericPractice = str_GenericPractice;
                    newRec.projectId = int_projectId;
                    newRec.processArea = str_processArea;

                    itemList.Add(newRec);

                    RS.MoveNext();
                }
            }

            RS.Close();
            CONN.Close();

            RS = null;
            CONN = null;

        }//ADODB_ReadItemListFromDatabase_ByProjectID



        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ADODB_ReadItemListFromDatabase_ByProcessArea - read all records from "theTable" insert them into this.itemList
        ///</summary>
        ///<input>List<improvement_opportunity> itemList - an ordinary List<> of type improvement_opportunity, will be cleared if not already empty </input>
        ///<output>List<improvement_opportunity>  itemList - an ordinary List<> of type improvement_opportunity, extracted from the database </output>
        public void ADODB_ReadItemListFromDatabase_ByProcessArea(String procArea)
        {
            itemList.Clear();  //First, empty the existing list contents

            ADODB.Connection CONN = new ADODB.Connection();
            ADODB.Recordset RS = new ADODB.Recordset();

            Console.WriteLine("Connection_String.ADO_ConnectionString=" + Connection_String.ADO_ConnectionString);
            //OPEN Connection
            CONN.Open(Connection_String.ADO_ConnectionString, "", "", -1);

            //OPEN RecordSet
            String strQuery = "SELECT * FROM " + theTable +
                " WHERE " +
                "processArea  LIKE  " + procArea;

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
                    String str_notes = RS.Fields["notes"].Value;
                    String str_specificGoal = RS.Fields["specificGoal"].Value;
                    String str_specificPractice = RS.Fields["specificPractice"].Value;
                    String str_GenericGoal = RS.Fields["GenericGoal"].Value;
                    String str_GenericPractice = RS.Fields["GenericPractice"].Value;
                    int int_projectId = Convert.ToInt32(RS.Fields["projectId"]);
                    String str_processArea = RS.Fields["processArea"].Value;

                    improvement_opportunity newRec = new improvement_opportunity();
                    newRec.ID = int_ID;
                    newRec.notes = str_notes;
                    newRec.specificGoal = str_specificGoal;
                    newRec.specificPractice = str_specificPractice;
                    newRec.genericGoal = str_GenericGoal;
                    newRec.genericPractice = str_GenericPractice;
                    newRec.projectId = int_projectId;
                    newRec.processArea = str_processArea;

                    itemList.Add(newRec);

                    RS.MoveNext();
                }
            }

            RS.Close();
            CONN.Close();

            RS = null;
            CONN = null;

        }//ADODB_ReadItemListFromDatabase_ByProcessArea


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ADODB_WriteItemListToDatabase - read all records from this.itemList and write to "theTable"
        ///</summary>
        ///<output>List<improvement_opportunity> itemList - an ordinary List<> of type improvement_opportunity, output to the "theTable" in the database </output>
        ///<reference> WriteItemToDatabase(improvement_opportunity r)</reference>
        public void ADODB_WriteItemListToDatabase()
        {
            foreach (var r in itemList)
            {
                ADODB_WriteItemToDatabase(r);
            }
        }//ADODB_WriteItemListToDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemToDatabase - write one improvement_opportunity record to "theTable" in the database
        ///</summary>
        ///<param name="improvement_opportunity r"></param>
        ///<output> r - output one object of type improvement_opportunity to the "theTable" in the database </output>
        public void ADODB_WriteItemToDatabase(improvement_opportunity r)
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

            // warning: ID is autonumberd
            object[] fieldnames = { "notes", "specificGoal", "specificPractice", "genericGoal", "genericPractice", "projectId", "processArea" };
            object[] fieldvalues = { r.notes, r.specificGoal, r.specificPractice, r.genericGoal, r.genericPractice, r.projectId, r.processArea };
            
            RS.AddNew(fieldnames, fieldvalues);
            RS.Update();

            RS.Close();
            CONN.Close();

            RS = null;
            CONN = null;
            // Console.WriteLine("DONE: ADODB_WriteItemToDatabase:" + theTable);
        }//WriteItemToDatabase



        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ADODB_UpdateItemListToDatabase - read all records from this.itemList and write to "theTable"
        ///</summary>
        ///<output>List<goal_practice> itemList - an ordinary List<> of type goal_practice, output to the "theTable" in the database </output>
        ///<reference> ADODB_UpdateItemToDatabase(goal_practice r)</reference>
        public void ADODB_UpdateItemListToDatabase()
        {
            foreach (var r in itemList)
            {
                ADODB_UpdateItemToDatabase(r);
            }
        }//ADODB_UpdateItemListToDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ADODB_UpdateItemToDatabase - update one goal_practice record within "theTable" in the database
        ///</summary>
        ///<param name="goal_practice r"></param>
        ///<output> r - output one object of type goal_practice to the "theTable" in the database </output>
        public void ADODB_UpdateItemToDatabase(improvement_opportunity r)
        {
            ADODB.Connection CONN = new ADODB.Connection();
            ADODB.Recordset RS = new ADODB.Recordset();

            //OPEN Connection
            CONN.Open(Connection_String.ADO_ConnectionString, "", "", -1);

            //Initialize the RecordSet object although there is no intention to use the records;  
            string strSQL = "UPDATE * FROM " + theTable +
                " WHERE  " +
                " project_id =" + "'" + r.projectId + "'" +
                " processArea LIKE " + "'" + r.processArea + "'"
                ;



            RS.Open(strSQL, CONN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic);

            //write the content of itemList to the SQLServerDatabase    

            // warning: ID is autonumberd
            object[] fieldnames = { "notes", "specificGoal", "specificPractice", "genericGoal", "genericPractice", "projectId", "processArea" };
            object[] fieldvalues = { r.notes, r.specificGoal, r.specificPractice, r.genericGoal, r.genericPractice, r.projectId, r.processArea };

            RS.AddNew(fieldnames, fieldvalues);
            RS.Update();

            RS.Close();
            CONN.Close();

            RS = null;
            CONN = null;
        }//ADODB_UpdateItemToDatabase


        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// ADODB_Clear_Database_Table - delete all records from the table "improvement_opportunity"  database
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
        }// ADODB_Clear_Database_Table



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
        /// Show -- display the contents of the itemList on the console
        /// </summary>
        public void Show()
        {
            foreach (var r in itemList)
            {
                r.Show();
            }
        }//Show

    }
}
