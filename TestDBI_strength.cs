using System;
using System.Collections.Generic;
using SQLServerDB;


namespace TestDBI
{
    class TestDBI_strength
    {

        //--------------------------------------------------------------------------------------------
        public static void SelectTest()
        {
            Console.WriteLine("  --START: TestDBI_strength.SelectTest()");

            switch (Program.iSubMenuSelection())
            {
                case 1:
                    TestDBI_T_strength_Write_to_DB();
                    break;
                case 2:
                    TestDBI_T_strength_Read_from_DB();
                    break;
                case 3:
                    TestDBI_T_strength_T3();
                    break;
                case 4:
                    TestDBI_T_strength_T4();
                    break;
                case 5:
                    TestDBI_T_strength_T5();
                    break;
                case 10:
                    TestDBI_T_strength_AutoCheck();
                    break;

                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_strength.SelectTest()");
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// TestDBI_T_strength_Write_to_DB -- write itemlist to DB
        /// </summary>
        static void TestDBI_T_strength_Write_to_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_strength_Write_to_DB");

            strength_Table myTable = new strength_Table();
            myTable.itemList = make_strength_list_1();
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

            Console.WriteLine("  --DONE: TestDBI_T_strength_Write_to_DB");
        }//TestDBI_T_strength_Write_to_DB


        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// TestDBI_T_strength_Read_from_DB -- read from DB
        /// </summary>
        static void TestDBI_T_strength_Read_from_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_strength_Read_from_DB");

            SQLServerDB.strength_Table myTable = new SQLServerDB.strength_Table();

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
            Console.WriteLine("  --DONE: TestDBI_T_strength_Read_from_DB");
        }//TestDBI_T_strength_Read_from_DB



        /// <summary>
        /// TestDBI_T_strength_T3 -  clear the SQLServer strength table, write some demo data to SQLServer DB, 
        /// query the affirmatin table by Project ID, 
        /// </summary>
        static void TestDBI_T_strength_T3()
        {
            Console.WriteLine("  --START: TestDBI_T_strength_T3");

            //Construct myTable in RAM
            SQLServerDB.strength_Table myTable = new SQLServerDB.strength_Table();

            //put demo records into myTable
            for (int i = 1; i < 10; i++)
            {
                SQLServerDB.strength sItem = new SQLServerDB.strength();
                sItem.ID = i;
                sItem.notes = "aff_Name_" + i.ToString();
                sItem.processArea = "aff_Type_" + i.ToString();
                sItem.specificGoal = "aff_specificGoal_" + i.ToString();
                sItem.specificPractice = "aff_specificPractice_" + i.ToString();
                sItem.genericGoal = "aff_genericGoal_" + i.ToString();
                sItem.genericPractice = "aff_genericPractice_" + i.ToString();
                sItem.projectId = i;

                myTable.itemList.Add(sItem);
            }
            
            //Count SQLServerDB strength table rows before clearing
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

            SQLServerDB.strength strengthItem = myTable.itemList[0];
            strengthItem.Show();

            Util.pause();

            Console.WriteLine("  --DONE: TestDBI_T_strength_T3");
        }

