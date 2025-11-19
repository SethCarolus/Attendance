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
    private readonly INavigationService _navigation;
    private readonly IDatabaseService _database;
    private readonly IPersonViewModelFactory _personViewModelFactory;
    
    [ObservableProperty]
    private ObservableCollection<PersonViewModel> _people;
    
    [ObservableProperty]
    private string? _firstName;
    
    [ObservableProperty]
    private string? _lastName;

    public PeopleViewModel(IAppContext appContext, IPersonViewModelFactory personViewModelFactory)
    {
        _navigation = appContext.NavigationService;
        _database = appContext.DatabaseService;
        _personViewModelFactory = personViewModelFactory;
        Refresh();
    }

    public PeopleViewModel()
    {
    }

    [RelayCommand]
    private void Back()
    {
        _navigation.NavigateTo<GroupsViewModel>();
    }

    [RelayCommand]
    private void Add()
    {
        if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName))
        {
            return;
        }
        
        var person = new PersonModel(FirstName, LastName);
        _database.AddPerson(person);

        FirstName = "";
        LastName = "";
        Refresh();
    }

    private void Refresh()
    {
        People = new();
        foreach (var person in _database.GetPeople())
        {
            People.Add(_personViewModelFactory.Create(person, null, [PersonState.Remove, PersonState.Edit]));
        }       
    }
}