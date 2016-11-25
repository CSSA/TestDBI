using System;

namespace SQLServerDB
{
    public class goal_practice
    {
        public const int Unassigned = -1;

        public int ID { get; set; }                          //Autonumbered by the DBMS; -1 if not initialized, otherwise assigned by the DBMS
        public int nodeId { get; set; }
        public int processAreaId { get; set; }
        public int projectId { get; set; }
        public string name { get; set; }        //primary key
        public bool isGoal { get; set; }
        public bool isPractice { get; set; }
        public string rating { get; set; }
        public bool coverage { get; set; }            //WARNING:  This should only be temporary & should not remain in the production system


        //Default Constructor with no data initialization
        public goal_practice()
        {
            this.ID = Unassigned;                    //this field is controlled by the DBMS, i.e., AutoNumbered
            this.nodeId = Unassigned;
            this.processAreaId = Unassigned;
            this.projectId = Unassigned;
            this.name = string.Empty;
            this.isGoal = false;
            this.isPractice = false;
            this.rating = string.Empty;
            this.coverage = false;
        }//goal_practice

        // Constructor, with initial values
        public goal_practice(int val_nodeId, int val_processAreaId, int val_projectId, string val_name, bool val_isGoal, bool val_isPractice, string val_rating, bool val_coverage)
        {
            this.ID = Unassigned;               //this field is controlled by the DBMS, i.e., AutoNumbered
            this.nodeId = val_nodeId;
            this.processAreaId = val_processAreaId;
            this.projectId = val_projectId;
            this.name = val_name;
            this.isGoal = val_isGoal;
            this.isPractice = val_isPractice;
            this.rating = val_rating;
            this.coverage = val_coverage;
        }//goal_practice

        // <summary>
        /// Equals - compare for equivalence of two objects, comparing each field individually, except for the autonumbered ID field
        /// true = identical content
        /// false = NOT identical content
        /// </summary>
        /// <param name="other"></param>
        /// <returns>bool</returns>
        public bool Equals(goal_practice other)
        {
            return (
            (this.nodeId == other.nodeId) &&
            (this.processAreaId == other.processAreaId) &&
            (this.projectId == other.projectId) &&
            (this.name == other.name) &&
            (this.isGoal == other.isGoal) &&
            (this.isPractice == other.isPractice) &&
            (this.isPractice == other.isPractice) &&
            (this.rating == other.rating) &&
            (this.coverage == other.coverage)
            );
        }//Equals
#if TESTDBI
        //----------------------------------------------------------------------------------
        /// <summary>
        /// Show - if TESTDBI is defined in the build, enable the Show Table feature for Console output
        /// </summary>
        public void Show()
        {
            Console.WriteLine("{0,5},{1,5},{2,5},{3,5},{4,5},{5,5},{6,5},{7,5},{8,5},",
                 ID, nodeId, processAreaId, projectId, name, isGoal, isPractice, rating, coverage );
        }//Show
#endif
    }
}
