using HankCollections.Models;

namespace HankCollections.DataManagers
{
    public class ItemManager
    {
        public static void SaveItemListToTxt(List<ItemModel> items)
        {
            StreamWriter writer = new(Constants.ItemDir);

            foreach (ItemModel item in items)
            {
                writer.WriteLine(
                    item.Id + ";!;" +
                    item.Name + ";!;" +
                    item.CollectionId + ";!;" +
                    item.ImageId + ";!;" +
                    item.Status + ";!;" +
                    item.Satisfaction + ";!;" +
                    item.Comment + ";!;" +
                    item.IsSold
                    );
            }

            writer.Close();
        }

        public static List<ItemModel> ReadItemListFromTxt()
        {
            if (!File.Exists(Constants.ItemDir))
            {
                return [];
            }

            StreamReader reader = new(Constants.ItemDir);
            List<ItemModel> items = [];
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split(";!;");
                items.Add(new ItemModel()
                {
                    Id = int.Parse(data[0]),
                    Name = data[1],
                    CollectionId = int.Parse(data[2]),
                    ImageId = int.Parse(data[3]),
                    Status = (Status)Enum.Parse(typeof(Status), data[4]),
                    Satisfaction = (Satisfaction)Enum.Parse(typeof(Satisfaction), data[5]),
                    Comment = data[6],
                    IsSold = bool.Parse(data[7]),
                });
            }

            reader.Close();
            return items;
        }

        public static List<ItemModel> GetItemsByCollectionId(int collectionId)
        {
            List<ItemModel> items = ReadItemListFromTxt();
            return items.Where(x => x.CollectionId == collectionId).ToList();
        }

        public static void AddItem(ItemModel item)
        {
            if (item.Id > 0)
            {
                UpdateItem(item);
                return;
            }

            List<ItemModel> items = ReadItemListFromTxt();
            item.Id = items.Count + 1;
            items.Add(item);
            SaveItemListToTxt(items);
        }

        public static void UpdateItem(ItemModel item)
        {
            List<ItemModel> items = ReadItemListFromTxt();
            items[item.Id - 1] = item;
            SaveItemListToTxt(items);
        }

        public static bool IsDuplicate(string itemName)
        {
            List<ItemModel> items = ReadItemListFromTxt();
            ItemModel item = items.Find(x => x.Name == itemName);

            if (item is null)
            {
                return false;
            }

            return true;
        }

        public static void RemoveItem(int Id)
        {
            List<ItemModel> items = ReadItemListFromTxt();
            items.RemoveAll(x => x.Id == Id);
            SaveItemListToTxt(items);
        }
    }
}
