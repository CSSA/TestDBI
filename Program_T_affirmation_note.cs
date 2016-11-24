using System;
using System.Collections.Generic;

using SQLServerDB;



namespace TestDBI
{
    partial class Program
    {
        static void TestDBI_T_affirmation_note()
        {
            Console.WriteLine("  --START: TestDBI_T_affirmation_note ");

            switch (iSubMenuSelection())
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
                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_T_affirmation_note ");
        }


        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_affirmation_note_Write_to_DB()
        {
            Console.WriteLine("  --START: TestDBI_T_affirmation_note_Write_to_DB");

            SQLServerDB.affirmation_note_Table myTable = new SQLServerDB.affirmation_note_Table();
            myTable.itemList = make_affirmation_note_list_1();
            int iRowsStart = myTable.itemList.Count;
            myTable.Show();
            pause();

            Console.WriteLine("  --before clear SQLServer database table");
            pause();
            myTable.Clear_Database_Table();
            int iRows2 = myTable.CountRows();
            if (iRows2 != 0)
                pause("Error.  iRows=" + iRows2 + " should be zero after Clear_Database_Table()");
            else
                pause("OK.  After Clear_Database_Table()");


            Console.WriteLine("Write the table from RAM the SQLServer  Database table");
            myTable.WriteItemListToDatabase();
            int iRows3 = myTable.CountRows();
            if (iRows3 != iRowsStart)
                pause("Error.  iRows3=" + iRows3 + " should be " + iRowsStart + " after WriteItemListToDatabase");
            else
                pause("OK.  After WriteItemListToDatabase()");

            Console.WriteLine("  --after writing to the SQLServer database table.  examine the table using SSMS");
            pause("visually inspect via SSMS?");

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

            pause();

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
            pause("--BEFORE the update, showing the planned updates in myTable.itemList");

            myTable.UpdateItemListToDatabase();
            pause("-- AFTER the update, examine the affirmation_note Table using SSMS");

            myTable.Clear_Database_Table_By_AffirmationID(2);
            myTable.Clear_Database_Table_By_AffirmationID(4);
            myTable.Clear_Database_Table_By_AffirmationID(6);
            pause("-- AFTER Clear_Database_Table_By_AffirmationID {2,4,6} using SSMS");

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

    }
}
