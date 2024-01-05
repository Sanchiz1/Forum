using Application.Common.Models;
using Application.Common.ViewModels;
using Application.UseCases.Users.Commands;
using Application.UseCases.Users.Queries;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<UserViewModel>> GetSearchedUsersAsync(GetSearchedUsersQuery getSearchedUsersQuery);
        Task<UserViewModel> GetUserByIdAsync(int User_Id);
        Task<UserViewModel> GetUserByUsernameAsync(GetUserByUsernameQuery getUserByUsernameQuery);
        Task<UserViewModel> GetUserByEmailAsync(GetUserByEmailQuery getUserByEmailQuery);
        Task<UserViewModel> GetUserByUsernameOrEmailAsync(string Username_Or_Email);
        Task<UserViewModel> GetUserByCredentialsAsync(string Username_Or_Email, string Password);
        Task<string> GetUserSaltAsync(int user_id);
        Task<string> GetUserPasswordAsync(int user_id);
        Task CreateUserAsync(string Username, string Email, string Bio, string Password, string Salt);
        Task UpdateUserAsync(UpdateUserCommand updateUserCommand);
        Task ChangeUserPasswordAsync(string Password, string Salt, int User_Id);
        Task UpdateUserRoleAsync(UpdateUserRoleCommand updateUserRoleCommand);
        Task DeleteUserAsync(DeleteUserCommand deleteUserCommand);
    }
}
