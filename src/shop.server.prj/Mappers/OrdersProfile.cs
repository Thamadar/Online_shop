using AutoMapper;
using Shop.Dto.Orders; 
using Shop.Server.Data;

namespace Shop.Server.Mappers;

public class OrdersProfile : Profile
{
	public OrdersProfile()
	{
		#region ToEntity

		CreateMap<GetOrderDto, OrderEntity>()
			.ForMember(d => d.Id, o => o.MapFrom(x => x.Id))
			.ForMember(d => d.UserId, o => o.MapFrom(x => x.UserId))
			.ForMember(d => d.Products, o => o.MapFrom(x => x.Products))
			.ForMember(d => d.OrderAddress, o => o.MapFrom(x => x.OrderAddress))
			.ForMember(d => d.CreatedAt, o => o.MapFrom(x => x.CreatedAt));

		CreateMap<CreateOrderRequest, OrderEntity>()
			.ForMember(d => d.UserId, o => o.MapFrom(x => x.UserId))
			.ForMember(d => d.Products, o => o.MapFrom(x => x.Products))
			.ForMember(d => d.OrderAddress, o => o.MapFrom(x => x.OrderAddress))
			.ForMember(d => d.CreatedAt, o => o.MapFrom(x => DateTime.UtcNow));

		#endregion

		#region ToResponse

		CreateMap<OrderEntity, GetOrderDto>()
			.ForCtorParam(nameof(GetOrderDto.Id), o => o.MapFrom(x => x.Id))
			.ForCtorParam(nameof(GetOrderDto.UserId), o => o.MapFrom(x => x.UserId))
			.ForCtorParam(nameof(GetOrderDto.Products), o => o.MapFrom(x => x.Products))
			.ForCtorParam(nameof(GetOrderDto.OrderAddress), o => o.MapFrom(x => x.OrderAddress))
			.ForCtorParam(nameof(GetOrderDto.CreatedAt), o => o.MapFrom(x => x.CreatedAt));

		CreateMap<OrderEntity, CreateOrderResponse>()
			.ForCtorParam(nameof(GetOrderDto.Id), o => o.MapFrom(x => x.Id))
			.ForCtorParam(nameof(GetOrderDto.UserId), o => o.MapFrom(x => x.UserId))
			.ForCtorParam(nameof(GetOrderDto.Products), o => o.MapFrom(x => x.Products))
			.ForCtorParam(nameof(GetOrderDto.OrderAddress), o => o.MapFrom(x => x.OrderAddress))
			.ForCtorParam(nameof(GetOrderDto.CreatedAt), o => o.MapFrom(x => x.CreatedAt));

		#endregion

	}
}
