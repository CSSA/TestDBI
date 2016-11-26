using System;
using System.Collections.Generic;

using SQLServerDB;



namespace TestDBI
{
    class TestDBI_affirmation_note
    {
        public static void SelectTest()
        {
            Console.WriteLine("  --START: TestDBI_affirmation_note.SelectTest()");

            switch (Program.iSubMenuSelection())
            {
                case 1:
                    TestDBI_T_affirmation_note_Write_to_DB();
                    break;
                case 2:
                    TestDBI_T_affirmation_note_Read_from_DB();
                    break;
                case 3:
                    TestDBI_T_affirmation_note_T3();
                    break;
                case 4:
                    TestDBI_T_affirmation_note_T4();
                    break;
                case 5:
                    TestDBI_T_affirmation_note_T5();
                    break;
                case 10:
                    TestDBI_T_affirmation_note_AutoCheck();
                    break;
                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_affirmation_note.SelectTest()");
        }


        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_affirmation_note_Write_to_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_affirmation_note_Write_to_DB");

            SQLServerDB.affirmation_note_Table myTable = new SQLServerDB.affirmation_note_Table();
            myTable.itemList = make_affirmation_note_list_1();
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

            Console.WriteLine("  --DONE: TestDBI_T_affirmation_note_Write_to_DB");
        }//TestDBI_T_affirmation_note_Write_to_DB

        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_affirmation_note_Read_from_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_affirmation_note_Read_from_DB");

            SQLServerDB.affirmation_note_Table myTable = new SQLServerDB.affirmation_note_Table();

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

