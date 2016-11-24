using System;
using System.Collections.Generic;
using SQLServerDB;


namespace TestDBI
{
    partial class Program
    {

        static void TestDBI_T_interview_session()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_session ");

            switch (iSubMenuSelection())
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
                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_T_interview_session ");
        }


        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_interview_session_Write_to_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_session_Write_to_DB");

            SQLServerDB.interview_session_Table myTable = new SQLServerDB.interview_session_Table();

            myTable.itemList = make_interview_session_list_1();
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

            pause();

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
    }
}
