using System;


namespace SQLServerDB
{
    public class appraisal
    {
        public const int Unassigned = -1;
        public int ID { get; set; }                         //Autonumbered by the DBMS; -1 if not initialized, otherwise assigned by the DBMS
        public string AppraisalName { get; set; }
        public string Creator { get; set; }              //the name of the CAP user who created this  
        public int MaturityLevel { get; set; }
        public string Projects { get; set; }            //RCC-TBD: A string-encoded list of projects related to this appraisal?  is it related to the BUSF_Profile from CAPV2?
        public bool SAMSelected { get; set; }
        public bool SSDSelected { get; set; }

        //Default Constructor with no data initialization
        public appraisal()
        {
            this.ID = Unassigned;               //this field is controlled by the DBMS, i.e., AutoNumbered
            this.AppraisalName = string.Empty;
            this.Creator = string.Empty;
            this.MaturityLevel = Unassigned;
            this.Projects = string.Empty;
            this.SAMSelected = false;
            this.SSDSelected = false;
        }

        // The preferred Constructor, with initial values
        public appraisal(String val_appraisalName, String val_creator, int val_maturityLevel, int val_currentProject, string val_projects, bool val_samSelected, bool val_ssdSelected)
        {
            this.ID = Unassigned;                                //this field is controlled by the DBMS, i.e., AutoNumbered
            this.AppraisalName = val_appraisalName;
            this.Creator = val_creator;
            this.MaturityLevel = val_maturityLevel;
            this.Projects = val_projects;
            this.SAMSelected = val_samSelected;
            this.SSDSelected = val_ssdSelected;
        }

        public void Show()
        {
            Console.WriteLine("{0,5},{1,5},{2,5},{3,5},{4,5},{5,5}",
            this.AppraisalName,
            this.Creator,
            this.MaturityLevel,
            this.Projects,
            this.SAMSelected,
            this.SSDSelected
            );
        }//Show
    }
}
