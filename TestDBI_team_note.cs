﻿using System;
using System.Collections.Generic;
using SQLServerDB;


namespace TestDBI
{
    class TestDBI_team_note
    {

        //--------------------------------------------------------------------------------------------
        public static void SelectTest()
        {
            Console.WriteLine("  --START: TestDBI_team_note.SelectTest()");

            switch (Program.iSubMenuSelection())
            {
                case 1:
                    TestDBI_T_team_note_Write_to_DB();
                    break;
                case 2:
                    TestDBI_T_team_note_Read_from_DB();
                    break;
                case 3:
                    TestDBI_T_team_note_T3();
                    break;
                case 4:
                    TestDBI_T_team_note_T4();
                    break;
                case 5:
                    TestDBI_T_team_note_T5();
                    break;
                case 10:
                    TestDBI_T_team_note_AutoCheck();
                    break;

                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_team_note.SelectTest()");
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// TestDBI_T_team_note_Write_to_DB -- write itemlist to DB
        /// </summary>
        static void TestDBI_T_team_note_Write_to_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_team_note_Write_to_DB");

            team_note_Table myTable = new team_note_Table();
            myTable.itemList = make_team_note_list_1();
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

            Console.WriteLine("  --DONE: TestDBI_T_team_note_Write_to_DB");
        }//TestDBI_T_team_note_Write_to_DB


        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// TestDBI_T_team_note_Read_from_DB -- read from DB
        /// </summary>
        static void TestDBI_T_team_note_Read_from_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_team_note_Read_from_DB");

            SQLServerDB.team_note_Table myTable = new SQLServerDB.team_note_Table();

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
            Console.WriteLine("  --DONE: TestDBI_T_team_note_Read_from_DB");
        }//TestDBI_T_team_note_Read_from_DB



        /// <summary>
        /// TestDBI_T_team_note_T3 -  clear the SQLServer team_note table, write some demo data to SQLServer DB, 
        /// query the affirmatin table by Project ID, 
        /// </summary>
        static void TestDBI_T_team_note_T3()
        {
            Console.WriteLine("  --START: TestDBI_T_team_note_T3");

            //Construct myTable in RAM
            SQLServerDB.team_note_Table myTable = new SQLServerDB.team_note_Table();
            myTable.itemList = make_team_note_list_3();
            int iRowsStart = myTable.itemList.Count;

            //Count SQLServerDB team_note table rows before clearing
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

            SQLServerDB.team_note itemSeek = myTable.itemList[0];
            itemSeek.Show();

            Util.pause();
            

            Console.WriteLine("  --DONE: TestDBI_T_team_note_T3");
        }

        /// <summary>
        /// TestDBI_T_team_note_T4 -- 
        /// </summary>
        static void TestDBI_T_team_note_T4()
        {
            Console.WriteLine("  --START: TestDBI_T_team_note_T4");

            //Construct a brand new myTable in RAM
            SQLServerDB.team_note_Table myTable = new SQLServerDB.team_note_Table();
            myTable.itemList = make_team_note_list_4();
            int iRowsStart = myTable.itemList.Count;


            //Count SQLServerDB team_note table rows before clearing
            int iRows = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows.ToString());

            Console.WriteLine("  --before clear SQLServer database table");
            Util.pause();

            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows2.ToString());
            Util.pause();

            foreach (SQLServerDB.team_note r in myTable.itemList)
            {
                myTable.WriteItemToDatabase(r);
            }
            Console.WriteLine("after writing to SQLServerDB");
            Util.pause();

            int iRows3 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows3.ToString());
            Util.pause();


            myTable.itemList = make_team_note_list_4B();
            int iRowsUpdate = myTable.itemList.Count;
            
            Console.WriteLine("---update the table");

            //revise the myTable contents
            myTable.itemList.Clear();


            Console.WriteLine("BEFORE the table update");
            Util.pause();
            myTable.UpdateItemListToDatabase();
            Console.WriteLine("AFTER the table update");
            Util.pause();

#if rewrite

            for (int i = 4; i <= 6; i++)
                myTable.Clear_Database_Table_By_team_noteID(i);
            Console.WriteLine("AFTER the table record deletions:  team_noteI={4,5,6}");
            Util.pause();

            myTable.Clear_Database_Table_By_projectD(102);
            myTable.Clear_Database_Table_By_projectD(108);
            Console.WriteLine("AFTER the table record deletions:  ProjectID = {102, 108}");
            Util.pause();
