using AutoMapper;
using Shop.Dto.Products;
using Shop.Server.Data;

namespace Shop.Server.Mappers;

public class ProductsProfile : Profile
{
	public ProductsProfile()
	{

		#region ToEntity

		CreateMap<ProductLocalizationRequest, ProductLocalizationEntity>()
		   .ForMember(d => d.LangCode, o => o.MapFrom(x => x.LangCode))
		   .ForMember(d => d.DisplayName, o => o.MapFrom(x => x.DisplayName))
		   .ForMember(d => d.ProductId, o => o.Ignore())
		   .ForMember(d => d.Product, o => o.Ignore());
		CreateMap<CreateProductRequest, ProductEntity>()
		   .ForMember(d => d.ProductName, o => o.MapFrom(x => x.ProductName))
		   .ForMember(d => d.CurrentCount, o => o.MapFrom(x => x.CurrentCount))
		   .ForMember(d => d.BasePrice, o => o.MapFrom(x => x.BasePrice))
		   .ForMember(d => d.DiscountValue, o => o.MapFrom(x => x.Discount.DiscountValue))
		   .ForMember(d => d.DiscountUnit, o => o.MapFrom(x => x.Discount.DiscountUnit))
		   .ForMember(d => d.Weight, o => o.MapFrom(x => x.Weight))
		   .ForMember(d => d.Image, o => o.MapFrom(x => x.Image)) 
		   .ForMember(d => d.Localizations, o => o.MapFrom(s => s.Localizations))
		   .ForMember(d => d.Id, o => o.Ignore()); 


		#endregion

		#region ToResponse

		CreateMap<ProductLocalizationEntity, ProductLocalizationResponse>(); 
		CreateMap<ProductEntity, CreateProductResponse>()
		   .ForCtorParam(nameof(GetProductResponse.Id), o => o.MapFrom(x => x.Id))
		   .ForCtorParam(nameof(GetProductResponse.ProductName), o => o.MapFrom(x => x.ProductName))
		   .ForCtorParam(nameof(GetProductResponse.CurrentCount), o => o.MapFrom(x => x.CurrentCount))
		   .ForCtorParam(nameof(GetProductResponse.BasePrice), o => o.MapFrom(x => x.BasePrice))
		   .ForCtorParam(nameof(GetProductResponse.ResultPrice), o => o.MapFrom(x => x.ResultPrice))
		   .ForCtorParam(nameof(GetProductResponse.Discount), o => o.MapFrom(x => new CreateProductDiscountDto(x.DiscountValue, x.DiscountUnit)))
		   .ForCtorParam(nameof(GetProductResponse.Weight), o => o.MapFrom(x => x.Weight))
		   .ForCtorParam(nameof(GetProductResponse.Image), o => o.MapFrom(x => x.Image))
		   .ForCtorParam(nameof(GetProductResponse.Localizations), o => o.MapFrom(s => s.Localizations));

		CreateMap<ProductEntity, GetProductResponse>()
		   .ForCtorParam(nameof(GetProductResponse.Id), o => o.MapFrom(x => x.Id))
		   .ForCtorParam(nameof(GetProductResponse.ProductName), o => o.MapFrom(x => x.ProductName))
		   .ForCtorParam(nameof(GetProductResponse.CurrentCount), o => o.MapFrom(x => x.CurrentCount))
		   .ForCtorParam(nameof(GetProductResponse.BasePrice), o => o.MapFrom(x => x.BasePrice))
		   .ForCtorParam(nameof(GetProductResponse.ResultPrice), o => o.MapFrom(x => x.ResultPrice))
		   .ForCtorParam(nameof(GetProductResponse.Discount), o => o.MapFrom(x => new CreateProductDiscountDto(x.DiscountValue, x.DiscountUnit)))
		   .ForCtorParam(nameof(GetProductResponse.Weight), o => o.MapFrom(x => x.Weight))
		   .ForCtorParam(nameof(GetProductResponse.Image), o => o.MapFrom(x => x.Image)) 
		   .ForCtorParam(nameof(GetProductResponse.Localizations), o => o.MapFrom(s => s.Localizations));

		#endregion

	}
}
