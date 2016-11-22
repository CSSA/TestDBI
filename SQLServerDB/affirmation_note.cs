﻿using System;


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

        //----------------------------------------------------------------------------------
        public void Show()
        {
            Console.WriteLine("{0,5},{1,5}",
            affirmationId,
            notes
                );
        }//Show
    }
}
