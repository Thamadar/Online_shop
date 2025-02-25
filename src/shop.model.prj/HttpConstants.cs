/// <summary>Константы стандартных URI ядра.</summary>
public static class HttpConstants
{ 
	public const string api = "api/";

	public const string products = api + "products/";
	public const string orders   = api + "orders/";
	public const string users    = api + "users/";


	public const string getTotalProductsTask = "getTotalProducts/";

	public const string postCreateOrderTask = "postOrder/";

	public const string postGetUserAddressByIdTask = "postGetUserAddressById/";
	public const string postGetUserIdByLoginTask   = "postGetUserIdByLogin/";

	public const string getTotalProducts = products + getTotalProductsTask;

	public const string postCreateOrder = orders + postCreateOrderTask;
	 
	public const string postGetUserAddressById = users + postGetUserAddressByIdTask; //TO DO: с заглвных + подчеркивания
	public const string postGetUserIdByLogin   = users + postGetUserIdByLoginTask;
}
