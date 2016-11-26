using System;
using System.Collections.Generic;
using SQLServerDB;


namespace TestDBI
{
    class TestDBI_mapping
    {

        //--------------------------------------------------------------------------------------------
        public static void SelectTest()
        {
            Console.WriteLine("  --START: TestDBI_mapping.SelectTest()");

            switch (Program.iSubMenuSelection())
            {
                case 1:
                    TestDBI_T_mapping_Write_to_DB();
                    break;
                case 2:
                    TestDBI_T_mapping_Read_from_DB();
                    break;
                case 3:
                    TestDBI_T_mapping_T3();
                    break;
                case 4:
                    TestDBI_T_mapping_T4();
                    break;
                case 5:
                    TestDBI_T_mapping_T5();
                    break;
                case 10:
                    TestDBI_T_mapping_AutoCheck();
                    break;

                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_mapping.SelectTest()");
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// TestDBI_T_mapping_Write_to_DB -- write itemlist to DB
        /// </summary>
        static void TestDBI_T_mapping_Write_to_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_mapping_Write_to_DB");

            mapping_Table myTable = new mapping_Table();
            myTable.itemList = make_mapping_list_1();
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

            Console.WriteLine("  --DONE: TestDBI_T_mapping_Write_to_DB");
        }//TestDBI_T_mapping_Write_to_DB


        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// TestDBI_T_mapping_Read_from_DB -- read from DB
        /// </summary>
        static void TestDBI_T_mapping_Read_from_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_mapping_Read_from_DB");

            SQLServerDB.mapping_Table myTable = new SQLServerDB.mapping_Table();

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
            Console.WriteLine("  --DONE: TestDBI_T_mapping_Read_from_DB");
        }//TestDBI_T_mapping_Read_from_DB



        /// <summary>
        /// TestDBI_T_mapping_T3 -  clear the SQLServer mapping table, write some demo data to SQLServer DB, 
        /// query the affirmatin table by Project ID, 
        /// </summary>
        static void TestDBI_T_mapping_T3()
        {
            Console.WriteLine("  --START: TestDBI_T_mapping_T3");

            //Construct myTable in RAM
            SQLServerDB.mapping_Table myTable = new SQLServerDB.mapping_Table();

            myTable.itemList = make_mapping_list_3();

            //Count SQLServerDB mapping table rows before clearing
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

            SQLServerDB.mapping affSeek = myTable.itemList[0];
            affSeek.Show();

            Util.pause();

            Console.WriteLine("  --DONE: TestDBI_T_mapping_T3");
        }

        /// <summary>
        /// TestDBI_T_mapping_T4 -- 
        /// </summary>
        static void TestDBI_T_mapping_T4()
        {
            Console.WriteLine("  --START: TestDBI_T_mapping_T4");

            //Construct a brand new myTable in RAM
            SQLServerDB.mapping_Table myTable = new SQLServerDB.mapping_Table();
            myTable.itemList = make_mapping_list_4A();


            //Count SQLServerDB mapping table rows before clearing
            int iRows = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows.ToString());

            Console.WriteLine("  --before clear SQLServer database table");
            Util.pause();

            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows2.ToString());
            Util.pause();

            foreach (SQLServerDB.mapping r in myTable.itemList)
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
            myTable.itemList = make_mapping_list_4B();

            Console.WriteLine("BEFORE the table update");
            Util.pause();
            myTable.UpdateItemListToDatabase();
            Console.WriteLine("AFTER the table update");
            Util.pause();


            for (int i = 4; i <= 6; i++)
                myTable.Clear_Database_Table_By_mappingId(i);
            Console.WriteLine("AFTER the table record deletions:  mappingI={4,5,6}");
            Util.pause();



