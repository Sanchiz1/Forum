using Application;
using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.UseCases.Users.Commands;
using Application.UseCases.Users.Queries;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(GetUserByIdQuery getUserByIdQuery);
        Task<User> GetUserByUsernameAsync(GetUserByUsernameQuery getUserByUsernameQuery);
        Task<User> GetUserByEmailAsync(GetUserByEmailQuery getUserByEmailQuery);
        Task<User> GetUserByCredentialsAsync(GetUserByCredentialsQuery getUserByCredentialsQuery);
        Task CreateUserAsync(CreateUserCommand createUserCommand);
        Task UpdateUserAsync(UpdateUserCommand updateUserCommand);
        Task DeleteUserAsync(DeleteUserCommand deleteUserCommand);
    }
}
