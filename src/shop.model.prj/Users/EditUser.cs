namespace Shop.Dto.Users;

public record EditUsersRequest(List<EditUserRequest> Users);
public record EditUserRequest(Guid Id, string EditAddress);

public record EditUsersResponse(List<EditUserResponse> Users); 
public record EditUserResponse(Guid Id, string NewAddress, DateTime UpdatedAt);

