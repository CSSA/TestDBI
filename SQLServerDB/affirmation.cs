using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLServerDB
{
    public class affirmation
    {
        public const int Unassigned = -1;
        public int ID { get; set; }                         //Autonumbered by the DBMS; -1 if not initialized, otherwise assigned by the DBMS
        public int affirmationId { get; set; }
        public string affirmationName { get; set; }              //the name of the CAP user who created this  
        public string affirmationType { get; set; }
        public string specificGoal { get; set; }
        public string specificPractice { get; set; }
        public string genericGoal { get; set; }
        public string genericPractice { get; set; }
        public string processArea { get; set; }
        public int projectId { get; set; }



        //Default Constructor with no data initialization
        public affirmation()
        {
            this.ID = Unassigned;               //this field is controlled by the DBMS, i.e., AutoNumbered
            this.affirmationId = Unassigned;
            this.affirmationName = string.Empty;
            this.affirmationType = string.Empty;
            this.specificGoal = string.Empty;
            this.specificPractice = string.Empty;
            this.genericGoal = string.Empty;
            this.genericPractice = string.Empty;
            this.processArea = string.Empty;
            this.projectId = Unassigned;
        }

        // The preferred Constructor, with initial values
        public affirmation(int val_affirmationId, String val_affirmationName, String val_affirmationType,
                                  String val_specificGoal, String val_specificPractice,
                                  String val_genericGoal, String val_genericPractice,
                                  String val_processArea, int projectId)
        {
            this.ID = Unassigned;               //this field is controlled by the DBMS, i.e., AutoNumbered
            this.affirmationId = val_affirmationId;
            this.affirmationName = val_affirmationName;
            this.affirmationType = val_affirmationType;
            this.specificGoal = val_specificGoal;
            this.specificPractice = val_specificPractice;
            this.genericGoal = val_genericGoal;
            this.genericPractice = val_genericPractice;
            this.processArea = val_processArea;
            this.projectId = Unassigned;
        }
        //----------------------------------------------------------------------------------
        public void Show()
        {
            Console.WriteLine("{0,5},{1,5},{2,5},{3,5},{4,5},{5,5},{6,5},{7,5},{8,5},{9,5}",
            ID,
            affirmationId,
            affirmationName,
            affirmationType,
            specificGoal,
            specificPractice,
            genericGoal,
            genericPractice,
            processArea,
            projectId
                );
        }//Show

    }
}
