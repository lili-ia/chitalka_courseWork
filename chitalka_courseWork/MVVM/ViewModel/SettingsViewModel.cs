using chitalka_courseWork.MVVM.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Windows;

namespace chitalka_courseWork.MVVM.ViewModel;

public partial class SettingsViewModel : ObservableObject
{
    private int _dailyPagesGoal;
    private int _annuallyBooksGoal;
    
}