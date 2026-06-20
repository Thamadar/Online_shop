using AutoMapper;
using Shop.Dto.Users;
using Shop.Server.Data;

namespace Shop.Server.Mappers;

public class UserProfile : Profile
{
	public UserProfile()
	{
		// Маппинг из сущности UserEntity в GetUserResponse
		CreateMap<UserEntity, GetUserResponse>()
		.ForMember(d => d.Id, opt => opt.MapFrom(s => s.Id))
		.ForMember(d => d.Address, opt => opt.MapFrom(s => s.Address));

		CreateMap<GetUserResponse, UserEntity>();

		CreateMap<UserEntity, GetUserItemDto>()
			.ForMember(dest => dest.Id,
					   opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.Address,
					   opt => opt.MapFrom(src => src.Address));
		CreateMap<GetUserItemDto, UserEntity>()
			.ForMember(dest => dest.Id,
					   opt => opt.MapFrom(src => src.Id))
			.ForMember(dest => dest.Address,
					   opt => opt.MapFrom(src => src.Address));

		CreateMap<IEnumerable<UserEntity>, GetUsersDto>()
			.ForMember(dest => dest.Users, opt => opt.MapFrom(src => src));
	}
}
