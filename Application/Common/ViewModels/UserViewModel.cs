using Application.Common.Constants;
using Application.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.ViewModels
{
    public class UserViewModel
    {
        public UserDto User { get; set; }
        public int Posts { get; set; }
        public int Comments { get; set; } = 0;
        public int Role_Id { get; set; } = 0;
        public string Role { get; set; }
    }
}
