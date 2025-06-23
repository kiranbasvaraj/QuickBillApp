using System;
using QuickBill.Models;

namespace QuickBill.Interfaces;

public interface IHomePageViewModel
{
    Task GenerateReceipt();

    Task AddItemsToReceipt();


}
