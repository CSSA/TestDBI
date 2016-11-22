using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ADODB;

namespace SQLServerDB
{
    /// <summary>
    /// Define the ADO-based support for appraisal_Table 
    /// Provide infrastructure for ADO-based database operations to support Database Interface (DBI) testing 
    /// </summary>
    public partial class appraisal_Table
    {

        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ReadItemListFromDatabase - read all records from "theTable" insert them into this.itemList
        ///</summary>
        ///<input>List<appraisal> itemList - an ordinary List<> of type appraisal, will be cleared if not already empty </input>
        ///<output>List<appraisal>  itemList - an ordinary List<> of type appraisal, extracted from the database </output>
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
                    String str_appraisalName = RS.Fields["appraisalName"].Value;
                    String str_creator = RS.Fields["creator"].Value;
                    int iMaturityLevel = RS.Fields["maturityLevel"].Value;
                    String str_projects = RS.Fields["projects"].Value;
                    Boolean bSAMSelected = RS.Fields["samSelected"].Value;
                    Boolean bSSDSelected = RS.Fields["ssdSelected"].Value;

                    appraisal newRec = new appraisal();
                    newRec.ID = int_ID;
                    newRec.AppraisalName = str_appraisalName;
                    newRec.Creator = str_creator;
                    newRec.MaturityLevel = iMaturityLevel;
                    newRec.Projects = str_projects;
                    newRec.SAMSelected = bSAMSelected;
                    newRec.SSDSelected = bSSDSelected;

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
        /// WriteItemListToDatabase - read all records from this.itemList and write to "theTable"
        ///</summary>
        ///<output>List<appraisal> itemList - an ordinary List<> of type appraisal, output to the "theTable" in the database </output>
        ///<reference> WriteItemToDatabase(appraisal r)</reference>
        public void ADODB_WriteItemListToDatabase()
        {
            foreach (var r in itemList)
            {
                ADODB_WriteItemToDatabase(r);
            }
        }//WriteItemListToDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemToDatabase - write one appraisal record to "theTable" in the database
        ///</summary>
        ///<param name="appraisal r"></param>
        ///<output> r - output one object of type appraisal to the "theTable" in the database </output>
        public void ADODB_WriteItemToDatabase(appraisal r)
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
            object[] fieldnames = { "appraisalName", "creator", "maturityLevel", "projects", "samSelected", "ssdSelected"};
            object[] fieldvalues = { r.AppraisalName, r.Creator, r.MaturityLevel, r.Projects, r.SAMSelected, r.SSDSelected };

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
        /// ADODB_Clear_Database_Table - delete all records from the table "appraisal"  database
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
        /// ADODB_Clear_Database_Table_By_appraisalID - delete appraisal table records by appraisalId
        /// </summary>
        /// <param name="appraisalId"></param>
        public void ADODB_Clear_Database_Table_By_appraisalName(String appraisalName)
        {
            String singleQuote = "'";

            string strQuery = "DELETE FROM  " + theTable +
               " WHERE " + " appraisalName LIKE " + singleQuote + appraisalName + singleQuote;

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
        /// Show -- display the contents of the itemList on the console
        /// </summary>
        public void Show()
        {
            foreach (var r in itemList)
            {
                r.Show();
            }
        }//Show

    }//class appraisal_Table
}//namespace SQLServerDB
