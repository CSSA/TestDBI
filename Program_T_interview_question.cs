using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDBI
{
    partial class Program
    {
       

        static void TestDBI_T_interview_question()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_question ");

            switch (iSubMenuSelection())
            {
                case 1:
                    TestDBI_T_interview_question_ADODB_to_SQLServer();
                    break;
                case 2:
                    TestDBI_T_interview_question_SQLServer_to_ADODB();
                    break;
                case 3:
                    TestDBI_T_interview_question_T3();
                    break;
                case 4:
                    TestDBI_T_interview_question_T4();
                    break;
                case 5:
                    TestDBI_T_interview_question_T5();
                    break;
                default:
                    Console.WriteLine("that is not a vaild option...");
                    break;
            }
            Console.WriteLine("  --DONE: TestDBI_T_interview_question ");
        }


        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_interview_question_ADODB_to_SQLServer()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_question_ADODB_to_SQLServer");


            SQLServerDB.interview_question_Table myTable = new SQLServerDB.interview_question_Table();

            int iRows = myTable.ADODB_CountRows();
            Console.WriteLine("myTable.ADODB_CountRows = " + iRows.ToString());

            Console.WriteLine("Fill the table in RAM from the ADODB  Database table");
            myTable.ADODB_ReadItemListFromDatabase();
            myTable.Show();

            pause("  --before clear SQLServer database table. item RAM-based itemList has been filled from MS Access table");

            myTable.Clear_Database_Table();
            pause("  --after clearing SQLServer database table.  examine the table using SSMS");


            Console.WriteLine("Write the table from RAM the SQLServer  Database table");
            myTable.WriteItemListToDatabase();
            pause("  --after writing to the SQLServer database table.  examine the table using SSMS");

            int iRows2 = myTable.CountRows();
            Console.WriteLine("myTable.CountRows = " + iRows2.ToString());
            pause();

            Console.WriteLine("  --DONE: TestDBI_T_interview_question_ADODB_to_SQLServer");
        }

        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_interview_question_SQLServer_to_ADODB()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_question_SQLServer_to_ADODB");
#if __COMMENT__

            SQLServerDB.interview_question_Table myTable = new SQLServerDB.interview_question_Table();

            int iRows = myTable.CountRows();
            Console.WriteLine("myTable.SQLServer_CountRows = " + iRows.ToString());

            Console.WriteLine("Fill the table in RAM from the SQLServer Database table");
            myTable.ReadItemListFromDatabase();
            myTable.Show();

            Console.WriteLine("  --before clear ADODB database table");
            myTable.ADODB_Clear_Database_Table();

            pause("  --after clearing ADODB database table.  examine the table using MSAccess");


            Console.WriteLine("Write the table from RAM the ADODB  Database table");
            myTable.ADODB_WriteItemListToDatabase();
            pause("  --after writing to the  ADODB database table.  examine the table using MSAccess");

#endif
            Console.WriteLine("  --DONE: TestDBI_T_interview_question_SQLServer_to_ADODB");
        }

        //-------------------------------------------------------------------------------------------
        static void TestDBI_T_interview_question_T3()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_question_T3");
#if __COMMENT__

            //Construct myTable in RAM
            SQLServerDB.interview_question_Table myTable = new SQLServerDB.interview_question_Table();


            //put demo records into myTable in RAM
            myTable.itemList.Clear();
            for (int i = 1; i < 10; i++)
            {
                SQLServerDB.interview_question iqlItem = new SQLServerDB.interview_question();

                iqlItem.ID = i;  //actually, a don't care; it will not be stored
                iqlItem.interview_questionName = "Name_" + i.ToString();
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


            //put demo records into myTable in RAM
            myTable.itemList.Clear();
            for (int i = 1; i < 10; i++)
            {
                SQLServerDB.interview_question interview_questionItem = new SQLServerDB.interview_question();
                interview_questionItem.ID = i;  //actually, a don't care; it will not be stored
                interview_questionItem.interview_questionName = "Name_" + i.ToString();
                interview_questionItem.Creator = "Creator_" + i.ToString() + "_REV-A";  //modify the text!
                interview_questionItem.MaturityLevel = i + 100;                //A valid maturity level is not really needed.  Test only demonstrates storing/retreiving integers.
                interview_questionItem.Projects = "Projects_" + i.ToString() + "_REV-A";       //modify the text!
                interview_questionItem.SAMSelected = Convert.ToBoolean(1 + (i % 2)); // Alternate between true/false
                interview_questionItem.SSDSelected = Convert.ToBoolean(1 + (i % 2)); // Alternate between true/false

                myTable.itemList.Add(interview_questionItem);
            }
            myTable.Show();
            pause("--BEFORE the update, showing the planned updates in myTable.itemList");

            SQLServerDB.interview_question appRef1 = myTable.itemList[0];
            appRef1.MaturityLevel = appRef1.MaturityLevel + 200;
            appRef1.Projects = appRef1.Projects + ",P99";

            SQLServerDB.interview_question appRef2 = myTable.itemList[3];
            appRef2.MaturityLevel = appRef2.MaturityLevel + 300;
            appRef2.Projects = appRef2.Projects + ",P99";

            myTable.UpdateItemListToDatabase();
            pause("-- AFTER the update, examine the interview_question Table using SSMS");

#endif
            Console.WriteLine("  --DONE: TestDBI_T_interview_question_T3");
        }//TestDBI_T_interview_question_T3


        static void TestDBI_T_interview_question_T4()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_question_T4");
            Console.WriteLine("  -----   TBD:   do something here??");
            Console.WriteLine("  --DONE: TestDBI_T_interview_question_T4");
        }

        static void TestDBI_T_interview_question_T5()
        {
            Console.WriteLine("  --START: TestDBI_T_interview_question_T5");
            Console.WriteLine("  -----   TBD:   do something here??");
            Console.WriteLine("  --DONE: TestDBI_T_interview_question_T5");
        }

    }
}