            Console.WriteLine("  --DONE: TestDBI_T_mapping_T4");
        }


        static void TestDBI_T_mapping_T5()
        {
            Console.WriteLine("  --START: TestDBI_T_mapping_T5");

            Console.WriteLine("    --TBD: do something here?");

            Console.WriteLine("  --DONE: TestDBI_T_mapping_T5");
        }


        static List<mapping> make_mapping_list_1()
        {
            List<mapping> myList = new List<mapping>()
            {
           //mapping(int val_mappingId, String val_mappingName, String val_mappingPath, 
           //                       String val_specificGoal, String val_specificPractice, String val_genericGoal, String val_genericPractice, int val_projectId)
              new  mapping( 1, "name_1", "path_1","sg_1", "sp_1", "gg_1", "gp_1", 1),
              new  mapping( 2, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
              new  mapping( 3, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
              new  mapping( 4, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
              new  mapping( 5, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 5),
           };
            return myList;
        }//make_mapping_list_1


        static List<mapping> make_mapping_list_2()
        {
            List<mapping> myList = new List<mapping>()
           {
         //mapping(int val_mappingId, String val_mappingName, String val_mappingPath,   String val_processArea, String val_goal, String val_practice, int val_projectId, bool val_upForDeletion)
              new  mapping( 1, "name_1", "path_1","sg_1", "sp_1", "gg_1", "gp_1", 1),
              new  mapping( 2, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
              new  mapping( 3, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
              new  mapping( 4, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
              new  mapping( 5, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 5),
           };
            return myList;
        }//make_mapping_list_2

        static List<mapping> make_mapping_list_3()
        {
            List<mapping> myList = new List<mapping>()
           {
              new  mapping( 1, "name_1", "path_1","sg_1", "sp_1", "gg_1", "gp_1", 1),
              new  mapping( 2, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
              new  mapping( 3, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
              new  mapping( 4, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
              new  mapping( 5, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 5),

           };
            return myList;
        }//make_mapping_list_3

        static List<mapping> make_mapping_list_4A()
        {
            List<mapping> myList = new List<mapping>()
           {
              new  mapping( 1, "name_1", "path_1","sg_1", "sp_1", "gg_1", "gp_1", 1),
              new  mapping( 2, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
              new  mapping( 3, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
              new  mapping( 4, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
              new  mapping( 5, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 5),

           };
            return myList;
        }

        static List<mapping> make_mapping_list_4B()
        {
            List<mapping> myList = new List<mapping>()
           {
              new  mapping( 1, "name_1", "path_1","sg_1", "sp_1", "gg_1", "gp_1", 1),
              new  mapping( 2, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
              new  mapping( 3, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
              new  mapping( 4, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
              new  mapping( 5, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 5),

           };
            return myList;
        }//make_mapping_list_3


        static List<mapping> make_mapping_list_5_404()
        {

            List<mapping> myList = new List<mapping>()
           {
              new  mapping( 1, "name_1", "path_1","sg_1", "sp_1", "gg_1", "gp_1", 1),
              new  mapping( 2, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
              new  mapping( 3, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
              new  mapping( 4, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
              new  mapping( 5, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 5),
            };
            return myList;
        }//make_mapping_list_5_404




        static void TestDBI_T_mapping_AutoCheck()
        {
            Console.WriteLine("START: TestDBI_T_mapping_AutoCheck()");
            int iResult = TestDBI_T_mapping_AutoCheck_WriteRead();
            if (iResult == 0)
                Console.WriteLine("OK: TestDBI_T_mapping_AutoCheck_WriteRead");
            else
                Console.WriteLine("ERROR: TestDBI_T_mapping_AutoCheck_WriteRead:    iResult=" + iResult);

            Console.WriteLine("DONE: TestDBI_T_mapping_AutoCheck()");
        }


        /// <summary>
        /// TestDBI_T_mapping_AutoCheck_WriteRead - Write,Read,Compare Item List;
        /// 1.1) Create test data: myTable1;
        /// 1.2) Clear DBTable;
        /// 1.3) Write myTable1 to DBTable; 
        /// 1.4) Get DBTable.CountRows, compare (myTable1.itemList.Count == DBTable.CountRows)
        /// 1.5) Read myTable2 from DBTable
        /// 1.6) Compare tables (myTable1 == myTable2)
        /// </summary>
        /// <returns></returns>
        static int TestDBI_T_mapping_AutoCheck_WriteRead()
        {
            const int OK = 0;
            int iResult = OK;
            Console.WriteLine("START: TestDBI_T_mapping_AutoCheck_WriteRead()");

            // 1.1) CreateTestData1: myTable1
            mapping_Table myTable1 = new mapping_Table();
            myTable1.itemList = new List<mapping>()
            {
                // mapping(int val_mappingId, String val_mappingName, String val_mappingPath, 
                //                  String val_processArea, String val_goal, String val_practice, int val_projectId, bool val_upForDeletion)
              new  mapping( 1, "name_1", "path_1","sg_1", "sp_1", "gg_1", "gp_1", 1),
              new  mapping( 2, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 2),
              new  mapping( 3, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 3),
              new  mapping( 4, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 4),
              new  mapping( 5, "name_1", "path_1", "sg_1", "sp_1", "gg_1", "gp_1", 5),
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
            mapping_Table myTable2 = new mapping_Table();
            myTable2.ReadItemListFromDatabase();

            /// 1.6) Compare tables (myTable1 == myTable2)
            if (!TestDBI_T_mapping_CompareLists(myTable1.itemList, myTable2.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as test data");
                return iResult;
            }
            Console.WriteLine("OK!  DBTable & test data match");

            Console.WriteLine("DONE: TestDBI_T_mapping_AutoCheck_WriteRead()");
            return iResult;
        }

        /// <summary>
        /// TestDBI_T_mapping_CompareLists --
        ///   true if same contents
        ///   false if there are any differences
        /// </summary>
        /// <param name="itemList1"></param>
        /// <param name="itemList2"></param>
        /// <returns></returns>
        static bool TestDBI_T_mapping_CompareLists(List<mapping> itemList1, List<mapping> itemList2)
        {
            if (itemList1.Count != itemList2.Count)
                return false;

            SortedList<int, mapping> sorteditemList1 = new SortedList<int, mapping>();
            foreach (var r in itemList1)
                sorteditemList1.Add(r.mappingId, r);//sort by key:  r.mappingId

            SortedList<int, mapping> sorteditemList2 = new SortedList<int, mapping>();
            foreach (var r in itemList2)
                sorteditemList2.Add(r.mappingId, r); //sort by key:  r.mappingId

            //compare sorted lists for equivalence for each row of data
            foreach (var iKey in sorteditemList1.Keys)
            {
                //method Compare directly compares each field individually
                if (!sorteditemList1[iKey].Equals(sorteditemList2[iKey]))
                    return false;
            }
            return true;
        }//TestDBI_T_mapping_CompareLists



    }//class
}//namespace
