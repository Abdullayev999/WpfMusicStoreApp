using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using MusicStore.Models;

namespace MusicStore.Messages
{
  public  class UserMessagecs : Messenger
    {
        public User  User { get; set; }
    }
}
