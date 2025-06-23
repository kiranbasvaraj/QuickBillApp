using System;
using QuickBill.Interfaces.LocalDbInterfaces;
using QuickBill.Models;

namespace QuickBill.DataServices;

public class ReceiptRepository : BaseRespository<ReceiptModel>, IReceiptRepository
{

}
