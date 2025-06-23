using System;
using System.Diagnostics;
using QuickBill.AppConstants;
using QuickBill.Interfaces;
using QuickBill.Models;
using SQLite;

namespace QuickBill.LocalDatabase;

public class SqliteDbHelper
{
    private static SQLiteAsyncConnection? dbConnection { get; set; }

    public static SQLiteAsyncConnection DbConnection
    {
        get
        {
            // if (DbConnection is null)
            // {
            return InitializeDatabase();
            //  }
            //  return dbConnection!;
        }
    }

    // The path to the SQLite database file
    // public string DatabasePath { get; set; } = Path.Combine(FileSystem.AppDataDirectory, Constants.DatabaseFilename);



    public static SQLiteAsyncConnection InitializeDatabase()
    {
        if (dbConnection is not null)
            return dbConnection;

        var databasePath = Path.Combine(FileSystem.AppDataDirectory, Constants.DatabaseFilename);
        dbConnection = new SQLiteAsyncConnection(databasePath, Constants.Flags);
        Debug.WriteLine($"SQLITE PATH--" + databasePath);
        return dbConnection;
    }



    public static async Task CreateTables()
    {

        await DbConnection.CreateTableAsync<ReceiptModel>();
        await DbConnection.CreateTableAsync<ReceiptItemModel>();
    }



    public Task BackupDatabase(string backupPath)
    {
        throw new NotImplementedException();
    }



    public async Task DeleteDatabase()
    {
        await DbConnection.DropTableAsync<ReceiptModel>();
    }

    public Task RestoreDatabase(string backupPath)
    {
        throw new NotImplementedException();
    }

}

