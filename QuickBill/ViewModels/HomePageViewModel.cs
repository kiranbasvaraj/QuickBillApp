using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Windows.Input;
using QuickBill.Interfaces;
using QuickBill.Models;
using QuickBill.PdfGeneratorHelper;

namespace QuickBill.ViewModels;

public class HomePageViewModel : BaseViewModel, IHomePageViewModel
{
    public HomePageViewModel()
    {
        _receiptList = new ObservableCollection<ReceiptModel>();
        OnGenerateInvoiceCommand = new Command(async () => await GenerateReceipt());
        AddItemReceiptItemCommand = new Command(AddItemsToReceipt);
        ReceiptItemModelList = new ObservableCollection<ReceiptItemModel>();

    }

    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        base.ApplyQueryAttributes(query);
    }

    private ObservableCollection<ReceiptModel> _receiptList;
    public ObservableCollection<ReceiptModel> ReceiptList
    {
        get { return _receiptList; }
        set { _receiptList = value; }
    }

    private string pdfSource;
    public string PdfSource
    {
        get { return pdfSource; }
        set
        {
            SetProperty(ref pdfSource, value);
        }
    }

    private ObservableCollection<ReceiptItemModel> _receiptItemModelList;
    public ObservableCollection<ReceiptItemModel> ReceiptItemModelList
    {
        get { return _receiptItemModelList; }
        set
        {
            SetProperty(ref _receiptItemModelList, value);
        }
    }

    private ReceiptItemModel _receiptItemModel;
    public ReceiptItemModel ReceiptItemModel
    {
        get { return _receiptItemModel; }
        set
        {
            SetProperty(ref _receiptItemModel, value);
        }
    }

    private string itemName;
    public string ItemName
    {
        get { return itemName; }
        set
        {
            SetProperty(ref itemName, value);
        }
    }

    private double? _totalAmount;
    public double? TotalAmount
    {
        get
        {
            return ReceiptItemModelList?.Sum(x => x.Price ?? 0) ?? 0;

        }
        set
        {
            SetProperty(ref _totalAmount, value);
        }
    }
    private int? quantity;
    public int? Quantity
    {
        get { return quantity; }
        set
        {
            SetProperty(ref quantity, value);
        }
    }

    private double? price;
    public double? Price
    {
        get { return price; }
        set
        {
            SetProperty(ref price, value);
        }
    }


    public ICommand OnGenerateInvoiceCommand { get; set; }
    public ICommand AddItemReceiptItemCommand { get; set; }


    public void AddItemsToReceipt()
    {
        ReceiptItemModel receiptItemModel = new ReceiptItemModel() { ItemName = this.ItemName, Quantity = Quantity, Price = Price };
        ReceiptItemModelList.Add(receiptItemModel);
        OnPropertyChanged("TotalAmount");
        // ItemName = string.Empty;
        // Quantity = null;
        // Price = null;
    }



    public async Task GenerateReceipt()
    {
        var pdfFile = await PdfHelper.OnGenerateInvoiceClicked();

#if ANDROID
        PdfSource = $"file:///android_asset/pdfjs/web/viewer.html?file=file://{WebUtility.UrlEncode(pdfFile)}";
#else
        PdfSource = pdfFile;
#endif
    }




}
