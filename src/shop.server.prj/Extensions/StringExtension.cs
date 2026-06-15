using System.Security.Cryptography;
using System.Text;

namespace Shop.Server.Extensions;

public static class StringExtension
{
	public static string HashEncrypt(this string value)
	{ 
		using var hmac = new HMACSHA256();
		var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(value));
		return Convert.ToBase64String(hash);
	}
}
