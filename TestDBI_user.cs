using System;
using System.Collections.Generic;
using SQLServerDB;


namespace TestDBI
{
    class TestDBI_user
    {

        //--------------------------------------------------------------------------------------------
        public static void SelectTest()
        {
            Console.WriteLine("  --START: TestDBI_user.SelectTest()");

            switch (Program.iSubMenuSelection())
            {
                case 1:
                    TestDBI_T_user_Write_to_DB();
                    break;
                case 2:
                    TestDBI_T_user_Read_from_DB();
                    break;
                case 3:
                    TestDBI_T_user_T3();
                    break;
                case 4:
                    TestDBI_T_user_T4();
                    break;
                case 5:
                    TestDBI_T_user_T5();
                    break;
                case 10:
                    TestDBI_T_user_AutoCheck();
                    break;

                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_user.SelectTest()");
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// TestDBI_T_user_Write_to_DB -- write itemlist to DB
        /// </summary>
        static void TestDBI_T_user_Write_to_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_user_Write_to_DB");

            user_Table myTable = new user_Table();
            myTable.itemList = make_user_list_1();
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

            Console.WriteLine("  --DONE: TestDBI_T_user_Write_to_DB");
        }//TestDBI_T_user_Write_to_DB


        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// TestDBI_T_user_Read_from_DB -- read from DB
        /// </summary>
        static void TestDBI_T_user_Read_from_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_user_Read_from_DB");

            SQLServerDB.user_Table myTable = new SQLServerDB.user_Table();

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
            Console.WriteLine("  --DONE: TestDBI_T_user_Read_from_DB");
        }//TestDBI_T_user_Read_from_DB



        /// <summary>
        /// TestDBI_T_user_T3 -  clear the SQLServer user table, write some demo data to SQLServer DB, 
        /// query the affirmatin table by Project ID, 
        /// </summary>
        static void TestDBI_T_user_T3()
        {
            Console.WriteLine("  --START: TestDBI_T_user_T3");

            //Construct myTable in RAM
            SQLServerDB.user_Table myTable = new SQLServerDB.user_Table();
#if rewrite
            //put demo records into myTable
            for (int i = 1; i < 10; i++)
            {
                SQLServerDB.user affItem = new SQLServerDB.user();
                affItem.userId = i;
                affItem.userName = "aff_Name_" + i.ToString();
                affItem.userType = "aff_Type_" + i.ToString();
                affItem.specificGoal = "aff_specificGoal_" + i.ToString();
                affItem.specificPractice = "aff_specificPractice_" + i.ToString();
                affItem.genericGoal = "aff_genericGoal_" + i.ToString();
                affItem.genericPractice = "aff_genericPractice_" + i.ToString();
                affItem.processArea = "aff_processArea_" + i.ToString();
                affItem.projectId = i;

                myTable.itemList.Add(affItem);
            }


            //Count SQLServerDB user table rows before clearing
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

            SQLServerDB.user affSeek = myTable.itemList[0];

            Console.WriteLine("affSeek.userId =" + affSeek.userId);
            Console.WriteLine("affSeek.userName =" + affSeek.userName);
            Console.WriteLine("affSeek.userType =" + affSeek.userType);
            Console.WriteLine("affSeek.specificGoal =" + affSeek.specificGoal);
            Console.WriteLine("affSeek.specificPractice =" + affSeek.specificPractice);
            Console.WriteLine("affSeek.genericGoal =" + affSeek.genericGoal);
            Console.WriteLine("affSeek.genericPractice =" + affSeek.genericPractice);
            Console.WriteLine("affSeek.processArea =" + affSeek.processArea);
            Console.WriteLine("affSeek.projectId =" + affSeek.projectId);

            Util.pause();

            //set the search-by criteria
            String strSeek_processArea = "aff_processArea_4";


            myTable.ReadItemListFromDatabase_By_processArea(strSeek_processArea);
            Console.WriteLine("SEEK items by processArea found: myTable.itemList.Count =" + myTable.itemList.Count.ToString());

            SQLServerDB.user affSeek_processArea = myTable.itemList[0];

            Console.WriteLine("affSeek_processArea.userId =" + affSeek_processArea.userId);
            Console.WriteLine("affSeek_processArea.userName =" + affSeek_processArea.userName);
            Console.WriteLine("affSeek_processArea.userType =" + affSeek_processArea.userType);
            Console.WriteLine("affSeek_processArea.specificGoal =" + affSeek_processArea.specificGoal);
            Console.WriteLine("affSeek_processArea.specificPractice =" + affSeek_processArea.specificPractice);
            Console.WriteLine("affSeek_processArea.genericGoal =" + affSeek_processArea.genericGoal);
            Console.WriteLine("affSeek_processArea.genericPractice =" + affSeek_processArea.genericPractice);
            Console.WriteLine("affSeek_processArea.processArea =" + affSeek_processArea.processArea);
            Console.WriteLine("affSeek_processArea.projectId =" + affSeek_processArea.projectId);

            Util.pause();
#endif
            Console.WriteLine("  --DONE: TestDBI_T_user_T3");
        }

