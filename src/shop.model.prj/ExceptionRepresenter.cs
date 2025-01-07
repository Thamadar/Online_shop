namespace Shop.Model;
public class ExceptionRepresenter
{
	/// <summary>
	/// Тип ошибки.
	/// </summary>
	public string ExceptionType { get; }

	/// <summary>
	/// Сообщение.
	/// </summary>
	public string Message { get; }

	/// <summary>
	/// Источник ошибки.
	/// </summary>
	public string? Source { get; }

	/// <summary>
	/// StackTrace.
	/// </summary>
	public string? StackTrace { get; }

	/// <summary>
	/// InnerException.
	/// </summary>
	public Exception? InnerException { get; }

	public ExceptionRepresenter(Exception exception)
	{ 
		ExceptionType  = GetExceptionTypeName(exception);

		Message        = exception.Message; 
		Source         = exception.Source; 
		StackTrace     = exception.StackTrace; 
		InnerException = exception.InnerException;
	}

	public override string ToString()
	{
		return string.Format("{0}: source = {1}. message = {2}. stackTrace = {3}. innerException = {4}", ExceptionType, Source, Message, StackTrace, InnerException?.ToString());
	}

	private string GetExceptionTypeName(Exception exception)
	{
		switch(exception)
		{
			case InvalidOperationException _:
				return nameof(InvalidOperationException);
			case ArgumentNullException _:
				return nameof(ArgumentNullException);
			case ArgumentOutOfRangeException _:
				return nameof(ArgumentOutOfRangeException);
			case ArgumentException _:
				return nameof(ArgumentException);
			case OperationCanceledException _:
				return nameof(OperationCanceledException);
			case TimeoutException _:
				return nameof(TimeoutException);
			case FormatException _:
				return nameof(FormatException);
			case FileNotFoundException _:
				return nameof(FileNotFoundException);
			case KeyNotFoundException _:
				return nameof(KeyNotFoundException);
			default:
				return nameof(Exception);
		}
	}
}
