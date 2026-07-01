using System.ComponentModel.DataAnnotations;

namespace Shop.Dto.Users;
  
public record GetUsersDto(
	IReadOnlyList<GetUserDto> Users,
	int TotalCount); 
public record GetUserDto(
	[Required] Guid Id,
	[Required] string Address);

