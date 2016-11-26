using System;
using System.Collections.Generic;
using SQLServerDB;

namespace TestDBI
{
     class TestDBI_interview_question
    {


        public static void SelectTest()
        {
            Console.WriteLine("  --START: TestDBI_interview_question.SelectTest()");

            switch (Program.iSubMenuSelection())
            {
                case 1:
                    TestDBI_T_interview_question_Write_to_DB();
                    break;
                case 2:
                    TestDBI_T_interview_question_Read_from_DB();
                    break;
                case 3:
                    TestDBI_T_interview_question_T3();
                    break;
                case 4:
                    TestDBI_T_interview_question_T4();
                    break;
                case 5:
                    TestDBI_T_interview_question_T5();
                    break;
                case 6:
                    TestDBI_T_interview_question_T6_CompareLists();
                    break;
                case 10:
                    TestDBI_T_interview_question_AutoCheck();
                    break;

                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_interview_question.SelectTest()");
        }


        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_interview_question_Write_to_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_question_Write_to_DB");

            SQLServerDB.interview_question_Table myTable = new SQLServerDB.interview_question_Table();
            myTable.itemList = make_interview_question_list_1();
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


            Console.WriteLine("  --DONE: TestDBI_T_interview_question_Write_to_DB");
        }//TestDBI_T_interview_question_Write_to_DB

        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_interview_question_Read_from_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_question_Read_from_DB");
            interview_question_Table myTable = new interview_question_Table();

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
            Console.WriteLine("  --DONE: TestDBI_T_interview_question_Read_from_DB");
        }//TestDBI_T_interview_question_Read_from_DB

        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_interview_question_T3()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_question_T3");

            //TBD


