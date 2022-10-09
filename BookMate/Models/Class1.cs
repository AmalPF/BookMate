using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMate.Models
{
    public class UserProfile
    {
        public Users UserTable { get; set; }
        public List<Address> AddressTable { get; set; }
    }
}