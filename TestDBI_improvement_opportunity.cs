using System;
using System.Collections.Generic;
using SQLServerDB;

namespace TestDBI
{
     class TestDBI_improvement_opportunity
    {

        public static void SelectTest()
        {
            Console.WriteLine("  --START: TestDBI_improvement_opportunity.SelectTest() ");

            switch (Program.iSubMenuSelection())
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
                case 10:
                    TestDBI_T_improvement_opportunity_AutoCheck();
                    break;

                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_improvement_opportunity.SelectTest()");
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
            //Count SQLServerDB improvement_opportunity table rows before clearing
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
            Util.pause();

            int iRows3 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows3.ToString());
            Util.pause();


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
            Util.pause("--BEFORE the update, showing the planned updates in myTable.itemList");

        
            myTable.UpdateItemListToDatabase();
            Util.pause("-- AFTER the update, examine the improvement_opportunity Table using SSMS");


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




        static void TestDBI_T_improvement_opportunity_AutoCheck()
        {
            Console.WriteLine("START: TestDBI_T_improvement_opportunity_AutoCheck()");
            int iResult;
            
            iResult = TestDBI_T_improvement_opportunity_AutoCheck_WriteRead();
            if (iResult == 0)
                Console.WriteLine("OK: TestDBI_T_improvement_opportunity_AutoCheck_WriteRead");
            else
                Console.WriteLine("ERROR: TestDBI_T_improvement_opportunity_AutoCheck_WriteRead:    iResult=" + iResult);


            iResult = TestDBI_T_improvement_opportunity_AutoCheck_Update();
            if (iResult == 0)
                Console.WriteLine("OK: TestDBI_T_improvement_opportunity_AutoCheck_Update");
            else
                Console.WriteLine("ERROR: TestDBI_T_improvement_opportunity_AutoCheck_Update:    iResult=" + iResult);

            iResult = TestDBI_T_improvement_opportunity_AutoCheck_Update();


            Console.WriteLine("DONE: TestDBI_T_improvement_opportunity_AutoCheck()");
        }


