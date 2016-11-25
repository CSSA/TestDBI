﻿using System;
using System.Collections.Generic;
using SQLServerDB;



namespace TestDBI
{
    class TestDBI_process_area
    {


        //--------------------------------------------------------------------------------------------
        public static void SelectTest()
        {
            Console.WriteLine("  --START: TestDBI_process_area.SelectTest()");

            switch (Program.iSubMenuSelection())
            {
                case 1:
                    TestDBI_T_process_area_Write_to_DB();
                    break;
                case 2:
                    TestDBI_T_process_area_Read_from_DB();
                    break;
                case 3:
                    TestDBI_T_process_area_T3();
                    break;
                case 4:
                    TestDBI_T_process_area_T4();
                    break;
                case 5:
                    TestDBI_T_process_area_T5();
                    break;
                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_process_area.SelectTest()");
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// TestDBI_T_process_area_Write_to_DB -- write itemlist to DB
        /// </summary>
        static void TestDBI_T_process_area_Write_to_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_process_area_Write_to_DB");

            process_area_Table myTable = new process_area_Table();
            myTable.itemList = make_process_area_list_1();
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

            Console.WriteLine("  --DONE: TestDBI_T_process_area_Write_to_DB");
        }//TestDBI_T_process_area_Write_to_DB


        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// TestDBI_T_process_area_Read_from_DB -- read from DB
        /// </summary>
        static void TestDBI_T_process_area_Read_from_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_process_area_Read_from_DB");

            SQLServerDB.process_area_Table myTable = new SQLServerDB.process_area_Table();

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
            Console.WriteLine("  --DONE: TestDBI_T_process_area_Read_from_DB");
        }//TestDBI_T_process_area_Read_from_DB



        /// <summary>
        /// TestDBI_T_process_area_T3 -  clear the SQLServer process_area table, write some demo data to SQLServer DB, 
        /// query the affirmatin table by Project ID, 
        /// </summary>
        static void TestDBI_T_process_area_T3()
        {
            Console.WriteLine("  --START: TestDBI_T_process_area_T3");

            //Construct myTable in RAM
            SQLServerDB.process_area_Table myTable = new SQLServerDB.process_area_Table();

#if Comment
            //put demo records into myTable
            for (int i = 1; i < 10; i++)
            {
                SQLServerDB.process_area affItem = new SQLServerDB.process_area();
                affItem.process_areaId = i;
                affItem.process_areaName = "aff_Name_" + i.ToString();
                affItem.process_areaType = "aff_Type_" + i.ToString();
                affItem.specificGoal = "aff_specificGoal_" + i.ToString();
                affItem.specificPractice = "aff_specificPractice_" + i.ToString();
                affItem.genericGoal = "aff_genericGoal_" + i.ToString();
                affItem.genericPractice = "aff_genericPractice_" + i.ToString();
                affItem.processArea = "aff_processArea_" + i.ToString();
                affItem.projectId = i;

                myTable.itemList.Add(affItem);
            }


            //Count SQLServerDB process_area table rows before clearing
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

            SQLServerDB.process_area affSeek = myTable.itemList[0];

            Console.WriteLine("affSeek.process_areaId =" + affSeek.process_areaId);
            Console.WriteLine("affSeek.process_areaName =" + affSeek.process_areaName);
            Console.WriteLine("affSeek.process_areaType =" + affSeek.process_areaType);
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

            SQLServerDB.process_area affSeek_processArea = myTable.itemList[0];

            Console.WriteLine("affSeek_processArea.process_areaId =" + affSeek_processArea.process_areaId);
            Console.WriteLine("affSeek_processArea.process_areaName =" + affSeek_processArea.process_areaName);
            Console.WriteLine("affSeek_processArea.process_areaType =" + affSeek_processArea.process_areaType);
            Console.WriteLine("affSeek_processArea.specificGoal =" + affSeek_processArea.specificGoal);
            Console.WriteLine("affSeek_processArea.specificPractice =" + affSeek_processArea.specificPractice);
            Console.WriteLine("affSeek_processArea.genericGoal =" + affSeek_processArea.genericGoal);
            Console.WriteLine("affSeek_processArea.genericPractice =" + affSeek_processArea.genericPractice);
            Console.WriteLine("affSeek_processArea.processArea =" + affSeek_processArea.processArea);
            Console.WriteLine("affSeek_processArea.projectId =" + affSeek_processArea.projectId);

            Util.pause();
#endif

            Console.WriteLine("  --DONE: TestDBI_T_process_area_T3");
        }

