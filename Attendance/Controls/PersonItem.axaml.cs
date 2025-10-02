using Attendance.Models;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Attendance.Controls;

public partial class PersonItem : UserControl
{
    public static readonly StyledProperty<int> IdProperty =
        AvaloniaProperty.Register<PersonItem, int>(nameof(Id), defaultValue: -1);
    public static readonly StyledProperty<string> FirstNameProperty =
        AvaloniaProperty.Register<PersonItem, string>(nameof(FirstName), defaultValue: "");

    public static readonly StyledProperty<string> LastNameProperty =
        AvaloniaProperty.Register<PersonItem, string>(nameof(LastName), defaultValue: "");
    public static readonly StyledProperty<GroupModel> GroupProperty =
        AvaloniaProperty.Register<PersonItem, GroupModel>(nameof(Group));

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

    public GroupModel Group
    {
        get => GetValue(GroupProperty);
        set => SetValue(GroupProperty, value);
    }
    public PersonItem()
    {
        InitializeComponent();
    }
}