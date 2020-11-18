using System.Collections.Generic;
using Backend.Models;

namespace Backend.Data
{
    public interface IBackendRepo
    {
        bool SaveChanges();

        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);

        IEnumerable<Expire> GetAllExpires();
        Expire GetExpireById(int id);

        IEnumerable<Item> GetAllItems();
        Item GetItemById(int id);

        IEnumerable<Storage> GetAllStorages();
        Storage GetStorageById(int id);

        void CreateCategory(Category category);
        void CreateExpire(Expire expire);
        void CreateItem(Item item);
        void CreateStorage(Storage storage);
    }
}