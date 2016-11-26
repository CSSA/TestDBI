using System;
using System.Collections.Generic;
using SQLServerDB;


namespace TestDBI
{
     class TestDBI_interview_session
    {

        public static void SelectTest()
        {
            Console.WriteLine("  --START: TestDBI_interview_session.SelectTest()");

            switch (Program.iSubMenuSelection())
            {
                case 1:
                    TestDBI_T_interview_session_Write_to_DB();
                    break;
                case 2:
                    TestDBI_T_interview_session_Read_from_DB();
                    break;
                case 3:
                    TestDBI_T_interview_session_T3();
                    break;
                case 4:
                    TestDBI_T_interview_session_T4();
                    break;
                case 5:
                    TestDBI_T_interview_session_T5();
                    break;
                case 10:
                    TestDBI_T_interview_session_AutoCheck();
                    break;

                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_interview_session.SelectTest()");
        }


        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_interview_session_Write_to_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_session_Write_to_DB");

            SQLServerDB.interview_session_Table myTable = new SQLServerDB.interview_session_Table();

            myTable.itemList = make_interview_session_list_1();
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
            Console.WriteLine("  --DONE: TestDBI_T_interview_session_Write_to_DB");
        }//TestDBI_T_interview_session_Write_to_DB

        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_interview_session_Read_from_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_session_Read_from_DB");


            interview_session_Table myTable = new interview_session_Table();

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

            Console.WriteLine("  --DONE: TestDBI_T_interview_session_Read_from_DB");
        }//TestDBI_T_interview_session_Read_from_DB

        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_interview_session_T3()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_session_T3");

            Console.WriteLine("  -----   TBD:   do something here??");

