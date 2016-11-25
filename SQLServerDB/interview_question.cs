using System;


namespace SQLServerDB
{
    public class interview_question
    {
        public const int Unassigned = -1;

        public int ID { get; set; }                                 //Autonumbered by the DBMS; -1 if not initialized, otherwise assigned by the DBMS
        public String interviewQuestions { get; set; }
        public String questionNotes { get; set; }
        public int sessionId { get; set; }



        //Default Constructor with no data initialization
        public interview_question()
        {
            this.ID = Unassigned;               //this field is controlled by the DBMS, i.e., AutoNumbered
            this.interviewQuestions = string.Empty;
            this.questionNotes = string.Empty;
            this.sessionId = Unassigned;
        }

        // Constructor, with initial values
        public interview_question(String val_interviewQuestions, String val_questionNotes, int val_sessionId)
        {
            this.ID = Unassigned;  //this field is controlled by the DBMS, i.e., AutoNumbered
            this.interviewQuestions = val_interviewQuestions;
            this.questionNotes = val_questionNotes;
            this.sessionId = val_sessionId;
        }
        // <summary>
        /// Equals - compare for equivalence of two objects, comparing each field individually, except for the autonumbered ID field
        /// true = identical content
        /// false = NOT identical content
        /// </summary>
        /// <param name="other"></param>
        /// <returns>bool</returns>
        public bool Equals(interview_question other)
        {
            return (
            (this.interviewQuestions == other.interviewQuestions) &&
            (this.questionNotes == other.questionNotes) &&
            (this.sessionId == other.sessionId)
            );
        }//Equals

 
    public void Show()
        {
            Console.WriteLine("{0,5},{1,5},{2,5},{3,5}",
                ID, interviewQuestions, questionNotes, sessionId);
        }
    }
}
