using chitalka_courseWork.MVVM.Model;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;


namespace chitalka_courseWork.MVVM.ViewModel;

public partial class LibraryViewModel : ObservableObject
{
    [ObservableProperty]
    private RelayCommand _deleteBookCommand;

    [ObservableProperty]
    private Book? _selectedBook;

    [ObservableProperty]
    private ObservableCollection<Book> _books;

    [ObservableProperty]
    private ObservableCollection<Book> _inProgressBooks;

    [ObservableProperty]
    private ObservableCollection<Book> _readBooks;



    public LibraryViewModel()
    {
        string jsonData = File.ReadAllText("C:\\Users\\Liliia\\source\\repos\\ogurtsy_new\\chitalka_courseWork\\DB\\all.json");
        Books = JsonConvert.DeserializeObject<ObservableCollection<Book>>(jsonData);
        DeleteBookCommand = new RelayCommand(DeleteBook);
    }
    
    public void DeleteBook()
    {
        if (SelectedBook != null)
        {
            Books.Remove(SelectedBook);
            string filePath = "C:\\Users\\Liliia\\source\\repos\\ogurtsy_new\\chitalka_courseWork\\DB\\data.json";
            string json = JsonConvert.SerializeObject(Books, Formatting.Indented);
            File.WriteAllText(filePath, json);
        }
        else
        {
            MessageBox.Show("Ви не обрали книгу!");
        }
    }
}
