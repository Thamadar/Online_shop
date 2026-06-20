using System.ComponentModel.DataAnnotations;

namespace Shop.Dto.Users;
  
public record GetUsersResponse(
	IReadOnlyList<GetUserResponse> Users,
	int TotalCount); 
public record GetUserResponse(
	Guid Id,
	string Address);

