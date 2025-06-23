using System;

namespace QuickBill.Constants;

public static class AppConstants
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

    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);


    public static void CreateTables()
    {

    }

}
