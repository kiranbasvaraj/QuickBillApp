using System;
using System.Text;
using QuickBill.Enums;
using QuickBill.Interfaces;
using QuickBill.LocalDatabase;
using SQLite;

namespace QuickBill.DataServices;

public class BaseRespository<T> : IBaseRepository<T> where T : new()
{
    public string PrimaryKeyName { get; set; }
    public string TableName { get; set; }
    public string ForeignKeyName { get; set; }
    // public ISqlite _sqlite;



    // public BaseRespository(ISqlite sqlite)
    // {
    //     _sqlite = sqlite;
    // }

    public SQLiteAsyncConnection Database
    {
        //get { return _sqlite.GetConnection(); }
        get { return SqliteDbHelper.DbConnection; }
    }

    SQLiteAsyncConnection IBaseRepository<T>.Database => throw new NotImplementedException();

    public async Task<List<T>> GetTable()
    {
        var lstitem = await Database.Table<T>().ToListAsync();

        return lstitem;

    }

    public async Task<T> FirstTableItem()
    {
        var lstitem = await Database.Table<T>().ToListAsync();

        return lstitem.FirstOrDefault();


    }

    public async Task<T> LastTableItem()
    {

        var lstitem = await Database.Table<T>().ToListAsync();

        return lstitem.LastOrDefault();

    }

    public virtual async Task Delete(T item)
    {
        await Database.DeleteAsync(item);

    }

    public virtual async Task DeleteAll()
    {
        List<T> tableItems = await FindAll();
        try
        {
            if (tableItems != null)
            {
                foreach (var item in tableItems)
                {
                    await Delete(item);
                }
            }
        }
        catch (Exception ex)
        {
           // ex.Log(GetType().FullName);
        }
        finally
        {


        }
    }


    public virtual async Task Delete(string id, StorageKeyType keyType)
    {
        // PerfLogger.StartLog("BaseStorageService.Delete(string id, StorageKeyType keyType)", $"Start Delete(string id, StorageKeyType keyType {id})");

        try
        {
            if (!string.IsNullOrEmpty(id))
            {
                var propertyName = keyType.Equals(StorageKeyType.PrimaryKey) ?
                                   PrimaryKeyName : ForeignKeyName;
                string[] args = { id };
                if (!string.IsNullOrEmpty(TableName))
                {
                    var query = string.Format("DELETE FROM {0} WHERE {1} = ?", TableName, propertyName);

                    await Execute(query, args);
                }
            }
        }
        catch (Exception ex)
        {
            // ex.Log(GetType().FullName);
        }
        finally
        {


        }

        // PerfLogger.EndLog("BaseStorageService.Delete(string id, StorageKeyType keyType)", $"End BaseStorageService.Delete(string id, StorageKeyType keyType {id})");
    }

    public virtual async Task Delete(int id, StorageKeyType keyType)
    {

        await Delete(id.ToString(), keyType);

    }

    public virtual async Task<T> Find(int id, StorageKeyType keyType)
    {

        return await Find(id.ToString(), keyType);
    }

    public virtual async Task<T> Find(string id, StorageKeyType keyType)
    {
        // PerfLogger.StartLog("BaseStorageService.Find(string id, StorageKeyType keyType)", $"Start BaseStorageService.Delete(string id, StorageKeyType keyType {id})");
        T item = default(T);

        try
        {
            if (!string.IsNullOrEmpty(id))
            {
                var propertyName = keyType.Equals(StorageKeyType.PrimaryKey) ?
                                    PrimaryKeyName : ForeignKeyName;

                if (!string.IsNullOrEmpty(TableName))
                {
                    var query = string.Format("SELECT * FROM {0} WHERE {1} = ?", TableName, propertyName);
                    string[] arguments = { id };
                    var lstitem = await Database.QueryAsync<T>(query, arguments);
                    item = lstitem.FirstOrDefault();
                }
            }
        }
        catch (Exception ex)
        {
            // ex.Log(this.GetType().FullName);
        }
        finally
        {


        }

        //PerfLogger.EndLog("BaseStorageService.Find(string id, StorageKeyType keyType)", $"End BaseStorageService.Find(string id, StorageKeyType keyType {id})");
        return item;
    }

    public virtual async Task<T> Find(string id, string propertyName)
    {
        //PerfLogger.StartLog("BaseStorageService.Find(string id, StorageKeyType keyType)", $"Start BaseStorageService.Delete(string id, StorageKeyType keyType {id})");
        T item = default(T);

        try
        {
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(TableName))
            {
                var query = string.Format("SELECT * FROM {0} WHERE {1} = ?", TableName, propertyName);
                string[] arguments = { id };
                var lstitem = await Database.QueryAsync<T>(query, arguments);
                item = lstitem.FirstOrDefault();
            }
        }

        catch (Exception ex)
        {
            // ex.Log(this.GetType().FullName);
        }

