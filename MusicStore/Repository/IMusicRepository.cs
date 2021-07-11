using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using MusicStore.Models;

namespace MusicStore.Repository
{
    public interface IMusicRepository
    {
        void AddUser(User user);
        void DeleteGenre(Genre genre);
        void DeleteUser(User user);
        void DeleteClientBoughtGood(ClientBoughtGood clientBoughtGood);
        void DeleteRecord(Record record);
        void DeleteCollective(Collective collective);
        void DeletePublisher(Publisher publisher);

        int GetGenreByName(string name);
        int GetCollectivByName(string name);
        int GetPublisherByName(string name);
        List<User> GetUsers();
        List<Record> GetRecords();
        List<Record> GetRecordsSearchByNameGenreCollectiv(string name = "", int select = -1);
        List<Collective> GetCollectives();
        List<Genre> GetGenres();
        List<Publisher> GetPublishers();
        void UpdateUser(User user);
        void UpdateRecord(Record record);


        User GetByLoginAndPassword(string password, string login);
        void AddGenre(Genre genre);
        void AddRecord(Record record);
        void AddPublisher(Publisher publisher);
        void AddCollective(Collective collective);
        void BuyRecord(User user, Record record);
        List<ClientBoughtGood> GetClientBoughtGoods(User user);
    }
}