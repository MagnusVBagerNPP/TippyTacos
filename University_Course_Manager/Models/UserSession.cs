using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace University_Course_Manager.Models;

public class UserSession
{
    public string Id { get; set; }
    public string DisplayName { get; set; }
    public string Role { get; set; }
}
