using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDBI
{
    partial class Program
    {

        static void TestDBI_T_improvement_opportunity()
        {
            Console.WriteLine("  --START: TestDBI_T_improvement_opportunity ");

            switch (iSubMenuSelection())
            {
                case 1:
                    TestDBI_T_improvement_opportunity_ADODB_to_SQLServer();
                    break;
                case 2:
                    TestDBI_T_improvement_opportunity_SQLServer_to_ADODB();
                    break;
                case 3:
                    TestDBI_T_improvement_opportunity_T3();
                    break;
                case 4:
                    TestDBI_T_improvement_opportunity_T4();
                    break;
                case 5:
                    TestDBI_T_improvement_opportunity_T5();
                    break;
                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_T_improvement_opportunity ");
        }


        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_improvement_opportunity_ADODB_to_SQLServer()
        {
            Console.WriteLine("  --START: TestDBI_T_affirmation__note_ADODB_to_SQLServer");


            SQLServerDB.improvement_opportunity_Table myTable = new SQLServerDB.improvement_opportunity_Table();

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

            Console.WriteLine("  --DONE: TestDBI_T_improvement_opportunity_ADODB_to_SQLServer");
        }

        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_improvement_opportunity_SQLServer_to_ADODB()
        {
            Console.WriteLine("  --START: TestDBI_T_improvement_opportunity_SQLServer_to_ADODB");


            SQLServerDB.improvement_opportunity_Table myTable = new SQLServerDB.improvement_opportunity_Table();

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

            Console.WriteLine("  --DONE: TestDBI_T_improvement_opportunity_SQLServer_to_ADODB");
        }

        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_improvement_opportunity_T3()
        {
            Console.WriteLine("  --START: TestDBI_T_improvement_opportunity_T3");


            //Construct myTable in RAM
            SQLServerDB.improvement_opportunity_Table myTable = new SQLServerDB.improvement_opportunity_Table();


            //put demo records into myTable in RAM
            myTable.itemList.Clear();
            for (int i = 1; i < 10; i++)
            {
                SQLServerDB.improvement_opportunity improvement_opportunityItem = new SQLServerDB.improvement_opportunity();

                improvement_opportunityItem.ID = i;  //actually, a don't care; it will not be stored
                improvement_opportunityItem.notes = "notes_" + i.ToString();
                improvement_opportunityItem.specificGoal = "sg_" + i.ToString();               
                improvement_opportunityItem.specificPractice = "sp_" + i.ToString(); 
                improvement_opportunityItem.genericGoal = "gg_" + i.ToString(); 
                improvement_opportunityItem.genericPractice = "gp_" + i.ToString(); 
                improvement_opportunityItem.projectId = i;
               improvement_opportunityItem.processArea = "process_area_" + i.ToString(); 

                myTable.itemList.Add(improvement_opportunityItem);
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
                SQLServerDB.improvement_opportunity improvement_opportunityItem = new SQLServerDB.improvement_opportunity();

                improvement_opportunityItem.ID = i;  //actually, a don't care; it will not be stored
                improvement_opportunityItem.notes = "notes_" + i.ToString();
                improvement_opportunityItem.specificGoal = "sg_" + i.ToString() + "_REV-A"; 
                improvement_opportunityItem.specificPractice = "sp_" + i.ToString() + "_REV-A"; 
                improvement_opportunityItem.genericGoal = "gg_" + i.ToString() + "_REV-A";
                improvement_opportunityItem.genericPractice = "gp_" + i.ToString() + "_REV-A";
                improvement_opportunityItem.projectId = i;
                improvement_opportunityItem.processArea = "process_area_" + i.ToString() + "_REV-A";

                myTable.itemList.Add(improvement_opportunityItem);
            }
            myTable.Show();
            pause("--BEFORE the update, showing the planned updates in myTable.itemList");

        
            myTable.UpdateItemListToDatabase();
            pause("-- AFTER the update, examine the improvement_opportunity Table using SSMS");


            Console.WriteLine("  --DONE: TestDBI_T_improvement_opportunity_T3");
        }//TestDBI_T_improvement_opportunity_T3


        static void TestDBI_T_improvement_opportunity_T4()
        {
            Console.WriteLine("  --START: TestDBI_T_improvement_opportunity_T4");
            Console.WriteLine("  -----   TBD:   do something here??");
            Console.WriteLine("  --DONE: TestDBI_T_improvement_opportunity_T4");
        }

        static void TestDBI_T_improvement_opportunity_T5()
        {
            Console.WriteLine("  --START: TestDBI_T_improvement_opportunity_T5");
            Console.WriteLine("  -----   TBD:   do something here??");
            Console.WriteLine("  --DONE: TestDBI_T_improvement_opportunity_T5");
        }
    }
}
