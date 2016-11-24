using System;
using System.Collections.Generic;
using SQLServerDB;


namespace TestDBI
{
    partial class Program
    {

        //--------------------------------------------------------------------------------------------
        static void TestDBI_T_affirmation()
        {
            Console.WriteLine("  --START: TestDBI_T_affirmation ");

            switch (iSubMenuSelection())
            {
                case 1:
                    TestDBI_T_affirmation_Write_to_SQLServer();
                    break;
                case 2:
                    TestDBI_T_affirmation_Read_from_SQLServer();
                    break;
                case 3:
                    TestDBI_T_affirmation_T3();
                    break;
                case 4:
                    TestDBI_T_affirmation_T4();
                    break;
                case 5:
                    TestDBI_T_affirmation_T5();
                    break;
                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_T_affirmation ");
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// TestDBI_T_affirmation_Write_To_SQLServer -- read affirmation table from MSAccess DB via ADODB & write to SQLServer DB
        /// </summary>
        static void TestDBI_T_affirmation_Write_to_SQLServer()
        {
            Console.WriteLine("  --START: TestDBI_T_affirmation_Write_to_SQLServer");

            SQLServerDB.affirmation_Table myTable = new SQLServerDB.affirmation_Table();
            myTable.itemList = make_affirmation_list_1();
            int iRowsStart = myTable.itemList.Count;
            myTable.Show();
            pause();

            Console.WriteLine("  --before clear SQLServer database table");
            pause();
            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            if (iRows2 != 0)
                pause("Error.  iRows=" + iRows2 + " should be zero after Clear_Database_Table()");
            else
                pause("OK.  After Clear_Database_Table()");


            Console.WriteLine("Write the table from RAM the SQLServer  Database table");
            myTable.WriteItemListToDatabase();
            int iRows3 = myTable.CountRows();
            if (iRows3 != iRowsStart)
                pause("Error.  iRows3=" + iRows3 + " should be " + iRowsStart + " after WriteItemListToDatabase");
            else
                pause("OK.  After WriteItemListToDatabase()");

            Console.WriteLine("  --after writing to the SQLServer database table.  examine the table using SSMS");
            pause("visually inspect via SSMS?");

            Console.WriteLine("  --DONE: TestDBI_T_affirmation_Write_To_SQLServer");
        }//TestDBI_T_affirmation_Write_to_SQLServer


        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// TestDBI_T_affirmation_Read_from_SQLServer -- read affirmation table from SQLServer DB and write to  MSAccess DB via ADODB
        /// </summary>
        static void TestDBI_T_affirmation_Read_from_SQLServer()
        {
            Console.WriteLine("  --START: TestDBI_T_affirmation_Read_From_SQLServer");

            SQLServerDB.affirmation_Table myTable = new SQLServerDB.affirmation_Table();

            int iRows = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows.ToString());

            Console.WriteLine("Fill the table in RAM from the SQLServer Database table");
            myTable.ReadItemListFromDatabase();
            myTable.Show();
            if (myTable.itemList.Count != iRows)
                Console.WriteLine("Error.  myTable.itemList.Count != myTable.CountRows." + " should be the same ReadItemListFromDatabase ()");
            else
                Console.WriteLine("OK.  After ReadItemListFromDatabase()");

            pause();
            Console.WriteLine("  --DONE: TestDBI_T_affirmation_Read_from_SQLServer");
        }//TestDBI_T_affirmation_Read_from_SQLServer



        /// <summary>
        /// TestDBI_T_affirmation_T3 -  clear the SQLServer affirmation table, write some demo data to SQLServer DB, 
        /// query the affirmatin table by Project ID, 
        /// </summary>
        static void TestDBI_T_affirmation_T3()
        {
            Console.WriteLine("  --START: TestDBI_T_affirmation_T3");

            //Construct myTable in RAM
            SQLServerDB.affirmation_Table myTable = new SQLServerDB.affirmation_Table();

            //put demo records into myTable
            for (int i = 1; i < 10; i++)
            {
                SQLServerDB.affirmation affItem = new SQLServerDB.affirmation();
                affItem.affirmationId = i;
                affItem.affirmationName = "aff_Name_" + i.ToString();
                affItem.affirmationType = "aff_Type_" + i.ToString();
                affItem.specificGoal = "aff_specificGoal_" + i.ToString();
                affItem.specificPractice = "aff_specificPractice_" + i.ToString();
                affItem.genericGoal = "aff_genericGoal_" + i.ToString();
                affItem.genericPractice = "aff_genericPractice_" + i.ToString();
                affItem.processArea = "aff_processArea_" + i.ToString();
                affItem.projectId = i;

                myTable.itemList.Add(affItem);
            }


            //Count SQLServerDB affirmation table rows before clearing
            int iRows = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows.ToString());

            Console.WriteLine("  --before clear SQLServer database table");
            pause();

            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows2.ToString());
            pause();

