using Shop.Dto.Products;
using Shop.Server.Data;
using Xunit;

namespace Shop.Server.Test.Entities;

public class ProductEntityTests
{
	[Fact]
	public void SetDiscountTo60Percent_WhenBasePrice500_ReturnsBasePrice200()
	{
		var basePrice = 500;
		var discountType = DiscountUnit.Percent;
		var discountValue = 60;
		var exceptedValue = basePrice - (basePrice / 100 * discountValue);
		var productEntity = new ProductEntity()
		{
			BasePrice = basePrice
		};

		productEntity.SetDiscount(discountValue, discountType);

		Assert.Equal(exceptedValue, productEntity.ResultPrice);
	}

	[Fact]
	public void SetDiscountToPercent100_WhenBasePrice500_ThrowsException()
	{
		var basePrice = 500;
		var discountType = DiscountUnit.Percent;
		var discountValue = 200; 
		var productEntity = new ProductEntity()
		{
			BasePrice = basePrice
		};

		Assert.Throws<InvalidOperationException>(() => productEntity.SetDiscount(discountValue, discountType));

	}

	[Fact]
	public void SetDiscountToPercent0_WhenBasePrice500_ThrowsException()
	{
		var basePrice = 500;
		var discountType = DiscountUnit.Percent;
		var discountValue = 0; 
		var productEntity = new ProductEntity()
		{
			BasePrice = basePrice
		};

		Assert.Throws<InvalidOperationException>(() => productEntity.SetDiscount(discountValue, discountType)); 
	}

	[Fact]
	public void SetDiscountToFixedAmount500_WhenBasePrice500_ThrowsException()
	{
		var basePrice = 500;
		var discountType = DiscountUnit.FixedAmount;
		var discountValue = 500;
		var productEntity = new ProductEntity()
		{
			BasePrice = basePrice
		};

		Assert.Throws<InvalidOperationException>(() => productEntity.SetDiscount(discountValue, discountType));

	}

	[Fact]
	public void SetDiscountToFixedAmount0_WhenBasePrice500_ThrowsException()
	{
		var basePrice = 500;
		var discountType = DiscountUnit.FixedAmount;
		var discountValue = 0;
		var productEntity = new ProductEntity()
		{
			BasePrice = basePrice
		};

		Assert.Throws<InvalidOperationException>(() => productEntity.SetDiscount(discountValue, discountType));
	}
}
