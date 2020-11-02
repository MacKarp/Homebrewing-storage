using System.Collections.Generic;
using Backend.Models;

namespace Backend.Data
{
    public interface IBackendRepo
    {
        IEnumerable<Category> GetAllCategories();
        Category GetCategoryById(int id);
        
        IEnumerable<Expire> GetAllExpires();
        Expire GetExpireById(int id);
        
        IEnumerable<Item> GetAllItems();
        Item GetItemById(int id);

        IEnumerable<Storage> GetAllStorages();
        Storage GetStorageById(int id);
    }
}