            Console.WriteLine("  --DONE: TestDBI_T_interview_session_T3");
        }//TestDBI_T_interview_session_T3


        static void TestDBI_T_interview_session_T4()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_session_T4");
            Console.WriteLine("  -----   TBD:   do something here??");
            Console.WriteLine("  --DONE: TestDBI_T_interview_session_T4");
        }

        static void TestDBI_T_interview_session_T5()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_session_T5");
            Console.WriteLine("  -----   TBD:   do something here??");
            Console.WriteLine("  --DONE: TestDBI_T_interview_session_T5");
        }


        static List<interview_session> make_interview_session_list_1()
        {
            List<interview_session> myList = new List<interview_session>()
                         {
                        new  interview_session(1, 1, "SesName1", 1, 1, "sg_1", "sp_1", "gg_1", "gp_1", "pa_1"),
                        new  interview_session(2, 2, "SesName2", 2, 2, "sg_2", "sp_2", "gg_2", "gp_2", "pa_2"),
                        new  interview_session(3, 3, "SesName3", 3, 3, "sg_3", "sp_3", "gg_3", "gp_3", "pa_3"),
                    };
            return myList;
        }//makeList1

        static List<interview_session> make_interview_session_list_2()
        {
            List<interview_session> myList = new List<interview_session>()
                         {
                        new  interview_session(1, 1, "SesName1-Rev-A", 1, 1, "sg_1", "sp_1", "gg_1", "gp_1", "pa_1-Rev-A"),
                        new  interview_session(2, 2, "SesName2-Rev-A", 2, 2, "sg_2", "sp_2", "gg_2", "gp_2", "pa_2-Rev-A"),
                        new  interview_session(3, 3, "SesName3-Rev-A", 3, 3, "sg_3", "sp_3", "gg_3", "gp_3", "pa_3-Rev-A"),
                    };
            return myList;
        }//makeList1



        static void TestDBI_T_interview_session_AutoCheck()
        {
            Console.WriteLine("START: TestDBI_T_interview_session_AutoCheck()");
            int iResult = TestDBI_T_interview_session_AutoCheck_WriteRead();
            if (iResult == 0)
                Console.WriteLine("OK: TestDBI_T_interview_session_AutoCheck_WriteRead");
            else
                Console.WriteLine("ERROR: TestDBI_T_interview_session_AutoCheck_WriteRead:    iResult=" + iResult);

            Console.WriteLine("DONE: TestDBI_T_interview_session_AutoCheck()");
        }


        /// <summary>
        /// TestDBI_T_interview_session_AutoCheck_WriteRead - Write,Read,Compare Item List;
        /// 1.1) Create test data: myTable1;
        /// 1.2) Clear DBTable;
        /// 1.3) Write myTable1 to DBTable; 
        /// 1.4) Get DBTable.CountRows, compare (myTable1.itemList.Count == DBTable.CountRows)
        /// 1.5) Read myTable2 from DBTable
        /// 1.6) Compare tables (myTable1 == myTable2)
        /// </summary>
        /// <returns></returns>
        static int TestDBI_T_interview_session_AutoCheck_WriteRead()
        {
            const int OK = 0;
            int iResult = OK;
            Console.WriteLine("START: TestDBI_T_interview_session_AutoCheck_WriteRead()");

            // 1.1) CreateTestData1: myTable1
            interview_session_Table myTable1 = new interview_session_Table();
            myTable1.itemList = new List<interview_session>()
            {
                //  interview_session(int val_sessionId, int val_sessionIndex, String val_sessionName, int val_sessionDurationHours, int val_sessionDurationMinutes,
                //                  String val_specificGoal, String val_specificPractice,
                //                  String val_genericGoal, String val_genericPractice,
                //                  String val_processArea)
        
                new interview_session(1, 1, "session_name_1", 1, 2, "sg_1", "sp_1", "gg_1", "gp_1", "pa_1"),
                new interview_session(2, 1, "session_name_1", 1, 2, "sg_1", "sp_1", "gg_1", "gp_1", "pa_1"),
                new interview_session(3, 1, "session_name_1", 1, 2, "sg_1", "sp_1", "gg_1", "gp_1", "pa_1"),
                new interview_session(4, 1, "session_name_1", 1, 2, "sg_1", "sp_1", "gg_1", "gp_1", "pa_1"),
                new interview_session(5, 1, "session_name_1", 1, 2, "sg_1", "sp_1", "gg_1", "gp_1", "pa_1")
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
            interview_session_Table myTable2 = new interview_session_Table();
            myTable2.ReadItemListFromDatabase();

            /// 1.6) Compare tables (myTable1 == myTable2)
            if (!TestDBI_T_interview_session_CompareLists(myTable1.itemList, myTable2.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as test data");
                return iResult;
            }
            Console.WriteLine("OK!  DBTable & test data match");

            Console.WriteLine("DONE: TestDBI_T_interview_session_AutoCheck_WriteRead()");
            return iResult;
        }

        /// <summary>
        /// TestDBI_T_interview_session_CompareLists --
        ///   true if same contents
        ///   false if there are any differences
        /// </summary>
        /// <param name="itemList1"></param>
        /// <param name="itemList2"></param>
        /// <returns></returns>
        static bool TestDBI_T_interview_session_CompareLists(List<interview_session> itemList1, List<interview_session> itemList2)
        {
            if (itemList1.Count != itemList2.Count)
                return false;

            SortedList<int, interview_session> sorteditemList1 = new SortedList<int, interview_session>();
            foreach (var r in itemList1)
                sorteditemList1.Add(r.sessionId, r);//sort by key:  r.sessionId

            SortedList<int, interview_session> sorteditemList2 = new SortedList<int, interview_session>();
            foreach (var r in itemList2)
                sorteditemList2.Add(r.sessionId, r); //sort by key:  r.sessionId

            //compare sorted lists for equivalence for each row of data
            foreach (var iKey in sorteditemList1.Keys)
            {
                //method Compare directly compares each field individually
                if (!sorteditemList1[iKey].Equals(sorteditemList2[iKey]))
                    return false;
            }
            return true;
        }//TestDBI_T_interview_session_CompareLists


    }
}
