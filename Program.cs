using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLServerDB;

namespace TestDBI
{
    partial class Program
    {
        const int iNoSelection = 0;
        //---------------------------------------------
        /// <summary>
        /// static int iMenuSelection -- provide the primary menu system for this console application.
        /// return the integer associated with the selected menu item.
        /// </summary>
        /// <returns></returns>
        static int iMenuSelection()
        {
            try
            {
                Console.WriteLine("Choose: 1=T_affirmation");
                Console.WriteLine("Choose: 2=T_affirmation_note");
                Console.WriteLine("Choose: 3=T_appraisal");
                Console.WriteLine("Choose: 4=T_goal_practice");
                Console.WriteLine("Choose: 5=T_improvement_opportunity");
                Console.WriteLine("Choose: 6=T_interview_question");
                Console.WriteLine("Choose: 7=T_interview_session");
                Console.WriteLine("Choose: 8=T_mapping");
                Console.WriteLine("Choose: 9=T_mapping_note");
                Console.WriteLine("Choose: 10=T_process_area");
                Console.WriteLine("Choose: 11=T_project");
                Console.WriteLine("Choose: 12=T_strength");
                Console.WriteLine("Choose: 13=T_team_note");
                Console.WriteLine("Choose: 14=T_user");
                Console.WriteLine("Choose: 15=T_weakness");
                Console.WriteLine("Choose: -1=QUIT");

                string s = Console.ReadLine();
                int iSelect = Convert.ToInt32(s);
                return iSelect;
            }
            catch (Exception)
            {
                Console.WriteLine("That is not a valid integer!");
                return iNoSelection;
            }
        }//static int iMenuSelection()


        //---------------------------------------------
        static int iSubMenuSelection()
        {
            try
            {
                Console.WriteLine("Choose: 1=Write_to_DB,  2=Read_from_DB, 3=T3, 4=T4, 5=T5");
                Console.WriteLine("Choose: -1=QUIT");

                string s = Console.ReadLine();
                int iSelect = Convert.ToInt32(s);
                return iSelect;
            }
            catch (Exception)
            {
                Console.WriteLine("That is not a valid integer!");
                return iNoSelection;
            }
        }//static int iSubMenuSelection()



        //-------------------------------------------------------------------------------------------------------------
        static void Main(string[] args)
        {

            Console.WriteLine("START: Main() -- Console Application SQLServer Database Admin");
            Console.WriteLine("                       -- Read MS Access Database & write to SQLServer Database");
            Console.WriteLine("                       -- OR ");
            Console.WriteLine("                       -- Read SQLServer Database & write to MS Access Database");

            //Initialize the Connection Strings
            Initialize_SQLServer_ConnectionString();
            Initialize_ADO_Connection_String();

            bool Done = false;
            do
            {
                switch (iMenuSelection())
                {
                    //DBI :== Database Interface
                    //TestDBI :== Test Database Interface
                    case 1: TestDBI_T_affirmation(); break;
                    case 2: TestDBI_T_affirmation_note(); break;
                    case 3: TestDBI_T_appraisal(); break;
                    case 4: TestDBI_T_goal_practice(); break;
                    case 5: TestDBI_T_improvement_opportunity(); break;
                    case 6: TestDBI_T_interview_question(); break;
                    case 7: TestDBI_T_interview_session(); break;
                    case 8: TestDBI_T_mapping(); break;
                    case 9: TestDBI_T_mapping_note(); break;
                    case 10: TestDBI_T_process_area(); break;
                    case 11: TestDBI_T_project(); break;
                    case 12: TestDBI_T_strength(); break;
                    case 13: TestDBI_T_team_note(); break;
                    case 14: TestDBI_T_user(); break;
                    case 15: TestDBI_T_weakness(); break;
                    case -1: Done = true; break;

                    default:
                        Console.WriteLine("not a valid choice!"); break;
                }//switch
            } while (Done == false);

            Console.WriteLine("DONE: Main() -- Console Application SQLServer Database Admin");
            pause();
        }// static void Main





        static void TestDBI_T_mapping() { }



        static void TestDBI_T_mapping_note() { }


        static void TestDBI_T_process_area() { }



        static void TestDBI_T_project() { }



        static void TestDBI_T_strength() { }


        static void TestDBI_T_team_note() { }



        static void TestDBI_T_user() { }



        static void TestDBI_T_weakness() { }


        //----------------------------------------------------------------------------------
        public static void pause()
        {
            Console.WriteLine("pausing...");
            Console.ReadKey();
        }//void pause()
        public static void pause(String msg)
        {
            Console.WriteLine(msg);
            Console.WriteLine("pausing...");
            Console.ReadKey();
        }//void pause()
    }
}
