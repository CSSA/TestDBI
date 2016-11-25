using System;


namespace SQLServerDB
{
    public class user
    {
        public const int Unassigned = -1;

        public string username { get; set; }                   //Required as a unique ID# for this project
        public string organization { get; set; }
        public string password { get; set; }        //WARNING:  This should only be temporary & should not remain in the production system


        //Default Constructor with no data initialization
        public user()
        {
            this.username = string.Empty;
            this.organization = string.Empty;
            this.password = string.Empty; //WARNING:  This should only be temporary & should not remain in the production system
        }
        // Constructor, with initial values
        public user(String val_username, String val_organization, String val_password)
        {
            this.username = val_username;
            this.organization = val_organization;
            this.password = val_password;  //WARNING:  This should only be temporary & should not remain in the production system  
        }

        // <summary>
        /// Equals - compare for equivalence of two objects, comparing each field individually, except for the autonumbered ID field
        /// true = identical content
        /// false = NOT identical content
        /// </summary>
        /// <param name="other"></param>
        /// <returns>bool</returns>
        public bool Equals(user other)
        {
            return (
            (this.username == other.username) &&
            (this.organization == other.organization) &&
            (this.password == other.password) 
            );
        }//Equals

#if TESTDBI
        //----------------------------------------------------------------------------------
        /// <summary>
        /// Show - if TESTDBI is defined in the build, enable the Show Table feature for Console output
        /// </summary>    
        public void Show()
        {
            Console.WriteLine("{0,5},{1,5},{2,5}", 
            username,
            organization,
            password
                );
        }//Show
#endif
    }//class user

}
