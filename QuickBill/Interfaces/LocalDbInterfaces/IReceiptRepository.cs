using System;
using QuickBill.Models;

namespace QuickBill.Interfaces.LocalDbInterfaces;

public interface IReceiptRepository : IBaseRepository<ReceiptModel>
{

}
public interface IReceiptItemRepository : IBaseRepository<ReceiptItemModel>
{

}