            myTable.WriteItemListToDatabase();
            Console.WriteLine("after writing to SQLServerDB");
            pause();

            int iRows3 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows3.ToString());
            pause();

            int iSeek_ProjectID = 3;
            Console.WriteLine("seek item:  iSeek_ProjectID= " + iSeek_ProjectID);
            myTable.ReadItemListFromDatabase_ByProjectID(iSeek_ProjectID);

            Console.WriteLine("SEEK items found: myTable.itemList.Count =" + myTable.itemList.Count.ToString());

            SQLServerDB.affirmation affSeek = myTable.itemList[0];

            Console.WriteLine("affSeek.affirmationId =" + affSeek.affirmationId);
            Console.WriteLine("affSeek.affirmationName =" + affSeek.affirmationName);
            Console.WriteLine("affSeek.affirmationType =" + affSeek.affirmationType);
            Console.WriteLine("affSeek.specificGoal =" + affSeek.specificGoal);
            Console.WriteLine("affSeek.specificPractice =" + affSeek.specificPractice);
            Console.WriteLine("affSeek.genericGoal =" + affSeek.genericGoal);
            Console.WriteLine("affSeek.genericPractice =" + affSeek.genericPractice);
            Console.WriteLine("affSeek.processArea =" + affSeek.processArea);
            Console.WriteLine("affSeek.projectId =" + affSeek.projectId);

            pause();

            //set the search-by criteria
            String strSeek_processArea = "aff_processArea_4";


            myTable.ReadItemListFromDatabase_By_processArea(strSeek_processArea);
            Console.WriteLine("SEEK items by processArea found: myTable.itemList.Count =" + myTable.itemList.Count.ToString());

            SQLServerDB.affirmation affSeek_processArea = myTable.itemList[0];

            Console.WriteLine("affSeek_processArea.affirmationId =" + affSeek_processArea.affirmationId);
            Console.WriteLine("affSeek_processArea.affirmationName =" + affSeek_processArea.affirmationName);
            Console.WriteLine("affSeek_processArea.affirmationType =" + affSeek_processArea.affirmationType);
            Console.WriteLine("affSeek_processArea.specificGoal =" + affSeek_processArea.specificGoal);
            Console.WriteLine("affSeek_processArea.specificPractice =" + affSeek_processArea.specificPractice);
            Console.WriteLine("affSeek_processArea.genericGoal =" + affSeek_processArea.genericGoal);
            Console.WriteLine("affSeek_processArea.genericPractice =" + affSeek_processArea.genericPractice);
            Console.WriteLine("affSeek_processArea.processArea =" + affSeek_processArea.processArea);
            Console.WriteLine("affSeek_processArea.projectId =" + affSeek_processArea.projectId);

            pause();

