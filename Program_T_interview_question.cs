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
    }
}
