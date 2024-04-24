using HankCollections.Models;

namespace HankCollections.DataManagers
{
    internal class ImageManager
    {
        public static void SaveImageListToTxt(List<ImageModel> imageList)
        {
            StreamWriter writer = new(Constants.ImageDir);

            foreach (ImageModel image in imageList)
            {
                writer.WriteLine($"{image.Id};{image.Data}");
            }

            writer.Close();
        }

        public static List<ImageModel> ReadImageListFromTxt()
        {
            if (!File.Exists(Constants.ImageDir))
            {
                return [];
            }

            StreamReader reader = new(Constants.ImageDir);
            List<ImageModel> imageList = [];
            string line;

            try
            {
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(";");
                    imageList.Add(new ImageModel()
                    {
                        Id = int.Parse(data[0]),
                        Data = data[1]
                    });
                }

                reader.Close();

                return imageList;
            }
            catch
            {
                reader.Close();
                return [];
            }
        }

        public static string ImageToBase64(string path)
        {
            if (!File.Exists(path))
            {
                return "";
            }

            byte[] imageArray = File.ReadAllBytes(path);
            string base64 = Convert.ToBase64String(imageArray);
            return base64;
        }

        public static ImageSource Base64ToImageSource(string base64)
        {
            try
            {
                byte[] imageBytes = Convert.FromBase64String(base64);
                ImageSource imgSrc = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                return imgSrc;
            }
            catch
            {
                ImageSource imgSrc = ImageSource.FromFile("warning.png");
                return imgSrc;
            }
        }

        public static int AddImage(string path)
        {
            List<ImageModel> images = ReadImageListFromTxt();
            int Id = images.Count + 1;
            ImageModel newImage = new ImageModel() { Id = Id, Data = ImageToBase64(path) };
            images.Add(newImage);
            SaveImageListToTxt(images);
            return Id;
        }
        public static int AddImage(ImageModel image)
        {
            List<ImageModel> images = ReadImageListFromTxt();
            int Id = images.Count + 1;
            image.Id = Id;
            images.Add(image);
            SaveImageListToTxt(images);
            return Id;
        }

        public static ImageModel GetImageById(int id)
        {
            List<ImageModel> images = ReadImageListFromTxt();

            if (images.FirstOrDefault(x => x.Id == id) is null)
            {
                return new ImageModel();
            }

            return images.FirstOrDefault(x => x.Id == id);
        }
    }
}
