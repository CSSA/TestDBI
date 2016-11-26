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
        public static int iMenuSelection()
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
        public static int iSubMenuSelection()
        {
            try
            {
                Console.WriteLine("Choose: 1=Write_to_DB,  2=Read_from_DB, 3=T3, 4=T4, 5=T5, 6=T6_Compare, 10=AutoCheck");
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


            bool Done = false;
            do
            {
                switch (iMenuSelection())
                {
                    //DBI :== Database Interface
                    //TestDBI :== Test Database Interface
                    case 1: TestDBI_affirmation.SelectTest(); break;
                    case 2: TestDBI_affirmation_note.SelectTest(); break;
                    case 3: TestDBI_appraisal.SelectTest(); break;
                    case 4: TestDBI_goal_practice.SelectTest(); break;
                    case 5: TestDBI_improvement_opportunity.SelectTest(); break;
                    case 6: TestDBI_interview_question.SelectTest(); break;
                    case 7: TestDBI_interview_session.SelectTest(); break;
                    case 8: TestDBI_mapping.SelectTest(); break;
                    case 9: TestDBI_mapping_note.SelectTest(); break;
                    case 10: TestDBI_process_area.SelectTest(); break;
                    case 11: TestDBI_project.SelectTest(); break;
                    case 12: TestDBI_strength.SelectTest(); break;
                    case 13: TestDBI_team_note.SelectTest(); break;
                    case 14: TestDBI_user.SelectTest(); break;
                    case 15: TestDBI_weakness.SelectTest(); break;
                    case -1: Done = true; break;

                    default:
                        Console.WriteLine("not a valid choice!"); break;
                }//switch
            } while (Done == false);

            Console.WriteLine("DONE: Main() -- Console Application SQLServer Database Admin");
            Util.pause();
        }// static void Main



   



       
    }
}
