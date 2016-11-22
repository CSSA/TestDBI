using System;
using ADODB;

namespace SQLServerDB
{
    /// <summary>
    /// Define the ADO-based support for affirmation_note_Table 
    /// Provide infrastructure for ADO-based database operations to support Database Interface (DBI) testing 
    /// </summary>
    public partial class affirmation_note_Table
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
                    String str_notes =  RS.Fields["notes"].Value;

                    affirmation_note newRec = new affirmation_note();
                    newRec.ID = int_ID;
                    newRec.affirmationId = int_affirmationId;
                    newRec.notes = str_notes;

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
        public void ADODB_WriteItemToDatabase(affirmation_note r)
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

            object[] fieldnames = { "ID", "affirmationId", "notes"};
            object[] fieldvalues = {r.ID, r.affirmationId, r.notes};
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
        /// ADODB_CountRows - count how many rows in the table
        /// </summary>
        /// <returns>int RowCount</returns>
        public int ADODB_CountRows()
        {
            string strQuery = "SELECT * FROM  " + theTable;
            int iCount = 0;

            ADODB.Connection CONN = new ADODB.Connection();
            ADODB.Recordset RS = new ADODB.Recordset();
            RS.CursorLocation = ADODB.CursorLocationEnum.adUseClient;  //OBSCURE CRITICAL CONSTRAINT!! Use the client side cursor such that RS.RecordCount will be accurate.

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
        /// <returns></returns>
        public int ADODB_CountRows_By_affirmationId(int affirmationId)
        {
            string strQuery = "SELECT * FROM  " + theTable +
                     " WHERE " + " affirmationId=" + affirmationId.ToString();
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
