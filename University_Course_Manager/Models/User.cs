using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Course_Manager.Models;

public class User
{
    public string Id { get; set; } 
    public string Name { get; set; } 
    public string Username { get; set; } 
    public string Password { get; set; }

    public void Login()
    {
        Console.WriteLine($"{Username} logged in.");
    }

    public void Logout()
    {
        Console.WriteLine($"{Username} logged out.");
    }
}
