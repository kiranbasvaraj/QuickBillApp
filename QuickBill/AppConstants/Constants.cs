using System;

namespace QuickBill.AppConstants;

public static class Constants
{
    public const string DatabaseFilename = "QuickBill.db3";


    public const SQLite.SQLiteOpenFlags Flags =
       // open the database in read/write mode
       SQLite.SQLiteOpenFlags.ReadWrite |
       // create the database if it doesn't exist   
       SQLite.SQLiteOpenFlags.Create |
       // enable multi-threaded database access
       SQLite.SQLiteOpenFlags.SharedCache |
       //The connection is opened in multi-threading mode.
       SQLite.SQLiteOpenFlags.NoMutex;
    // This class can be used to group enums or constants related to the application.
    // Currently, it only contains the PaymentStatusEnum.
   


    public static void CreateTables()
    {

    }

}
