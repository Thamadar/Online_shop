using AutoMapper;
using Shop.Dto.Orders; 
using Shop.Server.Data;

namespace Shop.Server.Mappers;

public class OrdersProfile : Profile
{
	public OrdersProfile()
	{
		#region ToEntity
		 
		CreateMap<OrderProductRequest, OrderProductEntity>()
		   .ForMember(d => d.Quantity, o => o.MapFrom(x => x.Quantity))
		   .ForMember(d => d.ProductId, o => o.MapFrom(x => x.ProductId))
		   .ForMember(d => d.OrderId, o => o.Ignore()) 
		   .ForMember(d => d.ProductEntity, o => o.Ignore())
		   .ForMember(d => d.OrderEntity, o => o.Ignore()); 

		CreateMap<CreateOrderRequest, OrderEntity>()
			.ForMember(d => d.UserId, o => o.MapFrom(x => x.UserId))
			.ForMember(d => d.OrderProducts, o => o.MapFrom(s => s.OrderProducts))
			.ForMember(d => d.OrderAddress, o => o.MapFrom(x => x.OrderAddress))
			.ForMember(d => d.CreatedAt, o => o.MapFrom(x => DateTime.UtcNow))
			.ForMember(d => d.Id, o => o.Ignore());

		#endregion

		#region ToResponse

		CreateMap<OrderProductEntity, OrderProductResponse>();
		CreateMap<OrderEntity, GetOrderResponse>()
			.ForCtorParam(nameof(GetOrderResponse.Id), o => o.MapFrom(x => x.Id))
			.ForCtorParam(nameof(GetOrderResponse.UserId), o => o.MapFrom(x => x.UserId))
			.ForCtorParam(nameof(GetOrderResponse.OrderProducts), o => o.MapFrom(x => x.OrderProducts))
			.ForCtorParam(nameof(GetOrderResponse.OrderAddress), o => o.MapFrom(x => x.OrderAddress))
			.ForCtorParam(nameof(GetOrderResponse.CreatedAt), o => o.MapFrom(x => x.CreatedAt));

		CreateMap<OrderEntity, CreateOrderResponse>()
			.ForCtorParam(nameof(CreateOrderResponse.Id), o => o.MapFrom(x => x.Id))
			.ForCtorParam(nameof(CreateOrderResponse.UserId), o => o.MapFrom(x => x.UserId))
			.ForCtorParam(nameof(CreateOrderResponse.OrderProducts), o => o.MapFrom(x => x.OrderProducts))
			.ForCtorParam(nameof(CreateOrderResponse.OrderAddress), o => o.MapFrom(x => x.OrderAddress))
			.ForCtorParam(nameof(CreateOrderResponse.CreatedAt), o => o.MapFrom(x => x.CreatedAt));

		#endregion

	}
}
