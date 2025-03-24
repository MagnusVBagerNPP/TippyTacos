using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University_Course_Manager.Models;

namespace University_Course_Manager.ViewModels
{
	public partial class EnrolledSubjectsViewModel : ViewModelBase
    {
        public ObservableCollection<Subject> EnrolledSubjectsList { get; }

        private readonly Action<Subject> _onSubjectSelected;

        public EnrolledSubjectsViewModel(Action<Subject> onSubjectSelected)
        {
            _onSubjectSelected = onSubjectSelected;

            EnrolledSubjectsList = new ObservableCollection<Subject>
            {
                new Subject("Thermodynamics", "About Energy...", "#D61C22", "1"),
                new Subject("Robotics", "Ai","#D61C22", "1"),
                new Subject("Poetry", "yappa yappa...", "#D61C22", "1")
            };
        }


        [RelayCommand]
        private void SelectSubject(Subject subject) => _onSubjectSelected?.Invoke(subject);

    }
}