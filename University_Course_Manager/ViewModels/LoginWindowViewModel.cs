using System;
using System.Threading.Tasks;

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


        private readonly Action _onLoginSuccess;

        public LoginWindowViewModel(Action onLoginSuccess)
        {
            _onLoginSuccess = onLoginSuccess;
        }

        [RelayCommand(CanExecute = nameof(CanLogin))]
        private async Task Login()
        {
            if (UserName == "admin" && Password == "password") // Simulated auth
            {
                _onLoginSuccess?.Invoke();
            }
        }

        private bool CanLogin => !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password);
    }
}
