using Application.Common.DTOs;
using Domain.Entities;

namespace Application.Common.ViewModels
{
    public class UserViewModelDto
    {
        public UserDto User { get; set; }
        public int Posts { get; set; }
        public int Comments { get; set; } = 0;
        public int Role_Id { get; set; } = 0;
        public string Role { get; set; }
    }
}
