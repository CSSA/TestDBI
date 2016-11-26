using System;
using System.Collections.Generic;

using SQLServerDB;

namespace TestDBI
{
     class TestDBI_appraisal
    {

        public static void SelectTest()
        {
            Console.WriteLine("  --START: TestDBI_T_appraisal.SelectTest()");

            switch (Program.iSubMenuSelection())
            {
                case 1:
                    TestDBI_T_appraisal_write_to_DB();
                    break;
                case 2:
                    TestDBI_T_appraisal_read_from_DB();
                    break;
                case 3:
                    TestDBI_T_appraisal_T3();
                    break;
                case 4:
                    TestDBI_T_appraisal_T4();
                    break;
                case 5:
                    TestDBI_T_appraisal_T5();
                    break;
                case 10:
                    TestDBI_T_appraisal_AutoCheck();
                    break;
                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_T_appraisal.SelectTest()");
        }


        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_appraisal_write_to_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_appraisal_write_to_DB");

            SQLServerDB.appraisal_Table myTable = new SQLServerDB.appraisal_Table();
         

            Console.WriteLine("  --DONE: TestDBI_T_appraisal_write_to_DB");
        }//TestDBI_T_appraisal_write_to_DB

        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_appraisal_read_from_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_appraisal_read_from_DB");
            SQLServerDB.appraisal_Table myTable = new SQLServerDB.appraisal_Table();



            Console.WriteLine("  --DONE: TestDBI_T_appraisal_read_from_DB");
        }//TestDBI_T_appraisal_read_from_DB

        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_appraisal_T3()
        {
            Console.WriteLine("  --START: TestDBI_T_appraisal_T3");


            //Construct myTable in RAM
            SQLServerDB.appraisal_Table myTable = new SQLServerDB.appraisal_Table();


            //put demo records into myTable in RAM
            myTable.itemList.Clear();
            for (int i = 1; i < 10; i++)
            {
                SQLServerDB.appraisal iqlItem = new SQLServerDB.appraisal();

                iqlItem.ID = i;  //actually, a don't care; it will not be stored
                iqlItem.AppraisalName = "Name_" + i.ToString();
                iqlItem.Creator = "Creator_" + i.ToString();
                iqlItem.MaturityLevel = i;                //A valid maturity level is not really needed.  Test only demonstrates storing/retreiving integers.
                iqlItem.Projects = "Projects_" + i.ToString();
                iqlItem.SAMSelected = Convert.ToBoolean(i % 2); // Alternate between true/false
                iqlItem.SSDSelected = Convert.ToBoolean(i % 2); // Alternate between true/false
                myTable.itemList.Add(iqlItem);
            }


            //Count SQLServerDB appraisal table rows before clearing
            int iRows = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows.ToString());

