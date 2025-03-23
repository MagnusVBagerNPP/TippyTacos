using System;
using System.Collections.Generic;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University_Course_Manager.Models;

namespace University_Course_Manager.ViewModels
{
	public partial class SubjectDetailViewModel(Subject subject, Action onBack) : ViewModelBase
    {
        [ObservableProperty] private Subject _subject = subject;

        private readonly Action _onBack = onBack;

        [RelayCommand]
        private void Back() => _onBack?.Invoke();
    }
}