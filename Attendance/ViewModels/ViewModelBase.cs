using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Attendance.ViewModels;

public class ViewModelBase : ObservableObject
{
    public IDictionary<string, object>? Parameters;
}