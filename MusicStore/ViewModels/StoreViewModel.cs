using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MusicStore.Messages;
using MusicStore.Models;
using MusicStore.Repository;

namespace MusicStore.ViewModels
{
    public class StoreViewModel : ViewModelBase
    {
        private readonly IMessenger _messenger;
        private readonly IMusicRepository _musicRepository;
        public User User { get; set; }

        public StoreViewModel(IMessenger messenger, IMusicRepository musicRepository)
        {
            _messenger = messenger;
            _musicRepository = musicRepository;
            messenger.Register<UserMessagecs>(this, (messenger) => { User = messenger.User; });
            Serching = new ObservableCollection<string>() { "Record", "Genre", "Collective" };
            try
            {
                Refresh(); 
            }
            catch (Exception){  }
            Collapsers();

        }
        public int SearchIndex { get; set; }
        public void Refresh()
        {
            Genres = new ObservableCollection<Genre>(_musicRepository.GetGenres());
            Collectives = new ObservableCollection<Collective>(_musicRepository.GetCollectives());
            Publishers = new ObservableCollection<Publisher>(_musicRepository.GetPublishers());
            Records = new ObservableCollection<Record>(_musicRepository.GetRecords());
            Users = new ObservableCollection<User>(_musicRepository.GetUsers());
            BuyRecords = new ObservableCollection<ClientBoughtGood>(_musicRepository.GetClientBoughtGoods(User));
            SearchRecords = new ObservableCollection<Record>(_musicRepository.GetRecordsSearchByNameGenreCollectiv());
        }


        public Visibility RecordVisibility { get; set; }
        public Visibility UserVisibility { get; set; }
        public Visibility CollectiveVisibility { get; set; }
        public Visibility GenreVisibility { get; set; }
        public Visibility PublisherVisibility { get; set; }
        public Visibility MyRoomVisibility { get; set; }
        public Visibility SearchVisibility { get; set; }
        public Visibility StatisticVisibility { get; set; }


        void Collapsers()
        {
            RecordVisibility = Visibility.Hidden;
            UserVisibility = Visibility.Hidden;
            CollectiveVisibility = Visibility.Hidden;
            GenreVisibility = Visibility.Hidden;
            PublisherVisibility = Visibility.Hidden;
            MyRoomVisibility = Visibility.Hidden;
            SearchVisibility = Visibility.Hidden;
            StatisticVisibility = Visibility.Hidden;
        }

        private RelayCommand recordCommand = null;
        public RelayCommand RecordCommand => recordCommand ??= new RelayCommand(() =>
        {
            Collapsers();
            RecordVisibility = Visibility.Visible;
        });

        private RelayCommand statisticCommand = null;
        public RelayCommand StatisticCommand => statisticCommand ??= new RelayCommand(() =>
        {
            Collapsers();
            StatisticVisibility = Visibility.Visible;
        });

        private RelayCommand userCommand = null;
        public RelayCommand UserCommand => userCommand ??= new RelayCommand(() =>
        {
            Collapsers();
            UserVisibility = Visibility.Visible;
        });

        private RelayCommand collectiveCommand = null;
        public RelayCommand CollectiveCommand => collectiveCommand ??= new RelayCommand(() =>
        {
            Collapsers();
            CollectiveVisibility = Visibility.Visible;
        });

        private RelayCommand genreCommand = null;
        public RelayCommand GenreCommand => genreCommand ??= new RelayCommand(() =>
        {
            Collapsers();
            GenreVisibility = Visibility.Visible;
        });

        private RelayCommand publisherCommand = null;
        public RelayCommand PublisherCommand => publisherCommand ??= new RelayCommand(() =>
        {
            Collapsers();
            PublisherVisibility = Visibility.Visible;
        });


        private RelayCommand myRoomCommand = null;
        public RelayCommand MyRoomCommand => myRoomCommand ??= new RelayCommand(() =>
        {
            Collapsers();
            MyRoomVisibility = Visibility.Visible;
        });

