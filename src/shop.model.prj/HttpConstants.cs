/// <summary>Константы стандартных URI ядра.</summary>
public static class HttpConstants
{ 
	public const string api = "api/";
	public const string products = api + "products/";

	public const string getTotalProducts = products + getTotalProductsTask;

	public const string getTotalProductsTask = "getTotalProducts/";
}
