using System;
using System.Collections.Generic;
using SQLServerDB;

namespace TestDBI
{
    partial class Program
    {

        static void TestDBI_T_goal_practice()
        {
            Console.WriteLine("  --START: TestDBI_T_goal_practice ");

            switch (iSubMenuSelection())
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
                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_T_goal_practice ");
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

            pause("visually inspect via SSMS?");

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

            pause();

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
            pause();

            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows2.ToString());
            pause();

            myTable.WriteItemListToDatabase();
            Console.WriteLine("after writing to SQLServerDB");
            pause();

            int iRows3 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows3.ToString());
            pause();


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
            pause("--BEFORE the update, showing the planned updates in myTable.itemList");

            myTable.UpdateItemListToDatabase();
            pause("-- AFTER the update, examine the goal_practice Table using SSMS");

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


    }
}