        private RelayCommand searchCommand = null;
        public RelayCommand SearchCommand => searchCommand ??= new RelayCommand(() =>
        {
            Collapsers();
            SearchVisibility = Visibility.Visible;
        });

        public ObservableCollection<Genre> Genres { get; set; }
        public string GenreName { get; set; }

        private RelayCommand saveGenreCommand = null;

        public RelayCommand SaveGenreCommand => saveGenreCommand ??= new RelayCommand(() =>
        {
            if (!string.IsNullOrWhiteSpace(GenreName))
            {
                try
                {
                    _musicRepository.AddGenre(new Genre() { Name = GenreName });
                    GenreName = string.Empty;
                    Refresh();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                MessageBox.Show("Incorrect data");
            }
        });


        private RelayCommand<Genre> deleteGenreCommand = null;

        public RelayCommand<Genre> DeleteGenreCommand => deleteGenreCommand ??= new RelayCommand<Genre>((genre) =>
        {

            try
            {
                _musicRepository.DeleteGenre(genre);
                Refresh();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        });


        /////////////////////////////////////


        public ObservableCollection<Collective> Collectives { get; set; }
        public string CollectiveName { get; set; }

        private RelayCommand saveCollectiveCommand = null;

        public RelayCommand SaveCollectiveCommand => saveCollectiveCommand ??= new RelayCommand(() =>
        {
            if (!string.IsNullOrWhiteSpace(CollectiveName))
            {
                try
                {
                    _musicRepository.AddCollective(new Collective() { Name = CollectiveName });
                    CollectiveName = string.Empty;
                    Refresh();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                MessageBox.Show("Incorrect data");
            }
        });


        private RelayCommand<Collective> deleteCollectiveCommand = null;

        public RelayCommand<Collective> DeleteCollectiveCommand => deleteCollectiveCommand ??= new RelayCommand<Collective>((collective) =>
        {
            try
            {
                _musicRepository.DeleteCollective(collective);
                Refresh();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        });

        ///////////////////////////////////////////////////

        public string PublisherName { get; set; }

        public ObservableCollection<Publisher> Publishers { get; set; }

        private RelayCommand savePublisherCommand = null;

        public RelayCommand SavePublisherCommand => savePublisherCommand ??= new RelayCommand(() =>
        {
            if (!string.IsNullOrWhiteSpace(PublisherName))
            {
                try
                {
                    _musicRepository.AddPublisher(new Publisher() { Name = PublisherName });
                    PublisherName = string.Empty;
                    Refresh();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                MessageBox.Show("Incorrect data");
            }
        });


        private RelayCommand<Publisher> deletePublisherCommand = null;

        public RelayCommand<Publisher> DeletePublisherCommand => deletePublisherCommand ??= new RelayCommand<Publisher>((publisher) =>
        {
            try
            {
                _musicRepository.DeletePublisher(publisher);
                Refresh();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        });

        /////////////////////////////////////////// 

        public string RecordName { get; set; }
        public int Quantity { get; set; }
        public int Year { get; set; }
        public int CostPrice { get; set; }
        public int PriceSale { get; set; }
        public string RecordCollectiv { get; set; }
        public string RecordPublisher { get; set; }
        public string RecordGenre { get; set; }

        public ObservableCollection<Record> Records { get; set; }

        private RelayCommand saveRecordCommand = null;

        public RelayCommand SaveRecordCommand => saveRecordCommand ??= new RelayCommand(() =>
        {
            if (!string.IsNullOrWhiteSpace(RecordName) && !string.IsNullOrWhiteSpace(RecordGenre) &&
                        !string.IsNullOrWhiteSpace(RecordPublisher) && !string.IsNullOrWhiteSpace(RecordCollectiv) &&
                        Quantity != -1 && CostPrice != -1 && PriceSale != -1)
            {
                try
                {
                    _musicRepository.AddRecord(new Record()
                    {
                        Name = RecordName,
                        YearPublishing = Year,
                        MusicQuantity = Quantity,
                        CostPrice = CostPrice,
                        PriceSale = PriceSale,
                        GenreId = _musicRepository.GetGenreByName(RecordGenre),
                        PublisherId = _musicRepository.GetPublisherByName(RecordPublisher),
                        CollectiveId = _musicRepository.GetCollectivByName(RecordCollectiv)
                    });

                    RecordName = string.Empty;
                    Quantity = 0;
                    CostPrice = -0;
                    PriceSale = -0;
                    RecordGenre = string.Empty;
                    RecordPublisher = string.Empty;
                    RecordCollectiv = string.Empty;
                    Refresh();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                MessageBox.Show("Incorrect data");
            }
        });


        private RelayCommand<Record> deleteRecordCommand = null;

        public RelayCommand<Record> DeleteRecordCommand => deleteRecordCommand ??= new RelayCommand<Record>((record) =>
        {
            try
            {
                _musicRepository.DeleteRecord(record);
                Refresh();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        });


        /////////////////////////////////////////// 

        public string FullName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }  

        public ObservableCollection<User> Users { get; set; }

        private RelayCommand saveUserCommand = null;

        public RelayCommand SaveUserCommand => saveUserCommand ??= new RelayCommand(() =>
        {
            if (!string.IsNullOrWhiteSpace(FullName) && !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password)
                 && !string.IsNullOrWhiteSpace(RepeatPassword) && Password.Equals(RepeatPassword))
            {
                try
                {
                    _musicRepository.AddUser(new User()
                    {
                        FullName = FullName,
                        Login = Login,
                        Password = Password, 
                    });

                    FullName = string.Empty; 
                    Login = string.Empty;
                    Password = string.Empty;
                    RepeatPassword = string.Empty;
                    Refresh();
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
            {
                MessageBox.Show("Incorrect data");
            }
        });


        private RelayCommand<User> deleteUserCommand = null;

        public RelayCommand<User> DeleteUserCommand => deleteUserCommand ??= new RelayCommand<User>((user) =>
        {
            try
            { 
                _musicRepository.DeleteUser(user);
                Refresh();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        });


        private RelayCommand<Record> buyBookCommand = null;

        public RelayCommand<Record> BuyBookCommand => buyBookCommand ??= new RelayCommand<Record>(
        (record) =>
        {
            if (record.MusicQuantity>0)
            {
                if (record.PriceSale<=User.Money)
                { 
                    record.MusicQuantity--;
                    User.Money -= record.PriceSale;

                    _musicRepository.UpdateUser(User);
                    _musicRepository.UpdateRecord(record);



                    Refresh(); 
                }
                else
                {
                    MessageBox.Show("Not enough money");
                }
            }
            else
            {
                MessageBox.Show("Out of stock");
            }
            _musicRepository.BuyRecord(User, record);
        });

        /////////////////////////////////////////////////////////
        ///

        public ObservableCollection<ClientBoughtGood> BuyRecords { get; set; }

        private RelayCommand<ClientBoughtGood> deleteMyRoomCommand = null;

        public RelayCommand<ClientBoughtGood> DeleteMyRoomCommand => deleteMyRoomCommand ??= new RelayCommand<ClientBoughtGood>(
        (clientBoughtGood) =>
        {
            _musicRepository.DeleteClientBoughtGood(clientBoughtGood);
            Refresh();
        });

        /// <summary>
        /// ///////////////////////////////////////////////////////
        /// 
        /// </summary>
        /// 

        public ObservableCollection<string> Serching { get; set; }
        public ObservableCollection<Record> SearchRecords { get; set; }
        public string SearchName { get; set; }

        private RelayCommand  searchRecordCommand = null; 
        public RelayCommand SearchRecordCommand => searchRecordCommand ??= new RelayCommand(
        () =>
        { 
            if (SearchIndex>=0&& SearchIndex<3 && !string.IsNullOrEmpty(SearchName))
            {
                 
                SearchRecords = new ObservableCollection<Record>(_musicRepository.GetRecordsSearchByNameGenreCollectiv(SearchName, SearchIndex));
            }
           
        });

    }
}