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
        Task<UserViewModel> GetUserByIdAsync(GetUserByIdQuery getUserByIdQuery);
        Task<UserViewModel> GetUserByUsernameAsync(GetUserByUsernameQuery getUserByUsernameQuery);
        Task<UserViewModel> GetUserByEmailAsync(GetUserByEmailQuery getUserByEmailQuery);
        Task<UserViewModel> GetUserByCredentialsAsync(GetUserByCredentialsQuery getUserByCredentialsQuery);
        Task<bool> CheckUserPasswordAsync(string password, int user_id);
        Task CreateUserAsync(CreateUserCommand createUserCommand);
        Task UpdateUserAsync(UpdateUserCommand updateUserCommand);
        Task ChangeUserPasswordAsync(ChangeUserPasswordCommand changeUserPasswordCommand);
        Task UpdateUserRoleAsync(UpdateUserRoleCommand updateUserRoleCommand);
        Task DeleteUserAsync(DeleteUserCommand deleteUserCommand);
    }
}
