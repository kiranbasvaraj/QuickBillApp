using System;
using SQLite;

namespace QuickBill.Interfaces;

public interface ISqlite
    {
        SQLiteAsyncConnection GetConnection();
        void DeleteSQLFile();
    }
