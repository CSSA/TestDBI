using System;
using System.Collections.Generic;
using SQLServerDB;


namespace TestDBI
{
    class TestDBI_mapping_note
    {


        //--------------------------------------------------------------------------------------------
        public static void SelectTest()
        {
            Console.WriteLine("  --START: TestDBI_mapping_note.SelectTest()");

            switch (Program.iSubMenuSelection())
            {
                case 1:
                    TestDBI_T_mapping_note_Write_to_DB();
                    break;
                case 2:
                    TestDBI_T_mapping_note_Read_from_DB();
                    break;
                case 3:
                    TestDBI_T_mapping_note_T3();
                    break;
                case 4:
                    TestDBI_T_mapping_note_T4();
                    break;
                case 5:
                    TestDBI_T_mapping_note_T5();
                    break;
                case 10:
                    TestDBI_T_mapping_note_AutoCheck();
                    break;

                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_mapping_note.SelectTest()");
        }
        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// TestDBI_T_mapping_note_Write_to_DB -- write itemlist to DB
        /// </summary>
        static void TestDBI_T_mapping_note_Write_to_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_mapping_note_Write_to_DB");

            mapping_note_Table myTable = new mapping_note_Table();
            myTable.itemList = make_mapping_note_list_1();
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

            Console.WriteLine("  --DONE: TestDBI_T_mapping_note_Write_to_DB");
        }//TestDBI_T_mapping_note_Write_to_DB


        //-------------------------------------------------------------------------------------------
        /// <summary>
        /// TestDBI_T_mapping_note_Read_from_DB -- read from DB
        /// </summary>
        static void TestDBI_T_mapping_note_Read_from_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_mapping_note_Read_from_DB");

            SQLServerDB.mapping_note_Table myTable = new SQLServerDB.mapping_note_Table();

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
            Console.WriteLine("  --DONE: TestDBI_T_mapping_note_Read_from_DB");
        }//TestDBI_T_mapping_note_Read_from_DB



        /// <summary>
        /// TestDBI_T_mapping_note_T3 -  clear the SQLServer mapping_note table, write some demo data to SQLServer DB, 
        /// query the affirmatin table by Project ID, 
        /// </summary>
        static void TestDBI_T_mapping_note_T3()
        {
            Console.WriteLine("  --START: TestDBI_T_mapping_note_T3");

            //Construct myTable in RAM
            SQLServerDB.mapping_note_Table myTable = new SQLServerDB.mapping_note_Table();
            myTable.itemList = make_mapping_note_list_3();


            //Count SQLServerDB mapping_note table rows before clearing
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

            SQLServerDB.mapping_note itemSeek = myTable.itemList[0];
            itemSeek.Show();

            Util.pause("visual inspection via SSMS?");

            Console.WriteLine("  --DONE: TestDBI_T_mapping_note_T3");
        }

        /// <summary>
        /// TestDBI_T_mapping_note_T4 -- 
        /// </summary>
        static void TestDBI_T_mapping_note_T4()
        {
            Console.WriteLine("  --START: TestDBI_T_mapping_note_T4");

            //Construct a brand new myTable in RAM
            SQLServerDB.mapping_note_Table myTable = new SQLServerDB.mapping_note_Table();
            myTable.itemList = make_mapping_note_list_4();
            int iRowsStart = myTable.itemList.Count;


            //Count SQLServerDB mapping_note table rows before clearing
            int iRows = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows.ToString());

