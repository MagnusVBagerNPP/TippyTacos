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
    public partial class StudentHomeWindowViewModel : ViewModelBase
    {
        [ObservableProperty] private UserControl _currentViewStudent;

        private readonly Dictionary<string, UserControl> _viewsStudent;

        private EnrolledSubjectsViewModel _enrolledVM;

        public StudentHomeWindowViewModel()
        {
            _viewsStudent = new Dictionary<string, UserControl>
            {
                {"Enrolled", new EnrolledSubjects {DataContext = new EnrolledSubjectsViewModel(ShowCourseDetail)}},
                {"Browse", new BrowseSubjects {DataContext = new BrowseSubjectsViewModel(ShowCourseDetail)}}
            };

            CurrentViewStudent = _viewsStudent["Enrolled"];
        }

        [RelayCommand]
        private void ShowEnrolled() => CurrentViewStudent = _viewsStudent["Enrolled"];

        [RelayCommand]
        private void ShowBrowse() => CurrentViewStudent = _viewsStudent["Browse"];

        private void ShowCourseDetail(Subject selectedSubject)
        {
           
            CurrentViewStudent = new SubjectDetail
            {
                DataContext = new SubjectDetailViewModel(selectedSubject, () =>
                {
                    ShowBrowse();
                })
            };
        }


    }
}
