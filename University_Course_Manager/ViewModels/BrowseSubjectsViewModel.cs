using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University_Course_Manager.Models;

namespace University_Course_Manager.ViewModels
{
	public partial class BrowseSubjectsViewModel : ViewModelBase
    {
        public ObservableCollection<Subject> BrowseSubjectsList { get; }

        private readonly Action<Subject> _onSubjectSelected;

        public BrowseSubjectsViewModel(Action<Subject> onSubjectSelected)
        {
            _onSubjectSelected = onSubjectSelected;

            BrowseSubjectsList = new ObservableCollection<Subject>
            {
                new Subject("Biology", "About Energy...", "#D61C22", "1"),
                new Subject("Mathematics", "Ai","#D61C22", "2"),
                new Subject("Chemistry", "yappa yappa...", "#D61C22", "1")
            };
        }
        

        [RelayCommand]
        private void SelectSubject(Subject subject) => _onSubjectSelected?.Invoke(subject);

    }
}