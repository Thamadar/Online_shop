using Avalonia;
using Avalonia.Data.Converters;
using Avalonia.Metadata;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq; 

namespace Shop.UI.Avalonia.Converters;

public class SwitchConverter : AvaloniaObject, ISwitchConverter, ICompositeConverter
{
	public static readonly StyledProperty<object> DefaultProperty = AvaloniaProperty.Register<SwitchConverter, object>(nameof(Default));
	public object Default
	{
		get { return GetValue(DefaultProperty); }
		set { SetValue(DefaultProperty, value); }
	}

	public IValueConverter? PreConverter { get; set; }
	public IValueConverter? PostConverter { get; set; }

	public object? PostConverterParameter { get; set; }
	public object? PreConverterParameter { get; set; }

	[Content]
	public CaseSet Cases { get; set; } = new();
	public bool TypeMode { get; set; }

	public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if(TypeMode)
		{
			value = value?.GetType().Name;
		}
		else
		{
			value = PreConverter == null ? value : PreConverter.Convert(value, targetType, PreConverterParameter, culture);
		}

		var pair = Cases.FirstOrDefault(x => Equals(TypeMode ? (x.KeyType?.Name ?? "") : x.Key, value) || SafeCompareAsStrings(TypeMode ? (x.KeyType?.Name ?? "") : x.Key, value));
		value = pair == null ? Default : pair.Value;
		return PostConverter == null ? value : PostConverter.Convert(value, targetType, PostConverterParameter, culture);
	}

	public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
	{
		if(TypeMode)
		{
			value = value?.GetType().Name;
		}
		else
		{
			value = PreConverter == null ? value : PreConverter.ConvertBack(value, targetType, PreConverterParameter, culture);
		}
		var pair = Cases.FirstOrDefault(x => Equals(x.Value, value) || SafeCompareAsStrings(x.Value, value));
		value = pair == null ? Default : pair.Key;
		return PostConverter == null ? value : PostConverter.ConvertBack(value, targetType, PostConverterParameter, culture);
	}

	private static bool SafeCompareAsStrings(object? a, object? b)
	{
		if(a == null && b == null) return true;
		if(a == null || b == null) return false;
		return string.Compare(a.ToString(), b.ToString(), StringComparison.OrdinalIgnoreCase) == 0;
	} 
}

public class CaseSet : List<ICase>
{
	public static readonly object UndefinedObject = new();
}

public interface ICase
{
	object? Key { get; set; }
	object? Value { get; set; }
	Type? KeyType { get; set; }
}

public class Case : ICase
{
	public object? Key { get; set; }
	public Type? KeyType { get; set; }
	public object? Value { get; set; }
}

public interface ICompositeConverter : IValueConverter
{
	IValueConverter PostConverter { get; set; }
	object PostConverterParameter { get; set; }
}

public interface ISwitchConverter : IValueConverter
{
	CaseSet Cases { get; }
	object Default { get; set; } 
}
