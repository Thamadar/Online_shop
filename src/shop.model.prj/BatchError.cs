namespace Shop.Dto;

public record BatchError(
	int Index,      // Индекс в исходном списке.
	string Message, // Ошибка.
	string? Field   // Поле с ошибкой.
);
