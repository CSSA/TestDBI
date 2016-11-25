using System;

namespace SQLServerDB
{
    public class affirmation_note
    {
        public const int Unassigned = -1;

        public int ID { get; set; }                                 //Autonumbered by the DBMS; -1 if not initialized, otherwise assigned by the DBMS
        public int affirmationId { get; set; }                                 //Autonumbered by the DBMS; -1 if not initialized, otherwise assigned by the DBMS
        public String notes { get; set; }





        //Default Constructor with no data initialization
        public affirmation_note()
        {
            this.ID = Unassigned;               //this field is controlled by the DBMS, i.e., AutoNumbered
            this.affirmationId = Unassigned;            
            this.notes = string.Empty;
        }

        // Constructor, with initial values
        public affirmation_note(int val_affirmationId, String val_notes)
        {
            this.ID = Unassigned;  //this field is controlled by the DBMS, i.e., AutoNumbered
            this.affirmationId = val_affirmationId;
            this.notes = val_notes;
        }

        // <summary>
        /// Equals - compare for equivalence of two objects, comparing each field individually, except for the autonumbered ID field
        /// true = identical content
        /// false = NOT identical content
        /// </summary>
        /// <param name="other"></param>
        /// <returns>bool</returns>
        public bool Equals(affirmation_note other)
        {
            return (
            (this.affirmationId == other.affirmationId) &&
            (this.notes == other.notes)
            );
        }//Equals


#if TESTDBI
        //----------------------------------------------------------------------------------
        /// <summary>
        /// Show - if TESTDBI is defined in the build, enable the Show Table feature for Console output
        /// </summary>        
        public void Show()
        {
            Console.WriteLine("{0,5},{1,5}",
            affirmationId,
            notes
                );
        }//Show
#endif
    }
}
