using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Attendance.Controls;

public partial class AttendanceItem : UserControl
{
    public AttendanceItem()
    {
        InitializeComponent();
    }
    
    public static readonly StyledProperty<int> IdProperty =
        AvaloniaProperty.Register<AttendanceItem, int>(nameof(Id), defaultValue: -1);
    public static readonly StyledProperty<string> FirstNameProperty =
        AvaloniaProperty.Register<AttendanceItem, string>(nameof(FirstName), defaultValue: "");

    public static readonly StyledProperty<string> LastNameProperty =
        AvaloniaProperty.Register<AttendanceItem, string>(nameof(LastName), defaultValue: "");

    public int Id
    {
        get => GetValue(IdProperty);
        set => SetValue(IdProperty, value);
    }
    
    public string FirstName
    {
        get => GetValue(FirstNameProperty);
        set => SetValue(FirstNameProperty, value);
    }

    public string LastName
    {
        get => GetValue(LastNameProperty);
        set => SetValue(LastNameProperty, value);
    }
}