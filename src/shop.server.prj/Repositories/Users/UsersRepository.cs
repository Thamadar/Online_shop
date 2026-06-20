using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Shop.Dto.Users;
using Shop.Server.Data; 

namespace Shop.Server.Repositories;

public class UsersRepository : IUsersRepository
{
	private readonly IMapper _mapper;
	private readonly UserContext _userContext; 

	public UsersRepository(IMapper mapper, UserContext userContext)
	{
		_mapper = mapper;
		_userContext = userContext;
	}
	 
	/// <inheritdoc/>
	public async Task<GetUsersResponse> GetUsers()
	{
		try
		{
			var userResponseList = await _userContext.Users
				.AsNoTracking()
				.ProjectTo<GetUserResponse>(_mapper.ConfigurationProvider)
				.ToListAsync();

			return new GetUsersResponse(
				Users: userResponseList,
				TotalCount: userResponseList.Count); 
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException("Не удалось получить пользователей", ex);
		}
	}

	/// <inheritdoc/>
	public bool IsAnyLoginExists(string login)
	{
		return _userContext.Users.Any(x => x.Login == login);
	}

	/// <inheritdoc/>
	public async Task<UserEntity> GetUserById(Guid id)
	{
		var user = await _userContext.Users
			.AsNoTracking()
			.Where(x => x.Id == id)
			.FirstOrDefaultAsync();

		if(user != null)
		{
			return user;
		}

		throw new InvalidOperationException($"Не удалось найти пользователя по Id: {id}");
	}

	/// <inheritdoc/>
	public async Task<UserEntity> GetUserByLogin(string login)
	{
		var user = await _userContext.Users
			.AsNoTracking()
			.Where(x => x.Login == login)
			.FirstOrDefaultAsync();

		if(user != null)
		{
			return user;
		}

		throw new InvalidOperationException($"Не удалось найти пользователя по Login: {login}");
	}

	/// <inheritdoc/>
	public async Task<UserEntity> CreateUser(CreateUserRequest request)
	{
		var id = Guid.NewGuid();

		for(int i = 0; i < 10; i++)
		{
			if(!_userContext.Users.Any(x => x.Id == id))
			{
				break;
			}

			id = Guid.NewGuid();
		}

		var entity = new UserEntity()
		{
			Id        = id,
			Login     = request.Login,
			Password  = request.Password,
			Address   = request.Address,
			CreatedAt = DateTime.Now,
			UpdatedAt = DateTime.Now,
		};
		try
		{ 
			await _userContext.Users.AddAsync(entity);
			await _userContext.SaveChangesAsync();
		}
		catch(Exception ex)
		{
			throw new InvalidOperationException("Не удалось создать пользователя", ex);
		} 

		return entity; 
	}

	/// <inheritdoc/>
	public async Task<List<UserEntity>> CreateUsersBatch(List<UserEntity> usersBatch)
	{
		using var transaction = await _userContext.Database.BeginTransactionAsync();
		try
		{
			await _userContext.Users.AddRangeAsync(usersBatch);
			await _userContext.SaveChangesAsync();  
			await transaction.CommitAsync();
			return usersBatch;
		}  
		catch(Exception ex)
		{ 
			throw new InvalidOperationException("Не удалось создать пользователей", ex);
		}
	}
}
