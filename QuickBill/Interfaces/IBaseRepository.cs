using System;
using QuickBill.Enums;
using SQLite;

namespace QuickBill.Interfaces;

public interface IBaseRepository<T>
    {
        SQLiteAsyncConnection Database { get; }

        Task<T> Find(string key, StorageKeyType keyType);
        Task<T> Find(int key, StorageKeyType keyType);
        Task<T> Find(string id, string propertyName);
        Task<T> Find(string[] ids, params string[] propertyNames);
        Task<List<T>> FindAll(string id, StorageKeyType keyType);
        Task<List<T>> FindAll(int id, StorageKeyType keyType);
        Task<List<T>> FindAll();

        Task Insert(T item);
        Task InsertAll(List<T> items);
        Task Update(T item);
        Task Upsert(string id, T item, StorageKeyType keyType);
        Task Upsert(string[] ids, T item, params string[] propertyNames);
        Task Upsert(int id, T item, StorageKeyType keyType);

        Task Delete(T item);
        Task Delete(string id, StorageKeyType keyType);
        Task Delete(int id, StorageKeyType keyType);
        Task DeleteAll();

        Task DropTable();
        Task CreateTable();
        Task<T> FirstTableItem();
        Task<T> LastTableItem();
        Task<List<T>> GetTable();

        //Task InsertOrReplace(T item, string primaryKeyProperty, string[] ids, params string[] propertyNames);
       // Task BulkInsertOrReplaceAsync(List<BulkDBModel<T>> items);
        string PrimaryKeyName { get; set; }
        string TableName { get; set; }
        string ForeignKeyName { get; set; }

    }

    
