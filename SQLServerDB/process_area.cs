using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SQLServerDB
{
    public  class process_area
    {
        public const int Unassigned = -1;

        public int ID { get; set; }
        public int processAreaId { get; set; }
        public int projectId { get; set; }
        public string paName { get; set; }
        public string text { get; set; }
        public bool active { get; set; }
        public bool canContainArtifact { get; set; }
        public bool canContainAffirmation { get; set; }
        public string rating { get; set; }
        public bool coverage { get; set; }

        public process_area()
        {
            this.processAreaId = Unassigned;
            this.projectId = Unassigned;
            this.paName = string.Empty;
            this.text = string.Empty;
            this.active = false;
            this.canContainArtifact = false;
            this.canContainAffirmation = false;
            this.rating = "btnNotExaminedA";
            this.coverage = false;
        }

        public process_area(int val_processAreaId, int val_projectID, string val_name, string val_text,
            bool val_active, bool val_hasArtifact, bool val_hasAffirmation, string val_rating, bool val_coverage)
        {
            this.ID = Unassigned;
            this.processAreaId = val_processAreaId;
            this.projectId = val_projectID;
            this.paName = val_name;
            this.text = val_text;
            this.active = val_active;
            this.canContainArtifact = val_hasArtifact;
            this.canContainAffirmation = val_hasAffirmation;
            this.rating = val_rating;
            this.coverage = val_coverage;
        }
        // <summary>
        /// Equals - compare for equivalence of two objects, comparing each field individually, except for the autonumbered ID field
        /// true = identical content
        /// false = NOT identical content
        /// </summary>
        /// <param name="other"></param>
        /// <returns>bool</returns>
        public bool Equals(process_area other)
        {
            return (
            (this.processAreaId == other.processAreaId) &&
            (this.projectId == other.projectId) &&
            (this.paName == other.paName) &&
            (this.text == other.text) &&
            (this.active == other.active) &&
            (this.canContainArtifact == other.canContainArtifact) &&
            (this.canContainAffirmation == other.canContainAffirmation) &&
            (this.rating == other.rating) &&
            (this.coverage == other.coverage)
            );
        }//Equals

        public void Show()
        {
            Console.WriteLine("{0,5},{1,5},{2,5},{3,5},{4,5},{5,5},{6,5},{7,5},{8,5},{9,5},{10,5}",
                 ID, processAreaId, projectId, paName, text, text, active, canContainArtifact, canContainAffirmation, rating, coverage);
        }//Show
    }
}
