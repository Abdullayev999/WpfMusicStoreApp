using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MusicStore.Messages;
using MusicStore.Repository;

namespace MusicStore.ViewModels
{
    public class SignInViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly IMusicRepository _musicRepository;
        public string Login { get; set; }
        public string Password { get; set; }

        public SignInViewModel(IMessenger messenger,IMusicRepository musicRepository)
        {
            _messenger = messenger;
            _musicRepository = musicRepository;
        }
        private RelayCommand signInCommand = null;

        public RelayCommand SignInCommand => signInCommand ??= new RelayCommand(() =>
       {
           if (!string.IsNullOrWhiteSpace(Password) && !string.IsNullOrWhiteSpace(Login))
           {
              var user =  _musicRepository.GetByLoginAndPassword(Password, Login);
              if (user!= null)
              {
                  _messenger.Send(new NavigationMessage() { ViewModelType = typeof(StoreViewModel) });
                  _messenger.Send(new UserMessagecs() { User = user});
                   App.Services.GetInstance<StoreViewModel>().Refresh();

              }
              else
              {
                  MessageBox.Show("Incorrect data");
              }
           }

       });
        private RelayCommand signUpCommand = null;

        public RelayCommand SignUpCommand => signUpCommand ??= new RelayCommand(() =>
        {
            _messenger.Send(new NavigationMessage(){ViewModelType = typeof(SignUpViewModel)});
        });
    }
}
