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


            //Count SQLServerDB affirmation table rows before clearing
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
        }//make_affirmation_list_1


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
        }//make_affirmation_list_2
    }
}
