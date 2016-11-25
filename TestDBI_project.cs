using System;
using System.Collections.Generic;
using SQLServerDB;


namespace TestDBI
{
    class TestDBI_project
    {

        //--------------------------------------------------------------------------------------------
        public static void SelectTest()
        {
            Console.WriteLine("  --START: TestDBI_project.SelectTest()");

            switch (Program.iSubMenuSelection())
            {
                case 1:
                    TestDBI_T_project_Write_to_DB();
                    break;
                case 2:
                    TestDBI_T_project_Read_from_DB();
                    break;
                case 3:
                    TestDBI_T_project_T3();
                    break;
                case 4:
                    TestDBI_T_project_T4();
                    break;
                case 5:
                    TestDBI_T_project_T5();
                    break;
                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_project.SelectTest()");
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// TestDBI_T_project_Write_to_DB -- write itemlist to DB
        /// </summary>
        static void TestDBI_T_project_Write_to_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_project_Write_to_DB");

            project_Table myTable = new project_Table();
            myTable.itemList = make_project_list_1();
            int iRowsStart = myTable.itemList.Count;
            myTable.Show();
            Util.pause();

            Console.WriteLine("  --before clear SQLServer database table");
            Util.pause();
            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            if (iRows2 != 0)
                Util.pause("Error.  iRows=" + iRows2 + " should be zero after Clear_Database_Table()");
            else
                Util.pause("OK.  After Clear_Database_Table()");


            Console.WriteLine("Write the table from RAM the SQLServer  Database table");
            myTable.WriteItemListToDatabase();
            int iRows3 = myTable.CountRows();
            if (iRows3 != iRowsStart)
                Util.pause("Error.  iRows3=" + iRows3 + " should be " + iRowsStart + " after WriteItemListToDatabase");
            else
                Util.pause("OK.  After WriteItemListToDatabase()");

            Console.WriteLine("  --after writing to the SQLServer database table.  examine the table using SSMS");
            Util.pause("visually inspect via SSMS?");

            Console.WriteLine("  --DONE: TestDBI_T_project_Write_to_DB");
        }//TestDBI_T_project_Write_to_DB


        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// TestDBI_T_project_Read_from_DB -- read from DB
        /// </summary>
        static void TestDBI_T_project_Read_from_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_project_Read_from_DB");

            SQLServerDB.project_Table myTable = new SQLServerDB.project_Table();

            int iRows = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows.ToString());

            Console.WriteLine("Fill the table in RAM from the SQLServer Database table");
            myTable.ReadItemListFromDatabase();
            myTable.Show();
            if (myTable.itemList.Count != iRows)
                Console.WriteLine("Error.  myTable.itemList.Count != myTable.CountRows." + " should be the same ReadItemListFromDatabase ()");
            else
                Console.WriteLine("OK.  After ReadItemListFromDatabase()");

            Util.pause();
            Console.WriteLine("  --DONE: TestDBI_T_project_Read_from_DB");
        }//TestDBI_T_project_Read_from_DB



        /// <summary>
        /// TestDBI_T_project_T3 -  clear the SQLServer project table, write some demo data to SQLServer DB, 
        /// query the affirmatin table by Project ID, 
        /// </summary>
        static void TestDBI_T_project_T3()
        {
            Console.WriteLine("  --START: TestDBI_T_project_T3");

            //Construct myTable in RAM
            SQLServerDB.project_Table myTable = new SQLServerDB.project_Table();
            myTable.itemList = make_project_list_2();
           int iStartRows = myTable.itemList.Count;

            //Count SQLServerDB project table rows before clearing
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

            int iSeek_ProjectID = 3;
            Console.WriteLine("seek item:  iSeek_ProjectID= " + iSeek_ProjectID);
            myTable.ReadItemListFromDatabase_ByProjectID(iSeek_ProjectID);

            Console.WriteLine("SEEK items found: myTable.itemList.Count =" + myTable.itemList.Count.ToString());

            SQLServerDB.project itemSeek = myTable.itemList[0];
            itemSeek.Show();  
            Util.pause();



            Console.WriteLine("  --DONE: TestDBI_T_project_T3");
        }

        /// <summary>
        /// TestDBI_T_project_T4 -- 
        /// </summary>
        static void TestDBI_T_project_T4()
        {
            Console.WriteLine("  --START: TestDBI_T_project_T4");

#if code_rewrite
            //Construct a brand new myTable in RAM
            SQLServerDB.project_Table myTable = new SQLServerDB.project_Table();

            //put demo records into myTable
            for (int i = 1; i < 10; i++)
            {
                SQLServerDB.project affItem = new SQLServerDB.project();
                affItem.projectId = i;
                affItem.projectName = "aff_Name_" + i.ToString();
                affItem.projectType = "aff_Type_" + i.ToString();
                affItem.specificGoal = "aff_specificGoal_" + i.ToString();
                affItem.specificPractice = "aff_specificPractice_" + i.ToString();
                affItem.genericGoal = "aff_genericGoal_" + i.ToString();
                affItem.genericPractice = "aff_genericPractice_" + i.ToString();
                affItem.processArea = "aff_processArea_" + i.ToString();
                affItem.projectId = i;

                myTable.itemList.Add(affItem);
            }

            //Count SQLServerDB project table rows before clearing
            int iRows = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows.ToString());