        /// <summary>
        /// TestDBI_T_user_T4 -- 
        /// </summary>
        static void TestDBI_T_user_T4()
        {
            Console.WriteLine("  --START: TestDBI_T_user_T4");

            //Construct a brand new myTable in RAM
            SQLServerDB.user_Table myTable = new SQLServerDB.user_Table();
#if rewrite
            //put demo records into myTable
            for (int i = 1; i < 10; i++)
            {
                SQLServerDB.user affItem = new SQLServerDB.user();
                affItem.userId = i;
                affItem.userName = "aff_Name_" + i.ToString();
                affItem.userType = "aff_Type_" + i.ToString();
                affItem.specificGoal = "aff_specificGoal_" + i.ToString();
                affItem.specificPractice = "aff_specificPractice_" + i.ToString();
                affItem.genericGoal = "aff_genericGoal_" + i.ToString();
                affItem.genericPractice = "aff_genericPractice_" + i.ToString();
                affItem.processArea = "aff_processArea_" + i.ToString();
                affItem.projectId = i;

                myTable.itemList.Add(affItem);
            }

            //Count SQLServerDB user table rows before clearing
            int iRows = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows.ToString());

            Console.WriteLine("  --before clear SQLServer database table");
            Util.pause();

            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows2.ToString());
            Util.pause();

            foreach (SQLServerDB.user r in myTable.itemList)
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
                SQLServerDB.user affItem = new SQLServerDB.user();
                affItem.userId = i;
                affItem.userName = "aff_Name_" + i.ToString() + "_A";
                affItem.userType = "aff_Type_" + i.ToString() + "_B";
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
                myTable.Clear_Database_Table_By_userID(i);
            Console.WriteLine("AFTER the table record deletions:  userI={4,5,6}");
            Util.pause();

            myTable.Clear_Database_Table_By_projectD(102);
            myTable.Clear_Database_Table_By_projectD(108);
            Console.WriteLine("AFTER the table record deletions:  ProjectID = {102, 108}");
            Util.pause();
#endif

