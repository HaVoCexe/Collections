using HankCollections.Models;

namespace HankCollections.DataManagers
{
    public class CollectionManager
    {
        public static void SaveToTxt(List<CollectionModel> collections)
        {
            StreamWriter writer = new(Constants.CollectionDir);

            foreach (CollectionModel collection in collections)
            {
                writer.WriteLine($"{collection.Id};!;{collection.Name}");
            }

            writer.Close();
        }

        public static List<CollectionModel> ReadFromTxt()
        {
            if (!File.Exists(Constants.CollectionDir))
            {
                return [];
            }

            StreamReader reader = new(Constants.CollectionDir);
            List<CollectionModel> items = new();
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] data = line.Split(";!;");
                CollectionModel newItem = new CollectionModel()
                {
                    Id = int.Parse(data[0]),
                    Name = data[1]
                };
                items.Add(newItem);
            }

            reader.Close();
            return items;
        }

        public static int CreateCollection(string name)
        {
            List<CollectionModel> collections = ReadFromTxt();
            int id = collections.Count + 1;
            CollectionModel collection = new()
            {
                Id = id,
                Name = name
            };
            collections.Add(collection);
            SaveToTxt(collections);

            return id;
        }

        public static CollectionModel FindCollection(int id)
        {
            List<CollectionModel> collections = ReadFromTxt();
            return collections.Find(x => x.Id == id);
        }
    }
}
