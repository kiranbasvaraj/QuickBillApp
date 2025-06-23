using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
#if ANDROID
using Android.Provider;
#endif
using QuickBill.Interfaces;

namespace QuickBill.ViewModels;

public class BaseViewModel : IQueryAttributable, INotifyPropertyChanged, IBaseViewModel
{
    public event PropertyChangedEventHandler? PropertyChanged;

    public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        //throw new NotImplementedException();
    }

    protected bool SetProperty<T>(ref T backingStore, T value, [CallerMemberName] string propertyName = null, Action onChanged = null)
    {
        if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

        backingStore = value;
        onChanged?.Invoke();
        OnPropertyChanged(propertyName);
        return true;
    }

    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