        /// <summary>
        /// TestDBI_T_improvement_opportunity_AutoCheck_WriteRead - Write,Read,Compare Item List;
        /// 1.1) Create test data: myTable1;
        /// 1.2) Clear DBTable;
        /// 1.3) Write myTable1 to DBTable; 
        /// 1.4) Get DBTable.CountRows, compare (myTable1.itemList.Count == DBTable.CountRows)
        /// 1.5) Read myTable2 from DBTable
        /// 1.6) Compare tables (myTable1 == myTable2)
        /// </summary>
        /// <returns></returns>
        static int TestDBI_T_improvement_opportunity_AutoCheck_WriteRead()
        {
            const int OK = 0;
            int iResult = OK;
            Console.WriteLine("START: TestDBI_T_improvement_opportunity_AutoCheck_WriteRead()");

            // 1.1) CreateTestData1: myTable1
            improvement_opportunity_Table myTable1 = new improvement_opportunity_Table();
            myTable1.itemList = new List<improvement_opportunity>()
            {
           //improvement_opportunity(String val_notes, String val_processArea, String val_specificGoal, String val_specificPractice,
           // String val_genericGoal, String val_genericPractice,int val_projectId)
            new improvement_opportunity("notes_1", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 1),
            new improvement_opportunity("notes_2", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
            new improvement_opportunity("notes_3", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
            new improvement_opportunity("notes_4", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
            new improvement_opportunity("notes_5", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 5),
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
            improvement_opportunity_Table myTable2 = new improvement_opportunity_Table();
            myTable2.ReadItemListFromDatabase();

            /// 1.6) Compare tables (myTable1 == myTable2)
            if (!TestDBI_T_improvement_opportunity_CompareLists(myTable1.itemList, myTable2.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as test data");
                return iResult;
            }
            Console.WriteLine("OK!  DBTable & test data match");

            Console.WriteLine("DONE: TestDBI_T_improvement_opportunity_AutoCheck_WriteRead()");
            return iResult;
        }


        /// <summary>
        /// TestDBI_T_improvement_opportunity_AutoCheck_Update - Update Item List;
        /// 1.1) Create test data: myTable1;
        /// 1.2) Clear DBTable;
        /// 1.3) Write myTable1 to DBTable; 
        /// 1.4) Get DBTable.CountRows, compare (myTable1.itemList.Count == DBTable.CountRows)
        /// 1.5) Read myTable2 from DBTable
        /// 1.6) Compare tables (myTable1 == myTable2)
        /// 1.7) Create the update table (myTableUpdate)
        /// 1.8) Update TableDB
        /// 1.9) Read myTable3
        /// 1.10) Compare tables.itemLists(myTableUpdate == myTable3)
        /// </summary>
        /// <returns></returns>
        static int TestDBI_T_improvement_opportunity_AutoCheck_Update()
        {
            const int OK = 0;
            int iResult = OK;
            Console.WriteLine("START: TestDBI_T_improvement_opportunity_AutoCheck_Update()");

            // 1.1) CreateTestData1: myTable1
            improvement_opportunity_Table myTable1 = new improvement_opportunity_Table();
            myTable1.itemList = new List<improvement_opportunity>()
            {
           //improvement_opportunity(String val_notes, String val_processArea, String val_specificGoal, String val_specificPractice,
           // String val_genericGoal, String val_genericPractice,int val_projectId)
            new improvement_opportunity("notes_1", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 1),
            new improvement_opportunity("notes_2", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
            new improvement_opportunity("notes_3", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
            new improvement_opportunity("notes_4", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
            new improvement_opportunity("notes_5", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 5),
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
            improvement_opportunity_Table myTable2 = new improvement_opportunity_Table();
            myTable2.ReadItemListFromDatabase();

            /// 1.6) Compare tables (myTable1 == myTable2)
            if (!TestDBI_T_improvement_opportunity_CompareLists(myTable1.itemList, myTable2.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as test data");
                return iResult;
            }

            /// 1.7) Create the update table (myTableUpdate)
            improvement_opportunity_Table myTableUpdate = new improvement_opportunity_Table();
            myTableUpdate.itemList = new List<improvement_opportunity>()
            {
           //improvement_opportunity(String val_notes, String val_processArea, String val_specificGoal, String val_specificPractice,
           // String val_genericGoal, String val_genericPractice,int val_projectId)
            new improvement_opportunity("notes_1", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 1),
            new improvement_opportunity("notes_2", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1-update", 2),
            new improvement_opportunity("notes_3", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
            new improvement_opportunity("notes_4", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1-update", 4),
            new improvement_opportunity("notes_5", "pa_1", "sg_1", "sp_1", "gg_1", "gp_1", 5),
            };


            //1.8) Update TableDB
            myTableUpdate.UpdateItemListToDatabase();

            //1.9) Read myTable3
            improvement_opportunity_Table myTable3 = new improvement_opportunity_Table();
            myTable3.ReadItemListFromDatabase();


            //1.10) Compare tables.itemLists (myTableUpdate == myTable3)
            if (!TestDBI_T_improvement_opportunity_CompareLists(myTableUpdate.itemList, myTable3.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as the update table");
                return iResult;
            }


            Console.WriteLine("DONE: TestDBI_T_improvement_opportunity_AutoCheck_Update()");
            return iResult;
        }



        /// <summary>
        /// TestDBI_T_improvement_opportunity_CompareLists --
        ///   true if same contents
        ///   false if there are any differences
        /// </summary>
        /// <param name="itemList1"></param>
        /// <param name="itemList2"></param>
        /// <returns></returns>
        static bool TestDBI_T_improvement_opportunity_CompareLists(List<improvement_opportunity> itemList1, List<improvement_opportunity> itemList2)
        {
            if (itemList1.Count != itemList2.Count)
                return false;

            SortedList<int, improvement_opportunity> sorteditemList1 = new SortedList<int, improvement_opportunity>();
            foreach (var r in itemList1)
                sorteditemList1.Add(r.projectId, r);//sort by key:  r.projectId

            SortedList<int, improvement_opportunity> sorteditemList2 = new SortedList<int, improvement_opportunity>();
            foreach (var r in itemList2)
                sorteditemList2.Add(r.projectId, r); //sort by key:  r.nodeId

            //compare sorted lists for equivalence for each row of data
            foreach (var iKey in sorteditemList1.Keys)
            {
                //method Compare directly compares each field individually
                if (!sorteditemList1[iKey].Equals(sorteditemList2[iKey]))
                    return false;
            }
            return true;
        }//TestDBI_T_improvement_opportunity_CompareLists


    }
}
