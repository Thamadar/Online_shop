using AutoMapper; 
using Shop.Dto.Users;
using Shop.Server.Data;

namespace Shop.Server.Mappers;

public class UserProfile : Profile
{
	public UserProfile()
	{ 

		#region ToEntity

		CreateMap<GetUserDto, UserEntity>()
			.ForMember(d => d.Id, o => o.MapFrom(x => x.Id))
			.ForMember(d => d.Address, o => o.MapFrom(x => x.Address));

		CreateMap<CreateUserRequest, UserEntity>() 
			.ForMember(d => d.Login, o => o.MapFrom(x => x.Login))
			.ForMember(d => d.Address, o => o.MapFrom(x => x.Address))
			.ForMember(d => d.Password, o => o.MapFrom(x => x.Password)) 
			.ForMember(d => d.CreatedAt, o => o.MapFrom(x => DateTime.UtcNow))
			.ForMember(d => d.UpdatedAt, o => o.MapFrom(x => DateTime.UtcNow)); 

		#endregion
		 
		#region ToResponse

		CreateMap<UserEntity, GetUserDto>()
			.ForCtorParam(nameof(GetUserDto.Id), o => o.MapFrom(x => x.Id))
			.ForCtorParam(nameof(GetUserDto.Address), o => o.MapFrom(x => x.Address));

		CreateMap<UserEntity, CreateUserResponse>()
			.ForCtorParam(nameof(CreateUserResponse.Id), o => o.MapFrom(x => x.Id))
			.ForCtorParam(nameof(CreateUserResponse.CreatedAt), o => o.MapFrom(x => x.CreatedAt)); 

		#endregion

	}
}
