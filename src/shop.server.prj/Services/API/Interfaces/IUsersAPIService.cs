using Shop.Dto;
using Shop.Dto.Users;
using Shop.Server.Services.API.Interfaces;

namespace Shop.Server.Services.API;

public interface IUsersAPIService : IAPIService
{

	Task<GetUsersResponse> GetUsers();

	Task<CreateUserResponse> CreateUser(CreateUserRequest createUserRequest);
	Task<CreateUsersResponse> CreateUsers(CreateUsersRequest createUsersRequest); 

	Task<EditUserResponse> EditUser(EditUserRequest editUserRequest); 
	//TO DO: add.
	//Task<EditUsersResponse> EditUsers(EditUsersRequest editUsersRequest);


	Task<bool> DeleteUser(Guid userId); 
	Task<List<BatchError>> DeleteUsers(List<Guid> usersId);

}
