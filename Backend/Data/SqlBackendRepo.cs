using System.Collections.Generic;
using System.Linq;
using Backend.Models;

namespace Backend.Data
{
    public class SqlBackendRepo : IBackendRepo
    {
       private readonly BackendContext _context;

        public SqlBackendRepo(BackendContext context)
        {
            _context = context;
        }

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
            return _context.Expires.ToList();
        }

        public Expire GetExpireById(int id)
        {
            return _context.Expires.FirstOrDefault(p => p.ExpireId == id);
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _context.Items.ToList();
        }

        public Item GetItemById(int id)
        {
            return _context.Items.FirstOrDefault(p => p.ItemId == id);
        }

        public IEnumerable<Storage> GetAllStorages()
        {
            return _context.Storages.ToList();
        }

        public Storage GetStorageById(int id)
        {
            return _context.Storages.FirstOrDefault(p => p.StorageId == id);
        }
    }
}