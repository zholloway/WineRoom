using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WineRoomAPI.DataContext;
using WineRoomAPI.Models;
using WineRoomAPI.Models.JsonReturnModels;

namespace WineRoomAPI.Services
{
    public class UserServices
    {
        public WineroomContext Database { get; } = new WineroomContext();

        public bool CheckForUser(User user)
        {
            if (Database.Users.Where(w => w.Password == user.Password && w.Username == user.Username).Count() == 1) return true;

            return false;
        }

        public User FindUser(User user)
        {
            return Database.Users.First(w => w.Password == user.Password && w.Username == user.Username);
        }

        public List<User> GetUser()
        {
            return Database.Users.ToList();
        }

        public JsonUserGet JsonUserGet(List<User> userList)
        {
            return new JsonUserGet {
                Success = true,
                Message = "we did it",
                Data = userList
            };
        }

        public User GetIndividualUserByID(int id)
        {
            return Database.Users.First(f => f.ID == id);
        }

        public void AddUser(User user)
        {
            Database.Users.Add(user);
            Database.SaveChanges();
        }

        public User FindNewUser(User user)
        {
            return Database.Users
                .Where(w => w.AdminStatus == user.AdminStatus)
                .Where(w => w.Username == user.Username)
                .Where(w => w.Password == user.Password)
                as User;
        }

        public void DeleteUser(int id)
        {
            Database.Users.Remove(GetIndividualUserByID(id));
            Database.SaveChanges();
        }

        public void EditUser(int id, User user)
        {
            User editUser = Database.Users.First(f => f.ID == id);

            editUser.Username = user.Username;
            editUser.Password = user.Password;
            editUser.AdminStatus = user.AdminStatus;

            Database.SaveChanges();
        }

        public JsonUserCrud JsonUserReturn(int id)
        {
            return new JsonUserCrud {
                Success = true,
                Message = "we did it",
                UserID = id
            };
        }
    }
}