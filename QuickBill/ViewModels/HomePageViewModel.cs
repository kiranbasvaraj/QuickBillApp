using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Windows.Input;
using QuickBill.Interfaces;
using QuickBill.Interfaces.LocalDbInterfaces;
using QuickBill.Models;
using QuickBill.PdfGeneratorHelper;

namespace QuickBill.ViewModels;

public class HomePageViewModel : BaseViewModel, IHomePageViewModel
{
    public IReceiptItemRepository _receiptItemRepository;
    public INavigationService _navigationService;
    public HomePageViewModel(IReceiptItemRepository receiptItemRepository, INavigationService navigationService)
    {
        _receiptList = new ObservableCollection<ReceiptModel>();
        OnGenerateInvoiceCommand = new Command(async () => await GenerateReceipt());
        AddItemReceiptItemCommand = new Command(async () => await AddItemsToReceipt());
        ClearCommand = new Command(async () => await ClearReceipt());
        ReceiptItemModelList = new ObservableCollection<ReceiptItemModel>();
        _receiptItemRepository = receiptItemRepository;
        _navigationService = navigationService;

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

    private string itemName = "test item";
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
    private int? quantity = 12;
    public int? Quantity
    {
        get { return quantity; }
        set
        {
            SetProperty(ref quantity, value);
        }
    }

    private double? price = 100;
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
    public ICommand ClearCommand { get; set; }


    public async Task AddItemsToReceipt()
    {
        ReceiptItemModel receiptItemModel = new ReceiptItemModel() { ItemName = this.ItemName, Quantity = Quantity, Price = Price };
        // ReceiptItemModelList.Add(receiptItemModel);

        await _receiptItemRepository.Insert(receiptItemModel);
        await GetAllReceiptItems();
        OnPropertyChanged("TotalAmount");
        // ItemName = string.Empty;
        // Quantity = null;
        // Price = null;
    }
    private async Task<List<ReceiptItemModel>> GetAllReceiptItems()
    {
        var items = await _receiptItemRepository.FindAll();

        // If it's already initialized
        ReceiptItemModelList.Clear();
        foreach (var item in items)
        {
            ReceiptItemModelList.Add(item);
        }
        return items;
    }

    private async Task ClearReceipt()
    {
        await _receiptItemRepository.DeleteAll();
        await OnAppearingHanlder();

        // If it's already initialized
    }




    public async Task GenerateReceipt()
    {
        var pdfFile = await PdfHelper.OnGenerateInvoiceClicked(await GetAllReceiptItems());

#if ANDROID
        PdfSource = $"file:///android_asset/pdfjs/web/viewer.html?file=file://{WebUtility.UrlEncode(pdfFile)}";
#else
        PdfSource = pdfFile;
#endif
        await _navigationService.NavigateAsync("PdfView");
    }

    public async Task OnAppearingHanlder()
    {
        await GetAllReceiptItems();
        OnPropertyChanged(nameof(TotalAmount));
    }


}
