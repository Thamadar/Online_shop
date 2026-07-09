using AutoMapper; 
using Shop.Dto;
using Shop.Dto.Users;  

namespace Shop.Server.Services.API;

public class UsersAPIService : IUsersAPIService
{
	private readonly IMapper _mapper;
	private readonly IUnitOfWork _unitOfWork;

	public UsersAPIService(IMapper mapper, IUnitOfWork unitOfWork) 
	{
		_mapper = mapper;
		_unitOfWork = unitOfWork;
	}

	/// <inheritdoc/>
	public async Task<GetUsersDto> GetUsersAsync(CancellationToken ct = default)
	{
		var usersResponse = await _unitOfWork.Users.GetUsersAsync(ct);
		return usersResponse;
	}

	/// <inheritdoc/>
	public async Task<GetUserDto> GetUserByIdAsync(Guid id, CancellationToken ct = default)
	{
		var userResponse = await _unitOfWork.Users.GetUserByIdAsync(id, ct);
		return userResponse;
	}

	/// <inheritdoc/>
	public async Task<GetUserDto> GetUserByLoginAsync(string login, CancellationToken ct = default)
	{
		var userResponse = await _unitOfWork.Users.GetUserByLoginAsync(login, ct);
		return userResponse;
	}

	/// <inheritdoc/>
	public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request)
	{
		await using var transaction = await _unitOfWork.BeginTransactionAsync();
		try
		{ 
			if(_unitOfWork.Users.IsAnyLoginExist(request.Login))
				throw new ArgumentException("Пользователь с таким логином уже существует. ");

			var resultResponse = await _unitOfWork.Users.CreateUserAsync(request);

			await transaction.CommitAsync();

			return resultResponse;
		}
		catch(Exception ex)
		{
			await transaction.RollbackAsync();
			throw new InvalidOperationException("Не удалось создать пользователя", ex);
		} 

	}
	/// <inheritdoc/>
	public async Task<CreateUsersResponse> CreateUsersAsync(CreateUsersRequest createUsersRequest)
	{

		await using var transaction = await _unitOfWork.BeginTransactionAsync();
		try
		{

			var logins = createUsersRequest.Users.Select(u => u.Login).ToList();
			var loginsExist = _unitOfWork.Users.CheckLoginsExist(logins);
			if(loginsExist.Count > 0)
				throw new ArgumentException($"Пользователи с таким логином уже существуют: {loginsExist}");

			var resultResponse = await _unitOfWork.Users.CreateUsersBatchAsync(createUsersRequest); 

			await transaction.CommitAsync();

			return resultResponse;
		}
		catch(Exception ex)
		{
			await transaction.RollbackAsync();
			throw new InvalidOperationException("Не удалось создать пользователей", ex);
		} 
	}

	/// <inheritdoc/>
	public async Task<EditUserResponse> EditUserAsync(EditUserRequest editUserRequest)
	{
		//TO DO...

		throw new ArgumentException($"Вызван нереализованный метод {nameof(EditUserAsync)}");
	}
	//TO DO: add.
	//Task<EditUsersResponse> EditUsers(EditUsersRequest editUsersRequest);


	/// <inheritdoc/>
	public async Task DeleteUserAsync(Guid userId)
	{

		//TO DO...

		throw new ArgumentException($"Вызван нереализованный метод {nameof(DeleteUserAsync)}");
	}
	/// <inheritdoc/>
	public async Task<List<BatchError>> DeleteUsersAsync(List<Guid> usersId)
	{

		//TO DO... 
		throw new ArgumentException($"Вызван нереализованный метод {nameof(DeleteUsersAsync)}");
	}

	/// <inheritdoc/>
	public void Initialization()
	{ }

}
