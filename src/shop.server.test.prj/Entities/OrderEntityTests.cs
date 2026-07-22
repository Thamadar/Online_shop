using Shop.Server.Data; 
using Xunit;

namespace Shop.Server.Test.Entities;

public class OrderEntityTests
{
	[Fact]
	public void CompleteOrder_WhenOrderIsNew_SetsStatusToCompleted()
	{
		var order = new OrderEntity();

		order.Complete();

		Assert.True(order.IsCompleted);
	}

	[Fact]
	public void CompleteOrder_WhenOrderIsCancelled_ThrowsException()
	{
		var order = new OrderEntity();

		order.Cancel();

		Assert.Throws<InvalidOperationException>(order.Complete);
	}
	  
	[Fact]
	public void CancelOrder_WhenOrderIsCompleted_ThrowsException()
	{
		var order = new OrderEntity();

		order.Complete();

		Assert.Throws<InvalidOperationException>(order.Cancel);
	}

	[Fact]
	public void CancelOrder_WhenOrderIsCancelled_ThrowsException()
	{
		var order = new OrderEntity();

		order.Cancel();

		Assert.Throws<InvalidOperationException>(order.Cancel);
	}

}
