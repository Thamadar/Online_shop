using ReactiveUI;

using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows; 

namespace Shop.UI.WPF.Controls;


[TemplatePart(Name = "PART_FullAddButtonIcon",     Type = typeof(ButtonIcon))]
[TemplatePart(Name = "PART_ShortAddButtonIcon",    Type = typeof(ButtonIcon))]
[TemplatePart(Name = "PART_ShortRemoveButtonIcon", Type = typeof(ButtonIcon))]
[TemplatePart(Name = "PART_CountTextBlock",        Type = typeof(TextBlock))] 
public class AddRemoveButton : Control
{
	private ButtonIcon _fullAddButtonIcon;
	private ButtonIcon _shortAddButtonIcon;
	private ButtonIcon _shortRemoveButtonIcon;
	private TextBlock _countTextBlock;
	private bool _areControlsAvailable;

	private bool _isMaxCount;


	public static readonly DependencyProperty AddCommandProperty =
		DependencyProperty.Register(nameof(AddCommand), typeof(IReactiveCommand), typeof(AddRemoveButton));

	public static readonly DependencyProperty RemoveCommandProperty =
		DependencyProperty.Register(nameof(RemoveCommand), typeof(IReactiveCommand), typeof(AddRemoveButton));

	public static readonly DependencyProperty CountProperty =
		DependencyProperty.Register(nameof(Count), typeof(int), typeof(AddRemoveButton), new PropertyMetadata(0));

	public static readonly DependencyProperty MaxCountProperty =
		DependencyProperty.Register(nameof(MaxCount), typeof(int), typeof(AddRemoveButton), new PropertyMetadata(0));  

	public IReactiveCommand AddCommand
	{
		get => (IReactiveCommand)GetValue(AddCommandProperty);
		set => SetValue(AddCommandProperty, value);
	}

	public IReactiveCommand RemoveCommand
	{
		get => (IReactiveCommand)GetValue(RemoveCommandProperty);
		set => SetValue(RemoveCommandProperty, value);
	}

	public int Count
	{
		get => (int)GetValue(CountProperty);
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

	public int MaxCount
	{
		get => (int)GetValue(MaxCountProperty);
		set => SetValue(MaxCountProperty, value);
	} 

	static AddRemoveButton()
	{
		//DefaultStyleKeyProperty.OverrideMetadata(typeof(AddRemoveButton), new FrameworkPropertyMetadata(typeof(AddRemoveButton)));
	}

	public AddRemoveButton()
	{
		AddCommand    = ReactiveCommand.Create(() => { Count++; });
		RemoveCommand = ReactiveCommand.Create(() => { Count--; });

		this.WhenAnyValue(x => x.Count)
			.Subscribe(count => UpdateUI());
	}

	public override void OnApplyTemplate()
	{
		base.OnApplyTemplate();

		_fullAddButtonIcon     = InitAddButtonIcon(GetTemplateChild("PART_FullAddButtonIcon")        as ButtonIcon);
		_shortAddButtonIcon    = InitAddButtonIcon(GetTemplateChild("PART_ShortAddButtonIcon")       as ButtonIcon);
		_shortRemoveButtonIcon = InitRemoveButtonIcon(GetTemplateChild("PART_ShortRemoveButtonIcon") as ButtonIcon);
		_countTextBlock        = InitCountTextBlock(GetTemplateChild("PART_CountTextBlock")          as TextBlock);

		if(_fullAddButtonIcon != null)
			_fullAddButtonIcon.Command = (ICommand)AddCommand;

		if(_shortAddButtonIcon != null)
			_shortAddButtonIcon.Command = (ICommand)AddCommand;

		if(_shortRemoveButtonIcon != null)
			_shortRemoveButtonIcon.Command = (ICommand)RemoveCommand;

		if(_countTextBlock != null)
		{
			var binding = new Binding(nameof(Count))
			{
				Source = this
			};
			_countTextBlock.SetBinding(TextBlock.TextProperty, binding);
		}

		_areControlsAvailable = true;
		UpdateUI();
	}


	private void UpdateUI()
	{
		if(_areControlsAvailable)
		{
			IsMaxCount();

			if(_fullAddButtonIcon != null)
				_fullAddButtonIcon.IsEnabled = !_isMaxCount;

			if(_shortAddButtonIcon != null)
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

	#region Controls initializtion

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
			Source              = this,
			UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
		};
		BindingOperations.SetBinding(control, TextBlock.TextProperty, bind);

		return control;
	}

	#endregion 
}
