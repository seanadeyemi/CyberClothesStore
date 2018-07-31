using ClassLibrary1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Store.UI.Models
{
    public class UserListViewModel
    {
        public IEnumerable<User> Users { get; set; } = Enumerable.Empty<User>();
    }
}