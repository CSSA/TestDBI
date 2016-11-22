using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ADODB;

namespace SQLServerDB
{
    /// <summary>
    /// class goal_practice_Table : Define the SQLServer-based support for goal_practice_Table 
    /// </summary>
    public partial class goal_practice_Table
    {

        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ReadItemListFromDatabase - read all records from "theTable" insert them into this.itemList
        ///</summary>
        ///<input>List<goal_practice> itemList - an ordinary List<> of type goal_practice, will be cleared if not already empty </input>
        ///<output>List<goal_practice>  itemList - an ordinary List<> of type goal_practice, extracted from the database </output>
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
                    int int_NodeId = RS.Fields["NodeId"].Value;
                    int int_processAreaId = RS.Fields["processAreaId"].Value;
                    int int_projectId = RS.Fields["projectId"].Value;
                    String str_Name = RS.Fields["name"].Value;
                    Boolean b_isGoal = RS.Fields["isGoal"].Value;
                    Boolean b_isPractice = RS.Fields["isPractice"].Value;
                    String str_rating = RS.Fields["rating"].Value;
                    Boolean b_coverage = RS.Fields["coverage"].Value;

                    goal_practice newRec = new goal_practice();
                    newRec.ID = int_ID;
                    newRec.nodeId = int_NodeId;
                    newRec.processAreaId = int_processAreaId;
                   newRec.projectId = int_projectId;
                    newRec.name = str_Name;
                    newRec.isGoal = b_isGoal;
                    newRec.isPractice = b_isPractice;
                    newRec.rating = str_rating;
                    newRec.coverage = b_coverage;

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
        /// ADODB_ReadItemListFromDatabase_ByProcessAreaId - read all records from "theTable" WHERE 
        /// processAreaId = paId and insert them into this.itemList
        ///</summary>
        ///<input>List<goal_practice> itemList - an ordinary List<> of type goal_practice, will be cleared if not already empty </input>
        ///<output>List<goal_practice>  itemList - an ordinary List<> of type goal_practice, extracted from the database </output>
        public void ADODB_ReadItemListFromDatabase_ByProcessAreaId(int paID)
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
                "processAreaId = " + "'" + paID.ToString() + "'"
                ;
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
                    int int_NodeId = RS.Fields["NodeId"].Value;
                    int int_processAreaId = RS.Fields["processAreaId"].Value;
                    int int_projectId = RS.Fields["projectId"].Value;
                    String str_Name = RS.Fields["name"].Value;
                    Boolean b_isGoal = RS.Fields["isGoal"].Value;
                    Boolean b_isPractice = RS.Fields["isPractice"].Value;
                    String str_rating = RS.Fields["rating"].Value;
                    Boolean b_coverage = RS.Fields["coverage"].Value;

                    goal_practice newRec = new goal_practice();
                    newRec.ID = int_ID;
                    newRec.nodeId = int_NodeId;
                    newRec.processAreaId = int_processAreaId;
                    newRec.projectId = int_projectId;
                    newRec.name = str_Name;
                    newRec.isGoal = b_isGoal;
                    newRec.isPractice = b_isPractice;
                    newRec.rating = str_rating;
                    newRec.coverage = b_coverage;

                    itemList.Add(newRec);