            Console.WriteLine("  --before clear SQLServer database table");
            Util.pause();

            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows2.ToString());
            Util.pause();

            foreach (SQLServerDB.project r in myTable.itemList)
            {
                myTable.WriteItemToDatabase(r);
            }
            Console.WriteLine("after writing to SQLServerDB");
            Util.pause();

            int iRows3 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows3.ToString());
            Util.pause();

            Console.WriteLine("---update the table");

            //revise the myTable contents
            myTable.itemList.Clear();

            //put demo records into myTable
            for (int i = 1; i < 10; i++)
            {
                SQLServerDB.project affItem = new SQLServerDB.project();
                affItem.projectId = i;
                affItem.projectName = "aff_Name_" + i.ToString() + "_A";
                affItem.projectType = "aff_Type_" + i.ToString() + "_B";
                affItem.specificGoal = "aff_specificGoal_" + i.ToString() + "_C";
                affItem.specificPractice = "aff_specificPractice_" + i.ToString() + "_D";
                affItem.genericGoal = "aff_genericGoal_" + i.ToString() + "_E";
                affItem.genericPractice = "aff_genericPractice_" + i.ToString() + "_F";
                affItem.processArea = "aff_processArea_" + i.ToString() + "_G";
                affItem.projectId = i + 100;

                myTable.itemList.Add(affItem);
            }

            Console.WriteLine("BEFORE the table update");
            Util.pause();
            myTable.UpdateItemListToDatabase();
            Console.WriteLine("AFTER the table update");
            Util.pause();


            for (int i = 4; i <= 6; i++)
                myTable.Clear_Database_Table_By_projectID(i);
            Console.WriteLine("AFTER the table record deletions:  projectI={4,5,6}");
            Util.pause();

            myTable.Clear_Database_Table_By_projectD(102);
            myTable.Clear_Database_Table_By_projectD(108);
            Console.WriteLine("AFTER the table record deletions:  ProjectID = {102, 108}");
            Util.pause();

#endif
            Console.WriteLine("  --DONE: TestDBI_T_project_T4");
        }


        static void TestDBI_T_project_T5()
        {
            Console.WriteLine("  --START: TestDBI_T_project_T5");
#if code_rewrite
            //Construct a brand new myTable in RAM
            SQLServerDB.project_Table myTable = new SQLServerDB.project_Table();

            int iRowsStart = 5;
            //put demo records into myTable
            for (int i = 1; i <= iRowsStart; i++)
            {
                SQLServerDB.project affItem = new SQLServerDB.project();
                affItem.projectId = i;
                affItem.projectName = "aff_Name_" + i.ToString();
                affItem.projectType = "aff_Type_" + i.ToString();
                affItem.specificGoal = "aff_specificGoal_" + i.ToString();
                affItem.specificPractice = "aff_specificPractice_" + i.ToString();
                affItem.genericGoal = "aff_genericGoal_" + i.ToString();
                affItem.genericPractice = "aff_genericPractice_" + i.ToString();
                affItem.processArea = "aff_processArea_" + i.ToString();
                affItem.projectId = 404;  //setting each item to the same projectID to support find by projectID

                myTable.itemList.Add(affItem);
            }

            //Count SQLServerDB project table rows before clearing
            int iRows1 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows1.ToString());

            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            if (iRows2 != 0)
                Console.WriteLine("Error! iRows2=" + iRows2 + ".  After Clear_Database_Table should be zero");

            myTable.WriteItemListToDatabase();

            int iRows3 = myTable.CountRows();
            if (iRows3 != iRowsStart)
                Console.WriteLine("Error! iRows3=" + iRows3 + ".  After WriteItemListToDatabase should be " + iRowsStart);
            else
                Console.WriteLine("OK. CountRows=" + iRows3 + " After WriteItemListToDatabase");

            Util.pause("examine table content with SSMS");

            Util.pause("before table query by projectID");
            int iProjectCount_404 = myTable.CountRows_By_projectId(404);
            if (iProjectCount_404 != iRowsStart)
                Console.WriteLine("ERROR.  iProjectCount_404=" + iProjectCount_404 + ". Expected " + iRowsStart);
            else
                Console.WriteLine("OK. CountRows=" + iProjectCount_404 + " After WriteItemListToDatabase");
            Util.pause();
#endif
            Console.WriteLine("  --DONE: TestDBI_T_project_T5");
        }


        static List<project> make_project_list_1()
        {
            List<project> myList = new List<project>()
            {
                //project(int val_projectId, int val_projectIndex, string val_projectName, string val_creator, bool val_standardProcess)
             new  project( 1, 1, "projName_A", "str_creator_a", true),
             new  project( 2, 2, "projName_A", "str_creator_a", false),
             new  project( 3, 3, "projName_A", "str_creator_a", true),
             new  project( 4,4, "projName_A", "str_creator_a", false),
           };
            return myList;
        }//make_project_list_1


        static List<project> make_project_list_2()
        {
            List<project> myList = new List<project>()
           {
             //project(int val_projectId, int val_projectIndex, string val_projectName, string val_creator, bool val_standardProcess)
             new  project( 1, 1, "projName_A", "str_creator_a", true),
             new  project( 2, 2, "projName_A", "str_creator_a", false),
             new  project( 3, 3, "projName_A", "str_creator_a", true),
             new  project( 4, 4, "projName_A", "str_creator_a", false),
           };
            return myList;
        }//make_project_list_2

        static List<project> make_project_list_3()
        {
            List<project> myList = new List<project>()
           {
             //project(int val_projectId, int val_projectIndex, string val_projectName, string val_creator, bool val_standardProcess)
             new  project( 1, 1, "projName_A", "str_creator_a", true),
             new  project( 2, 2, "projName_A", "str_creator_a", false),
             new  project( 3, 3, "projName_A", "str_creator_a", true),
             new  project( 4,4, "projName_A", "str_creator_a", false),
           };
            return myList;
        }//make_project_list_3

    }
}
