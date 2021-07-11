using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MusicStore.Messages;
using MusicStore.Models;
using MusicStore.Repository;

namespace MusicStore.ViewModels
{
    public class SignUpViewModel :ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly IMusicRepository _musicRepository;
        public string FullName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public SignUpViewModel(IMessenger messenger,IMusicRepository musicRepository)
        {
            _messenger = messenger;
            _musicRepository = musicRepository;
        }
        private RelayCommand backCommand = null;

        public RelayCommand BackCommand => backCommand ??= new RelayCommand(() =>
        {
            _messenger.Send(new NavigationMessage(){ViewModelType = typeof(SignInViewModel)});
        });

        private RelayCommand registerCommand = null;

        public RelayCommand RegisterCommand => registerCommand ??= new RelayCommand(() =>
        {
            
            if (!string.IsNullOrWhiteSpace(RepeatPassword) && !string.IsNullOrWhiteSpace(Password) && 
                Password.Equals(RepeatPassword) && !string.IsNullOrWhiteSpace(Login)
                && !string.IsNullOrWhiteSpace(FullName)) {
                User user = new User{FullName = FullName,Login = Login,Password = Password};
                _musicRepository.AddUser(user);
                _messenger.Send(new NavigationMessage() { ViewModelType = typeof(SignInViewModel) }); 
            } 
        });
    }
}