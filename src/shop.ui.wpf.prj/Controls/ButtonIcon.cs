using System.Windows.Controls;
using System.Windows;
using System.Reactive.Linq;

using ReactiveUI;

namespace Shop.UI.WPF.Controls;

[TemplatePart(Name = "PART_TextTextBlock",      Type = typeof(TextBlock))]
[TemplatePart(Name = "PART_IconContentControl", Type = typeof(ContentControl))] 
public class ButtonIcon : Button
{  
	public static readonly DependencyProperty IconContentProperty =
		DependencyProperty.Register(nameof(IconContent), typeof(ControlTemplate), typeof(ButtonIcon));

	public static readonly DependencyProperty IsShortSizeStyleProperty =
		DependencyProperty.Register(nameof(IsShortSizeStyle), typeof(bool), typeof(ButtonIcon));

	public static readonly DependencyProperty IconWidthProperty =
		DependencyProperty.Register(nameof(IconWidth), typeof(double), typeof(ButtonIcon), new PropertyMetadata(24.0));

	public static readonly DependencyProperty IconHeightProperty =
		DependencyProperty.Register(nameof(IconHeight), typeof(double), typeof(ButtonIcon), new PropertyMetadata(24.0));

	public static readonly DependencyProperty TextButtonProperty =
		DependencyProperty.Register(nameof(TextButton), typeof(string), typeof(ButtonIcon));
	 
	public static readonly DependencyProperty IsRedProperty =
		DependencyProperty.Register(nameof(IsRed), typeof(bool), typeof(ButtonIcon));

	public static readonly DependencyProperty IsSelectedProperty =
		DependencyProperty.Register(nameof(IsSelected), typeof(bool), typeof(ButtonIcon));

	/// <summary>
	/// Иконка в кнопке.
	/// </summary>
	public ControlTemplate IconContent
	{
		get => (ControlTemplate)GetValue(IconContentProperty);
		set => SetValue(IconContentProperty, value);
	}

	/// <summary>
	/// Красный стиль кнопки?
	/// </summary>
	public bool IsRed
	{
		get => (bool)GetValue(IsRedProperty);
		set => SetValue(IsRedProperty, value);
	}

	/// <summary>
	/// Selected стиль кнопки?
	/// </summary>
	public bool IsSelected
	{
		get => (bool)GetValue(IsSelectedProperty);
		set => SetValue(IsSelectedProperty, value);
	} 

	/// <summary>
	/// Скукожена ли кнопка?
	/// </summary>
	public bool IsShortSizeStyle
	{
		get => (bool)GetValue(IsShortSizeStyleProperty);
		set => SetValue(IsShortSizeStyleProperty, value);
	}

	/// <summary>
	/// Отображаемый текст в кнопке
	/// </summary>
	public string TextButton
	{
		get => (string)GetValue(TextButtonProperty);
		set => SetValue(TextButtonProperty, value);
	}

	/// <summary>
	/// Ширина иконки.
	/// </summary>
	public double IconWidth
	{
		get => (double)GetValue(IconWidthProperty);
		set => SetValue(IconWidthProperty, value);
	}

	/// <summary>
	/// Длина иконки.
	/// </summary>
	public double IconHeight
	{
		get => (double)GetValue(IconHeightProperty);
		set => SetValue(IconHeightProperty, value);
	}

	public ButtonIcon()
	{
		this.WhenAnyValue(x => x.IsShortSizeStyle)
			.Do(x => ChangeShortSizeStyle(x))
			.Subscribe(); 
	}

	private void ChangeShortSizeStyle(bool IsShortSizeStyle)
	{
		if(IsShortSizeStyle)
		{
			this.Style = (Style)Application.Current.Resources["IsShortSizeStyle"];
		}
		else
		{
			this.Style = (Style)FindResource(typeof(ButtonIcon));
		} 
	}
}
