using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Attendance.ViewModels;

public partial class GroupViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _name;
    
    [ObservableProperty]
    private string _description;
    
    [ObservableProperty]
    private ObservableCollection<PersonViewModel> _people;
    
    public GroupViewModel(string name, string description,ObservableCollection<PersonViewModel> people)
    {
        _name = name;
        _description = description;
        _people = people;
    }
}