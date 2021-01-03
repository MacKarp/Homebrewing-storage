using System;
using System.Collections.Generic;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Data
{
    public interface IBackendRepo
    {
        bool SaveChanges();

        //GET methods
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);
        IEnumerable<Expire> GetAllExpires();
        Expire GetExpireById(int id);
        IEnumerable<Expire> GetExpiresByUserId(string userId);
        IEnumerable<Item> GetAllItems();
        Item GetItemById(int id);
        IEnumerable<Storage> GetAllStorages();
        Storage GetStorageById(int id);
        IEnumerable<IdentityUser> GetAllUsers();
        IdentityUser GetUserById(string id);


        IEnumerable<Expire> GetAllExpiresByExpirationTimeLeft(double days);  
      
        //CREATE methods
        void CreateCategory(Category category);
        void CreateExpire(Expire expire);
        void CreateItem(Item item);
        void CreateStorage(Storage storage);
        void CreateUser(IdentityUser user);

        //UPDATE methods
        void UpdateCategory(Category category);
        void UpdateExpire(Expire expire);
        void UpdateItem(Item item);
        void UpdateStorage(Storage storage);
      //  void UpdateUser(User user);

        //DELETE methods
        void DeleteCategory(Category category);
        void DeleteExpire(Expire expire);
        void DeleteItem(Item item);
        void DeleteStorage(Storage storage);
     //  void DeleteUser(User user);
    }
}