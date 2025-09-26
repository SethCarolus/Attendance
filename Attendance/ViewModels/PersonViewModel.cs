using CommunityToolkit.Mvvm.ComponentModel;

namespace Attendance.ViewModels;

public partial class PersonViewModel : ViewModelBase
{

    [ObservableProperty]
    private int _id;
    
    [ObservableProperty]
    private string _firstName;
    
    [ObservableProperty]
    private string _lastName;
    
    public PersonViewModel(int id, string firstName, string lastName)
    {
        _id = id;
        _firstName = firstName;
        _lastName = lastName;
    }
}