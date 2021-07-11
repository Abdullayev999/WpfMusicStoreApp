﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using MusicStore.Messages;

namespace MusicStore.ViewModels
{
    class MainViewModel : ViewModelBase
    {
        public MainViewModel(IMessenger messenger)
        {
            messenger.Register<NavigationMessage>(this, message =>
            {
                var viewModel = App.Services.GetInstance(message.ViewModelType) as ViewModelBase;
                CurrentViewModel = viewModel;
            });
        }
        private ViewModelBase currentViewModel;

        public ViewModelBase CurrentViewModel { get => currentViewModel; set => Set(ref currentViewModel, value); }

    }
}
