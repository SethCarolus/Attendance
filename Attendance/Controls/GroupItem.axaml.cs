using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Attendance.Controls;

public partial class GroupItem : UserControl
{
    public static readonly StyledProperty<string> GroupNameProperty =
        AvaloniaProperty.Register<GroupItem, string>(nameof(GroupName), defaultValue: "");

    public static readonly StyledProperty<string> DescriptionProperty =
        AvaloniaProperty.Register<GroupItem, string>(nameof(Description), defaultValue: "");

    public string GroupName
    {
        get => GetValue(GroupNameProperty);
        set => SetValue(GroupNameProperty, value);
    }

    public string Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }
    
    public GroupItem()
    {
        InitializeComponent();
    }
}