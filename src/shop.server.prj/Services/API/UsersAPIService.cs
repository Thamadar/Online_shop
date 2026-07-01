using AutoMapper; 
using Shop.Dto;
using Shop.Dto.Users; 
using Shop.Server.Repositories;

namespace Shop.Server.Services.API;

public class UsersAPIService : IUsersAPIService
{
	private readonly IUsersRepository _usersRepository;
	private readonly IMapper _mapper;

	public UsersAPIService(IMapper mapper, IUsersRepository repository) 
	{
		_mapper = mapper;
		_usersRepository = repository;
	}

	/// <inheritdoc/>
	public async Task<GetUsersDto> GetUsersAsync()
	{
		var usersResponse = await _usersRepository.GetUsersAsync();
		return usersResponse;
	}

	/// <inheritdoc/>
	public async Task<GetUserDto> GetUserByIdAsync(Guid id)
	{
		var userResponse = await _usersRepository.GetUserByIdAsync(id);
		return userResponse;
	}

	/// <inheritdoc/>
	public async Task<GetUserDto> GetUserByLoginAsync(string login)
	{
		var userResponse = await _usersRepository.GetUserByLoginAsync(login);
		return userResponse;
	}

	/// <inheritdoc/>
	public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request)
	{
		if(_usersRepository.IsAnyLoginExist(request.Login))
			throw new ArgumentException("Пользователь с таким логином уже существует. ");
		 
		var resultResponse = await _usersRepository.CreateUserAsync(request);
		return resultResponse;

	}
	/// <inheritdoc/>
	public async Task<CreateUsersResponse> CreateUsersAsync(CreateUsersRequest createUsersRequest)
	{ 

		var logins = createUsersRequest.Users.Select(u => u.Login).ToList();
		var loginsExist = _usersRepository.CheckLoginsExist(logins); 
		if(loginsExist.Count > 0)
			throw new ArgumentException($"Пользователи с таким логином уже существуют: {loginsExist}");
		 
		var resultResponse = await _usersRepository.CreateUsersBatchAsync(createUsersRequest);
		return resultResponse;
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
