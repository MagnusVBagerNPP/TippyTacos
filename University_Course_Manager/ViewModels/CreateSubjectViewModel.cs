using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University_Course_Manager.Models;

namespace University_Course_Manager.ViewModels
{
	public partial class CreateSubjectViewModel : ViewModelBase
    {
        [ObservableProperty] [NotifyCanExecuteChangedFor(nameof(CreateSubjectCommand))] private string subjectName;
        [ObservableProperty] private string description;
        [ObservableProperty] private string selectedColor;

        public ObservableCollection<string> AvailableColors { get; }


        private readonly string _teacherId;
        private readonly Action<Subject> _onSubjectCreated;

        public CreateSubjectViewModel(string teacherId, Action<Subject> onSubjectCreated)
        {
            _onSubjectCreated = onSubjectCreated;
            _teacherId = teacherId;

            AvailableColors = new ObservableCollection<string>
            {
                "#f0f0f0",
                "#ff90ee",
                "#fefa55"
            };
            SelectedColor = AvailableColors[0];
        }

        [RelayCommand(CanExecute = nameof(CanCreate))]
        private void CreateSubject()
        {
            var newSubject = new Subject(
                name: SubjectName,
                teacherId: _teacherId,
                description: Description,
                color: SelectedColor
            );

            _onSubjectCreated?.Invoke(newSubject);

            // Optionally reset form
            subjectName = "";
            Description = "";
            SelectedColor = AvailableColors[0];
        }

        public bool CanCreate => !string.IsNullOrWhiteSpace(SubjectName);
    }
}
