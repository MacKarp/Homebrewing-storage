using System;
using System.Collections.Generic;
using System.Linq;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Backend.Data
{
    public class SqlBackendRepo : IBackendRepo
    {
        private readonly BackendContext _context;
        
        //Constructor
        public SqlBackendRepo(BackendContext context)
        {
            _context = context;
        }
        
        //GET methods
        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public Category GetCategoryById(int id)
        {
            return _context.Categories.FirstOrDefault(p => p.CategoryId == id);
        }
        
        public IEnumerable<Expire> GetAllExpires()
        {
            return _context.Expires.Include(storage => storage.IdStorage).Include(item => item.IdItem).Include(user => user.IdUser).ToList();
        }

        public Expire GetExpireById(int id)
        {
            return _context.Expires.Include(storage => storage.IdStorage).Include(item => item.IdItem).Include(user => user.IdUser).FirstOrDefault(p => p.ExpireId == id);
        }
        
        public IEnumerable<Item> GetAllItems()
        {
            return _context.Items.Include(category => category.IdCategory).ToList();
        }

        public Item GetItemById(int id)
        {
            return _context.Items.Include(category => category.IdCategory).FirstOrDefault(p => p.ItemId == id);
        }
        
        public IEnumerable<Storage> GetAllStorages()
        {
            return _context.Storages.Include(user => user.IdUser).ToList();
        }

        public Storage GetStorageById(int id)
        {
            return _context.Storages.Include(user => user.IdUser).FirstOrDefault(p => p.StorageId == id);
        }

        public IEnumerable<IdentityUser> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public IdentityUser GetUserById(string idGuid)
        {
            return _context.Users.FirstOrDefault(p => p.Id == idGuid);
        }

        public IEnumerable<Expire> GetExpiresByUserId(string userId)
        {
            return _context.Expires.Where(user=>user.IdUser.Id==userId).Include(storage => storage.IdStorage).Include(item => item.IdItem).Include(user => user.IdUser).ToList();
        }

        public IEnumerable<Expire> GetAllExpiresByExpirationTimeLeft(double days)
        {
            return _context.Expires.Where(p => p.ExpirationDate >= DateTime.Now.Date && p.ExpirationDate <= DateTime.Now.AddDays(days)).Include(storage => storage.IdStorage).Include(item => item.IdItem).Include(user => user.IdUser).ToList();
        }

        //CREATE methods
        public void CreateCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }

            _context.Categories.Add(category);
        }

        public void CreateExpire(Expire expire)
        {
            if (expire == null)
            {
                throw new ArgumentNullException(nameof(expire));
            }

            _context.Expires.Add(expire);
        }

        public void CreateItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            _context.Items.Add(item);
        }

        public void CreateStorage(Storage storage)
        {
            if (storage == null)
            {
                throw new ArgumentNullException(nameof(storage));
            }

            _context.Storages.Add(storage);
        }

        public void CreateUser(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Add(user);
        }

        //UPDATE methods
        public void UpdateCategory(Category category)
        {
            // Nothing
        }

        public void UpdateExpire(Expire expire)
        {
            // Nothing
        }

        public void UpdateItem(Item item)
        {
            // Nothing
        }

        public void UpdateStorage(Storage storage)
        {
            // Nothing
        }

        public void UpdateUser(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            _context.Users.Update(user);
        }

        //DELETE methods
        public void DeleteCategory(Category category)
        {
            if (category == null)
            {
                throw new ArgumentNullException(nameof(category));
            }
            _context.Categories.Remove(category);
        }

        public void DeleteExpire(Expire expire)
        {
            if (expire == null)
            {
                throw new ArgumentNullException(nameof(expire));
            }
            _context.Expires.Remove(expire);
        }

        public void DeleteItem(Item item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            _context.Items.Remove(item);
        }

        public void DeleteStorage(Storage storage)
        {
            if (storage == null)
            {
                throw new ArgumentNullException(nameof(storage));
            }
            _context.Storages.Remove(storage);
        }


        public void DeleteUser(IdentityUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            _context.Users.Remove(user);
        }

        //Saving cahanges to DB method
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}