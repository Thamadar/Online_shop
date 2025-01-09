namespace Shop.Server;

public static class ConsoleLog
{  
	public static void Write(String message)
	{ 
		Console.WriteLine(message + "\n"); 
	}


	public static void WriteError(String message)
	{
		var oldColor = Console.ForegroundColor;
		Console.ForegroundColor = ConsoleColor.Red;
		Console.WriteLine(message + "\n");
		Console.ForegroundColor = oldColor;
	}

	public static void WriteGreen(String message)
	{
		var oldColor = Console.ForegroundColor;
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine(message + "\n");
		Console.ForegroundColor = oldColor;
	}

	public static void Write(String message, ConsoleColor consoleColor)
	{
		var oldColor = Console.ForegroundColor;
		Console.ForegroundColor = consoleColor;
		Console.WriteLine(message + "\n");
		Console.ForegroundColor = oldColor;
	} 
}
