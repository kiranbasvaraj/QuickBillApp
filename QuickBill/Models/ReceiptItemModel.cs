using System;

namespace QuickBill.Models;

public class ReceiptItemModel
{
    public string? ItemName { get; set; }
    public int? Quantity { get; set; } 
    public double? Price { get; set; } 

}
