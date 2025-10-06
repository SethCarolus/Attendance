using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace Attendance.Controls;

public partial class SessionItem : UserControl
{
    public static readonly StyledProperty<string> SessionNameProperty =
        AvaloniaProperty.Register<SessionItem, string>(nameof(SessionName), defaultValue: "");

    public static readonly StyledProperty<string> DescriptionProperty =
        AvaloniaProperty.Register<SessionItem, string>(nameof(Description), defaultValue: "");

    public string SessionName
    {
        get => GetValue(SessionNameProperty);
        set => SetValue(SessionNameProperty, value);
    }

    public string Description
    {
        get => GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }
    public SessionItem()
    {
        InitializeComponent();
    }
}