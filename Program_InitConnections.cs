using System;

using SQLServerDB;

using System.IO; //Path

namespace TestDBI
{
    partial class Program
    {


        public static void Initialize_SQLServer_ConnectionString()
        {
            Connection_String.SQLServer_ConnectionString = @"Server=XPS8700\SQLEXPRESS2014;Database=CAPDevDb;" +
                "Trusted_Connection=True;" +
                "Integrated Security=true;";

            Console.WriteLine("SQLServer_ConnectionString=" + Connection_String.SQLServer_ConnectionString);
        }



    }
}
