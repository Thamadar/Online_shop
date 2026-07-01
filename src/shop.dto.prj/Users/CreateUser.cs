using System.ComponentModel.DataAnnotations;

namespace Shop.Dto.Users;

public record CreateUsersRequest([Required][MinLength(1)] List<CreateUserRequest> Users); 
public record CreateUserRequest(
	[Required][StringLength(100)] string Login,
	[Required] string Password,
	[Required][StringLength(150)] string Address);

public record CreateUsersResponse(List<CreateUserResponse> Created); 
public record CreateUserResponse(
	Guid Id,
	DateTime CreatedAt);
