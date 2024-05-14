using chitalka_courseWork.MVVM.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;


namespace chitalka_courseWork.MVVM.ViewModel;

public partial class LibraryViewModel : ObservableObject
{

    [ObservableProperty]
    private Book? _selectedBook;

    [ObservableProperty]
    private ObservableCollection<Book> _books;

    public LibraryViewModel()
    {
        string jsonData = File.ReadAllText("C:\\Users\\Liliia\\source\\repos\\ogurtsy_new\\chitalka_courseWork\\DB\\data.json");
        Books = JsonConvert.DeserializeObject<ObservableCollection<Book>>(jsonData);

    }
    
 
}
