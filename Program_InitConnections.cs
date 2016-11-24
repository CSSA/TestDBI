using System;

using SQLServerDB;

using System.IO; //Path

namespace TestDBI
{
    partial class Program
    {


        public static void Initialize_ADO_Connection_String()
        {
            System.Reflection.Assembly myAssembly = typeof(Connection_String).Assembly;
            string strAssemblyLocation = Path.GetDirectoryName(myAssembly.Location);

            string ADODB_DatabasePath = strAssemblyLocation + @"\assess.mdb";

            //string strProvider_Jet = "Provider=Microsoft.Jet.OLEDB.4.0;";
            string strProvider_ACE = "Provider=Microsoft.ACE.OLEDB.12.0;";
            string strProvider = strProvider_ACE;

            Connection_String.ADO_ConnectionString = "Provider = Microsoft.Jet.OLEDB.4.0;Data Source = " + ADODB_DatabasePath;

            Console.WriteLine("ADO_ConnectionString=" + Connection_String.ADO_ConnectionString);
        }
        public static void Initialize_SQLServer_ConnectionString()
        {
            Connection_String.SQLServer_ConnectionString = @"Server=XPS8700\SQLEXPRESS2014;Database=CAPDevDb;" +
                "Trusted_Connection=True;" +
                "Integrated Security=true;";

            Console.WriteLine("SQLServer_ConnectionString=" + Connection_String.SQLServer_ConnectionString);
        }



    }
}
