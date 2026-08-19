using System;
using System.Collections.Generic;
using System.Text;

namespace GoldDigger.Model
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }
}