                    RS.MoveNext();
                }
            }

            RS.Close();
            CONN.Close();

            RS = null;
            CONN = null;
        }//ADODB_ReadItemListFromDatabase_ByProcessAreaId



        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ADODB_ReadItemListFromDatabase_ByProjectId - read all records from "theTable" WHERE
        /// projectId = prjId and insert them into this.itemList
        ///</summary>
        ///<input>List<goal_practice> itemList - an ordinary List<> of type goal_practice, will be cleared if not already empty </input>
        ///<output>List<goal_practice>  itemList - an ordinary List<> of type goal_practice, extracted from the database </output>
        public void ADODB_ReadItemListFromDatabase_ByProjectId(int prjID)
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
                "projectId = " + "'" + prjID.ToString() + "'"
                ;
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
                    int int_NodeId = RS.Fields["NodeId"].Value;
                    int int_processAreaId = RS.Fields["processAreaId"].Value;
                    int int_projectId = RS.Fields["projectId"].Value;
                    String str_Name = RS.Fields["name"].Value;
                    Boolean b_isGoal = RS.Fields["isGoal"].Value;
                    Boolean b_isPractice = RS.Fields["isPractice"].Value;
                    String str_rating = RS.Fields["rating"].Value;
                    Boolean b_coverage = RS.Fields["coverage"].Value;

                    goal_practice newRec = new goal_practice();
                    newRec.ID = int_ID;
                    newRec.nodeId = int_NodeId;
                    newRec.processAreaId = int_processAreaId;
                    newRec.projectId = int_projectId;
                    newRec.name = str_Name;
                    newRec.isGoal = b_isGoal;
                    newRec.isPractice = b_isPractice;
                    newRec.rating = str_rating;
                    newRec.coverage = b_coverage;

                    itemList.Add(newRec);

                    RS.MoveNext();
                }
            }

            RS.Close();
            CONN.Close();

            RS = null;
            CONN = null;

        }//ADODB_ReadItemListFromDatabase_ByProjectId


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ADODB_ReadItemListFromDatabase_ByProjectIdAndProcessAreaId - read all records from "theTable" WHERE
        /// projectId = prjId and insert them into this.itemList
        ///</summary>
        ///<input>List<goal_practice> itemList - an ordinary List<> of type goal_practice, will be cleared if not already empty </input>
        ///<output>List<goal_practice>  itemList - an ordinary List<> of type goal_practice, extracted from the database </output>
        public void ADODB_ReadItemListFromDatabase_ByProjectIdAndProcessAreaId(int prjID, int paId)
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
                "projectId = " + "'" + prjID.ToString() + "'" +
                " AND processAreaID = " + "'" + paId.ToString() + "'" 
                ;
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
                    int int_NodeId = RS.Fields["NodeId"].Value;
                    int int_processAreaId = RS.Fields["processAreaId"].Value;
                    int int_projectId = RS.Fields["projectId"].Value;
                    String str_Name = RS.Fields["name"].Value;
                    Boolean b_isGoal = RS.Fields["isGoal"].Value;
                    Boolean b_isPractice = RS.Fields["isPractice"].Value;
                    String str_rating = RS.Fields["rating"].Value;
                    Boolean b_coverage = RS.Fields["coverage"].Value;

                    goal_practice newRec = new goal_practice();
                    newRec.ID = int_ID;
                    newRec.nodeId = int_NodeId;
                    newRec.processAreaId = int_processAreaId;
                    newRec.projectId = int_projectId;
                    newRec.name = str_Name;
                    newRec.isGoal = b_isGoal;
                    newRec.isPractice = b_isPractice;
                    newRec.rating = str_rating;
                    newRec.coverage = b_coverage;

                    itemList.Add(newRec);

                    RS.MoveNext();
                }
            }

            RS.Close();
            CONN.Close();

            RS = null;
            CONN = null;

        }//ADODB_ReadItemListFromDatabase_ByProjectIdAndProcessAreaId


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// ADODB_ReadItemFromDatabase_ByProjectIDAndNodeName - read all records from "theTable" WHERE
        /// projectId = prjId AND NodeName = name and insert them into this.itemList
        ///</summary>
        ///<input>List<goal_practice> itemList - an ordinary List<> of type goal_practice, will be cleared if not already empty </input>
        ///<output>List<goal_practice>  itemList - an ordinary List<> of type goal_practice, extracted from the database </output>
        public void ADODB_ReadItemFromDatabase_ByProjectIDAndNodeName(int projectId, string nodeName)
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
                "projectId = " + "'" + projectId.ToString() + "'" +
                " AND name   LIKE " + "'" + nodeName + "'"
                ;
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
                    int int_NodeId = RS.Fields["NodeId"].Value;
                    int int_processAreaId = RS.Fields["processAreaId"].Value;
                    int int_projectId = RS.Fields["projectId"].Value;
                    String str_Name = RS.Fields["name"].Value;
                    Boolean b_isGoal = RS.Fields["isGoal"].Value;
                    Boolean b_isPractice = RS.Fields["isPractice"].Value;
                    String str_rating = RS.Fields["rating"].Value;
                    Boolean b_coverage = RS.Fields["coverage"].Value;

                    goal_practice newRec = new goal_practice();
                    newRec.ID = int_ID;
                    newRec.nodeId = int_NodeId;
                    newRec.processAreaId = int_processAreaId;
                    newRec.projectId = int_projectId;
                    newRec.name = str_Name;
                    newRec.isGoal = b_isGoal;
                    newRec.isPractice = b_isPractice;
                    newRec.rating = str_rating;
                    newRec.coverage = b_coverage;

                    itemList.Add(newRec);

                    RS.MoveNext();
                }
            }

            RS.Close();
            CONN.Close();

            RS = null;
            CONN = null;

        }//ADODB_ReadItemFromDatabase_ByProjectIDAndNodeName



        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemListToDatabase - read all records from this.itemList and write to "theTable"
        ///</summary>
        ///<output>List<goal_practice> itemList - an ordinary List<> of type goal_practice, output to the "theTable" in the database </output>
        ///<reference> WriteItemToDatabase(goal_practice r)</reference>
        public void ADODB_WriteItemListToDatabase()
        {
            foreach (var r in itemList)
            {
                ADODB_WriteItemToDatabase(r);
            }
        }//WriteItemListToDatabase


        //---------------------------------------------------------------------------------------------------------------
        ///<summary>
        /// WriteItemToDatabase - write one goal_practice record to "theTable" in the database
        ///</summary>
        ///<param name="goal_practice r"></param>
        ///<output> r - output one object of type goal_practice to the "theTable" in the database </output>
        public void ADODB_WriteItemToDatabase(goal_practice r)
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
            object[] fieldnames = { "nodeId", "processAreaId", "projectId", "name", "isGoal", "isPractice", "rating", "coverage" };
            object[] fieldvalues = { r.nodeId, r.processAreaId, r.projectId, r.name, r.isGoal, r.isPractice, r.rating, r.coverage };

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
        public void ADODB_UpdateItemToDatabase(goal_practice r)
        {
            //Console.WriteLine("START: ADODB_WriteItemToDatabase:" + theTable);

            ADODB.Connection CONN = new ADODB.Connection();
            ADODB.Recordset RS = new ADODB.Recordset();

            //OPEN Connection
            CONN.Open(Connection_String.ADO_ConnectionString, "", "", -1);

            //Initialize the RecordSet object although there is no intention to use the records;  
            string strSQL = "UPDATE * FROM " + theTable +
                " WHERE  " +
                " project_id =" + "'" + r.projectId + "'" +
                " processAreaId =" + "'" + r.processAreaId + "'"
                ; 


      
            RS.Open(strSQL, CONN, ADODB.CursorTypeEnum.adOpenStatic, ADODB.LockTypeEnum.adLockOptimistic);

            //write the content of itemList to the SQLServerDatabase    

            // warning: ID is autonumberd
            object[] fieldnames = { "nodeId", "processAreaId", "projectId", "name", "isGoal", "isPractice", "rating", "coverage" };
            object[] fieldvalues = { r.nodeId, r.processAreaId, r.projectId, r.name, r.isGoal, r.isPractice, r.rating, r.coverage };

            RS.AddNew(fieldnames, fieldvalues);
            RS.Update();

            RS.Close();
            CONN.Close();

            RS = null;
            CONN = null;
            // Console.WriteLine("DONE: ADODB_WriteItemToDatabase:" + theTable);
        }//ADODB_UpdateItemToDatabase



        //---------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// ADODB_Clear_Database_Table - delete all records from the table "goal_practice"  database
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
        /// ADODB_Clear_Database_Table_By_processAreaId - delete goal_practice table records by processAreaId
        /// </summary>
        /// <param name="processAreaId"></param>
        public void ADODB_Clear_Database_Table_By_goal_processAreaId(int processAreaId)
        {
            String singleQuote = "'";

            string strQuery = "DELETE FROM  " + theTable +
               " WHERE " + 
               " processAreaId = " + singleQuote + processAreaId + singleQuote;

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
        /// ADODB_Clear_Database_Table_By_processAreaId - delete goal_practice table records by processAreaId
        /// </summary>
        /// <param name="processAreaId"></param>
        public void ADODB_Clear_Database_Table_By_goal_projectId(int projectId)
        {
            String singleQuote = "'";

            string strQuery = "DELETE FROM  " + theTable +
               " WHERE " +
               " projectId = " + singleQuote + projectId + singleQuote;

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

    }
}
