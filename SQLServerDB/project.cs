﻿using System;


namespace SQLServerDB
{
    public class project
    {
        public const int Unassigned = -1;
    
        public int projectId { get; set; }                   //Required as a unique ID# for this project
        public int projectIndex { get; set; }
        public string projectName { get; set; }
        public string creator { get; set; }
        public bool standardProcess { get; set; }

        //Default Constructor with no data initialization
        public project()
        {
            this.projectId = Unassigned;
            this.projectIndex = 0;
            this.projectName = string.Empty;
            this.creator = string.Empty;
            this.standardProcess = false;
        }
        // Constructor, with initial values
        public project(int val_projectId, int val_projectIndex, string val_projectName, string val_creator, bool val_standardProcess)
        {
            this.projectId = val_projectId;
            this.projectIndex = val_projectIndex;
            this.projectName = val_projectName;
            this.creator = val_creator;
            this.standardProcess = val_standardProcess;
        }
        // <summary>
        /// Equals - compare for equivalence of two objects, comparing each field individually, except for the autonumbered ID field
        /// true = identical content
        /// false = NOT identical content
        /// </summary>
        /// <param name="other"></param>
        /// <returns>bool</returns>
        public bool Equals(project other)
        {
            return (
            (this.projectId == other.projectId) &&
            (this.projectIndex == other.projectIndex) &&
            (this.projectName == other.projectName) &&
            (this.creator == other.creator) &&
            (this.standardProcess == other.standardProcess) 
            );
        }//Equals
#if TESTDBI
        //----------------------------------------------------------------------------------
        /// <summary>
        /// Show - if TESTDBI is defined in the build, enable the Show Table feature for Console output
        /// </summary>
        public void Show()
        {
            Console.WriteLine("{0,5},{1,5},{2,5},{3,5},{4,5},{5,5}",
                 projectId, projectIndex, projectName, projectName, creator, standardProcess);
        }//Show
#endif
    }
  }
