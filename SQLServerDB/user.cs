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
        //----------------------------------------------------------------------------------
        public void Show()
        {
            Console.WriteLine("{0,5},{1,5},{2,5}", 
            username,
            organization,
            password
                );
        }//Show

    }//class user

}