        /// <summary>
        /// TestDBI_T_process_area_T4 -- 
        /// </summary>
        static void TestDBI_T_process_area_T4()
        {
            Console.WriteLine("  --START: TestDBI_T_process_area_T4");

 #if Comment
            //Construct a brand new myTable in RAM
            SQLServerDB.process_area_Table myTable = new SQLServerDB.process_area_Table();

            //put demo records into myTable
            for (int i = 1; i < 10; i++)
            {
                SQLServerDB.process_area affItem = new SQLServerDB.process_area();
                affItem.process_areaId = i;
                affItem.process_areaName = "aff_Name_" + i.ToString();
                affItem.process_areaType = "aff_Type_" + i.ToString();
                affItem.specificGoal = "aff_specificGoal_" + i.ToString();
                affItem.specificPractice = "aff_specificPractice_" + i.ToString();
                affItem.genericGoal = "aff_genericGoal_" + i.ToString();
                affItem.genericPractice = "aff_genericPractice_" + i.ToString();
                affItem.processArea = "aff_processArea_" + i.ToString();
                affItem.projectId = i;

                myTable.itemList.Add(affItem);
            }

            //Count SQLServerDB process_area table rows before clearing
            int iRows = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows.ToString());

            Console.WriteLine("  --before clear SQLServer database table");
            Util.pause();

            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows2.ToString());
            Util.pause();

            foreach (SQLServerDB.process_area r in myTable.itemList)
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
                SQLServerDB.process_area affItem = new SQLServerDB.process_area();
                affItem.process_areaId = i;
                affItem.process_areaName = "aff_Name_" + i.ToString() + "_A";
                affItem.process_areaType = "aff_Type_" + i.ToString() + "_B";
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
                myTable.Clear_Database_Table_By_process_areaID(i);
            Console.WriteLine("AFTER the table record deletions:  process_areaI={4,5,6}");
            Util.pause();

            myTable.Clear_Database_Table_By_projectD(102);
            myTable.Clear_Database_Table_By_projectD(108);
            Console.WriteLine("AFTER the table record deletions:  ProjectID = {102, 108}");
            Util.pause();

#endif
            Console.WriteLine("  --DONE: TestDBI_T_process_area_T4");
        }


        static void TestDBI_T_process_area_T5()
        {
            Console.WriteLine("  --START: TestDBI_T_process_area_T5");
#if Comment
            //Construct a brand new myTable in RAM
            SQLServerDB.process_area_Table myTable = new SQLServerDB.process_area_Table();

            int iRowsStart = 5;
            //put demo records into myTable
            for (int i = 1; i <= iRowsStart; i++)
            {
                SQLServerDB.process_area affItem = new SQLServerDB.process_area();
                affItem.process_areaId = i;
                affItem.process_areaName = "aff_Name_" + i.ToString();
                affItem.process_areaType = "aff_Type_" + i.ToString();
                affItem.specificGoal = "aff_specificGoal_" + i.ToString();
                affItem.specificPractice = "aff_specificPractice_" + i.ToString();
                affItem.genericGoal = "aff_genericGoal_" + i.ToString();
                affItem.genericPractice = "aff_genericPractice_" + i.ToString();
                affItem.processArea = "aff_processArea_" + i.ToString();
                affItem.projectId = 404;  //setting each item to the same projectID to support find by projectID

                myTable.itemList.Add(affItem);
            }

            //Count SQLServerDB process_area table rows before clearing
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
#endif
            Console.WriteLine("  --DONE: TestDBI_T_process_area_T5");
        }


        static List<process_area> make_process_area_list_1()
        {
            List<process_area> myList = new List<process_area>()
            {
                //process_area(int val_processAreaId, int val_projectID, string val_name, string val_text,     bool val_active, bool val_hasArtifact, bool val_hasAffirmation, string val_rating, bool val_coverage)
             new process_area(1, 1, "name_1", "text_1", true, true, true, "rating", true),
             new process_area(2, 2, "name_1", "text_1", true, true, true, "rating", true),
             new process_area(3, 3, "name_1", "text_1", true, true, true, "rating", true),
             new process_area(4, 4, "name_1", "text_1", true, true, true, "rating", true),
             new process_area(5, 5, "name_1", "text_1", true, true, true, "rating", true)
          };
            return myList;
        }//make_process_area_list_1


        static List<process_area> make_process_area_list_2()
        {
            List<process_area> myList = new List<process_area>()
           {
                //process_area(int val_processAreaId, int val_projectID, string val_name, string val_text,     bool val_active, bool val_hasArtifact, bool val_hasAffirmation, string val_rating, bool val_coverage)
             new process_area(1, 1, "name_1", "text_1", true, true, true, "rating", true),
             new process_area(2, 2, "name_1", "text_1", true, true, true, "rating", true),
             new process_area(3, 3, "name_1", "text_1", true, true, true, "rating", true),
             new process_area(4, 4, "name_1", "text_1", true, true, true, "rating", true),
             new process_area(5, 5, "name_1", "text_1", true, true, true, "rating", true)
           };
            return myList;
        }//make_process_area_list_2

    }
}