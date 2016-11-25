using System;


namespace SQLServerDB
{
    public class interview_session
    {
        public const int Unassigned = -1;
        public int ID { get; set; }                         //Autonumbered by the DBMS; -1 if not initialized, otherwise assigned by the DBMS
        public int sessionId { get; set; }
        public int sessionIndex { get; set; }
        public string sessionName { get; set; }              //the name of the CAP user who created this appraisal 
        public int sessionDurationHours { get; set; }
        public int sessionDurationMinutes { get; set; }
        public string sessionDuration { get; set; }
        public string specificGoal { get; set; }
        public string specificPractice { get; set; }
        public string genericGoal { get; set; }
        public string genericPractice { get; set; }
        public string processArea { get; set; }
        public int projectId { get; set; }



        //Default Constructor with no data initialization
        public interview_session()
        {
            this.ID = Unassigned;               //this field is controlled by the DBMS, i.e., AutoNumbered
            this.sessionId = Unassigned;
            this.sessionIndex = 0;
            this.sessionName = string.Empty;
            this.sessionDurationHours = Unassigned;
            this.sessionDurationMinutes = Unassigned;
            this.sessionDuration = sessionDurationHours.ToString() + ":" + sessionDurationMinutes.ToString();
            this.specificGoal = string.Empty;
            this.specificPractice = string.Empty;
            this.genericGoal = string.Empty;
            this.genericPractice = string.Empty;
            this.processArea = string.Empty;
        }

        // The preferred Constructor, with initial values
        public interview_session(int val_sessionId, int val_sessionIndex, String val_sessionName, int val_sessionDurationHours, int val_sessionDurationMinutes,
                                  String val_specificGoal, String val_specificPractice,
                                  String val_genericGoal, String val_genericPractice,
                                  String val_processArea)
        {

            this.ID = Unassigned;               //this field is controlled by the DBMS, i.e., AutoNumbered
            this.sessionId = val_sessionId;
            this.sessionIndex = val_sessionIndex;
            this.sessionName = val_sessionName;
            this.sessionDurationHours = val_sessionDurationHours;
            this.sessionDurationMinutes = val_sessionDurationMinutes;
            this.sessionDuration = sessionDurationHours.ToString() + ":" + sessionDurationMinutes.ToString();
            this.specificGoal = val_specificGoal;
            this.specificPractice = val_specificPractice;
            this.genericGoal = val_genericGoal;
            this.genericPractice = val_genericPractice;
            this.processArea = val_processArea;

        }
        // <summary>
        /// Equals - compare for equivalence of two objects, comparing each field individually, except for the autonumbered ID field
        /// true = identical content
        /// false = NOT identical content
        /// </summary>
        /// <param name="other"></param>
        /// <returns>bool</returns>
        public bool Equals(interview_session other)
        {
            return (
            (this.sessionId == other.sessionId) &&
            (this.sessionIndex == other.sessionIndex) &&
            (this.sessionName == other.sessionName) &&
            (this.sessionDurationHours == other.sessionDurationHours) &&
            (this.sessionDurationMinutes == other.sessionDurationMinutes) &&
            (this.sessionDuration == other.sessionDuration) &&
            (this.specificGoal == other.specificGoal) &&
            (this.specificPractice == other.specificPractice) &&
            (this.genericGoal == other.genericGoal) &&
            (this.genericPractice == other.genericPractice) &&
            (this.processArea == other.processArea)
            );
        }//Equals
#if TESTDBI
        //----------------------------------------------------------------------------------
        /// <summary>
        /// Show - if TESTDBI is defined in the build, enable the Show Table feature for Console output
        /// </summary>
        public void Show()
        {
            Console.WriteLine("{0,5},{1,5},{2,5},{3,5},{4,5},{5,5},{6,5},{7,5},{8,5},{9,5},{10,5},{11,5}",
                ID, sessionId, sessionIndex, sessionName, sessionDurationHours, sessionDurationMinutes, sessionDuration, specificGoal, specificPractice,
                genericGoal, genericPractice, processArea);
        }//Show
#endif
    }
}
