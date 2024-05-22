using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;


namespace chitalka_courseWork.MVVM.Model;

public class User
{
    public Dictionary<Book, Statistics> BookStats { get; set; }
    public User() => BookStats = [];

}
