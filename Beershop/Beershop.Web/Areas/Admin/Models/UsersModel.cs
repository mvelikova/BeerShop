using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BeerShop.Web.Areas.Admin.Models
{
    public class UsersModel
    {  public string Username { get; set; }

        public string Email { get; set; }
        public string[] Roles { get; set; }

    }
}
