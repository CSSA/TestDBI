using System;
using System.IO; //Path


namespace SQLServerDB
{
    public class Connection_String
    {
        public static string SQLServer_ConnectionString { get; set; }

        public static string ADO_ConnectionString { get; set; }

        public static void Initialize_ADO_Connection_String()
        {
            System.Reflection.Assembly myAssembly = typeof(Connection_String).Assembly;
            string strAssemblyLocation = Path.GetDirectoryName(myAssembly.Location);

            string ADODB_DatabasePath = strAssemblyLocation + @"\assess.mdb";

            //string strProvider_Jet = "Provider=Microsoft.Jet.OLEDB.4.0;";
            string strProvider_ACE = "Provider=Microsoft.ACE.OLEDB.12.0;";
            string strProvider = strProvider_ACE;

            ADO_ConnectionString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source = " + ADODB_DatabasePath;

            Console.WriteLine("ADO_ConnectionString=" + ADO_ConnectionString);
        }
        public static void Initialize_SQLServer_ConnectionString()
        {
            SQLServer_ConnectionString = @"Server=XPS8700\SQLEXPRESS2014;Database=CAPDevDb;" +
                "Trusted_Connection=True;" +
                "Integrated Security=true;";

            Console.WriteLine("SQLServer_ConnectionString=" + SQLServer_ConnectionString);
        }
    }//class Connection_String

    }//namespace
