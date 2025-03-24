using System;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace University_Course_Manager.ViewModels
{
    public partial class LoginWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string? _userName;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
        private string? _password;


        private readonly Action<string> _onLoginSuccess;

        public LoginWindowViewModel(Action<string> onLoginSuccess)
        {
            _onLoginSuccess = onLoginSuccess;
        }

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task Login()
        {
            if (UserName == "teacher" && Password == "password") // Simulated auth
            {
                _onLoginSuccess?.Invoke("Teacher");
            } else if (UserName == "student" && Password == "password")
            {
                _onLoginSuccess?.Invoke("Student");
            }
        }

        private bool CanLogin => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
    }
}
