using System;
using System.Collections.Generic;
using SQLServerDB;

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
                    TestDBI_T_improvement_opportunity_Write_to_DB();
                    break;
                case 2:
                    TestDBI_T_improvement_opportunity_Read_from_DB();
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
        static void TestDBI_T_improvement_opportunity_Write_to_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_improvement_opportunity_Write_to_DB");

            SQLServerDB.improvement_opportunity_Table myTable = new SQLServerDB.improvement_opportunity_Table();

        //TBD

            Console.WriteLine("  --DONE: TestDBI_T_improvement_opportunity_Write_to_DB");
        }

        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_improvement_opportunity_Read_from_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_improvement_opportunity_Read_from_DB");

//tbd

            Console.WriteLine("  --DONE: TestDBI_T_improvement_opportunity_Read_from_DB");
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

#if __COMMENT_
            //Count SQLServerDB affirmation table rows before clearing
            int iRows = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows.ToString());

            Console.WriteLine("  --before clear SQLServer database table");
            pause();
#endif

            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            if (iRows2 != 0)
                Console.WriteLine("ERROR! myTable.CountRows = " + iRows2.ToString() + "; should be zero(0)");
   

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

        static List<improvement_opportunity> make_improvement_opportunity_list_1()
        {
            List<improvement_opportunity> ioList = new List<improvement_opportunity>();

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

                ioList.Add(improvement_opportunityItem);
            }//for

            return ioList;
        }//make_improvement_opportunity_list_1


    }
}
