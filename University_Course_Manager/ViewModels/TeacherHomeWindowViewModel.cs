using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using University_Course_Manager.Models;
using University_Course_Manager.Views;

namespace University_Course_Manager.ViewModels
{
    public partial class TeacherHomeWindowViewModel : ViewModelBase
    {
        [ObservableProperty] private UserControl _currentViewTeacher;

        private readonly Dictionary<string, UserControl> _viewsTeacher;

        private EnrolledSubjectsViewModel _enrolledVM;

        int teacherId = 1;
        public TeacherHomeWindowViewModel()
        {
            _viewsTeacher = new Dictionary<string, UserControl>
            {
                {"Taught", new TaughtSubjects {DataContext = new TaughtSubjectsViewModel(ShowCourseDetail)}},
                {"Create", new CreateSubject {DataContext = new CreateSubjectViewModel(teacherId, onSubjectCreated)}}
            };

            CurrentViewTeacher = _viewsTeacher["Taught"];
        }

        [RelayCommand]
        private void ShowTaught() => CurrentViewTeacher = _viewsTeacher["Taught"];

        [RelayCommand]
        private void ShowCreate() => CurrentViewTeacher = _viewsTeacher["Create"];

        private void ShowCourseDetail(Subject selectedSubject)
        {
           
            CurrentViewTeacher = new SubjectDetail
            {
                DataContext = new SubjectDetailViewModel(selectedSubject, () =>
                {
                    ShowTaught();
                })
            };
        }

        private void onSubjectCreated(Subject subject)
        {
            Console.WriteLine("Save to Database");

            CurrentViewTeacher = _viewsTeacher["Taught"];
        }

    }
}
