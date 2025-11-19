using Attendance.Models;
using Attendance.Services.Contracts;
using Attendance.ViewModels.Contracts;
using Attendance.ViewModels.Parameters;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Attendance.ViewModels;

public partial class EditPersonViewModel: ViewModelBase, IParameterReceiver<EditPersonParameters>
{
    private readonly INavigationService _navigation;
    private readonly IDatabaseService _database;
    
    [ObservableProperty]
    private PersonViewModel _person;

    public EditPersonViewModel(INavigationService navigationService, IDatabaseService databaseService)
    {
        _navigation = navigationService;
        _database =  databaseService;
    }

    public EditPersonViewModel()
    {
    }
    
    public void ReceiveParameter(EditPersonParameters parameters)
    {
        Person = parameters.Person;
    }
    
    [RelayCommand]
    private void Back()
    {
        _navigation.NavigateTo<PeopleViewModel>();
    }

    [RelayCommand]
    private void Edit()
    {
        if (string.IsNullOrWhiteSpace(Person.FirstName) || string.IsNullOrWhiteSpace(Person.LastName))
            return;

        var person = new PersonModel(Person.Id, Person.FirstName, Person.LastName);
        _database.EditPerson(person);
        Back();
    }

}