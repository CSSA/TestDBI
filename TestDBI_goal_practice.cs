using System;
using System.Collections.Generic;
using SQLServerDB;

namespace TestDBI
{
    class TestDBI_goal_practice
    {

        public static void SelectTest()
        {
            Console.WriteLine("  --START: TestDBI_goal_practice.SelectTest()");

            switch (Program.iSubMenuSelection())
            {
                case 1:
                    TestDBI_T_goal_practice_Write_to_DB();
                    break;
                case 2:
                    TestDBI_T_goal_practice_Read_from_DB();
                    break;
                case 3:
                    TestDBI_T_goal_practice_T3();
                    break;
                case 4:
                    TestDBI_T_goal_practice_T4();
                    break;
                case 5:
                    TestDBI_T_goal_practice_T5();
                    break;
                case 10:
                    TestDBI_T_goal_practice_AutoCheck();
                    break;
                    

                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_goal_practice.SelectTest()");
        }


        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_goal_practice_Write_to_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_goal_practice_Write_to_DB");

            goal_practice_Table myTable = new goal_practice_Table();
            myTable.itemList = make_goal_practice_list_1();
            myTable.Show();
            int iRowsStart = myTable.itemList.Count;

            myTable.Clear_Database_Table();
            int iRows1 = myTable.CountRows();
            if (iRows1 != 0)
                Console.WriteLine("Error!  iRows1 after Clear_Database_Table() should be 0");
            else
                Console.WriteLine("OK.  After Clear_Database_Table()");

            Console.WriteLine("Write the table from RAM to the Database");
            myTable.WriteItemListToDatabase();


            int iRows2 = myTable.CountRows();
            if (iRows2 != iRowsStart)
                Console.WriteLine("Error!  iRows2 after WriteItemListToDatabase() should be " + iRowsStart);
            else
                Console.WriteLine("OK. After WriteItemListToDatabase()");

            Util.pause("visually inspect via SSMS?");

            Console.WriteLine("  --DONE: TestDBI_T_goal_practice_Write_to_DB");
        }//TestDBI_T_goal_practice_Write_to_DB



        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_goal_practice_Read_from_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_goal_practice_Read_From_DB");

            goal_practice_Table myTable = new goal_practice_Table();
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

