using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using ReactiveUI;
using System.Reactive.Linq;
using System;

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

	public IReactiveCommand AddCommand { get; private set; }
	public IReactiveCommand RemoveCommand { get; private set; }

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
			_fullAddButtonIcon.IsEnabled  = !_isMaxCount;
			_shortAddButtonIcon.IsEnabled = !_isMaxCount;

			_сountTextBlock.Text = $"{Count}";
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
		 
		return control;
	}

	#endregion
}
