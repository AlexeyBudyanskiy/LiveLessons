using System;
using System.IO;
using System.Web;

namespace LiveLesson.WEB.Infrastructure
{
    public static class ImageLoader
    {
        public static void SaveImage(HttpPostedFile file, string basePath)
        {
            var filename = Path.GetFileName(file.FileName);

            if (!CheckImageExtenison(filename))
            {
                throw new ArgumentException("Invalid file type");
            }

            file.SaveAs(Path.Combine(basePath + GlobalVariables.ImagePath, filename));
        }

        private static bool CheckImageExtenison(string imageName)
        {
            var ext = imageName.Split('.')[imageName.Split('.').Length - 1];

            return ext.Equals("png") || ext.Equals("jpg") || ext.Equals("jpeg");
        }
    }
}