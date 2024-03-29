﻿using System.Windows;
using System.Windows.Controls;
using DCT_CryptoMonitor.Desktop.MVVM.Model;
using DCT_CryptoMonitor.Desktop.Services.Coins;

namespace DCT_CryptoMonitor.Desktop.Components;

public partial class MarketItem : UserControl
{
    public Market Market
    {
        get => (Market)GetValue(MarketProperty);
        set => SetValue(MarketProperty, value);
    }

    public static readonly DependencyProperty MarketProperty = DependencyProperty.Register(
        nameof(Market), 
        typeof(Market), 
        typeof(MarketItem), 
        new PropertyMetadata(default(Market)));
    
    public ICoinService CoinService
    {
        get => (ICoinService)GetValue(CoinServiceProperty);
        set => SetValue(CoinServiceProperty, value);
    }
    
    public static readonly DependencyProperty CoinServiceProperty = DependencyProperty.Register(
        nameof(CoinService),
        typeof(ICoinService),
        typeof(MarketItem),
        new PropertyMetadata(default(ICoinService)));
    
    public Exchange? Exchange { get; set; }
    
    public MarketItem()
    {
        InitializeComponent();
    }
}