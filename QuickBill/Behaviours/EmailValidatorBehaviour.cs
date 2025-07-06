using System;
using System.Text.RegularExpressions;

namespace QuickBill.Behaviours;

public class EmailValidatorBehaviour : Behavior<Entry>
{
    static readonly Regex _emailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static readonly BindableProperty IsValidProperty =
        BindableProperty.Create(nameof(IsValid), typeof(bool), typeof(EmailValidatorBehaviour), false, BindingMode.TwoWay);

    public bool IsValid
    {
        get => (bool)GetValue(IsValidProperty);
        set => SetValue(IsValidProperty, value);
    }

    protected override void OnAttachedTo(Entry bindable)
    {
        bindable.TextChanged += OnTextChanged;

        // 👇 Inherit BindingContext from Entry
        BindingContext = bindable.BindingContext;
        bindable.BindingContextChanged += OnBindingContextChanged;

        base.OnAttachedTo(bindable);
    }

    protected override void OnDetachingFrom(Entry bindable)
    {
        bindable.TextChanged -= OnTextChanged;
        bindable.BindingContextChanged -= OnBindingContextChanged;
        base.OnDetachingFrom(bindable);
    }

    private void OnBindingContextChanged(object sender, EventArgs e)
    {
        BindingContext = ((BindableObject)sender).BindingContext;
    }

    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        IsValid = _emailRegex.IsMatch(e.NewTextValue ?? string.Empty);

        var entry = (Entry)sender;
        entry.TextColor = IsValid ? Colors.Black : Colors.Red;
    }
}
