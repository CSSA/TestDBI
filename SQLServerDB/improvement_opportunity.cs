using System;


namespace SQLServerDB
{
    public class improvement_opportunity
    {
        public const int Unassigned = -1;

        public int ID { get; set; }                                 //Autonumbered by the DBMS; -1 if not initialized, otherwise assigned by the DBMS
        public String notes { get; set; }
        public string processArea { get; set; }
        public string specificGoal { get; set; }
        public string specificPractice { get; set; }
        public string genericGoal { get; set; }
        public string genericPractice { get; set; }
        public int projectId { get; set; }                        //Required to identify the specific related project




        //Default Constructor with no data initialization
        public improvement_opportunity()
        {
            this.ID = Unassigned;               //this field is controlled by the DBMS, i.e., AutoNumbered
            this.notes = string.Empty;
            this.processArea = string.Empty;
            this.specificGoal = string.Empty;
            this.specificPractice = string.Empty;
            this.genericGoal = string.Empty;
            this.genericPractice = string.Empty;
            this.projectId = Unassigned;
        }

        // Constructor, with initial values
        public improvement_opportunity(String val_notes, String val_processArea,
            String val_specificGoal, String val_specificPractice,
           String val_genericGoal, String val_genericPractice,
           int val_projectId)
        {
            this.ID = Unassigned;  //this field is controlled by the DBMS, i.e., AutoNumbered
            this.notes = val_notes;
            this.processArea = val_processArea;
            this.specificGoal = val_specificGoal;
            this.specificPractice = val_specificPractice;
            this.genericGoal = val_genericGoal;
            this.genericPractice = val_genericPractice;
            this.projectId = val_projectId;
        }

        public void Show()
        {
            Console.WriteLine("{0,5}, {1,5}, {2,5}, {3,5}, {4,5}, {5,5}, {6,5}, {7,5}",
                this.ID, 
                this.notes, 
                this.processArea, 
                this.specificGoal, 
                this.specificPractice, 
                this.genericGoal, 
                this.genericPractice, 
                this.projectId);


        }//Show
    }
}
