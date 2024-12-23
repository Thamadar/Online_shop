using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Markup.Xaml.Templates; 

namespace Shop.UI.Controls;
 
[TemplatePart("PART_TextTextBlock", typeof(TextBlock))]
[TemplatePart("PART_IconContentControl", typeof(ContentControl))]

public class ButtonIcon : Button
{

	public static readonly StyledProperty<ControlTemplate> IconContentProperty =
		AvaloniaProperty.Register<ButtonIcon, ControlTemplate>(nameof(IconContent));

	public static readonly StyledProperty<bool> IsShortSizeProperty =
		AvaloniaProperty.Register<ButtonIcon, bool>(nameof(IsShortSize));

	public static readonly StyledProperty<double> IconWidthProperty =
		AvaloniaProperty.Register<ButtonIcon, double>(nameof(IconWidth), defaultValue: 24);

	public static readonly StyledProperty<double> IconHeightProperty =
		AvaloniaProperty.Register<ButtonIcon, double>(nameof(IconHeight), defaultValue: 24);

	public static readonly StyledProperty<string> TextButtonProperty =
		AvaloniaProperty.Register<ButtonIcon, string>(nameof(TextButton));


	/// <summary>
	/// Иконка в кнопке.
	/// </summary>
	public ControlTemplate IconContent
	{
		get => GetValue(IconContentProperty);
		set => SetValue(IconContentProperty, value);
	}

	/// <summary>
	/// Скукожена ли кнопка?
	/// </summary>
	public bool IsShortSize
	{
		get => GetValue(IsShortSizeProperty);
		set => SetValue(IsShortSizeProperty, value);
	}

	/// <summary>
	/// Отображаемый текст в кнопке
	/// </summary>
	public string TextButton
	{
		get => GetValue(TextButtonProperty);
		set => SetValue(TextButtonProperty, value);
	}
	 
	/// <summary>
	/// Ширина иконки.
	/// </summary>
	public double IconWidth
	{
		get => GetValue(IconWidthProperty);
		set => SetValue(IconWidthProperty, value);
	}

	/// <summary>
	/// Длина иконки.
	/// </summary>
	public double IconHeight
	{
		get => GetValue(IconHeightProperty);
		set => SetValue(IconHeightProperty, value);
	} 

	public ButtonIcon()
	{ 
	}
	 
}
