using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chitalka_courseWork.MVVM.Model;

public class User
{
    public string Nickname {  get; set; }
    public int Id { get; set; }
    private string Password { get; set; }
    //List<BookList> bookLists { get; set; }

    public User(string nickname, string password)
    {
        Nickname = nickname;
        Password = password;
        //bookLists = [];
    }


}
