using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MusicStore.Models;

namespace MusicStore.Repository
{
    public class MusicRepository : IMusicRepository
    {
        public void AddUser(User user)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Users.Add(user);
                db.SaveChanges();
            }
        }

        public void DeletePublisher(Publisher publisher)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Publishers.Remove(publisher);
                db.SaveChanges();
            }
        }

        public List<User> GetUsers()
        {
            using (var db = new ApplicationDbContext())
            {
                return new List<User>(db.Users);
            }
        }

        public User GetByLoginAndPassword(string password, string login)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Users.Where(item => item.Password.Equals(password) && item.Login.Equals(login)).FirstOrDefault();
            }
        }

        public void AddGenre(Genre genre)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Genres.Add(genre);
                db.SaveChanges();
            }
        }

        public void AddPublisher(Publisher publisher)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Publishers.Add(publisher);
                db.SaveChanges();
            }
        }

        public void AddCollective(Collective collective)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Collectives.Add(collective);
                db.SaveChanges();
            }
        }

        public List<Genre> GetGenres()
        {
            using (var db = new ApplicationDbContext())
            {
                return new List<Genre>(db.Genres);
            }
        }

        public void DeleteGenre(Genre genre)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Genres.Remove(genre);
                db.SaveChanges();
            }
        }

        public void DeleteCollective(Collective collective)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Collectives.Remove(collective);
                db.SaveChanges();
            }
        }

        public List<Collective> GetCollectives()
        {
            using (var db = new ApplicationDbContext())
            {
                return new List<Collective>(db.Collectives);
            }
        }

        public List<Publisher> GetPublishers()
        {
            using (var db = new ApplicationDbContext())
            {
                return new List<Publisher>(db.Publishers);
            }
        }

        public void AddRecord(Record record)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Records.Add(record);
                db.SaveChanges();
            }
        }

        public List<Record> GetRecords()
        {
            using (var db = new ApplicationDbContext())
            { 
                return new List<Record>(db.Records.Include(i => i.Collective).Include(i => i.Genre).Include(i => i.Publisher));
            }
        }

        public void DeleteRecord(Record record)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Records.Remove(record);
                db.SaveChanges();
            }
        }

        public int GetGenreByName(string name)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Genres.Where(i => i.Name.Equals(name)).FirstOrDefault().Id; 
            }
        }

        public int GetCollectivByName(string name)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Collectives.Where(i => i.Name.Equals(name)).FirstOrDefault().Id;
            }
        }

        public int GetPublisherByName(string name)
        {
            using (var db = new ApplicationDbContext())
            {
                return db.Publishers.Where(i => i.Name.Equals(name)).FirstOrDefault().Id;
            }
        }

        public void DeleteUser(User user)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
        }

        public void BuyRecord(User user, Record record)
        {
            using (var db = new ApplicationDbContext())
            {
                db.ClientBoughtGoods.Add(new ClientBoughtGood() { UserId = user.Id,RecordId = record.Id});
                db.SaveChanges();
            }
        }

        public void UpdateUser(User user)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Users.Update(user);
                db.SaveChanges();
            }
        }

        public void UpdateRecord(Record record)
        {
            using (var db = new ApplicationDbContext())
            {
                db.Records.Update(record);
                db.SaveChanges();
            }
        }

        public List<ClientBoughtGood> GetClientBoughtGoods(User user)
        {
            if (user!=null)
            {
                using (var db = new ApplicationDbContext())
                {
                    return db.ClientBoughtGoods.Include(i => i.User).Include(i => i.Record).Where(i => i.User.Id == user.Id).ToList();
                }
            }
            throw new NullReferenceException("Not user");
        }

        public void DeleteClientBoughtGood(ClientBoughtGood clientBoughtGood)
        {
            using (var db = new ApplicationDbContext())
            {
                db.ClientBoughtGoods.Remove(clientBoughtGood);
                db.SaveChanges();
            }
        }

        public List<Record> GetRecordsSearchByNameGenreCollectiv(string name = "", int select=-1)
        {
            using (var db = new ApplicationDbContext())
            {
                if (select ==0)
                {
                    return new List<Record>(db.Records.Include(i => i.Collective).Include(i => i.Genre).Include(i => i.Publisher).Where(i => EF.Functions.Like(i.Name, $"{name}%")));
                }
                else if (select == 1)
                {
                    return new List<Record>(db.Records.Include(i => i.Collective).Include(i => i.Genre).Include(i => i.Publisher).Where(i => EF.Functions.Like(i.Genre.Name, $"{name}%")));
                }
                else if (select == 2)
                {
                    return new List<Record>(db.Records.Include(i => i.Collective).Include(i => i.Genre).Include(i => i.Publisher).Where(i => EF.Functions.Like(i.Collective.Name, $"{name}%")));
                }
                else
                {
                    return new List<Record>();
                }
            }
        }
    }
}