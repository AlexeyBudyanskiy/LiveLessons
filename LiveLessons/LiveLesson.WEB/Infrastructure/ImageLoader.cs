using System;
using System.IO;
using System.Web;

namespace LiveLesson.WEB.Infrastructure
{
    public static class ImageLoader
    {
        public static void SaveImage(HttpPostedFileBase file)
        {
            var filename = Path.GetFileName(file.FileName);

            if (!CheckImageExtenison(filename))
            {
                throw new ArgumentException("Invalid file type");
            }

            if (filename != null)
            {
                file.SaveAs(Path.Combine(GlobalVariables.ImagePath, GenreateImageName(filename)));
            }
        }

        private static string GenreateImageName(string imageName)
        {
            imageName = imageName.Replace(" ", "-");

            if (File.Exists(GlobalVariables.ImagePath + imageName))
            {
                imageName = GenreateImageName(string.Concat("1", imageName));
            }

            return imageName;
        }

        private static bool CheckImageExtenison(string imageName)
        {
            var ext = imageName.Split('.')[imageName.Split('.').Length - 1];

            return ext.Equals("png") || ext.Equals("jpg") || ext.Equals("jpeg");
        }
    }
}