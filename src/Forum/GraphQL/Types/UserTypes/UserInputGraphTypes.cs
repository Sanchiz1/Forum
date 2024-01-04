using Application.UseCases.Replies.Commands;
using Application.UseCases.Users.Commands;
using Application.UseCases.Users.Queries;
using GraphQL.Types;

namespace Forum.GraphQL.Types.UserTypes
{
    public class GetSearchedUsersInputGraphType : InputObjectGraphType<GetSearchedUsersQuery>
    {
        public GetSearchedUsersInputGraphType()
        {
            Field(p => p.Next, type: typeof(IntGraphType));
            Field(p => p.Offset, type: typeof(IntGraphType));
            Field(p => p.User_Timestamp, type: typeof(DateTimeGraphType));
            Field(p => p.Search, type: typeof(StringGraphType));
        }
    }
    public class GetUserByIdInputGraphType : InputObjectGraphType<GetUserByIdQuery>
    {
        public GetUserByIdInputGraphType()
        {
            Field(p => p.User_Id, type: typeof(IntGraphType));
        }
    }
    public class GetUserByUsernameInputGraphType : InputObjectGraphType<GetUserByUsernameQuery>
    {
        public GetUserByUsernameInputGraphType()
        {
            Field(p => p.Username, type: typeof(StringGraphType));
        }
    }
    public class GetUserByEmailInputGraphType : InputObjectGraphType<GetUserByEmailQuery>
    {
        public GetUserByEmailInputGraphType()
        {
            Field(p => p.Email, type: typeof(StringGraphType));
        }
    }
    public class GetUserByCredentialsInputGraphType : InputObjectGraphType<GetUserByCredentialsQuery>
    {
        public GetUserByCredentialsInputGraphType()
        {
            Field(p => p.Username_Or_Email, type: typeof(StringGraphType));
            Field(p => p.Password, type: typeof(StringGraphType));
        }
    }

    public class CreateUserInputGraphType : InputObjectGraphType<CreateUserCommand>
    {
        public CreateUserInputGraphType()
        {
            Field(c => c.Username, type: typeof(StringGraphType));
            Field(c => c.Email, type: typeof(StringGraphType));
            Field(c => c.Bio, type: typeof(StringGraphType));
            Field(c => c.Password, type: typeof(StringGraphType));
        }
    }
    public class UpdateUserInputGraphType : InputObjectGraphType<UpdateUserCommand>
    {
        public UpdateUserInputGraphType()
        {
            Field(c => c.User_Id, type: typeof(IntGraphType));
            Field(c => c.Username, type: typeof(StringGraphType));
            Field(c => c.Email, type: typeof(StringGraphType));
            Field(c => c.Bio, type: typeof(StringGraphType));
        }
    }
    public class UpdateUserRoleInputGraphType : InputObjectGraphType<UpdateUserRoleCommand>
    {
        public UpdateUserRoleInputGraphType()
        {
            Field(c => c.User_Id, type: typeof(IntGraphType));
            Field(c => c.Role_Id, type: typeof(IntGraphType), nullable: true);
        }
    }
    public class ChangeUserPasswordInputGraphType : InputObjectGraphType<ChangeUserPasswordCommand>
    {
        public ChangeUserPasswordInputGraphType()
        {
            Field(c => c.Password, type: typeof(StringGraphType));
            Field(c => c.New_Password, type: typeof(StringGraphType));
            Field(c => c.User_Id, type: typeof(IntGraphType));
        }
    }
    public class DeleteUserInputGraphType : InputObjectGraphType<DeleteUserCommand>
    {
        public DeleteUserInputGraphType()
        {
            Field(c => c.Password, type: typeof(StringGraphType), nullable: true);
            Field(c => c.User_Id, type: typeof(IntGraphType));
        }
    }
}
