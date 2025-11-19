using System.Collections.Generic;
using System.Collections.ObjectModel;
using Attendance.Enums;
using Attendance.Factories.Contracts;
using Attendance.Models;
using Attendance.Services.Contracts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Attendance.ViewModels;

public partial class PeopleViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IDatabaseService _databaseService;
    private readonly IPersonViewModelFactory _personViewModelFactory;
    
    [ObservableProperty]
    private ObservableCollection<PersonViewModel> _people;
    
    [ObservableProperty]
    private string? _firstName;
    
    [ObservableProperty]
    private string? _lastName;

    public PeopleViewModel(IAppContext appContext, IPersonViewModelFactory personViewModelFactory)
    {
        _navigationService = appContext.NavigationService;
        _databaseService = appContext.DatabaseService;
        _personViewModelFactory = personViewModelFactory;
        Refresh();
    }

    public PeopleViewModel()
    {
    }

    [RelayCommand]
    private void Back()
    {
        _navigationService.NavigateTo<GroupsViewModel>();
    }

    [RelayCommand]
    private void Add()
    {
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
        {
            return;
        }
        
        var person = new PersonModel(FirstName, LastName);
        _databaseService.AddPerson(person);

        FirstName = "";
        LastName = "";
        Refresh();
    }

    private void Refresh()
    {
        People = new();
        foreach (var person in _databaseService.GetPeople())
        {
            People.Add(_personViewModelFactory.Create(person, null, [PersonState.Remove, PersonState.Edit]));
        }       
    }
}