            Console.WriteLine("  --DONE: TestDBI_T_affirmation_note_Read_from_DB");
        }//TestDBI_T_affirmation_note_Read_from_DB

        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_affirmation_note_T3()
        {
            Console.WriteLine("  --START: TestDBI_T_affirmation_note_T3");


            //Construct myTable in RAM
            SQLServerDB.affirmation_note_Table myTable = new SQLServerDB.affirmation_note_Table();

            //put demo records into myTable
            for (int i = 1; i < 10; i++)
            {
                SQLServerDB.affirmation_note affItem = new SQLServerDB.affirmation_note();
                affItem.affirmationId = i;
                affItem.notes = "aff_note_" + i.ToString();

                myTable.itemList.Add(affItem);
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


            //put demo records into myTable
            myTable.itemList.Clear();
            for (int i = 1; i < 10; i++)
            {
                SQLServerDB.affirmation_note affItem = new SQLServerDB.affirmation_note();
                affItem.affirmationId = i;
                affItem.notes = "aff_note_" + i.ToString() + "_Rev_A";

                myTable.itemList.Add(affItem);
            }
            myTable.Show();
            Util.pause("--BEFORE the update, showing the planned updates in myTable.itemList");

            myTable.UpdateItemListToDatabase();
            Util.pause("-- AFTER the update, examine the affirmation_note Table using SSMS");

            myTable.Clear_Database_Table_By_AffirmationID(2);
            myTable.Clear_Database_Table_By_AffirmationID(4);
            myTable.Clear_Database_Table_By_AffirmationID(6);
            Util.pause("-- AFTER Clear_Database_Table_By_AffirmationID {2,4,6} using SSMS");

            Console.WriteLine("  --DONE: TestDBI_T_affirmation_note_T3");
        }//TestDBI_T_affirmation_note_T3


        static void TestDBI_T_affirmation_note_T4()
        {
            Console.WriteLine("  --START: TestDBI_T_affirmation_note_T4");
            Console.WriteLine("  -----   TBD:   do something here??");
            Console.WriteLine("  --DONE: TestDBI_T_affirmation_note_T4");
        }

        static void TestDBI_T_affirmation_note_T5()
        {
            Console.WriteLine("  --START: TestDBI_T_affirmation_note_T5");
            Console.WriteLine("  -----   TBD:   do something here??");
            Console.WriteLine("  --DONE: TestDBI_T_affirmation_note_T5");
        }



        static List<affirmation_note> make_affirmation_note_list_1()
        {
            List<affirmation_note> myList = new List<affirmation_note>()
                         {
                          new  affirmation_note(1, "val_notes_1"),
                          new  affirmation_note(2, "val_notes_2"),
                          new  affirmation_note(3, "val_notes_3"),
                          new  affirmation_note(4, "val_notes_4"),
                          new  affirmation_note(5, "val_notes_5"),
           };
            return myList;
        }//make_affirmation_list_1


        static List<affirmation_note> make_affirmation_note_list_2()
        {
            List<affirmation_note> myList = new List<affirmation_note>()
                       {
                       new  affirmation_note(1, "val_notes_1-Rev-A"),
                       new  affirmation_note(2, "val_notes_2-Rev-A"),
                       new  affirmation_note(3, "val_notes_3-Rev-A"),
                       new  affirmation_note(4, "val_notes_4-Rev-A"),
                       new  affirmation_note(5, "val_notes_5-Rev-A"),            };
            return myList;
        }//make_affirmation_list_2





        static void TestDBI_T_affirmation_note_AutoCheck()
        {
            Console.WriteLine("START: TestDBI_T_affirmation_note_AutoCheck()");
            int iResult;

            iResult = TestDBI_T_affirmation_note_AutoCheck_WriteRead();
            if (iResult == 0)
                Console.WriteLine("OK: TestDBI_T_affirmation_note_AutoCheck_WriteRead");
            else
                Console.WriteLine("ERROR: TestDBI_T_affirmation_note_AutoCheck_WriteRead:    iResult=" + iResult);



            iResult = TestDBI_T_affirmation_note_AutoCheck_Update();
            if (iResult == 0)
                Console.WriteLine("OK: TestDBI_T_affirmation_note_AutoCheck_Update");
            else
                Console.WriteLine("ERROR: TestDBI_T_affirmation_note_AutoCheck_Update:    iResult=" + iResult);

            iResult = TestDBI_T_affirmation_note_AutoCheck_Update();

            Console.WriteLine("DONE: TestDBI_T_affirmation_note_AutoCheck()");
        }


        /// <summary>
        /// TestDBI_T_affirmation_note_AutoCheck_WriteRead - Write,Read,Compare Item List;
        /// 1.1) Create test data: myTable1;
        /// 1.2) Clear DBTable;
        /// 1.3) Write myTable1 to DBTable; 
        /// 1.4) Get DBTable.CountRows, compare (myTable1.itemList.Count == DBTable.CountRows)
        /// 1.5) Read myTable2 from DBTable
        /// 1.6) Compare tables (myTable1 == myTable2)
        /// </summary>
        /// <returns></returns>
        static int TestDBI_T_affirmation_note_AutoCheck_WriteRead()
        {
            const int OK = 0;
            int iResult = OK;
            Console.WriteLine("START: TestDBI_T_affirmation_note_AutoCheck_WriteRead()");

            // 1.1) CreateTestData1: myTable1
            affirmation_note_Table myTable1 = new affirmation_note_Table();
            myTable1.itemList = new List<affirmation_note>()
            {
                // affirmation_note(int val_affirmationId, String val_notes)
           new  affirmation_note( 1, "aff_notes_1"),
           new  affirmation_note( 2, "aff_notes_2"),
           new  affirmation_note( 3, "aff_notes_3"),
           new  affirmation_note( 4, "aff_notes_4"),
           new  affirmation_note( 5, "aff_notes_5"),
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
            affirmation_note_Table myTable2 = new affirmation_note_Table();
            myTable2.ReadItemListFromDatabase();

            /// 1.6) Compare tables (myTable1 == myTable2)
            if (!TestDBI_T_affirmation_note_CompareLists(myTable1.itemList, myTable2.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as test data");
                return iResult;
            }
            Console.WriteLine("OK!  DBTable & test data match");

            Console.WriteLine("DONE: TestDBI_T_affirmation_note_AutoCheck_WriteRead()");
            return iResult;
        }


        /// <summary>
        /// TestDBI_T_affirmation_note_AutoCheck_Update - Update Item List;
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
        static int TestDBI_T_affirmation_note_AutoCheck_Update()
        {
            const int OK = 0;
            int iResult = OK;
            Console.WriteLine("START: TestDBI_T_affirmation_note_AutoCheck_Update()");

            // 1.1) CreateTestData1: myTable1
            affirmation_note_Table myTable1 = new affirmation_note_Table();
            myTable1.itemList = new List<affirmation_note>()
            {
                // affirmation_note(int val_affirmationId, String val_notes)
           new  affirmation_note( 1, "aff_notes_1"),
           new  affirmation_note( 2, "aff_notes_2"),
           new  affirmation_note( 3, "aff_notes_3"),
           new  affirmation_note( 4, "aff_notes_4"),
           new  affirmation_note( 5, "aff_notes_5")
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
            affirmation_note_Table myTable2 = new affirmation_note_Table();
            myTable2.ReadItemListFromDatabase();

            /// 1.6) Compare tables (myTable1 == myTable2)
            if (!TestDBI_T_affirmation_note_CompareLists(myTable1.itemList, myTable2.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as test data");
                return iResult;
            }

            /// 1.7) Create the update table (myTableUpdate)
            affirmation_note_Table myTableUpdate = new affirmation_note_Table();
            myTableUpdate.itemList = new List<affirmation_note>()
            {
                // affirmation_note(int val_affirmationId, String val_notes)
           new  affirmation_note( 1, "aff_notes_1"),
           new  affirmation_note( 2, "aff_notes_2-modified"),
           new  affirmation_note( 3, "aff_notes_3"),
           new  affirmation_note( 4, "aff_notes_4-modified"),
           new  affirmation_note( 5, "aff_notes_5")
            };


            //1.8) Update TableDB
            myTableUpdate.UpdateItemListToDatabase();

            //1.9) Read myTable3
            affirmation_note_Table myTable3 = new affirmation_note_Table();
            myTable3.ReadItemListFromDatabase();


            //1.10) Compare tables.itemLists (myTableUpdate == myTable3)
            if (!TestDBI_T_affirmation_note_CompareLists(myTableUpdate.itemList, myTable3.itemList))
            {
                iResult = -1;
                Console.WriteLine("Error: DBTable should be same as the update table");
                return iResult;
            }


            Console.WriteLine("DONE: TestDBI_T_affirmation_note_AutoCheck_Update()");
            return iResult;
        }



        /// <summary>
        /// TestDBI_T_affirmation_CompareLists --
        ///   true if same contents
        ///   false if there are any differences
        /// </summary>
        /// <param name="itemList1"></param>
        /// <param name="itemList2"></param>
        /// <returns></returns>
        static bool TestDBI_T_affirmation_note_CompareLists(List<affirmation_note> itemList1, List<affirmation_note> itemList2)
        {
            if (itemList1.Count != itemList2.Count)
                return false;

            SortedList<int, affirmation_note> sorteditemList1 = new SortedList<int, affirmation_note>();
            foreach (var r in itemList1)
                sorteditemList1.Add(r.affirmationId, r);//sort by key:  r.affirmationId

            SortedList<int, affirmation_note> sorteditemList2 = new SortedList<int, affirmation_note>();
            foreach (var r in itemList2)
                sorteditemList2.Add(r.affirmationId, r); //sort by key:  r.affirmationId

            //compare sorted lists for equivalence for each row of data
            foreach (int iKey in sorteditemList1.Keys)
            {
                //method Compare directly compares each field individually
                if (!sorteditemList1[iKey].Equals(sorteditemList2[iKey]))
                    return false;
            }
            return true;
        }//TestDBI_T_affirmation_CompareLists
    }
}