        /// <summary>
        /// TestDBI_T_strength_T4 -- 
        /// </summary>
        static void TestDBI_T_strength_T4()
        {
            Console.WriteLine("  --START: TestDBI_T_strength_T4");

            //Construct a brand new myTable in RAM
            SQLServerDB.strength_Table myTable = new SQLServerDB.strength_Table();
            myTable.itemList = new List<strength>()
            {
            //strength(String val_notes, String val_processArea, String val_specificGoal, String val_specificPractice, String val_genericGoal, String val_genericPractice,   int val_projectId)
            new strength("notes_1", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 1),
            new strength("notes_2", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
            new strength("notes_3", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
            new strength("notes_4", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
            new strength("notes_5", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 5)
            };
            int iRowsStart = myTable.itemList.Count;
            myTable.Clear_Database_Table();
            int iRows1 = myTable.CountRows();
            if (iRows1 != iRowsStart)
                Console.WriteLine("ERR:  iRows should be zero after Clear_Database_Table");
            Util.pause();

            myTable.WriteItemListToDatabase();
            int iRows2 = myTable.CountRows();
            if (iRows2 != iRowsStart)
                Console.WriteLine("ERR: iRows2 should equal iRowsStart!  iRows2=" + iRows2);
            else
               Console.WriteLine("OK");

            //Construct a brand new myTable in RAM
            SQLServerDB.strength_Table myTable2 = new SQLServerDB.strength_Table();
            myTable2.itemList = new List<strength>()
            {
            //strength(String val_notes, String val_processArea, String val_specificGoal, String val_specificPractice, String val_genericGoal, String val_genericPractice,   int val_projectId)
            new strength("notes_1A", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 1),
            new strength("notes_2B", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
            new strength("notes_3", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
            new strength("notes_4D", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
            new strength("notes_5E", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 5)
            };
            
            Console.WriteLine("---update the table");
 
            Console.WriteLine("BEFORE the table update");
            myTable2.Show();
            Util.pause("ready to update");

            myTable.UpdateItemListToDatabase();
            Console.WriteLine("AFTER the table update");
            myTable2.Show();
            Util.pause();
            
            Console.WriteLine("  --DONE: TestDBI_T_strength_T4");
        }
        
        static void TestDBI_T_strength_T5()
        {
            Console.WriteLine("  --START: TestDBI_T_strength_T5");

            //Construct a brand new myTable in RAM
            SQLServerDB.strength_Table myTable = new SQLServerDB.strength_Table();
            myTable.itemList = new List<strength>()
            {
            //strength(String val_notes, String val_processArea, String val_specificGoal, String val_specificPractice, String val_genericGoal, String val_genericPractice,   int val_projectId)
            new strength("notes_1", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 404),
            new strength("notes_2", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 404),
            new strength("notes_3", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 404),
            new strength("notes_4", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 404),
            new strength("notes_5", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 404)
            };
            int iRowsStart = myTable.itemList.Count;

            //Count SQLServerDB strength table rows before clearing
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

            Console.WriteLine("  --DONE: TestDBI_T_strength_T5");
        }


        static List<strength> make_strength_list_1()
        {
            List<strength> myList = new List<strength>()
            {
            //strength(String val_notes, String val_processArea, String val_specificGoal, String val_specificPractice, String val_genericGoal, String val_genericPractice,   int val_projectId)
            new strength("notes_1", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 1),
            new strength("notes_2", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
            new strength("notes_3", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
            new strength("notes_4", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
            new strength("notes_5", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 5)
            };
            return myList;
        }//make_strength_list_1


        static List<strength> make_strength_list_2()
        {
            List<strength> myList = new List<strength>()
            {
            //strength(String val_notes, String val_processArea, String val_specificGoal, String val_specificPractice, String val_genericGoal, String val_genericPractice,   int val_projectId)
            new strength("notes_1", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 1),
            new strength("notes_2", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
            new strength("notes_3", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
            new strength("notes_4", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
            new strength("notes_5", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 5)
            };
            return myList;
        }//make_strength_list_2


        static void TestDBI_T_strength_AutoCheck()
        {
            Console.WriteLine("START: TestDBI_T_strength_AutoCheck()");
            int iResult;
             
            iResult = TestDBI_T_strength_AutoCheck_WriteRead();
            if (iResult == 0)
                Console.WriteLine("OK: TestDBI_T_strength_AutoCheck_WriteRead");
            else
                Console.WriteLine("ERROR: TestDBI_T_strength_AutoCheck_WriteRead:    iResult=" + iResult);


            iResult = TestDBI_T_strength_AutoCheck_Update();
            if (iResult == 0)
                Console.WriteLine("OK: TestDBI_T_strength_AutoCheck_Update");
            else
                Console.WriteLine("ERROR: TestDBI_T_strength_AutoCheck_Update:    iResult=" + iResult);

            iResult = TestDBI_T_strength_AutoCheck_Update();


            Console.WriteLine("DONE: TestDBI_T_strength_AutoCheck()");
        }


        /// <summary>
        /// TestDBI_T_strength_AutoCheck_WriteRead - Write,Read,Compare Item List;
        /// 1.1) Create test data: myTable1;
        /// 1.2) Clear DBTable;
        /// 1.3) Write myTable1 to DBTable; 
        /// 1.4) Get DBTable.CountRows, compare (myTable1.itemList.Count == DBTable.CountRows)
        /// 1.5) Read myTable2 from DBTable
        /// 1.6) Compare tables (myTable1 == myTable2)
        /// </summary>
        /// <returns></returns>
        static int TestDBI_T_strength_AutoCheck_WriteRead()
        {
            const int OK = 0;
            int iResult = OK;
            Console.WriteLine("START: TestDBI_T_strength_AutoCheck_WriteRead()");

            // 1.1) CreateTestData1: myTable1
            strength_Table myTable1 = new strength_Table();
            myTable1.itemList = new List<strength>()
            {
                //strength(String val_notes, String val_processArea, String val_specificGoal, String val_specificPractice, String val_genericGoal, String val_genericPractice,   int val_projectId)
            new strength("notes_1", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 1),
            new strength("notes_2", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
            new strength("notes_3", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
            new strength("notes_4", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
            new strength("notes_5", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 5)
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
            strength_Table myTable2 = new strength_Table();
            myTable2.ReadItemListFromDatabase();

            /// 1.6) Compare tables (myTable1 == myTable2)
            if (!TestDBI_T_strength_CompareLists(myTable1.itemList, myTable2.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as test data");
                return iResult;
            }
            Console.WriteLine("OK!  DBTable & test data match");

            Console.WriteLine("DONE: TestDBI_T_strength_AutoCheck_WriteRead()");
            return iResult;
        }


        /// <summary>
        /// TestDBI_T_strength_AutoCheck_Update - Update Item List;
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
        static int TestDBI_T_strength_AutoCheck_Update()
        {
            const int OK = 0;
            int iResult = OK;
            Console.WriteLine("START: TestDBI_T_strength_AutoCheck_Update()");

            // 1.1) CreateTestData1: myTable1
            strength_Table myTable1 = new strength_Table();
            myTable1.itemList = new List<strength>()
            {
                //strength(String val_notes, String val_processArea, String val_specificGoal, String val_specificPractice, String val_genericGoal, String val_genericPractice,   int val_projectId)
            new strength("notes_1", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 1),
            new strength("notes_2", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
            new strength("notes_3", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
            new strength("notes_4", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
            new strength("notes_5", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 5)
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
            strength_Table myTable2 = new strength_Table();
            myTable2.ReadItemListFromDatabase();

            /// 1.6) Compare tables (myTable1 == myTable2)
            if (!TestDBI_T_strength_CompareLists(myTable1.itemList, myTable2.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as test data");
                return iResult;
            }

            /// 1.7) Create the update table (myTableUpdate)
            strength_Table myTableUpdate = new strength_Table();
            myTableUpdate.itemList = new List<strength>()
            {
                //strength(String val_notes, String val_processArea, String val_specificGoal, String val_specificPractice, String val_genericGoal, String val_genericPractice,   int val_projectId)
            new strength("notes_1", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 1),
            new strength("notes_2", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1--update", 2),
            new strength("notes_3", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
            new strength("notes_4", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1-update", 4),
            new strength("notes_5", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 5)
            };


            //1.8) Update TableDB
            myTableUpdate.UpdateItemListToDatabase();

            //1.9) Read myTable3
            strength_Table myTable3 = new strength_Table();
            myTable3.ReadItemListFromDatabase();


            //1.10) Compare tables.itemLists (myTableUpdate == myTable3)
            if (!TestDBI_T_strength_CompareLists(myTableUpdate.itemList, myTable3.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as the update table");
                return iResult;
            }


            Console.WriteLine("DONE: TestDBI_T_strength_AutoCheck_Update()");
            return iResult;
        }


        /// <summary>
        /// TestDBI_T_strength_CompareLists --
        ///   true if same contents
        ///   false if there are any differences
        /// </summary>
        /// <param name="itemList1"></param>
        /// <param name="itemList2"></param>
        /// <returns></returns>
        static bool TestDBI_T_strength_CompareLists(List<strength> itemList1, List<strength> itemList2)
        {
            if (itemList1.Count != itemList2.Count)
                return false;

            SortedList<int, strength> sorteditemList1 = new SortedList<int, strength>();
            foreach (var r in itemList1)
                sorteditemList1.Add(r.projectId, r);//sort by key:  r.projectId

            SortedList<int, strength> sorteditemList2 = new SortedList<int, strength>();
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
        }//TestDBI_T_strength_CompareLists


    }
}
