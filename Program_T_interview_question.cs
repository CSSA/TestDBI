using System;
using System.Collections.Generic;
using SQLServerDB;

namespace TestDBI
{
    partial class Program
    {


        static void TestDBI_T_interview_question()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_question ");

            switch (iSubMenuSelection())
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

                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_T_interview_question ");
        }


        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_interview_question_Write_to_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_question_Write_to_DB");

            SQLServerDB.interview_question_Table myTable = new SQLServerDB.interview_question_Table();
            myTable.itemList = make_interview_question_list_1();
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

            pause();
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
                if (itemList1[i].Compare(itemList2[i]))
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
                if (itemList1[i].Compare(itemList2[i]))
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
                if (itemList1[i].Compare(itemList2[i]))
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
                if (sorteditemList1[i].Compare(sorteditemList2[i]))
                    Console.WriteLine("Equals");
                else
                    Console.WriteLine("  NOT  Equals");
            }
            Console.WriteLine("   use keys");
            foreach (int iKey in sorteditemList1.Keys)
            {
                if (sorteditemList1[iKey].Compare(sorteditemList2[iKey]))
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
                sorteditemList1.Add(r.sessionId, r);//sort by key:  r.sessionID

            SortedList<int, interview_question> sorteditemList2 = new SortedList<int, interview_question>();
            foreach (var r in itemList2)
                sorteditemList2.Add(r.sessionId, r); //sort by key:  r.sessionID
 
            //compare sorted lists for equivalence for each row of data
            foreach (int iKey in sorteditemList1.Keys)
            {
                //method Compare directly compares each field individually
                if (! sorteditemList1[iKey].Compare(sorteditemList2[iKey]))
                     return false;
            }
             return true;
        }//TestDBI_T_interview_question_CompareLists

    }
}
