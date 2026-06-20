using System.ComponentModel.DataAnnotations;

namespace Shop.Dto.Users;

public record CreateUsersRequest([Required][MinLength(1)] List<CreateUserRequest> Users); 
public record CreateUserRequest(
	[Required][StringLength(100)] string Login,
	[Required] string Password,
	[Required] string Address);

public record CreateUsersResponse(
	List<CreateUserResponse> Created,
	List<BatchError> Failed,
	int TotalCreated,
	int TotalFailed); 
public record CreateUserResponse(
	Guid Id,
	string Login,
	string Password,
	string Address,
	DateTime CreatedAt);