            Console.WriteLine("  --DONE: TestDBI_T_goal_practice_Read_From_DB");
        }//TestDBI_T_goal_practice_Read_From_DB

        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_goal_practice_T3()
        {
            Console.WriteLine("  --START: TestDBI_T_goal_practice_T3");


            //Construct myTable in RAM
            SQLServerDB.goal_practice_Table myTable = new SQLServerDB.goal_practice_Table();


            //put demo records into myTable in RAM
            myTable.itemList.Clear();
            for (int i = 1; i < 10; i++)
            {
                SQLServerDB.goal_practice goal_practiceItem = new SQLServerDB.goal_practice();
                goal_practiceItem.ID = i;  //actually, a don't care; it will not be stored
                goal_practiceItem.nodeId = i;    // Test only demonstrates storing/retreiving integers.
                goal_practiceItem.processAreaId = i ;        // Test only demonstrates storing/retreiving integers.
                goal_practiceItem.projectId = i ;                // Test only demonstrates storing/retreiving integers.
                goal_practiceItem.name = "name_" + i.ToString();
                goal_practiceItem.isGoal = Convert.ToBoolean(i % 2);
                goal_practiceItem.isPractice = Convert.ToBoolean(i % 2); // Alternate between true/false
                goal_practiceItem.rating = "rating_" + i.ToString();
                goal_practiceItem.coverage = Convert.ToBoolean(i % 2);
                myTable.itemList.Add(goal_practiceItem);
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


            //put demo records into myTable in RAM
            myTable.itemList.Clear();
            for (int i = 1; i < 10; i++)
            {
                SQLServerDB.goal_practice goal_practiceItem = new SQLServerDB.goal_practice();
 
                goal_practiceItem.ID = i;  //actually, a don't care; it will not be stored
                goal_practiceItem.nodeId = i;    // Test only demonstrates storing/retreiving integers.
                goal_practiceItem.processAreaId = i ;        // Test only demonstrates storing/retreiving integers.
                goal_practiceItem.projectId = i ;            //  this one, used for matching, should not change 
                goal_practiceItem.name = "name_" + i.ToString();  //  this one, used for matching, should not change 
                goal_practiceItem.isGoal = Convert.ToBoolean(i % 2);
                goal_practiceItem.isPractice = Convert.ToBoolean(i % 2); // Alternate between true/false
                goal_practiceItem.rating = "rating_" + i.ToString() + "_REV-A";
                goal_practiceItem.coverage = Convert.ToBoolean(i % 2);

                myTable.itemList.Add(goal_practiceItem);
            }
            myTable.Show();
            Util.pause("--BEFORE the update, showing the planned updates in myTable.itemList");

            myTable.UpdateItemListToDatabase();
            Util.pause("-- AFTER the update, examine the goal_practice Table using SSMS");

            Console.WriteLine("  --DONE: TestDBI_T_goal_practice_T3");
        }//TestDBI_T_goal_practice_T3


        static void TestDBI_T_goal_practice_T4()
        {
            Console.WriteLine("  --START: TestDBI_T_goal_practice_T4");
            Console.WriteLine("  -----   TBD:   do something here??");
            Console.WriteLine("  --DONE: TestDBI_T_goal_practice_T4");
        }

        static void TestDBI_T_goal_practice_T5()
        {
            Console.WriteLine("  --START: TestDBI_T_goal_practice_T5");
            Console.WriteLine("  -----   TBD:   do something here??");
            Console.WriteLine("  --DONE: TestDBI_T_goal_practice_T5");
        }



        static List<goal_practice> make_goal_practice_list_1()
        {
            List<goal_practice> myList = new List<goal_practice>()
            {
                 //goal_practice(int val_nodeId, int val_processAreaId, int val_projectId, string val_name, bool val_isGoal, bool val_isPractice, string val_rating, bool val_coverage)
                 new goal_practice(1, 1, 1, "name", true, true, "str_rating", true),
                 new goal_practice(1, 1, 1, "name", true, true, "str_rating", true)

           };
            return myList;
        }//make_affirmation_list_1





        static void TestDBI_T_goal_practice_AutoCheck()
        {
            Console.WriteLine("START: TestDBI_T_goal_practice_AutoCheck()");
            int iResult = TestDBI_T_goal_practice_AutoCheck_WriteRead();
            if (iResult == 0)
                Console.WriteLine("OK: TestDBI_T_goal_practice_AutoCheck_WriteRead");
            else
                Console.WriteLine("ERROR: TestDBI_T_goal_practice_AutoCheck_WriteRead:    iResult=" + iResult);

            Console.WriteLine("DONE: TestDBI_T_goal_practice_AutoCheck()");
        }


        /// <summary>
        /// TestDBI_T_goal_practice_AutoCheck_WriteRead - Write,Read,Compare Item List;
        /// 1.1) Create test data: myTable1;
        /// 1.2) Clear DBTable;
        /// 1.3) Write myTable1 to DBTable; 
        /// 1.4) Get DBTable.CountRows, compare (myTable1.itemList.Count == DBTable.CountRows)
        /// 1.5) Read myTable2 from DBTable
        /// 1.6) Compare tables (myTable1 == myTable2)
        /// </summary>
        /// <returns></returns>
        static int TestDBI_T_goal_practice_AutoCheck_WriteRead()
        {
            const int OK = 0;
            int iResult = OK;
            Console.WriteLine("START: TestDBI_T_goal_practice_AutoCheck_WriteRead()");

            // 1.1) CreateTestData1: myTable1
            goal_practice_Table myTable1 = new goal_practice_Table();
            myTable1.itemList = new List<goal_practice>()
            {
                // goal_practice(int val_nodeId, int val_processAreaId, int val_projectId, string val_name, bool val_isGoal, bool val_isPractice, string val_rating, bool val_coverage)
            new goal_practice(1, 1, 1, "name_1", true, true, "rating_1", true),
            new goal_practice(2, 1, 1, "name_2", true, true, "rating_2", true),
            new goal_practice(3, 1, 1, "name_3", true, true, "rating_3", true),
            new goal_practice(4, 1, 1, "name_4", true, true, "rating_4", true),
            new goal_practice(5, 1, 1, "name_5", true, true, "rating_5", true),
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
            goal_practice_Table myTable2 = new goal_practice_Table();
            myTable2.ReadItemListFromDatabase();

            /// 1.6) Compare tables (myTable1 == myTable2)
            if (!TestDBI_T_goal_practice_CompareLists(myTable1.itemList, myTable2.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as test data");
                return iResult;
            }
            Console.WriteLine("OK!  DBTable & test data match");

            Console.WriteLine("DONE: TestDBI_T_goal_practice_AutoCheck_WriteRead()");
            return iResult;
        }

        /// <summary>
        /// TestDBI_T_goal_practice_CompareLists --
        ///   true if same contents
        ///   false if there are any differences
        /// </summary>
        /// <param name="itemList1"></param>
        /// <param name="itemList2"></param>
        /// <returns></returns>
        static bool TestDBI_T_goal_practice_CompareLists(List<goal_practice> itemList1, List<goal_practice> itemList2)
        {
            if (itemList1.Count != itemList2.Count)
                return false;

            SortedList<int, goal_practice> sorteditemList1 = new SortedList<int, goal_practice>();
            foreach (var r in itemList1)
                sorteditemList1.Add(r.nodeId, r);//sort by key:  r.nodeId

            SortedList<int, goal_practice> sorteditemList2 = new SortedList<int, goal_practice>();
            foreach (var r in itemList2)
                sorteditemList2.Add(r.nodeId, r); //sort by key:  r.nodeId

            //compare sorted lists for equivalence for each row of data
            foreach (var iKey in sorteditemList1.Keys)
            {
                //method Compare directly compares each field individually
                if (!sorteditemList1[iKey].Equals(sorteditemList2[iKey]))
                    return false;
            }
            return true;
        }//TestDBI_T_goal_practice_CompareLists


    }
}