        finally
        {

        }
        // PerfLogger.EndLog("BaseStorageService.Find(string id, StorageKeyType keyType)", $"End BaseStorageService.Find(string id, StorageKeyType keyType {id})");
        return item;
    }

    //Use this method when you want to find records with multiple columns. Ids are the values and Propertynames are the column. 
    //Make sure to pass the values respective with it's parameter order. 
    public virtual async Task<T> Find(string[] ids, params string[] propertyNames)
    {
        //PerfLogger.StartLog("BaseStorageService.Find(string[] ids, params string[] propertyNames)", $"Start BaseStorageService.Find(string[] ids, params string[] propertyNames)");
        T item = default(T);

        try
        {
            if (!string.IsNullOrEmpty(TableName))
            {
                StringBuilder query = new StringBuilder("SELECT * FROM ");
                query.Append(TableName);
                query.Append(" Where ");
                query.Append(propertyNames[0]);
                query.Append(" = ?");

                for (int i = 1; i < propertyNames.Count(); i++)
                {
                    query.Append(" AND ");
                    query.Append(propertyNames[i]);
                    query.Append(" = ?");
                }

                var lstitem = await Database.QueryAsync<T>(query.ToString(), ids);
                item = lstitem.FirstOrDefault();
                query.Clear();
            }
        }
        catch (Exception ex)
        {
            // ex.Log(this.GetType().FullName);
        }

        // PerfLogger.EndLog("BaseStorageService.Find(string[] ids, params string[] propertyNames)", $"End BaseStorageService.Find(string[] ids, params string[] propertyNames)");
        return item;
    }

    public async Task<List<T>> FindAll(string id, StorageKeyType keyType)
    {
        // PerfLogger.StartLog("BaseStorageService.FindAll(string id, StorageKeyType keyType)", $"Start BaseStorageService.FindAll(string id, StorageKeyType keyType {id})");
        List<T> items = null;

        try
        {
            if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(TableName))
            {
                var propertyName = keyType.Equals(StorageKeyType.PrimaryKey) ?
                                   PrimaryKeyName : ForeignKeyName;

                var query = string.Format("SELECT * FROM {0} WHERE {1} = ?", TableName, propertyName);
                string[] arguments = { id };
                items = await Database.QueryAsync<T>(query, arguments);
            }
        }
        catch (Exception ex)
        {
            // ex.Log(this.GetType().FullName);
        }

        // PerfLogger.EndLog("BaseStorageService.FindAll(string id, StorageKeyType keyType)", $"End BaseStorageService.FindAll(string id, StorageKeyType keyType {id})");
        return items;
    }

    public async Task<List<T>> FindAll(int id, StorageKeyType keyType)
    {

        return await FindAll(id.ToString(), keyType);
    }

    public virtual async Task<List<T>> FindAll()
    {
        return await GetTable();
    }

    public async Task Upsert(string id, T item, StorageKeyType keyType)
    {
        // PerfLogger.StartLog("BaseStorageService.Upsert(string id, T item, StorageKeyType keyType))", $"Start BaseStorageService.Upsert(int id, T item, StorageKeyType keyType {id})");

        try
        {
            T results = await Find(id, keyType);
            if (EqualityComparer<T>.Default.Equals(results, default(T)))
            {
                await Insert(item);
            }
            else
            {
                await Update(item);
            }

        }
        catch (Exception ex)
        {
            //ex.Log(this.GetType().FullName);
        }
        finally
        {

        }

        // PerfLogger.EndLog("BaseStorageService.Upsert(string id, T item, StorageKeyType keyType)", $"End BaseStorageService.Upsert(int id, T item, StorageKeyType keyType {id})");
    }

    public async Task Upsert(string[] ids, T item, params string[] propertyNames)
    {
        // PerfLogger.StartLog("BaseStorageService.Upsert(string id, T item, StorageKeyType keyType))", $"Start BaseStorageService.Upsert(int id, T item, StorageKeyType keyType {ids})");

        try
        {
            T results = await Find(ids, propertyNames);
            if (EqualityComparer<T>.Default.Equals(results, default(T)))
            {
                await Insert(item);
            }
            else
            {
                await Update(item);
            }

        }
        catch (Exception ex)
        {
            // ex.Log(this.GetType().FullName);
        }
        finally
        {

        }
        //PerfLogger.EndLog("BaseStorageService.Upsert(string id, T item, StorageKeyType keyType)", $"End BaseStorageService.Upsert(int id, T item, StorageKeyType keyType {ids})");
    }

    public async Task Upsert(int id, T item, StorageKeyType keyType)
    {

        await Upsert(id.ToString(), item, keyType);
    }

    public virtual async Task Insert(T item)
    {
        await Database.InsertAsync(item);

    }

    public virtual async Task InsertAll(List<T> items)
    {


        if (items != null && items.Count > 0)
        {
            var itemsCopy = new List<T>(items);

            await Database.RunInTransactionAsync(dbConnection =>
            {
                dbConnection.InsertAll(itemsCopy);
            });
        }

    }

    public virtual async Task Update(T item)
    {

        await Database.UpdateAsync(item);
    }

    public virtual async Task DropTable()
    {
        if (!string.IsNullOrEmpty(TableName))
        {
            var command = string.Format("DROP TABLE IF EXISTS {0}", TableName);
            await Execute(command);
        }

    }

    public virtual async Task CreateTable()
    {
        await Database.CreateTableAsync<T>();
    }

    private async Task Execute(string command, object[] args = null)
    {

        try
        {
            if (args == null)
                await Database.ExecuteAsync(command);
            else
                await Database.ExecuteAsync(command, args);
        }
        catch (Exception ex)
        {
            // ex.Log(this.GetType().FullName);
        }
        finally
        {
        }
    }


    private static readonly SemaphoreSlim _dbSemaphore = new SemaphoreSlim(1, 1);


    // public async Task BulkInsertOrReplaceAsync(List<BulkDBModel<T>> items)
    // {
    //     bool success = false;
    //     const int delayBetweenRetriesInMilliseconds = 5000;
    //     int retryAttempts = 0;
    //     while (!success && retryAttempts < 3)
    //     {
    //         {
    //             await _dbSemaphore.WaitAsync(); // Wait to enter the critical section

    //             try
    //             {
    //                 await Database.RunInTransactionAsync((conn) =>
    //                 {
    //                     var itemsToInsert = new List<T>();
    //                     var itemsToUpdate = new List<T>();

    //                     // Only insertions when it is fresh login
    //                     if (Common.Core.Settings.IsCompanyFreshLoginProcess)
    //                     {
    //                         foreach (var bulkObj in items)
    //                             itemsToInsert.Add(bulkObj.Item);
    //                     }
    //                     else
    //                     {
    //                         foreach (var bulkObj in items)
    //                         {
    //                             var item = bulkObj.Item;
    //                             string[] ids = bulkObj.Ids;
    //                             string[] propertyNames = bulkObj.Props;
    //                             string primaryKeyProperty = bulkObj.PrimaryKeyName;

    //                             if (ids != null && ids.Length > 0)
    //                             {
    //                                 var query = new StringBuilder($"SELECT * FROM {TableName} WHERE {propertyNames[0]} = ?");

    //                                 for (int i = 1; i < propertyNames.Length; i++)
    //                                 {
    //                                     query.Append($" AND {propertyNames[i]} = ?");
    //                                 }

    //                                 var lstitem = conn.Query<T>(query.ToString(), ids);
    //                                 var result = lstitem.FirstOrDefault();
    //                                 query.Clear();

    //                                 if (result != null)
    //                                 {
    //                                     if (!string.IsNullOrEmpty(primaryKeyProperty))
    //                                     {
    //                                         var primaryKeyValue = result.GetType().GetProperty(primaryKeyProperty).GetValue(result);
    //                                         item.GetType().GetProperty(primaryKeyProperty).SetValue(item, primaryKeyValue);
    //                                     }
    //                                     itemsToUpdate.Add(item);
    //                                 }
    //                                 else
    //                                 {
    //                                     itemsToInsert.Add(item);
    //                                 }
    //                             }
    //                             else
    //                             {
    //                                 itemsToInsert.Add(item);
    //                             }
    //                         }
    //                     }

    //                     if (itemsToInsert.Count > 0)
    //                     {
    //                         conn.InsertAll(itemsToInsert);
    //                     }

    //                     if (itemsToUpdate.Count > 0)
    //                     {
    //                         conn.UpdateAll(itemsToUpdate);
    //                     }

    //                 });

    //                 // If the operation succeeds, break out of the retry loop
    //                 success = true;
    //             }
    //             catch (Exception ex)
    //             {
    //                 retryAttempts++;
    //                 if (retryAttempts >= 3)
    //                 {
    //                    // ex.Log(this.GetType().FullName);
    //                    // Utility.APPDLOGSendEvent("Failed in sqlite", ex.Message.ToString());

    //                     // If the operation failed, break out of the retry loop
    //                     break;
    //                 }

    //                 if (ex != null && ex.Message != null && ex.Message.Contains("already in a transaction"))
    //                 {
    //                     Repositories.MGMDatabase.ResetDbConnection();
    //                     await Task.Delay(1000);
    //                 }
    //                 else
    //                 {
    //                     // Wait before retrying
    //                     await Task.Delay(delayBetweenRetriesInMilliseconds);
    //                 }
    //             }
    //             finally
    //             {
    //                 _dbSemaphore.Release(); // Always release the semaphore
    //             }

    //         }
    //     }
    // }
}
