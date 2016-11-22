using System;


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
                    TestDBI_T_goal_practice_ADODB_to_SQLServer();
                    break;
                case 2:
                    TestDBI_T_goal_practice_SQLServer_to_ADODB();
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
        static void TestDBI_T_goal_practice_ADODB_to_SQLServer()
        {
            Console.WriteLine("  --START: TestDBI_T_affirmation__note_ADODB_to_SQLServer");




            SQLServerDB.goal_practice_Table myTable = new SQLServerDB.goal_practice_Table();

            int iRows = myTable.ADODB_CountRows();
            Console.WriteLine("myTable.ADODB_CountRows = " + iRows.ToString());

            Console.WriteLine("Fill the table in RAM from the ADODB  Database table");
            myTable.ADODB_ReadItemListFromDatabase();
            myTable.Show();

            pause("  --before clear SQLServer database table. item RAM-based itemList has been filled from MS Access table");

            myTable.Clear_Database_Table();
            pause("  --after clearing SQLServer database table.  examine the table using SSMS");


            Console.WriteLine("Write the table from RAM the SQLServer  Database table");
            myTable.WriteItemListToDatabase();
            pause("  --after writing to the SQLServer database table.  examine the table using SSMS");

            int iRows2 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows2.ToString());
            pause();


            Console.WriteLine("  --DONE: TestDBI_T_goal_practice_ADODB_to_SQLServer");
        }

        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_goal_practice_SQLServer_to_ADODB()
        {
            Console.WriteLine("  --START: TestDBI_T_goal_practice_SQLServer_to_ADODB");



            SQLServerDB.goal_practice_Table myTable = new SQLServerDB.goal_practice_Table();

            int iRows = myTable.CountRows();
            Console.WriteLine("myTable.SQLServer_CountRows = " + iRows.ToString());

            Console.WriteLine("Fill the table in RAM from the SQLServer Database table");
            myTable.ReadItemListFromDatabase();
            myTable.Show();

            Console.WriteLine("  --before clear ADODB database table");
            myTable.ADODB_Clear_Database_Table();

            pause("  --after clearing ADODB database table.  examine the table using MSAccess");


            Console.WriteLine("Write the table from RAM the ADODB  Database table");
            myTable.ADODB_WriteItemListToDatabase();
            pause("  --after writing to the  ADODB database table.  examine the table using MSAccess");

            Console.WriteLine("  --DONE: TestDBI_T_goal_practice_SQLServer_to_ADODB");
        }

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

    }
}
