using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using ReactiveUI;
using System.Reactive.Linq;
using System;
using Avalonia.Data;

namespace Shop.UI.Controls;

[TemplatePart("PART_FullAddButtonIcon",     typeof(ButtonIcon))]
[TemplatePart("PART_ShortAddButtonIcon",    typeof(ButtonIcon))]
[TemplatePart("PART_ShortRemoveButtonIcon", typeof(ButtonIcon))]
[TemplatePart("PART_CountTextBlock",        typeof(TextBlock))]

public class AddRemoveButton : TemplatedControl
{

	private ButtonIcon? _fullAddButtonIcon;
	private ButtonIcon? _shortAddButtonIcon;
	private ButtonIcon? _shortRemoveButtonIcon;
	private TextBlock? _сountTextBlock;
	private bool _areControlsAvailable;

	private bool _isMaxCount;

	public static readonly StyledProperty<int> CountProperty =
		AvaloniaProperty.Register<AddRemoveButton, int>(nameof(Count), defaultValue: 0);

	public static readonly StyledProperty<int> MaxCountProperty =
		AvaloniaProperty.Register<AddRemoveButton, int>(nameof(MaxCount), defaultValue: 0);

	public static readonly StyledProperty<IReactiveCommand> AddCommandProperty =
		AvaloniaProperty.Register<AddRemoveButton, IReactiveCommand>(nameof(AddCommand));

	public static readonly StyledProperty<IReactiveCommand> RemoveCommandProperty =
		AvaloniaProperty.Register<AddRemoveButton, IReactiveCommand>(nameof(RemoveCommand));

	public IReactiveCommand AddCommand
	{
		get => GetValue(AddCommandProperty);
		set => SetValue(AddCommandProperty, value);
	}

	public IReactiveCommand RemoveCommand
	{
		get => GetValue(RemoveCommandProperty);
		set => SetValue(RemoveCommandProperty, value);
	}

	/// <summary>
	/// Счетчик.
	/// </summary>
	public int Count
	{
		get => GetValue(CountProperty);
		set
		{
			_isMaxCount = false;

			if(value < 0)
			{
				return;
			}
			if(MaxCount == 0)
			{
				_isMaxCount = false;
				SetValue(CountProperty, value);
			}
			else
			{
				_isMaxCount = value >= MaxCount ? true : false;
				if(value > MaxCount)
				{
					SetValue(CountProperty, MaxCount);
				}
				else
				{
					SetValue(CountProperty, value);
				} 
			} 
		}
	}

	/// <summary>
	/// Лимит счетчика.
	/// </summary>
	public int MaxCount
	{
		get => GetValue(MaxCountProperty);
		set => SetValue(MaxCountProperty, value);
	} 

	public AddRemoveButton()
	{
		AddCommand    = ReactiveCommand.Create(() => { Count++; });
		RemoveCommand = ReactiveCommand.Create(() => { Count--; });

		this.WhenAnyValue(x => x.Count)
			.Subscribe(count => UpdateUI());
	}

	protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
	{
		_areControlsAvailable = false;

		base.OnApplyTemplate(e);

		_fullAddButtonIcon     = InitAddButtonIcon(e.NameScope.Find<ButtonIcon>("PART_FullAddButtonIcon"));
		_shortAddButtonIcon    = InitAddButtonIcon(e.NameScope.Find<ButtonIcon>("PART_ShortAddButtonIcon"));
		_shortRemoveButtonIcon = InitRemoveButtonIcon(e.NameScope.Find<ButtonIcon>("PART_ShortRemoveButtonIcon"));
		_сountTextBlock        = InitCountTextBlock(e.NameScope.Find<TextBlock>("PART_CountTextBlock"));

		_areControlsAvailable = true;
	}

	private void UpdateUI()
	{
		if(_areControlsAvailable)
		{
			IsMaxCount();

			_fullAddButtonIcon.IsEnabled  = !_isMaxCount;
			_shortAddButtonIcon.IsEnabled = !_isMaxCount; 
		}
	}

	private void IsMaxCount()
	{
		_isMaxCount = false;

		if(Count < 0)
		{
			return;
		}
		if(MaxCount == 0)
		{
			_isMaxCount = false; 
		}
		else
		{
			_isMaxCount = Count >= MaxCount ? true : false; 
		}
	}

	#region Init 
	 
	private ButtonIcon InitAddButtonIcon(ButtonIcon? control)
	{
		if(control == null)
		{
			return control;
		}

		control.Command = (System.Windows.Input.ICommand?)AddCommand;

		return control;
	}

	private ButtonIcon InitRemoveButtonIcon(ButtonIcon? control)
	{
		if(control == null)
		{
			return control;
		}

		control.Command = (System.Windows.Input.ICommand?)RemoveCommand;

		return control;
	}

	private TextBlock InitCountTextBlock(TextBlock? control)
	{
		var bind = new Binding(nameof(Count))
		{
			Source = this
		};
		control.Bind(TextBlock.TextProperty, bind);

		return control; 
	}

	#endregion
}
