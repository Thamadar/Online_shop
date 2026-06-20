using AutoMapper;
using Azure.Core;
using Shop.Dto;
using Shop.Dto.Users;
using Shop.Server.Data;
using Shop.Server.Repositories;

namespace Shop.Server.Services.API;

public class UsersAPIService : IUsersAPIService
{
	private readonly IUsersRepository _usersRepository;
	private readonly IMapper _mapper;

	public UsersAPIService(IMapper mapper, IUsersRepository usersRepository) 
	{
		_mapper = mapper;
		_usersRepository = usersRepository;
	}

	/// <inheritdoc/>
	public async Task<GetUsersResponse> GetUsers()
	{
		var usersResponse = await _usersRepository.GetUsers();
		return usersResponse;
	}

	/// <inheritdoc/>
	public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
	{
		if(_usersRepository.IsAnyLoginExists(request.Login))
			throw new ArgumentException("Login already exists");

		//var userEntity = _mapper.Map<UserEntity>(createUserRequest);
		var result = _usersRepository.CreateUser(request);
		return _mapper.Map<CreateUserResponse>(result);

	}
	/// <inheritdoc/>
	public async Task<CreateUsersResponse> CreateUsers(CreateUsersRequest createUsersRequest)
	{
		// 1. Проверка на дубликаты (бизнес-логика)
		var logins = request.Users.Select(u => u.Login).ToList();
		var existing = await _userRepository.FindByLogins(logins);
		if(existing.Count > 0)
			throw new DuplicateException($"Logins already exist: {existing.Select(e => e.Login)}");

		// 2. bulk INSERT через CreateBatch
		var usersEntities = request.Users.Select(u => _mapper.Map<User>(u)).ToList();
		var results = await _usersRepository.CreateBatch(usersEntities);

		// 3. Формируем ответ
		return new CreateUsersResponse(
			Created: results.Select(r => _mapper.Map<CreateUserResponse>(r)).ToList(),
			Failed: Enumerable.Empty<BatchError>(),
			TotalCreated: results.Count(),
			TotalFailed 
		);
	}

	/// <inheritdoc/>
	public async Task<EditUserResponse> EditUser(EditUserRequest editUserRequest)
	{

	}
	//TO DO: add.
	//Task<EditUsersResponse> EditUsers(EditUsersRequest editUsersRequest);


	/// <inheritdoc/>
	public async Task<bool> DeleteUser(Guid userId)
	{

	}
	/// <inheritdoc/>
	public async Task<List<BatchError>> DeleteUsers(List<Guid> usersId)
	{

	}

	/// <inheritdoc/>
	public void Initialization()
	{ }

}