            Console.WriteLine("  --DONE: TestDBI_T_affirmation_T3");
        }

        /// <summary>
        /// TestDBI_T_affirmation_T4 -- 
        /// </summary>
        static void TestDBI_T_affirmation_T4()
        {
            Console.WriteLine("  --START: TestDBI_T_affirmation_T4");

            //Construct a brand new myTable in RAM
            SQLServerDB.affirmation_Table myTable = new SQLServerDB.affirmation_Table();

            //put demo records into myTable
            for (int i = 1; i < 10; i++)
            {
                SQLServerDB.affirmation affItem = new SQLServerDB.affirmation();
                affItem.affirmationId = i;
                affItem.affirmationName = "aff_Name_" + i.ToString();
                affItem.affirmationType = "aff_Type_" + i.ToString();
                affItem.specificGoal = "aff_specificGoal_" + i.ToString();
                affItem.specificPractice = "aff_specificPractice_" + i.ToString();
                affItem.genericGoal = "aff_genericGoal_" + i.ToString();
                affItem.genericPractice = "aff_genericPractice_" + i.ToString();
                affItem.processArea = "aff_processArea_" + i.ToString();
                affItem.projectId = i;

                myTable.itemList.Add(affItem);
            }

            //Count SQLServerDB affirmation table rows before clearing
            int iRows = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows.ToString());

            Console.WriteLine("  --before clear SQLServer database table");
            pause();

            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows2.ToString());
            pause();

            foreach (SQLServerDB.affirmation r in myTable.itemList)
            {
                myTable.WriteItemToDatabase(r);
            }
            Console.WriteLine("after writing to SQLServerDB");
            pause();

            int iRows3 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows3.ToString());
            pause();

            Console.WriteLine("---update the table");

            //revise the myTable contents
            myTable.itemList.Clear();

            //put demo records into myTable
            for (int i = 1; i < 10; i++)
            {
                SQLServerDB.affirmation affItem = new SQLServerDB.affirmation();
                affItem.affirmationId = i;
                affItem.affirmationName = "aff_Name_" + i.ToString() + "_A";
                affItem.affirmationType = "aff_Type_" + i.ToString() + "_B";
                affItem.specificGoal = "aff_specificGoal_" + i.ToString() + "_C";
                affItem.specificPractice = "aff_specificPractice_" + i.ToString() + "_D";
                affItem.genericGoal = "aff_genericGoal_" + i.ToString() + "_E";
                affItem.genericPractice = "aff_genericPractice_" + i.ToString() + "_F";
                affItem.processArea = "aff_processArea_" + i.ToString() + "_G";
                affItem.projectId = i + 100;

                myTable.itemList.Add(affItem);
            }

            Console.WriteLine("BEFORE the table update");
            pause();
            myTable.UpdateItemListToDatabase();
            Console.WriteLine("AFTER the table update");
            pause();


            for (int i = 4; i <= 6; i++)
                myTable.Clear_Database_Table_By_AffirmationID(i);
            Console.WriteLine("AFTER the table record deletions:  AffirmationI={4,5,6}");
            pause();

            myTable.Clear_Database_Table_By_projectD(102);
            myTable.Clear_Database_Table_By_projectD(108);
            Console.WriteLine("AFTER the table record deletions:  ProjectID = {102, 108}");
            pause();


            Console.WriteLine("  --DONE: TestDBI_T_affirmation_T4");
        }


        static void TestDBI_T_affirmation_T5()
        {
            Console.WriteLine("  --START: TestDBI_T_affirmation_T5");

            //Construct a brand new myTable in RAM
            SQLServerDB.affirmation_Table myTable = new SQLServerDB.affirmation_Table();

            int iRowsStart = 5;
            //put demo records into myTable
            for (int i = 1; i <= iRowsStart; i++)
            {
                SQLServerDB.affirmation affItem = new SQLServerDB.affirmation();
                affItem.affirmationId = i;
                affItem.affirmationName = "aff_Name_" + i.ToString();
                affItem.affirmationType = "aff_Type_" + i.ToString();
                affItem.specificGoal = "aff_specificGoal_" + i.ToString();
                affItem.specificPractice = "aff_specificPractice_" + i.ToString();
                affItem.genericGoal = "aff_genericGoal_" + i.ToString();
                affItem.genericPractice = "aff_genericPractice_" + i.ToString();
                affItem.processArea = "aff_processArea_" + i.ToString();
                affItem.projectId = 404;  //setting each item to the same projectID to support find by projectID

                myTable.itemList.Add(affItem);
            }

            //Count SQLServerDB affirmation table rows before clearing
            int iRows1 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows1.ToString());

            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            if (iRows2 != 0)
                Console.WriteLine("Error! iRows2=" + iRows2 + ".  After Clear_Database_Table should be zero");

            myTable.WriteItemListToDatabase();

            int iRows3 = myTable.CountRows();
            if (iRows3 != iRowsStart)
                Console.WriteLine("Error! iRows3=" + iRows3 + ".  After WriteItemListToDatabase should be " + iRowsStart);
            else
                Console.WriteLine("OK. CountRows=" + iRows3 + " After WriteItemListToDatabase");

           pause("examine table content with SSMS");

            pause("before table query by projectID");    
            int iProjectCount_404 = myTable.CountRows_By_projectId(404);
            if (iProjectCount_404 != iRowsStart)
                Console.WriteLine("ERROR.  iProjectCount_404=" + iProjectCount_404 + ". Expected " + iRowsStart);
            else
                Console.WriteLine("OK. CountRows=" + iProjectCount_404 + " After WriteItemListToDatabase");
            pause();

            Console.WriteLine("  --DONE: TestDBI_T_affirmation_T5");
        }


        static List<affirmation> make_affirmation_list_1()
        {
            List<affirmation> myList = new List<affirmation>()
            { 
             new  affirmation( 1, "affName_1",  "affType_2", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7", 1),
             new  affirmation( 2, "affName_2",  "affType_2", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7", 2),
             new  affirmation( 3, "affName_3",  "affType_2", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7", 2),
             new  affirmation( 4, "affName_4",  "affType_2", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7", 2),
             new  affirmation( 5, "affName_5",  "affType_2", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7", 2),
           };
            return myList;
        }//make_affirmation_list_1


        static List<affirmation> make_affirmation_list_2()
        {
            List<affirmation> myList = new List<affirmation>()
           {
           new  affirmation( 1, "affName_1",  "affType_2-REV-A", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7-REV-A", 1),
           new  affirmation( 2, "affName_2",  "affType_2-REV-A", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7-REV-A", 2),
           new  affirmation( 3, "affName_3",  "affType_2-REV-A", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7-REV-A", 2),
           new  affirmation( 4, "affName_4",  "affType_2-REV-A", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7-REV-A", 2),
           new  affirmation( 5, "affName_5",  "affType_2-REV-A", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7-REV-A", 2),

           };
            return myList;
        }//make_affirmation_list_2

    }
}
