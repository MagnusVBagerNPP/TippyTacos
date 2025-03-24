using System.Collections.Generic;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University_Course_Manager.Models;
using University_Course_Manager.Views;

namespace University_Course_Manager.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        [ObservableProperty] private UserControl _currentView;

        private readonly Dictionary<string, UserControl> _views;

        public MainWindowViewModel()
        {
            _views = new Dictionary<string, UserControl>
            {
                {"Login", new LoginWindow {DataContext = new LoginWindowViewModel(SwitchToRoleBasedView)}},
                {"Teacher", new TeacherHomeWindow {DataContext = new TeacherHomeWindowViewModel()}},
                {"Student", new StudentHomeWindow {DataContext = new StudentHomeWindowViewModel()}}
            };

            CurrentView = _views["Login"];
        }

        [RelayCommand]
        private void SwitchToRoleBasedView(string role)
        {
            if (_views.ContainsKey(role))
            {
                CurrentView = _views[role];
            }
        }
    }
}
