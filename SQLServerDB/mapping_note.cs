using System;


namespace SQLServerDB
{
    public class mapping_note
    {
        public const int Unassigned = -1;

        public int ID { get; set; }                                 //Autonumbered by the DBMS; -1 if not initialized, otherwise assigned by the DBMS
        public int mappingId { get; set; }                                 //Autonumbered by the DBMS; -1 if not initialized, otherwise assigned by the DBMS
        public String notes { get; set; }
        




        //Default Constructor with no data initialization
        public mapping_note()
        {
            this.ID = Unassigned;               //this field is controlled by the DBMS, i.e., AutoNumbered
            this.mappingId = Unassigned;               //this field is controlled by the DBMS, i.e., AutoNumbered
            this.notes = string.Empty;
        }

        // Constructor, with initial values
        public mapping_note(int val_mappingId, String val_notes)
        {
            this.ID = Unassigned;  //this field is controlled by the DBMS, i.e., AutoNumbered
            this.mappingId = val_mappingId;
            this.notes = val_notes;
        }
        // <summary>
        /// Equals - compare for equivalence of two objects, comparing each field individually, except for the autonumbered ID field
        /// true = identical content
        /// false = NOT identical content
        /// </summary>
        /// <param name="other"></param>
        /// <returns>bool</returns>
        public bool Equals(mapping_note other)
        {
            return (
            (this.mappingId == other.mappingId) &&
            (this.notes == other.notes) 
            );
        }//Equals

        public void Show()
        {
            Console.WriteLine("{0,5},{1,5},{2,5}",
                 ID, mappingId, notes);
        }//Show
    }
}