            Console.WriteLine("  --before clear SQLServer database table");
            Util.pause();

            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows2.ToString());
            Util.pause();

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
                SQLServerDB.appraisal appraisalItem = new SQLServerDB.appraisal();
                appraisalItem.ID = i;  //actually, a don't care; it will not be stored
                appraisalItem.AppraisalName = "Name_" + i.ToString();
                appraisalItem.Creator = "Creator_" + i.ToString() + "_REV-A";  //modify the text!
                appraisalItem.MaturityLevel = i + 100;                //A valid maturity level is not really needed.  Test only demonstrates storing/retreiving integers.
                appraisalItem.Projects = "Projects_" + i.ToString() + "_REV-A";       //modify the text!
                appraisalItem.SAMSelected = Convert.ToBoolean(1 + (i % 2)); // Alternate between true/false
                appraisalItem.SSDSelected = Convert.ToBoolean(1 + (i % 2)); // Alternate between true/false

                myTable.itemList.Add(appraisalItem);
            }
            myTable.Show();
            Util.pause("--BEFORE the update, showing the planned updates in myTable.itemList");

            SQLServerDB.appraisal appRef1 = myTable.itemList[0];
            appRef1.MaturityLevel = appRef1.MaturityLevel + 200;
            appRef1.Projects = appRef1.Projects + ",P99";

            SQLServerDB.appraisal appRef2 = myTable.itemList[3];
            appRef2.MaturityLevel = appRef2.MaturityLevel + 300;
            appRef2.Projects = appRef2.Projects + ",P99";

            myTable.UpdateItemListToDatabase();
            Util.pause("-- AFTER the update, examine the appraisal Table using SSMS");


            Console.WriteLine("  --DONE: TestDBI_T_appraisal_T3");
        }//TestDBI_T_appraisal_T3


        static void TestDBI_T_appraisal_T4()
        {
            Console.WriteLine("  --START: TestDBI_T_appraisal_T4");
            Console.WriteLine("  -----   TBD:   do something here??");
            Console.WriteLine("  --DONE: TestDBI_T_appraisal_T4");
        }

        static void TestDBI_T_appraisal_T5()
        {
            Console.WriteLine("  --START: TestDBI_T_appraisal_T5");
            Console.WriteLine("  -----   TBD:   do something here??");
            Console.WriteLine("  --DONE: TestDBI_T_appraisal_T5");
        }



        static List<appraisal> make_appraisal_list_1()
        {
            const int iML = 1;
            const int projNum = 1;
            const bool bsamSelectedT = true;
            const bool bsamSelectedF = false;
            const bool bssdSelectedT = true;
            const bool bssdSelectedF = false;

            List<appraisal> myList = new List<appraisal>()
            {
            new appraisal("appraisalName_1", "s_creator_1", iML, projNum, "projects_1", bsamSelectedT, bssdSelectedT),
           new appraisal("appraisalName_2", "s_creator_2", iML, projNum, "projects_2", bsamSelectedF, bssdSelectedF),
           new appraisal("appraisalName_3", "s_creator_3", iML, projNum, "projects_3", bsamSelectedT, bssdSelectedT),
           new appraisal("appraisalName_4", "s_creator_4", iML, projNum, "projects_4", bsamSelectedF, bssdSelectedF)
        };
            return myList;
        }//make_appraisal_list_1


        static List<appraisal> make_appraisal_list_2()
        {
            const int iML = 1;
            const int projNum = 1;
            const bool bsamSelectedT = true;
            const bool bsamSelectedF = false;
            const bool bssdSelectedT = true;
            const bool bssdSelectedF = false;

            List<appraisal> myList = new List<appraisal>()
           {
            new appraisal("appraisalName_1", "s_creator_1", iML, projNum, "projects_1", bsamSelectedT, bssdSelectedT),
           new appraisal("appraisalName_2", "s_creator_2", iML, projNum, "projects_2", bsamSelectedF, bssdSelectedF),
           new appraisal("appraisalName_3", "s_creator_3", iML, projNum, "projects_3", bsamSelectedT, bssdSelectedT),
           new appraisal("appraisalName_4", "s_creator_4", iML, projNum, "projects_4", bsamSelectedF, bssdSelectedF)
        };
            return myList;
        }//make_appraisal_list_2





        static void TestDBI_T_appraisal_AutoCheck()
        {
            Console.WriteLine("START: TestDBI_T_appraisal_AutoCheck()");
            int iResult = TestDBI_T_appraisal_AutoCheck_WriteRead();
            if (iResult == 0)
                Console.WriteLine("OK: TestDBI_T_appraisal_AutoCheck_WriteRead");
            else
                Console.WriteLine("ERROR: TestDBI_T_appraisal_AutoCheck_WriteRead:    iResult=" + iResult);

            Console.WriteLine("DONE: TestDBI_T_appraisal_AutoCheck()");
        }


        /// <summary>
        /// TestDBI_T_appraisal_AutoCheck_WriteRead - Write,Read,Compare Item List;
        /// 1.1) Create test data: myTable1;
        /// 1.2) Clear DBTable;
        /// 1.3) Write myTable1 to DBTable; 
        /// 1.4) Get DBTable.CountRows, compare (myTable1.itemList.Count == DBTable.CountRows)
        /// 1.5) Read myTable2 from DBTable
        /// 1.6) Compare tables (myTable1 == myTable2)
        /// </summary>
        /// <returns></returns>
        static int TestDBI_T_appraisal_AutoCheck_WriteRead()
        {
            const int OK = 0;
            int iResult = OK;
            Console.WriteLine("START: TestDBI_T_appraisal_AutoCheck_WriteRead()");

            // 1.1) CreateTestData1: myTable1
            appraisal_Table myTable1 = new appraisal_Table();
            myTable1.itemList = new List<appraisal>()
            {
                // appraisal(String val_appraisalName, String val_creator, int val_maturityLevel, int val_currentProject, string val_projects, bool val_samSelected, bool val_ssdSelected)
            new appraisal("appraisalName_1", "s_creator_1", 1, 1, "projects_1", true, true),
            new appraisal("appraisalName_2", "s_creator_2", 2, 2, "projects_2", true, true),
            new appraisal("appraisalName_3", "s_creator_3", 3, 3, "projects_3", true, true),
            new appraisal("appraisalName_4", "s_creator_4", 4, 4, "projects_4", true, true),
            new appraisal("appraisalName_5", "s_creator_5", 5, 5, "projects_5", true, true),
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
            appraisal_Table myTable2 = new appraisal_Table();
            myTable2.ReadItemListFromDatabase();

            /// 1.6) Compare tables (myTable1 == myTable2)
            if (!TestDBI_T_appraisal_CompareLists(myTable1.itemList, myTable2.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as test data");
                return iResult;
            }
            Console.WriteLine("OK!  DBTable & test data match");

            Console.WriteLine("DONE: TestDBI_T_appraisal_AutoCheck_WriteRead()");
            return iResult;
        }

        /// <summary>
        /// TestDBI_T_appraisal_CompareLists --
        ///   true if same contents
        ///   false if there are any differences
        /// </summary>
        /// <param name="itemList1"></param>
        /// <param name="itemList2"></param>
        /// <returns></returns>
        static bool TestDBI_T_appraisal_CompareLists(List<appraisal> itemList1, List<appraisal> itemList2)
        {
            if (itemList1.Count != itemList2.Count)
                return false;

            SortedList<String, appraisal> sorteditemList1 = new SortedList<String, appraisal>();
            foreach (var r in itemList1)
                sorteditemList1.Add(r.AppraisalName, r);//sort by key:  r.appraisalId

            SortedList<String, appraisal> sorteditemList2 = new SortedList<String, appraisal>();
            foreach (var r in itemList2)
                sorteditemList2.Add(r.AppraisalName, r); //sort by key:  r.appraisalId

            //compare sorted lists for equivalence for each row of data
            foreach (var iKey in sorteditemList1.Keys)
            {
                //method Compare directly compares each field individually
                if (!sorteditemList1[iKey].Equals(sorteditemList2[iKey]))
                    return false;
            }
            return true;
        }//TestDBI_T_appraisal_CompareLists

    }
}