            Console.WriteLine("  --before clear SQLServer database table");
            Util.pause();

            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows2.ToString());
            Util.pause();

            foreach (SQLServerDB.mapping_note r in myTable.itemList)
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

            myTable.itemList = make_mapping_note_list_4B();

            Console.WriteLine("BEFORE the table update");
            Util.pause();
            myTable.UpdateItemListToDatabase();
            Console.WriteLine("AFTER the table update");
            Util.pause();


            for (int i = 4; i <= 6; i++)
                myTable.Clear_Database_Table_By_mappingId(i);
            Console.WriteLine("AFTER the table record deletions:  mapping_noteI={4,5,6}");
            Util.pause();

            Console.WriteLine("  --DONE: TestDBI_T_mapping_note_T4");
        }


        static void TestDBI_T_mapping_note_T5()
        {
            Console.WriteLine("  --START: TestDBI_T_mapping_note_T5");

            //Construct a brand new myTable in RAM
            SQLServerDB.mapping_note_Table myTable = new SQLServerDB.mapping_note_Table();
            myTable.itemList = make_mapping_note_list_4();
            int iRowsStart = myTable.itemList.Count;

            //Count SQLServerDB mapping_note table rows before clearing
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


            Console.WriteLine("  --DONE: TestDBI_T_mapping_note_T5");
        }


        static List<mapping_note> make_mapping_note_list_1()
        {
            List<mapping_note> myList = new List<mapping_note>()
            {
                //mappingId, notes
             new  mapping_note( 1, "notes_1"),
             new  mapping_note( 2, "notes_2"),
             new  mapping_note( 3, "notes_3"),
             new  mapping_note( 4, "notes_4"),
             new  mapping_note( 5, "notes_5")
           };
            return myList;
        }//make_mapping_note_list_1


        static List<mapping_note> make_mapping_note_list_2()
        {
            List<mapping_note> myList = new List<mapping_note>()
           {
              new  mapping_note( 1, "notes_1_REV_a"),
             new  mapping_note( 2, "notes_2_REV_b"),
             new  mapping_note( 3, "notes_3_REV_c"),
             new  mapping_note( 4, "notes_4_REV_d"),
             new  mapping_note( 5, "notes_5_REV_e")
           };
            return myList;
        }//make_mapping_note_list_2

        static List<mapping_note> make_mapping_note_list_3()
        {
            List<mapping_note> myList = new List<mapping_note>()
           {
              new  mapping_note( 1, "notes_1"),
             new  mapping_note( 2, "notes_2"),
             new  mapping_note( 3, "notes_3"),
             new  mapping_note( 4, "notes_4"),
             new  mapping_note( 5, "notes_5")
           };
            return myList;
        }//make_mapping_note_list_3


        static List<mapping_note> make_mapping_note_list_4()
        {
            List<mapping_note> myList = new List<mapping_note>()
           {
              new  mapping_note( 1, "notes_1"),
             new  mapping_note( 2, "notes_2"),
             new  mapping_note( 3, "notes_3"),
             new  mapping_note( 4, "notes_4"),
             new  mapping_note( 5, "notes_5")
           };
            return myList;
        }//make_mapping_note_list_4

        static List<mapping_note> make_mapping_note_list_4B()
        {
            List<mapping_note> myList = new List<mapping_note>()
           {
              new  mapping_note( 1, "notes_1_A"),
             new  mapping_note( 2, "notes_2_B"),
             new  mapping_note( 3, "notes_3_C"),
             new  mapping_note( 4, "notes_4_D"),
             new  mapping_note( 5, "notes_5_E")
           };
            return myList;
        }//make_mapping_note_list_4


        static List<mapping_note> make_mapping_note_list_5()
        {
            List<mapping_note> myList = new List<mapping_note>()
           {
              new  mapping_note(1, "notes_1"),
             new  mapping_note( 2, "notes_2"),
             new  mapping_note( 3, "notes_3"),
             new  mapping_note( 4, "notes_4"),
             new  mapping_note( 5, "notes_5")
           };
            return myList;
        }//make_mapping_note_list_5



        static void TestDBI_T_mapping_note_AutoCheck()
        {
            Console.WriteLine("START: TestDBI_T_mapping_note_AutoCheck()");
            int iResult = TestDBI_T_mapping_note_AutoCheck_WriteRead();
            if (iResult == 0)
                Console.WriteLine("OK: TestDBI_T_mapping_note_AutoCheck_WriteRead");
            else
                Console.WriteLine("ERROR: TestDBI_T_mapping_note_AutoCheck_WriteRead:    iResult=" + iResult);

            Console.WriteLine("DONE: TestDBI_T_mapping_note_AutoCheck()");
        }


        /// <summary>
        /// TestDBI_T_mapping_note_AutoCheck_WriteRead - Write,Read,Compare Item List;
        /// 1.1) Create test data: myTable1;
        /// 1.2) Clear DBTable;
        /// 1.3) Write myTable1 to DBTable; 
        /// 1.4) Get DBTable.CountRows, compare (myTable1.itemList.Count == DBTable.CountRows)
        /// 1.5) Read myTable2 from DBTable
        /// 1.6) Compare tables (myTable1 == myTable2)
        /// </summary>
        /// <returns></returns>
        static int TestDBI_T_mapping_note_AutoCheck_WriteRead()
        {
            const int OK = 0;
            int iResult = OK;
            Console.WriteLine("START: TestDBI_T_mapping_note_AutoCheck_WriteRead()");

            // 1.1) CreateTestData1: myTable1
            mapping_note_Table myTable1 = new mapping_note_Table();
            myTable1.itemList = new List<mapping_note>()
            {
            // mapping_note(int val_mappingId, String val_notes)
            new mapping_note(1, "note_1"),
            new mapping_note(2, "note_2"),
            new mapping_note(3, "note_3"),
            new mapping_note(4, "note_4"),
            new mapping_note(5, "note_5")
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
            mapping_note_Table myTable2 = new mapping_note_Table();
            myTable2.ReadItemListFromDatabase();

            /// 1.6) Compare tables (myTable1 == myTable2)
            if (!TestDBI_T_mapping_note_CompareLists(myTable1.itemList, myTable2.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as test data");
                return iResult;
            }
            Console.WriteLine("OK!  DBTable & test data match");

            Console.WriteLine("DONE: TestDBI_T_mapping_note_AutoCheck_WriteRead()");
            return iResult;
        }

        /// <summary>
        /// TestDBI_T_mapping_note_CompareLists --
        ///   true if same contents
        ///   false if there are any differences
        /// </summary>
        /// <param name="itemList1"></param>
        /// <param name="itemList2"></param>
        /// <returns></returns>
        static bool TestDBI_T_mapping_note_CompareLists(List<mapping_note> itemList1, List<mapping_note> itemList2)
        {
            if (itemList1.Count != itemList2.Count)
                return false;

            SortedList<int, mapping_note> sorteditemList1 = new SortedList<int, mapping_note>();
            foreach (var r in itemList1)
                sorteditemList1.Add(r.mappingId, r);//sort by key:  r.mappingId

            SortedList<int, mapping_note> sorteditemList2 = new SortedList<int, mapping_note>();
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
        }//TestDBI_T_mapping_note_CompareLists


    }
}
