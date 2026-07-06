using AutoMapper;
using AutoMapper.QueryableExtensions; 
using Microsoft.EntityFrameworkCore; 
using Shop.Dto.Users;
using Shop.Server.Data; 

namespace Shop.Server.Repositories;

public class UsersRepository : IUsersRepository
{
	private readonly IMapper _mapper;
	private readonly ServerContext _serverContext; 

	public UsersRepository(IMapper mapper, ServerContext serverContext)
	{
		_mapper = mapper;
		_serverContext = serverContext;
	}
	 
	/// <inheritdoc/>
	public async Task<GetUsersDto> GetUsersAsync()
	{
		try
		{
			var userDtoList = await _serverContext.Users
				.AsNoTracking()
				.ProjectTo<GetUserDto>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return new GetUsersDto(
				Users: userDtoList,
				TotalCount: userDtoList.Count); 
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException("Не удалось получить пользователей", ex);
		}
	}

	/// <inheritdoc/>
	public async Task<GetUserDto> GetUserByIdAsync(Guid id)
	{
		try
		{
			var userEntity = await _serverContext.Users.FindAsync(id);

			return _mapper.Map<GetUserDto>(userEntity);  
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException($"Не удалось получить пользователей по Id: {id}", ex);
		} 
	}

	/// <inheritdoc/>
	public async Task<GetUserDto> GetUserByLoginAsync(string login)
	{
		try
		{
			var user = await _serverContext.Users
				.AsNoTracking()
				.Where(x => x.Login == login)
				.ProjectTo<GetUserDto>(_mapper.ConfigurationProvider)
				.FirstAsync();

			return user;
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException($"Не удалось получить пользователей по Login: {login}", ex);
		} 
	}

	/// <inheritdoc/>
	public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request)
	{
		await using var transaction = await _serverContext.Database.BeginTransactionAsync();
		try
		{
			var entity = _mapper.Map<UserEntity>(request);
			await _serverContext.Users.AddAsync(entity);
			await _serverContext.SaveChangesAsync(); 

			var userResponse = _mapper.Map<CreateUserResponse>(entity);  
			await transaction.CommitAsync();

			return userResponse;
		}
		catch(Exception ex)
		{
			await transaction.RollbackAsync();

			throw new InvalidOperationException("Не удалось создать и добавить пользователя в БД", ex);
		}  
	}

	/// <inheritdoc/>
	public async Task<CreateUsersResponse> CreateUsersBatchAsync(CreateUsersRequest requestBatch)
	{   
		await using var transaction = await _serverContext.Database.BeginTransactionAsync();
		try
		{
			var entities = _mapper.Map<List<UserEntity>>(requestBatch.Users); 
			 
			await _serverContext.Users.AddRangeAsync(entities);
			await _serverContext.SaveChangesAsync();   

			var usersResponse = new CreateUsersResponse(_mapper.Map<List<CreateUserResponse>>(entities));
			await transaction.CommitAsync();

			return usersResponse;

		}  
		catch(Exception ex)
		{
			await transaction.RollbackAsync();

			throw new InvalidOperationException("Не удалось создать и добавить пользователей в БД", ex);
		}
	}


	/// <inheritdoc/>
	public bool IsAnyLoginExist(string login)
	{
		return _serverContext.Users
			.AsNoTracking()
			.Any(x => x.Login == login);
	}

	/// <inheritdoc/>
	public List<string> CheckLoginsExist(List<string> logins)
	{
		var existLogins = _serverContext.Users
			.ToList()
			.Select(x => x.Login);

		return logins.Where(x => existLogins.Any(y => y == x)).ToList();
	}

}
