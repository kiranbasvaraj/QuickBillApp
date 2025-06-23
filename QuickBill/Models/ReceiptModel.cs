using System;
using SQLite;

namespace QuickBill.Models;

public class ReceiptModel
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    public string? ReceiptNumber { get; set; }
    public DateTime Date { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public string? CustomerPhone { get; set; }
    public decimal TotalAmount { get; set; }
    public string? PaymentMethod { get; set; }
    public string? Notes { get; set; }
    public string? FilePath { get; set; } // Path to the generated PDF file
    public string? Status { get; set; } // e.g., "Pending", "Paid", "Cancelled"
    public DateTime CreatedAt { get; set; } = DateTime.Now; // Automatically set to current date and time
    public DateTime UpdatedAt { get; set; } = DateTime.Now; // Automatically set to current date and time

    // Additional properties can be added as needed
    public string? CompanyName { get; set; }
    public string? CompanyAddress { get; set; }
    public string? CompanyPhone { get; set; }
    public string? CompanyEmail { get; set; }
    public string? CompanyLogoPath { get; set; }

}
