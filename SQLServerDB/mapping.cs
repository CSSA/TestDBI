using System;

namespace SQLServerDB
{
    public class mapping
    {
        public const int Unassigned = -1;
        public int ID { get; set; }                         //Autonumbered by the DBMS; -1 if not initialized, otherwise assigned by the DBMS
        public int mappingId { get; set; }
        public string mappingName { get; set; }              //the name of the CAP user who created this appraisal 
        public string mappingPath { get; set; }
        public string mappingType { get; set; }
        public string processArea { get; set; }
        public string goal { get; set; }
        public string practice { get; set; }
        public int projectId { get; set; }
        public bool itemChecked { get; set; }



        //Default Constructor with no data initialization
        public mapping()
        {
            this.ID = Unassigned;               //this field is controlled by the DBMS, i.e., AutoNumbered
            this.mappingId = Unassigned;
            this.mappingName = string.Empty;
            this.mappingPath = string.Empty;
            this.mappingType = string.Empty;
            this.processArea = string.Empty;
            this.goal = string.Empty;
            this.practice = string.Empty;
            this.projectId = Unassigned;
            this.itemChecked = false;
        }

        // The preferred Constructor, with initial values
        public mapping(int val_mappingId, String val_mappingName, String val_mappingPath, String val_mappingType,
                                  String val_processArea, String val_goal, String val_practice, int val_projectId, bool val_upForDeletion)
        {
            this.ID = Unassigned;               //this field is controlled by the DBMS, i.e., AutoNumbered
            this.mappingId = val_mappingId;
            this.mappingName = val_mappingName;
            this.mappingPath = val_mappingPath;
            this.mappingType = val_mappingType;
            this.processArea = val_processArea;
            this.goal = val_goal;
            this.practice = val_practice;
            this.projectId = Unassigned;
            this.itemChecked = val_upForDeletion;
        }
        // <summary>
        /// Equals - compare for equivalence of two objects, comparing each field individually, except for the autonumbered ID field
        /// true = identical content
        /// false = NOT identical content
        /// </summary>
        /// <param name="other"></param>
        /// <returns>bool</returns>
        public bool Equals(mapping other)
        {
            return (
            (this.mappingId == other.mappingId) &&
            (this.mappingName == other.mappingName) &&
            (this.mappingPath == other.mappingPath) &&
            (this.mappingType == other.mappingType) &&
            (this.processArea == other.processArea) &&
            (this.goal == other.goal) &&
            (this.practice == other.practice) &&
            (this.projectId == other.projectId) &&
            (this.itemChecked == other.itemChecked)
            );
        }//Equals
#if TESTDBI
        //----------------------------------------------------------------------------------
        /// <summary>
        /// Show - if TESTDBI is defined in the build, enable the Show Table feature for Console output
        /// </summary>

        public void Show()
        {
            Console.WriteLine("{0,5},{1,5},{2,5},{3,5},{4,5},{5,5},{6,5},{7,5},{8,5},{9,5}",
                 ID, mappingId, mappingName, mappingPath, mappingType, processArea, goal, practice, projectId, itemChecked);
        }//Show
#endif
    }
}
