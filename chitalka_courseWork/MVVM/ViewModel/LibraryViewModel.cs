using CommunityToolkit.Mvvm.ComponentModel;


namespace chitalka_courseWork.MVVM.ViewModel;

public partial class LibraryViewModel : ObservableObject
{

    [ObservableProperty]
    private Book? _selectedBook;

    [ObservableProperty]
    private List<Book> _books;

    public LibraryViewModel() => Books =
        [
            new Book("A Tale of Two Cities", "Charles Dickens", "desc1", 350),
            new Book("The Little Prince", "Antoine de Saint-Exupéry", "desc2", 365),
            new Book("The Alchemist", "Paulo Coelho", "desc3", 350),
            new Book("Harry Potter and the Philosopher's Stone", "J. K. Rowling", "desc4", 350),
            new Book("And Then There Were None", "Agatha Christie", "desc5", 350),
        ];


}
