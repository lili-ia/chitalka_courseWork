using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chitalka_courseWork.MVVM.Model;

public partial class User : ObservableObject
{
    [ObservableProperty]
    private string _nickname;
    [ObservableProperty]
    private List<ObservableCollection<Book>> _bookLists;
    public User(string nickname)
    {
        Nickname = nickname;
        BookLists = [];
    }

    

}
