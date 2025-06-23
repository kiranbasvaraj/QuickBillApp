using System;
using SQLite;

namespace QuickBill.Models;

public class ReceiptItemModel
{
    [PrimaryKey, AutoIncrement]
     public int Id { get; set; }
    public string? ItemName { get; set; }
    public int? Quantity { get; set; }
    public double? Price { get; set; }

}
