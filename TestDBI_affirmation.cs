using System;
using System.Collections.Generic;
using SQLServerDB;


namespace TestDBI
{
     class TestDBI_affirmation
    {
        
        //--------------------------------------------------------------------------------------------
        public static void SelectTest()
        {
            Console.WriteLine("  --START: TestDBI_affirmation.SelectTest()");

            switch (Program.iSubMenuSelection())
            {
                case 1:
                    TestDBI_T_affirmation_Write_to_DB();
                    break;
                case 2:
                    TestDBI_T_affirmation_Read_from_DB();
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
                case 6:
                    TestDBI_T_affirmation_Write_Read_T6();
                    break;
                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_affirmation.SelectTest()");
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// TestDBI_T_affirmation_Write_to_DB -- write itemlist to DB
        /// </summary>
        static void TestDBI_T_affirmation_Write_to_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_affirmation_Write_to_DB");

            affirmation_Table myTable = new affirmation_Table();
            myTable.itemList = make_affirmation_list_1();
            int iRowsStart = myTable.itemList.Count;
            myTable.Show();
            Util.pause();

            Console.WriteLine("  --before clear SQLServer database table");
            Util.pause();
            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            if (iRows2 != 0)
                Util.pause("Error.  iRows=" + iRows2 + " should be zero after Clear_Database_Table()");
            else
                Util.pause("OK.  After Clear_Database_Table()");


            Console.WriteLine("Write the table from RAM the SQLServer  Database table");
            myTable.WriteItemListToDatabase();
            int iRows3 = myTable.CountRows();
            if (iRows3 != iRowsStart)
                Util.pause("Error.  iRows3=" + iRows3 + " should be " + iRowsStart + " after WriteItemListToDatabase");
            else
                Util.pause("OK.  After WriteItemListToDatabase()");

            Console.WriteLine("  --after writing to the SQLServer database table.  examine the table using SSMS");
            Util.pause("visually inspect via SSMS?");

            Console.WriteLine("  --DONE: TestDBI_T_affirmation_Write_to_DB");
        }//TestDBI_T_affirmation_Write_to_DB


        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// TestDBI_T_affirmation_Read_from_DB -- read from DB
        /// </summary>
        static void TestDBI_T_affirmation_Read_from_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_affirmation_Read_from_DB");

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

            Util.pause();
            Console.WriteLine("  --DONE: TestDBI_T_affirmation_Read_from_DB");
        }//TestDBI_T_affirmation_Read_from_DB



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
            Util.pause();

            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows2.ToString());
            Util.pause();

            myTable.WriteItemListToDatabase();
            Console.WriteLine("after writing to SQLServerDB");
            Util.pause();

            int iRows3 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows3.ToString());
            Util.pause();

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

            Util.pause();

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

            Util.pause();

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
            Util.pause();

            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows2.ToString());
            Util.pause();

            foreach (SQLServerDB.affirmation r in myTable.itemList)
            {
                myTable.WriteItemToDatabase(r);
            }
            Console.WriteLine("after writing to SQLServerDB");
            Util.pause();

            int iRows3 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows3.ToString());
            Util.pause();

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
            Util.pause();
            myTable.UpdateItemListToDatabase();
            Console.WriteLine("AFTER the table update");
            Util.pause();


            for (int i = 4; i <= 6; i++)
                myTable.Clear_Database_Table_By_AffirmationID(i);
            Console.WriteLine("AFTER the table record deletions:  AffirmationI={4,5,6}");
            Util.pause();

            myTable.Clear_Database_Table_By_projectD(102);
            myTable.Clear_Database_Table_By_projectD(108);
            Console.WriteLine("AFTER the table record deletions:  ProjectID = {102, 108}");
            Util.pause();


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

            Util.pause("examine table content with SSMS");

            Util.pause("before table query by projectID");    
            int iProjectCount_404 = myTable.CountRows_By_projectId(404);
            if (iProjectCount_404 != iRowsStart)
                Console.WriteLine("ERROR.  iProjectCount_404=" + iProjectCount_404 + ". Expected " + iRowsStart);
            else
                Console.WriteLine("OK. CountRows=" + iProjectCount_404 + " After WriteItemListToDatabase");
            Util.pause();

            Console.WriteLine("  --DONE: TestDBI_T_affirmation_T5");
        }



        static void TestDBI_T_affirmation_Write_Read_T6()
        {

            //create demo data: table1
            affirmation_Table myTable1 = new affirmation_Table();
            myTable1.itemList = make_affirmation_list_1();
            int iRows1 = myTable1.itemList.Count;
            myTable1.Show();

            //write to DB
            myTable1.WriteItemListToDatabase();

            //create demo table: table2, read from DB
            affirmation_Table myTable2 = new affirmation_Table();
            int iRows2 = myTable2.itemList.Count;
            if (iRows1 != iRows2)
                Console.WriteLine("Error. row counts don't match");
            else
                Console.WriteLine("OK.  row countsmatch()");

            //compare table1 & table2
            int iResult = CompareLists(myTable1.itemList, myTable2.itemList);
            if (iResult != 0)
                Console.WriteLine("Error. itemLists don't match");
            else
                Console.WriteLine("OK.  itemLists match");
            Util.pause();
        }


        static int CompareLists(List<affirmation> list1, List<affirmation> list2)
        {
            int iCount1 = list1.Count;
            int iCount2 = list2.Count;
            if (iCount1 != iCount2) 
                return -1;

            for (int i = 0; i < iCount1; i++)
            {
                if (list1[i] != list2[i])
                    return -1;  //NOT EQUAL
            }
            return 0;  // EQUAL
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