            Console.WriteLine("  --DONE: TestDBI_T_interview_question_T3");
        }//TestDBI_T_interview_question_T3


        static void TestDBI_T_interview_question_T4()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_question_T4");
            Console.WriteLine("  -----   TBD:   do something here??");
            Console.WriteLine("  --DONE: TestDBI_T_interview_question_T4");
        }

        static void TestDBI_T_interview_question_T5()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_question_T5");
            Console.WriteLine("  -----   TBD:   do something here??");
            Console.WriteLine("  --DONE: TestDBI_T_interview_question_T5");
        }


        static List<interview_question> make_interview_question_list_1()
        {
            List<interview_question> myList = new List<interview_question>()
                         {
                        new  interview_question( "question_1",  "notes_1", 1),
                        new  interview_question( "question_2",  "notes_2", 2),
                        new  interview_question( "question_3",  "notes_3", 3),
                        new  interview_question( "question_4",  "notes_4", 4),
                        new  interview_question( "question_5",  "notes_5", 5),
                        new  interview_question( "question_6",  "notes_6", 6),
                        new  interview_question( "question_7",  "notes_7", 7),
                    };
            return myList;
        }//makeList1

        static List<interview_question> make_interview_question_list_1B()
        {
            List<interview_question> myList = new List<interview_question>()
                         {
                        new  interview_question( "question_1",  "notes_1", 1),
                        new  interview_question( "question_2B",  "notes_2", 2),
                        new  interview_question( "question_3",  "notes_3", 3),
                        new  interview_question( "question_4B",  "notes_4", 4),
                        new  interview_question( "question_5",  "notes_5", 5),
                        new  interview_question( "question_6B",  "notes_6", 6),
                        new  interview_question( "question_7",  "notes_7", 7),
                    };
            return myList;
        }//makeList1
        static List<interview_question> make_interview_question_list_2()
        {
            List<interview_question> myList = new List<interview_question>()
                         {
                        new  interview_question( "question_1",  "notes_1", 1),
                        new  interview_question( "question_2_A",  "notes_A", 2),
                        new  interview_question( "question_3",  "notes_3", 3),
                        new  interview_question( "question_4_B",  "notes_4_B", 4),
                        new  interview_question( "question_5",  "notes_5", 5),
                        new  interview_question( "question_6_C",  "notes_6_C", 6),
                        new  interview_question( "question_7",  "notes_7", 7),
                    };
            return myList;
        }//makeList1


        static void TestDBI_T_interview_question_T6_CompareLists()
        {

            Console.WriteLine("  --START: TestDBI_T_interview_question_T6");
            TestDBI_T_interview_question_T6_CompareLists_1();
            TestDBI_T_interview_question_T6_CompareLists_2();
            TestDBI_T_interview_question_T6_CompareLists_3();
            TestDBI_T_interview_question_T6_CompareLists_4();
            Console.WriteLine("  --DONE: TestDBI_T_interview_question_T6");

        }

        static void TestDBI_T_interview_question_T6_CompareLists_1()
        {

            Console.WriteLine("  --START: TestDBI_T_interview_question_T6_CompareLists_1");

            List<interview_question> itemList1 = new List<interview_question>()
                         {
                        new  interview_question( "question_1",  "notes_1", 1),
                        new  interview_question( "question_2",  "notes_2", 2),
                        new  interview_question( "question_3",  "notes_3", 3),
                        new  interview_question( "question_4",  "notes_4", 4),
                        new  interview_question( "question_5",  "notes_5", 5),
                        new  interview_question( "question_6",  "notes_6", 6),
                        new  interview_question( "question_7",  "notes_7", 7)
                    };
            List<interview_question> itemList2 = new List<interview_question>()
                         {
                        new  interview_question( "question_1",  "notes_1", 1),
                        new  interview_question( "question_2",  "notes_2", 2),
                        new  interview_question( "question_3",  "notes_3", 3),
                        new  interview_question( "question_4",  "notes_4", 4),
                        new  interview_question( "question_5",  "notes_5", 5),
                        new  interview_question( "question_6",  "notes_6", 6),
                        new  interview_question( "question_7",  "notes_7", 7)
                    };

            int iRows = itemList1.Count;
            for (int i = 0; i < iRows; i++)
            {
                if (itemList1[i].Equals(itemList2[i]))
                    Console.WriteLine("Equals");
                else
                    Console.WriteLine("  NOT  Equals");
            }
            Console.WriteLine("  --DONE: TestDBI_T_interview_question_T6_CompareLists_1");
        }

        static void TestDBI_T_interview_question_T6_CompareLists_2()
        {

            Console.WriteLine("  --START: TestDBI_T_interview_question_T6_CompareLists_2");

            List<interview_question> itemList1 = new List<interview_question>()
                         {
                        new  interview_question( "question_1",  "notes_1", 1),
                        new  interview_question( "question_2",  "notes_2", 2),
                        new  interview_question( "question_3",  "notes_3", 3),
                        new  interview_question( "question_4",  "notes_4", 4),
                        new  interview_question( "question_5",  "notes_5", 5),
                        new  interview_question( "question_6",  "notes_6", 6),
                        new  interview_question( "question_7",  "notes_7", 7)
                    };
            List<interview_question> itemList2 = new List<interview_question>()
                         {
                        new  interview_question( "question_1",  "notes_1", 1),
                        new  interview_question( "question_2a",  "notes_2", 2),
                        new  interview_question( "question_3",  "notes_3", 3),
                        new  interview_question( "question_4a",  "notes_4", 4),
                        new  interview_question( "question_5",  "notes_5", 5),
                        new  interview_question( "question_6a",  "notes_6", 6),
                        new  interview_question( "question_7",  "notes_7", 7)
                    };

            int iRows = itemList1.Count;
            for (int i = 0; i < iRows; i++)
            {
                if (itemList1[i].Equals(itemList2[i]))
                    Console.WriteLine("Equals");
                else
                    Console.WriteLine("  NOT  Equals");
            }
            Console.WriteLine("  --DONE: TestDBI_T_interview_question_T6_CompareLists_2");
        }


        static void TestDBI_T_interview_question_T6_CompareLists_3()
        {

            Console.WriteLine("  --START: TestDBI_T_interview_question_T6_CompareLists_3");

            List<interview_question> itemList1 = new List<interview_question>()
                         {
                        new  interview_question( "question_1",  "notes_1", 1),
                        new  interview_question( "question_2",  "notes_2", 2),
                        new  interview_question( "question_3",  "notes_3", 3),
                        new  interview_question( "question_4",  "notes_4", 4),
                        new  interview_question( "question_5",  "notes_5", 5),
                        new  interview_question( "question_6",  "notes_6", 6),
                        new  interview_question( "question_7",  "notes_7", 7)
                    };
            List<interview_question> itemList2 = new List<interview_question>()
                         {
                        new  interview_question( "question_1",  "notes_1", 1),
                        new  interview_question( "question_3",  "notes_3", 3),
                        new  interview_question( "question_4",  "notes_4", 4),
                        new  interview_question( "question_5",  "notes_5", 5),
                        new  interview_question( "question_6",  "notes_6", 6),
                        new  interview_question( "question_7",  "notes_7", 7),
                        new  interview_question( "question_2",  "notes_2", 2)
                    };

            int iRows = itemList1.Count;
            for (int i = 0; i < iRows; i++)
            {
                if (itemList1[i].Equals(itemList2[i]))
                    Console.WriteLine("Equals");
                else
                    Console.WriteLine("  NOT  Equals");
            }
            Console.WriteLine("  --DONE: TestDBI_T_interview_question_T6_CompareLists_3");
        }



        static void TestDBI_T_interview_question_T6_CompareLists_4()
        {

            Console.WriteLine("  --START: TestDBI_T_interview_question_T6_CompareLists_4");

            List<interview_question> itemList1 = new List<interview_question>()
                         {
                        new  interview_question( "question_1",  "notes_1", 1),
                        new  interview_question( "question_2",  "notes_2", 2),
                        new  interview_question( "question_3",  "notes_3", 3),
                        new  interview_question( "question_4",  "notes_4", 4),
                        new  interview_question( "question_5",  "notes_5", 5),
                        new  interview_question( "question_6",  "notes_6", 6),
                        new  interview_question( "question_7",  "notes_7", 7)
                    };
            SortedList<int, interview_question> sorteditemList1 = new SortedList<int, interview_question>();
            foreach (var r in itemList1)
                sorteditemList1.Add(r.sessionId, r);

            List<interview_question> itemList2 = new List<interview_question>()
                         {
                        new  interview_question( "question_1",  "notes_1", 1),
                        new  interview_question( "question_4",  "notes_4", 4),
                                                new  interview_question( "question_3",  "notes_3", 3),

                        new  interview_question( "question_5",  "notes_5", 5),

                        new  interview_question( "question_7",  "notes_7", 7),
                                                new  interview_question( "question_6",  "notes_6", 6),

                        new  interview_question( "question_2",  "notes_2", 2)
                    };
            SortedList<int, interview_question> sorteditemList2 = new SortedList<int, interview_question>();
            foreach (var r in itemList2)
                sorteditemList2.Add(r.sessionId, r);

            Console.WriteLine("   use ints");
            int iRows = sorteditemList1.Count;
            for (int i = 1; i <= iRows; i++)
            {
                if (sorteditemList1[i].Equals(sorteditemList2[i]))
                    Console.WriteLine("Equals");
                else
                    Console.WriteLine("  NOT  Equals");
            }
            Console.WriteLine("   use keys");
            foreach (int iKey in sorteditemList1.Keys)
            {
                if (sorteditemList1[iKey].Equals(sorteditemList2[iKey]))
                    Console.WriteLine("Equals");
                else
                    Console.WriteLine("  NOT  Equals");
            }

            Console.WriteLine("   use keys and ==");
            foreach (int iKey in sorteditemList1.Keys)
            {
                if (  sorteditemList1[iKey]  == sorteditemList2[iKey]   )
                    Console.WriteLine("Equals");
                else
                    Console.WriteLine("  NOT  Equals");
            }
 

            Console.WriteLine("   use hashcode");
 
                if (sorteditemList1.GetHashCode() == sorteditemList2.GetHashCode() )
                    Console.WriteLine("Equals");
                else
                    Console.WriteLine("  NOT  Equals");
          
                Console.WriteLine( "use comparison function");
                if (TestDBI_T_interview_question_CompareLists( itemList1,  itemList2) == true)
                    Console.WriteLine("  comparison function says:  true");
                else
                Console.WriteLine("  comparison function says:  false");
            Console.WriteLine("  --DONE: TestDBI_T_interview_question_T6_CompareLists_4");
        }



        static void TestDBI_T_interview_question_AutoCheck()
        {
            Console.WriteLine("START: TestDBI_T_interview_question_AutoCheck()");
            int iResult = TestDBI_T_interview_question_AutoCheck_WriteRead();
            if (iResult == 0)
                Console.WriteLine("OK: TestDBI_T_interview_question_AutoCheck_WriteRead");
            else
                Console.WriteLine("ERROR: TestDBI_T_interview_question_AutoCheck_WriteRead:    iResult=" + iResult);

            Console.WriteLine("DONE: TestDBI_T_interview_question_AutoCheck()");
        }


        /// <summary>
        /// TestDBI_T_interview_question_AutoCheck_WriteRead - Write,Read,Compare Item List;
        /// 1.1) Create test data: myTable1;
        /// 1.2) Clear DBTable;
        /// 1.3) Write myTable1 to DBTable; 
        /// 1.4) Get DBTable.CountRows, compare (myTable1.itemList.Count == DBTable.CountRows)
        /// 1.5) Read myTable2 from DBTable
        /// 1.6) Compare tables (myTable1 == myTable2)
        /// </summary>
        /// <returns></returns>
        static int TestDBI_T_interview_question_AutoCheck_WriteRead()
        {
            const int OK = 0;
            int iResult = OK;
            Console.WriteLine("START: TestDBI_T_interview_question_AutoCheck_WriteRead()");

            // 1.1) CreateTestData1: myTable1
            interview_question_Table myTable1 = new interview_question_Table();
            myTable1.itemList = new List<interview_question>()
            {
             //  interview_question(String val_interviewQuestions, String val_questionNotes, int val_sessionId)
            new interview_question("question_1", "notes_1", 1),
            new interview_question("question_1", "notes_1", 2),
            new interview_question("question_1", "notes_1", 3),
            new interview_question("question_1", "notes_1", 4),
            new interview_question("question_1", "notes_1", 5)
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
            interview_question_Table myTable2 = new interview_question_Table();
            myTable2.ReadItemListFromDatabase();

            /// 1.6) Compare tables (myTable1 == myTable2)
            if (!TestDBI_T_interview_question_CompareLists(myTable1.itemList, myTable2.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as test data");
                return iResult;
            }
            Console.WriteLine("OK!  DBTable & test data match");

            Console.WriteLine("DONE: TestDBI_T_interview_question_AutoCheck_WriteRead()");
            return iResult;
        }

        /// <summary>
        /// TestDBI_T_interview_question_CompareLists --
        ///   true if same contents
        ///   false if there are any differences
        /// </summary>
        /// <param name="itemList1"></param>
        /// <param name="itemList2"></param>
        /// <returns></returns>
        static bool TestDBI_T_interview_question_CompareLists(List<interview_question> itemList1, List<interview_question> itemList2)
        {
            if (itemList1.Count != itemList2.Count)
                return false;

            SortedList<int, interview_question> sorteditemList1 = new SortedList<int, interview_question>();
            foreach (var r in itemList1)
                sorteditemList1.Add(r.sessionId, r);//sort by key:  r.sessionId

            SortedList<int, interview_question> sorteditemList2 = new SortedList<int, interview_question>();
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
        }//TestDBI_T_interview_question_CompareLists


    }
}