            Console.WriteLine("  --DONE: TestDBI_T_user_T4");
        }


        static void TestDBI_T_user_T5()
        {
            Console.WriteLine("  --START: TestDBI_T_user_T5");
#if rewrite
            //Construct a brand new myTable in RAM
            SQLServerDB.user_Table myTable = new SQLServerDB.user_Table();

            int iRowsStart = 5;
            //put demo records into myTable
            for (int i = 1; i <= iRowsStart; i++)
            {
                SQLServerDB.user affItem = new SQLServerDB.user();
                affItem.userId = i;
                affItem.userName = "aff_Name_" + i.ToString();
                affItem.userType = "aff_Type_" + i.ToString();
                affItem.specificGoal = "aff_specificGoal_" + i.ToString();
                affItem.specificPractice = "aff_specificPractice_" + i.ToString();
                affItem.genericGoal = "aff_genericGoal_" + i.ToString();
                affItem.genericPractice = "aff_genericPractice_" + i.ToString();
                affItem.processArea = "aff_processArea_" + i.ToString();
                affItem.projectId = 404;  //setting each item to the same projectID to support find by projectID

                myTable.itemList.Add(affItem);
            }

            //Count SQLServerDB user table rows before clearing
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
            Console.WriteLine("  --DONE: TestDBI_T_user_T5");
        }


        static List<user> make_user_list_1()
        {

            List<user> myList = new List<user>()
            {
#if rewrite
                new  user( 1, "affName_1",  "affType_2", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7", 1),
             new  user( 2, "affName_2",  "affType_2", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7", 2),
             new  user( 3, "affName_3",  "affType_2", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7", 2),
             new  user( 4, "affName_4",  "affType_2", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7", 2),
             new  user( 5, "affName_5",  "affType_2", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7", 2), 
#endif
           };
            return myList;
        }//make_user_list_1


        static List<user> make_user_list_2()
        {
            List<user> myList = new List<user>()
           {
#if rewrite
           new  user( 1, "affName_1",  "affType_2-REV-A", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7-REV-A", 1),
           new  user( 2, "affName_2",  "affType_2-REV-A", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7-REV-A", 2),
           new  user( 3, "affName_3",  "affType_2-REV-A", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7-REV-A", 2),
           new  user( 4, "affName_4",  "affType_2-REV-A", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7-REV-A", 2),
           new  user( 5, "affName_5",  "affType_2-REV-A", "sg_3", "sp_4", "gg_5", "gp_6", "pa_7-REV-A", 2),
#endif
           };
            return myList;
        }//make_user_list_2




        static void TestDBI_T_user_AutoCheck()
        {
            Console.WriteLine("START: TestDBI_T_user_AutoCheck()");
            int iResult = TestDBI_T_user_AutoCheck_WriteRead();
            if (iResult == 0)
                Console.WriteLine("OK: TestDBI_T_user_AutoCheck_WriteRead");
            else
                Console.WriteLine("ERROR: TestDBI_T_user_AutoCheck_WriteRead:    iResult=" + iResult);

            Console.WriteLine("DONE: TestDBI_T_user_AutoCheck()");
        }


        /// <summary>
        /// TestDBI_T_user_AutoCheck_WriteRead - Write,Read,Compare Item List;
        /// 1.1) Create test data: myTable1;
        /// 1.2) Clear DBTable;
        /// 1.3) Write myTable1 to DBTable; 
        /// 1.4) Get DBTable.CountRows, compare (myTable1.itemList.Count == DBTable.CountRows)
        /// 1.5) Read myTable2 from DBTable
        /// 1.6) Compare tables (myTable1 == myTable2)
        /// </summary>
        /// <returns></returns>
        static int TestDBI_T_user_AutoCheck_WriteRead()
        {
            const int OK = 0;
            int iResult = OK;
            Console.WriteLine("START: TestDBI_T_user_AutoCheck_WriteRead()");

            // 1.1) CreateTestData1: myTable1
            user_Table myTable1 = new user_Table();
            myTable1.itemList = new List<user>()
            {
                // user(String val_username, String val_organization, String val_password)
           new user("username_1","org_1", "password_1"),
           new user("username_2","org_1", "password_2"),
           new user("username_3","org_1", "password_3"),
           new user("username_4","org_1", "password_4"),
           new user("username_5","org_1", "password_5"),
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
            user_Table myTable2 = new user_Table();
            myTable2.ReadItemListFromDatabase();

            /// 1.6) Compare tables (myTable1 == myTable2)
            if (!TestDBI_T_user_CompareLists(myTable1.itemList, myTable2.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as test data");
                return iResult;
            }
            Console.WriteLine("OK!  DBTable & test data match");

            Console.WriteLine("DONE: TestDBI_T_user_AutoCheck_WriteRead()");
            return iResult;
        }

        /// <summary>
        /// TestDBI_T_user_CompareLists --
        ///   true if same contents
        ///   false if there are any differences
        /// </summary>
        /// <param name="itemList1"></param>
        /// <param name="itemList2"></param>
        /// <returns></returns>
        static bool TestDBI_T_user_CompareLists(List<user> itemList1, List<user> itemList2)
        {
            if (itemList1.Count != itemList2.Count)
                return false;

            SortedList<String, user> sorteditemList1 = new SortedList<String, user>();
            foreach (var r in itemList1)
                sorteditemList1.Add(r.username, r);//sort by key:  r.username

            SortedList<String, user> sorteditemList2 = new SortedList<String, user>();
            foreach (var r in itemList2)
                sorteditemList2.Add(r.username, r); //sort by key:  r.username

            //compare sorted lists for equivalence for each row of data
            foreach (var iKey in sorteditemList1.Keys)
            {
                //method Compare directly compares each field individually
                if (!sorteditemList1[iKey].Equals(sorteditemList2[iKey]))
                    return false;
            }
            return true;
        }//TestDBI_T_user_CompareLists


    }
}