#endif

            Console.WriteLine("  --DONE: TestDBI_T_team_note_T4");
        }


        static void TestDBI_T_team_note_T5()
        {
            Console.WriteLine("  --START: TestDBI_T_team_note_T5");

            //Construct a brand new myTable in RAM
            SQLServerDB.team_note_Table myTable = new SQLServerDB.team_note_Table();

#if rewrite
            int iRowsStart = 5;
            //put demo records into myTable
            for (int i = 1; i <= iRowsStart; i++)
            {
                SQLServerDB.team_note affItem = new SQLServerDB.team_note();
                affItem.team_noteId = i;
                affItem.team_noteName = "aff_Name_" + i.ToString();
                affItem.team_noteType = "aff_Type_" + i.ToString();
                affItem.specificGoal = "aff_specificGoal_" + i.ToString();
                affItem.specificPractice = "aff_specificPractice_" + i.ToString();
                affItem.genericGoal = "aff_genericGoal_" + i.ToString();
                affItem.genericPractice = "aff_genericPractice_" + i.ToString();
                affItem.processArea = "aff_processArea_" + i.ToString();
                affItem.projectId = 404;  //setting each item to the same projectID to support find by projectID

                myTable.itemList.Add(affItem);
            }

            //Count SQLServerDB team_note table rows before clearing
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
            Console.WriteLine("  --DONE: TestDBI_T_team_note_T5");
        }


        static List<team_note> make_team_note_list_1()
        {
            List<team_note> myList = new List<team_note>()
            {
             //team_note(String val_notes, String val_processArea,         String val_specificGoal, String val_specificPractice,    String val_genericGoal, String val_genericPractice,   int val_projectId)
            new  team_note( "note_1", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1", 1),
            new  team_note( "note_2", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
            new  team_note( "note_2", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
            new  team_note( "note_2", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
            new  team_note( "note_2", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1",5),
           };
            return myList;
        }//make_team_note_list_1


        static List<team_note> make_team_note_list_2()
        {
            List<team_note> myList = new List<team_note>()
           {
             //team_note(String val_notes, String val_processArea,         String val_specificGoal, String val_specificPractice,    String val_genericGoal, String val_genericPractice,   int val_projectId)
            new  team_note( "note_1", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1", 1),
            new  team_note( "note_2", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
            new  team_note( "note_2", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
            new  team_note( "note_2", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
            new  team_note( "note_2", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1",5),
           };
            return myList;
        }//make_team_note_list_2

        static List<team_note> make_team_note_list_3()
        {
            List<team_note> myList = new List<team_note>()
           {
             //team_note(String val_notes, String val_processArea,         String val_specificGoal, String val_specificPractice,    String val_genericGoal, String val_genericPractice,   int val_projectId)
            new  team_note( "note_1", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1", 1),
            new  team_note( "note_2", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
            new  team_note( "note_2", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
            new  team_note( "note_2", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
            new  team_note( "note_2", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1",5),
           };
            return myList;
        }//make_team_note_list_3



        static List<team_note> make_team_note_list_4()
        {
            List<team_note> myList = new List<team_note>()
           {
             //team_note(String val_notes, String val_processArea,         String val_specificGoal, String val_specificPractice,    String val_genericGoal, String val_genericPractice,   int val_projectId)
            new  team_note( "note_1", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1", 1),
            new  team_note( "note_2", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
            new  team_note( "note_2", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
            new  team_note( "note_2", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
            new  team_note( "note_2", "proc_area_1", "sg_1", "sp_1", "gg_1", "gp_1",5),
           };
            return myList;
        }//make_team_note_list_4

        static List<team_note> make_team_note_list_4B()
        {
            List<team_note> myList = new List<team_note>()
           {
             //team_note(String val_notes, String val_processArea,         String val_specificGoal, String val_specificPractice,    String val_genericGoal, String val_genericPractice,   int val_projectId)

            new  team_note( "note_2", "proc_area_1_A", "sg_1", "sp_1", "gg_1", "gp_1", 2),
            new  team_note( "note_2", "proc_area_1_B", "sg_1", "sp_1", "gg_1", "gp_1", 3),
            new  team_note( "note_2", "proc_area_1_C", "sg_1", "sp_1", "gg_1", "gp_1", 4),

           };
            return myList;
        }//make_team_note_list_4



        static void TestDBI_T_team_note_AutoCheck()
        {
            Console.WriteLine("START: TestDBI_T_team_note_AutoCheck()");
            int iResult;
            
            iResult = TestDBI_T_team_note_AutoCheck_WriteRead();
            if (iResult == 0)
                Console.WriteLine("OK: TestDBI_T_team_note_AutoCheck_WriteRead");
            else
                Console.WriteLine("ERROR: TestDBI_T_team_note_AutoCheck_WriteRead:    iResult=" + iResult);


            iResult = TestDBI_T_team_note_AutoCheck_Update();
            if (iResult == 0)
                Console.WriteLine("OK: TestDBI_T_team_note_AutoCheck_Update");
            else
                Console.WriteLine("ERROR: TestDBI_T_team_note_AutoCheck_Update:    iResult=" + iResult);

            iResult = TestDBI_T_team_note_AutoCheck_Update();


            Console.WriteLine("DONE: TestDBI_T_team_note_AutoCheck()");
        }


        /// <summary>
        /// TestDBI_T_team_note_AutoCheck_WriteRead - Write,Read,Compare Item List;
        /// 1.1) Create test data: myTable1;
        /// 1.2) Clear DBTable;
        /// 1.3) Write myTable1 to DBTable; 
        /// 1.4) Get DBTable.CountRows, compare (myTable1.itemList.Count == DBTable.CountRows)
        /// 1.5) Read myTable2 from DBTable
        /// 1.6) Compare tables (myTable1 == myTable2)
        /// </summary>
        /// <returns></returns>
        static int TestDBI_T_team_note_AutoCheck_WriteRead()
        {
            const int OK = 0;
            int iResult = OK;
            Console.WriteLine("START: TestDBI_T_team_note_AutoCheck_WriteRead()");

            // 1.1) CreateTestData1: myTable1
            team_note_Table myTable1 = new team_note_Table();
            myTable1.itemList = new List<team_note>()
            {
                // team_note(String val_notes, String val_processArea, String val_specificGoal, String val_specificPractice,
           //String val_genericGoal, String val_genericPractice, int val_projectId)
           new team_note("notes_1", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 1),
           new team_note("notes_2", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
           new team_note("notes_3", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
           new team_note("notes_4", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
           new team_note("notes_5", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 5)
            };
            int iRowsAtStart = myTable1.itemList.Count;

            // 1.2) ClearDBTable
            myTable1.Clear_Database_Table();
            int iRowsAfterClear = myTable1.CountRows();
            if (iRowsAfterClear != 0)
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be empty after Clear_Database_Table.  iRowsAfterClear=" + iRowsAfterClear);
                return iResult;
            }

            // 1.3) Write myTable1 to DBTable 
            myTable1.WriteItemListToDatabase();

            // 1.4) Get DBTable.CountRows, compare (myTable1.itemList.Count == DBTable.CountRows)
            int iRowsAfterWriteItemListr = myTable1.CountRows();
            if (iRowsAfterWriteItemListr != iRowsAtStart)
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as iRowsAtStart after WriteItemListToDatabase.  iRowsAfterWriteItemListr=" + iRowsAfterWriteItemListr);
                return iResult;
            }

            /// 1.5) Read myTable2 from DBTable
            team_note_Table myTable2 = new team_note_Table();
            myTable2.ReadItemListFromDatabase();

            /// 1.6) Compare tables (myTable1 == myTable2)
            if (!TestDBI_T_team_note_CompareLists(myTable1.itemList, myTable2.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as test data");
                return iResult;
            }
            Console.WriteLine("OK!  DBTable & test data match");

            Console.WriteLine("DONE: TestDBI_T_team_note_AutoCheck_WriteRead()");
            return iResult;
        }


        /// <summary>
        /// TestDBI_T_team_note_AutoCheck_Update - Update Item List;
        /// 1.1) Create test data: myTable1;
        /// 1.2) Clear DBTable;
        /// 1.3) Write myTable1 to DBTable; 
        /// 1.4) Get DBTable.CountRows, compare (myTable1.itemList.Count == DBTable.CountRows)
        /// 1.5) Read myTable2 from DBTable
        /// 1.6) Compare tables (myTable1 == myTable2)
        /// 1.7) Create the update table (myTableUpdate)
        /// 1.8) Update TableDB
        /// 1.9) Read myTable3
        /// 1.10) Compare tables.itemLists(myTableUpdate == myTable3)
        /// </summary>
        /// <returns></returns>
        static int TestDBI_T_team_note_AutoCheck_Update()
        {
            const int OK = 0;
            int iResult = OK;
            Console.WriteLine("START: TestDBI_T_team_note_AutoCheck_Update()");

            // 1.1) CreateTestData1: myTable1
            team_note_Table myTable1 = new team_note_Table();
            myTable1.itemList = new List<team_note>()
            {
              // team_note(String val_notes, String val_processArea, String val_specificGoal, String val_specificPractice,
           //String val_genericGoal, String val_genericPractice, int val_projectId)
           new team_note("notes_1", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 1),
           new team_note("notes_2", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
           new team_note("notes_3", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
           new team_note("notes_4", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
           new team_note("notes_5", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 5)
            };
            int iRowsAtStart = myTable1.itemList.Count;

            // 1.2) ClearDBTable
            myTable1.Clear_Database_Table();
            int iRowsAfterClear = myTable1.CountRows();
            if (iRowsAfterClear != 0)
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be empty after Clear_Database_Table.  iRowsAfterClear=" + iRowsAfterClear);
                return iResult;
            }

            // 1.3) Write myTable1 to DBTable 
            myTable1.WriteItemListToDatabase();

            // 1.4) Get DBTable.CountRows, compare (myTable1.itemList.Count == DBTable.CountRows)
            int iRowsAfterWriteItemListr = myTable1.CountRows();
            if (iRowsAfterWriteItemListr != iRowsAtStart)
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as iRowsAtStart after WriteItemListToDatabase.  iRowsAfterWriteItemListr=" + iRowsAfterWriteItemListr);
                return iResult;
            }

            /// 1.5) Read myTable2 from DBTable
            team_note_Table myTable2 = new team_note_Table();
            myTable2.ReadItemListFromDatabase();

            /// 1.6) Compare tables (myTable1 == myTable2)
            if (!TestDBI_T_team_note_CompareLists(myTable1.itemList, myTable2.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as test data");
                return iResult;
            }

            /// 1.7) Create the update table (myTableUpdate)
            team_note_Table myTableUpdate = new team_note_Table();
            myTableUpdate.itemList = new List<team_note>()
            {
              // team_note(String val_notes, String val_processArea, String val_specificGoal, String val_specificPractice,
           //String val_genericGoal, String val_genericPractice, int val_projectId)
           new team_note("notes_1", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 1),
           new team_note("notes_2", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1-update", 2),
           new team_note("notes_3", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
           new team_note("notes_4", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1-update", 4),
           new team_note("notes_5", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 5)
            };


            //1.8) Update TableDB
            myTableUpdate.UpdateItemListToDatabase();

            //1.9) Read myTable3
            team_note_Table myTable3 = new team_note_Table();
            myTable3.ReadItemListFromDatabase();


            //1.10) Compare tables.itemLists (myTableUpdate == myTable3)
            if (!TestDBI_T_team_note_CompareLists(myTableUpdate.itemList, myTable3.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as the update table");
                return iResult;
            }


            Console.WriteLine("DONE: TestDBI_T_team_note_AutoCheck_Update()");
            return iResult;
        }



        /// <summary>
        /// TestDBI_T_team_note_CompareLists --
        ///   true if same contents
        ///   false if there are any differences
        /// </summary>
        /// <param name="itemList1"></param>
        /// <param name="itemList2"></param>
        /// <returns></returns>
        static bool TestDBI_T_team_note_CompareLists(List<team_note> itemList1, List<team_note> itemList2)
        {
            if (itemList1.Count != itemList2.Count)
                return false;

            SortedList<int, team_note> sorteditemList1 = new SortedList<int, team_note>();
            foreach (var r in itemList1)
                sorteditemList1.Add(r.projectId, r);//sort by key:  r.projectId

            SortedList<int, team_note> sorteditemList2 = new SortedList<int, team_note>();
            foreach (var r in itemList2)
                sorteditemList2.Add(r.projectId, r); //sort by key:  r.projectId

            //compare sorted lists for equivalence for each row of data
            foreach (var iKey in sorteditemList1.Keys)
            {
                //method Compare directly compares each field individually
                if (!sorteditemList1[iKey].Equals(sorteditemList2[iKey]))
                    return false;
            }
            return true;
        }//TestDBI_T_team_note_CompareLists

    }
}
