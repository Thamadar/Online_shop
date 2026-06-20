namespace Shop.Utilities;

public static class ConsoleLog
{  
	public static void Write(string message)
	{ 
		Console.WriteLine(message + "\n"); 
	}
	 
	public static void WriteError(string message)
	{
		var oldColor = Console.ForegroundColor;
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine(message + "\n");
		Console.ForegroundColor = oldColor;
	}

	public static void WriteGreen(string message)
	{
		var oldColor = Console.ForegroundColor;
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine(message + "\n");
		Console.ForegroundColor = oldColor;
	}

	public static void Write(string message, ConsoleColor consoleColor)
	{
		var oldColor = Console.ForegroundColor;
		Console.ForegroundColor = consoleColor;
		Console.WriteLine(message + "\n");
		Console.ForegroundColor = oldColor;
	}
}
